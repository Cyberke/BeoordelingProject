using BeoordelingProject.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.UnitOfWork
{
    public class UnitOfWork : BeoordelingProject.DAL.UnitOfWork.IUnitOfWork
    {
        private BeoordelingsContext context = null;

        public UnitOfWork(BeoordelingsContext context)
        {
            this.context = context;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}