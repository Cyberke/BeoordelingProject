using BeoordelingProject.DAL.Repositories;
using BeoordelingProject.DAL.UnitOfWork;
using BeoordelingProject.Engine;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Services {
    public class BeoordelingsService : BeoordelingProject.DAL.Services.IBeoordelingsService {
        IUnitOfWork uow = null;
        IGenericRepository<Matrix> beoordelingsRepository = null;
        IGenericRepository<Resultaat> resultaatRepository = null;
        IBeoordelingsEngine beoordelingsEngine = null;

        public BeoordelingsService() {
        }

        public BeoordelingsService(IUnitOfWork uow,
            IGenericRepository<Matrix> beoordelingsRepository,
            IGenericRepository<Resultaat> resultaatRepository,
            IBeoordelingsEngine beoordelingsEngine) {
                this.uow = uow;
                this.beoordelingsRepository = beoordelingsRepository;
                this.resultaatRepository = resultaatRepository;
                this.beoordelingsEngine = beoordelingsEngine;
        }

        public List<Resultaat> GetResultaten() {
            return resultaatRepository.All().ToList<Resultaat>();
        }

        public Matrix GetMatrix() {
            return beoordelingsRepository.All().ToList<Matrix>()[0];
        }

        public void CreateBeoordeling() {
            //
        }
    }
}