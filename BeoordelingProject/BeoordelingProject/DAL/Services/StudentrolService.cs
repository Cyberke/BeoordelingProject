using BeoordelingProject.DAL.Repositories;
using BeoordelingProject.DAL.UnitOfWork;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Services
{
    public class StudentrolService : BeoordelingProject.DAL.Services.IStudentrolService
    {

        IUnitOfWork uow = null;
        GenericRepository<StudentRollen> studentrolRepository = null;

        public StudentrolService() {

        }

        public StudentrolService(IUnitOfWork uow,
                                   GenericRepository<StudentRollen> studentrolRepository) 
        {
            this.uow = uow;
            this.studentrolRepository = studentrolRepository;
        }
        public StudentRollen CreateStudentrol(Student student, List<Rol> rollen)
        {
            StudentRollen newStudentrol = new StudentRollen()
            {
                Student = student,
                Rollen = rollen
            };
            newStudentrol = studentrolRepository.Insert(newStudentrol);
            uow.SaveChanges();
            return newStudentrol;
        }

        public StudentRollen CreateStudentrol(Student student, List<Rol> rollen) {
            throw new NotImplementedException();
        }
    }
}