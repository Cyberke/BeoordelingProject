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

        public BeoordelingsService() {
        }

        public BeoordelingsService(IUnitOfWork uow,
            IMatrixRepository matrixRepository,
            IResultaatRepository resultaatRepository,
            IBeoordelingsEngine beoordelingsEngine) {
                this.uow = uow;
                this.matrixRepository = matrixRepository;
                this.resultaatRepository = resultaatRepository;
                this.beoordelingsEngine = beoordelingsEngine;
        }

        public List<Resultaat> GetResultaten() {
            return resultaatRepository.All().ToList<Resultaat>();
        }

        public Matrix GetMatrix(int id)
        {
            return matrixRepository.GetMatrixByID(id);
        }

        public Matrix GetMatrixForRol(int matrixID, int rolID)
        {
            return matrixRepository.GetMatrixForRol(matrixID, rolID);
        }
        public void CreateBeoordeling(BeoordelingsVM vm) {
            
            Matrix m = matrixRepository.GetMatrixByID(vm.MatrixID);
            int studentid = resultaatRepository.ifExistsGetStudentId(vm.Student.ID);

            if (studentid != 0) //bestaande record aanpassen
            {
                Resultaat exist = resultaatRepository.getByStudentId(studentid);

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

                    List<double> scores = GetListScore(exist.DeelaspectResultaten);
                    List<int> wegingen = GetListWegingen(exist.DeelaspectResultaten);

                    exist.TotaalTussentijdResultaat = beoordelingsEngine.totaalScore(scores, wegingen);

                    resultaatRepository.Update(exist);
                    uow.SaveChanges();
                }
                else
                {
                    //eindscoreberekening bestaand resultaat
                }

            }
            else //nieuw record in database
            {
                Resultaat newres = new Resultaat();
                newres.StudentId = vm.Student.ID;
                if(m.Tussentijds == true)
                {
                    newres.TussentijdseId = m.ID;
                    newres.DeelaspectResultaten = FillDeelaspectResultaten(m, vm.Scores);

                    List<double> scores = GetListScore(newres.DeelaspectResultaten);
                    List<int> wegingen = GetListWegingen(newres.DeelaspectResultaten);

                    newres.TotaalTussentijdResultaat = beoordelingsEngine.totaalScore(scores, wegingen);

                    resultaatRepository.Insert(newres);
                    uow.SaveChanges();
                }
                else
                {
                    // eindscore nieuw resultaat
                    // Ultieme eindscore kan pas berekend worden eens alle rollen de beoordeling hebben voltooid
                    // totaalscores hoofdaspecten optellen en delen door (maximum te behalen punten / gewogen score)
                    // de behaalde resultaten allemaal optellen en delen door 10
                    // Math.Ceiling voor afronding

                    //hoofdaspectresultaten in de database steken !checken of ze al bestaan, zo ja, overschrijven!
                    


                    //controleren of alle rollen de beoordeling hebben voltooid
                    List<string> namen = resultaatRepository.CheckIfRolesCompleted(vm.Student.ID);

                    //later if-statement aanpassen of CFaanwezig aangeduid is of niet
                    if(namen.Count == 3)
                    {
                        //totaal eindscoreberekening
                    }
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

        public List<double> GetListScore (List<DeelaspectResultaat> deelreslist)
        {
            List<double> scores = new List<double>();

            foreach(DeelaspectResultaat deelres in deelreslist)
            {
                double score = deelres.Score;
                scores.Add(score);
            }
            return scores;
        }
        public List<int> GetListWegingen (List<DeelaspectResultaat> deelreslist)
        {
            List<int> wegingen = new List<int>();

            foreach(DeelaspectResultaat deelres in deelreslist)
            {
                int weging = matrixRepository.GetWegingForDeelaspect(deelres.DeelaspectId);
                wegingen.Add(weging);
            }

            return wegingen;
        }
    }
}