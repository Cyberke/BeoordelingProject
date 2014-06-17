using BeoordelingProject.DAL.Repositories;
using BeoordelingProject.DAL.UnitOfWork;
using BeoordelingProject.Engine;
using BeoordelingProject.Models;
using BeoordelingProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Services {
    public class BeoordelingsService : BeoordelingProject.DAL.Services.IBeoordelingsService{
        IUnitOfWork uow = null;
        IMatrixRepository matrixRepository = null;
        IResultaatRepository resultaatRepository = null;
        IBeoordelingsEngine beoordelingsEngine = null;
        IGenericRepository<Rol> rolRepository = null;

        public BeoordelingsService() {
        }

        public BeoordelingsService(IUnitOfWork uow,
            IMatrixRepository matrixRepository,
            IResultaatRepository resultaatRepository,
            IBeoordelingsEngine beoordelingsEngine,
            IGenericRepository<Rol> rolRepository) {
                this.uow = uow;
                this.matrixRepository = matrixRepository;
                this.resultaatRepository = resultaatRepository;
                this.beoordelingsEngine = beoordelingsEngine;
                this.rolRepository = rolRepository;
        }

        public List<Resultaat> GetResultaten() {
            return resultaatRepository.All().ToList<Resultaat>();
        }

        public Matrix GetMatrix(int id)
        {
            return matrixRepository.GetByID(id);
        }

        public Matrix GetMatrixForRol(int matrixID, int rolID)
        {
            return matrixRepository.GetMatrixForRol(matrixID, rolID);
        }
        public void CreateBeoordeling(BeoordelingsVM vm) {
            
            Matrix m = matrixRepository.GetByID(vm.MatrixID);
            int studentid = resultaatRepository.ifExistsGetStudentId(vm.Student.ID);

            if (studentid != 0) //bestaande record aanpassen
            {
                Resultaat exist = resultaatRepository.getByStudentId(studentid);
                exist.CustomFeedback = vm.feedback;
                if (m.Tussentijds == true)
                {
                    exist.TussentijdseId = m.ID;
                    
                    //de bestaande lijst met deelaspectresultaten updaten we met de nieuw gekozen score
                    //We hergebruiken de bestaande lijst zodat we zijn eigen ID waarde behouden zodat we de database niet overspoelen met onnodige records
                    List<DeelaspectResultaat> newdeelres = FillDeelaspectResultaten(m, vm.Scores);
                    List<DeelaspectResultaat> olddeelres = exist.DeelaspectResultaten;

                    for (int i = 0; i < newdeelres.Count(); i++ )
                    {
                        olddeelres[i].DeelaspectId = newdeelres[i].DeelaspectId;
                        olddeelres[i].Score = newdeelres[i].Score;
                    }

                    exist.DeelaspectResultaten = olddeelres;

                    List<double> scores = GetListDeelaspectScore(exist.DeelaspectResultaten);
                    List<int> wegingen = GetListDeelaspectWegingen(exist.DeelaspectResultaten);

                    exist.TotaalTussentijdResultaat = beoordelingsEngine.totaalScore(scores, wegingen);

                    resultaatRepository.Update(exist);
                    uow.SaveChanges();
                }
                else
                {
                    // eindscoreberekening bestaand resultaat
                    // Ultieme eindscore kan pas berekend worden eens alle rollen de beoordeling hebben voltooid
                    // totaalscores hoofdaspecten optellen en delen door (maximum te behalen punten / gewogen score)
                    // de behaalde resultaten allemaal optellen en delen door 10
                    // Math.Ceiling voor afronding

                    //hoofdaspectresultaten in de database steken !checken of ze al bestaan, zo ja, overschrijven!
                    if(exist.HoofdaspectResultaten.Any(h => h.Rol.ID == vm.Rol_ID))
                    {
                        int hoofdaspectcounter = 0;
                        //Matrix mat = matrixRepository.GetMatrixForRol(vm.MatrixID, vm.Rol_ID);

                        List<double> hoofdaspectScore = new List<double>();
                        List<int> weging = new List<int>();

                        foreach(HoofdaspectResultaat h in exist.HoofdaspectResultaten)
                        {
                            if (h.Rol.ID == vm.Rol_ID)
                            {
                                for (int i = 0; i < matrixRepository.GetDeelaspectenCountForHoofdaspect(h.HoofdaspectId); i++ )
                                {
                                    hoofdaspectScore.Add(vm.Scores[i]);
                                }

                                weging.Add(matrixRepository.GetWegingForHoofdaspect(h.HoofdaspectId));

                                h.Score = beoordelingsEngine.totaalScore(hoofdaspectScore, weging);

                                hoofdaspectScore.Clear();
                                weging.Clear();

                                hoofdaspectcounter++;
                            }
                        }
                        
                        /*
                        resultaatRepository.Update(exist);
                        uow.SaveChanges();
                        */ 
                    }
                    else //er zijn nog GEEN hoofdaspectresultaten van deze rol
                    {
                        List<HoofdaspectResultaat> hoofdreslist = new List<HoofdaspectResultaat>();

                        Matrix mat = matrixRepository.GetMatrixForRol(m.ID, vm.Rol_ID);
                        int counter = 0;

                        List<double> hoofdaspectScore = new List<double>();
                        List<int> wegingen = new List<int>();
                        
                        foreach(Hoofdaspect h in mat.Hoofdaspecten)
                        {
                            HoofdaspectResultaat hoofdres = new HoofdaspectResultaat();
                            foreach(Deelaspect d in h.Deelaspecten)
                            {
                                hoofdaspectScore.Add(vm.Scores[counter]);
                                counter++;
                            }
                            hoofdres.HoofdaspectId = h.ID;
                            hoofdres.Rol = rolRepository.GetByID(vm.Rol_ID);

                            wegingen.Add(matrixRepository.GetWegingForHoofdaspect(h.ID));

                            hoofdres.Score = beoordelingsEngine.totaalScore(hoofdaspectScore, wegingen);

                            hoofdreslist.Add(hoofdres);

                            hoofdaspectScore.Clear();
                            wegingen.Clear();
                        }

                        exist.HoofdaspectResultaten.AddRange(hoofdreslist);
                    }
                    
                    exist.EindId = m.ID;

                    //controleren of alle rollen de beoordeling hebben voltooid
                    List<string> namen = resultaatRepository.CheckIfRolesCompleted(vm.Student.ID);

                    //later if-statement aanpassen of CFaanwezig aangeduid is of niet
                    if (namen.Count == 3)
                    {
                        //totaalscore berekenen
                        List<double> eindscore = new List<double>();
                        List<int> wegingen = new List<int>();
                        List<double> tussenscores = new List<double>();

                        List<Hoofdaspect> test = matrixRepository.GetHoofdaspectenForMatrix(vm.MatrixID);

                        foreach(Hoofdaspect h in test)
                        {
                            List<double> hoofdaspectscores = resultaatRepository.GetScoresForHoofdaspect(h.ID, vm.Student.ID);

                            double totaalaspectscore = 0;
                            foreach(double score in hoofdaspectscores)
                            {
                                totaalaspectscore += score;
                            }

                            int aantalRollen = resultaatRepository.GetAantalRollenForHoofdaspect(h.ID);
                            double delingfactor = h.Deelaspecten.Count() * aantalRollen * 20;
                            totaalaspectscore = totaalaspectscore / (delingfactor / h.GewogenScore);
                            tussenscores.Add(totaalaspectscore);
                        }

                        double somtotaal = 0;

                        foreach(double score in tussenscores)
                        {
                            somtotaal += score;
                        }

                        somtotaal = Math.Ceiling(somtotaal / 10);

                        exist.TotaalEindresultaat = somtotaal;
                    }

                    vm.Matrix = matrixRepository.GetByID(vm.MatrixID);

                    resultaatRepository.Update(exist);
                    uow.SaveChanges();
                }

            }
            else //nieuw record in database
            {
                Resultaat newres = new Resultaat();
                newres.CustomFeedback = vm.feedback;
                newres.StudentId = vm.Student.ID;
                if(m.Tussentijds == true)
                {
                    newres.TussentijdseId = m.ID;
                    newres.DeelaspectResultaten = FillDeelaspectResultaten(m, vm.Scores);

                    List<double> scores = GetListDeelaspectScore(newres.DeelaspectResultaten);
                    List<int> wegingen = GetListDeelaspectWegingen(newres.DeelaspectResultaten);

                    newres.TotaalTussentijdResultaat = beoordelingsEngine.totaalScore(scores, wegingen);

                    resultaatRepository.Insert(newres);
                    uow.SaveChanges();
                }
                else
                {
                    newres.EindId = m.ID;

                    List<HoofdaspectResultaat> hoofdreslist = new List<HoofdaspectResultaat>();

                    Matrix mat = matrixRepository.GetByID(m.ID);
                    int counter = 0;

                    List<double> hoofdaspectScore = new List<double>();
                    List<int> wegingen = new List<int>();

                    foreach (Hoofdaspect h in mat.Hoofdaspecten)
                    {
                        if(h.Rollen.Any(r => r.ID == vm.Rol_ID))
                        {
                            HoofdaspectResultaat hoofdres = new HoofdaspectResultaat();

                            foreach(Deelaspect d in h.Deelaspecten)
                            {
                                hoofdaspectScore.Add(vm.Scores[counter]);
                                counter++;
                            }

                            hoofdres.HoofdaspectId = h.ID;
                            hoofdres.Rol = rolRepository.GetByID(vm.Rol_ID);

                            wegingen.Add(matrixRepository.GetWegingForHoofdaspect(h.ID));

                            hoofdres.Score = beoordelingsEngine.totaalScore(hoofdaspectScore, wegingen);

                            hoofdreslist.Add(hoofdres);
                            wegingen.Clear();
                        }
                    }

                    newres.HoofdaspectResultaten = hoofdreslist;
                    resultaatRepository.Insert(newres);
                    uow.SaveChanges();
                }
            }
        }

        public List<Resultaat> GetTussentijdseResultaten(int id)
        {
            return resultaatRepository.GetTussentijdseResultaten(id).ToList<Resultaat>();
        }
        public List<DeelaspectResultaat> FillDeelaspectResultaten(Matrix m, List<double> scores)
        {
            List<DeelaspectResultaat> deelreslist = new List<DeelaspectResultaat>();

            int counter = 0;

            for (int i = 0; i < m.Hoofdaspecten.Count; i++)
            {
                for (int j = 0; j < m.Hoofdaspecten[i].Deelaspecten.Count; j++)
                {
                    DeelaspectResultaat deelres = new DeelaspectResultaat();

                    deelres.DeelaspectId = m.Hoofdaspecten[i].Deelaspecten[j].ID;
                    deelres.Score = scores[counter];

                    deelreslist.Add(deelres);

                    counter++;
                }
            }

            List<DeelaspectResultaat> test = deelreslist;

            return deelreslist;
        }

        public List<double> GetListDeelaspectScore (List<DeelaspectResultaat> deelreslist)
        {
            List<double> scores = new List<double>();

            foreach(DeelaspectResultaat deelres in deelreslist)
            {
                double score = deelres.Score;
                scores.Add(score);
            }
            return scores;
        }
        public List<int> GetListDeelaspectWegingen (List<DeelaspectResultaat> deelreslist)
        {
            List<int> wegingen = new List<int>();

            foreach(DeelaspectResultaat deelres in deelreslist)
            {
                int weging = matrixRepository.GetWegingForDeelaspect(deelres.DeelaspectId);
                wegingen.Add(weging);
            }

            return wegingen;
        }

        public List<double> GetListHoofdAspectScore(List<HoofdaspectResultaat> hoofdreslist)
        {
            List<double> scores = new List<double>();

            foreach(HoofdaspectResultaat hoofdres in hoofdreslist)
            {
                double score = hoofdres.Score;
                
                scores.Add(score);
            }

            return scores;
        }
        public List<int> GetListHoofdAspectWegingen(List<HoofdaspectResultaat> hoofdreslist)
        {
            List<int> wegingen = new List<int>();

            foreach(HoofdaspectResultaat hoofdres in hoofdreslist)
            {
                int weging = matrixRepository.GetWegingForHoofdaspect(hoofdres.HoofdaspectId);

                wegingen.Add(weging);
            }

            return wegingen;
        }

        public int GetMatrixIdByRichtingByType(bool type, string richting)
        {
            return matrixRepository.GetMatrixIdByRichtingByType(type, richting);
        }
        public Resultaat getResultaatByStudentId(int studentid)
        {
            return resultaatRepository.getByStudentId(studentid);
        }

        public bool isCFaanwezig(int studentid)
        {
            return resultaatRepository.isCFaanwezig(studentid);
        }
    }
}