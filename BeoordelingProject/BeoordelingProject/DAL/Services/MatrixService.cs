using BeoordelingProject.DAL.Repositories;
using BeoordelingProject.DAL.UnitOfWork;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Services
{
    public class MatrixService : BeoordelingProject.DAL.Services.IMatrixService
    {
        IUnitOfWork uow = null;
        IMatrixRepository matrixrepository = null;

        public MatrixService()
        {

        }

        public MatrixService(IUnitOfWork uow, IMatrixRepository matrixrepository)
        {
            this.uow = uow;
            this.matrixrepository = matrixrepository;
        }

        public Matrix GetMatrixByID (int id)
        {
            return matrixrepository.GetMatrixByID(id);
        }

    }
}