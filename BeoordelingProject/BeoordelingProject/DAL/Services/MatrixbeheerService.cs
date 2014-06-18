using BeoordelingProject.DAL.Repositories;
using BeoordelingProject.DAL.UnitOfWork;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Services
{
    public class MatrixbeheerService : BeoordelingProject.DAL.Services.IMatrixbeheerService
    {
        IUnitOfWork uow = null;
        IMatrixRepository matrixrepository = null;
        public MatrixbeheerService()
        {

        }
        public MatrixbeheerService(IUnitOfWork uow, IMatrixRepository matrixrepository)
        {
            this.uow = uow;
            this.matrixrepository = matrixrepository;
        }

        public List<string> GetOpleidingen()
        {
            return matrixrepository.GetOpleidingen();
        }

        public Matrix GetMatrixByRichtingByTussentijds(string opleiding, bool tussentijds)
        {
            int matid = matrixrepository.GetMatrixIdByRichtingByType(tussentijds, opleiding);
            return matrixrepository.GetByID(matid);
        }

    }
}