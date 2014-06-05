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
        public void CreateBeoordeling(Resultaat res) {
            
        }

        public List<Resultaat> GetTussentijdseResultaten(int id)
        {
            return resultaatRepository.GetTussentijdseResultaten(id).ToList<Resultaat>();
        }

        //public List<DeelaspectResultaat> FillDeelaspectResultaten(BeoordelingsVM vm)
        public List<DeelaspectResultaat> FillDeelaspectResultaten(Matrix m, List<DeelaspectResultaat> scores)
        {
            List<DeelaspectResultaat> deelreslist = new List<DeelaspectResultaat>();

            int counter = 0;

            for (int i = 0; i < m.Hoofdaspecten.Count; i++)
            {
                for (int j = 0; j < m.Hoofdaspecten[i].Deelaspecten.Count; j++)
                {
                    DeelaspectResultaat deelres = new DeelaspectResultaat();

                    deelres.DeelaspectId = m.Hoofdaspecten[i].Deelaspecten[j].ID;
                    deelres.Score = scores[counter].Score;

                    deelreslist.Add(deelres);

                    counter++;
                }
            }

            return deelreslist;
        }

    }
}