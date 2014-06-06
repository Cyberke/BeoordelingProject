using BeoordelingProject.DAL.Repositories;
using BeoordelingProject.DAL.UnitOfWork;
using BeoordelingProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Services
{
    public class StudentService : BeoordelingProject.DAL.Services.IStudentService
    {
        IUnitOfWork uow = null;
        IStudentRepository studentRepository = null;
        IAccountRepository accountRepository = null;

        public StudentService()
        {

        }

        public StudentService(IUnitOfWork uow, IStudentRepository studentRepository, IAccountRepository accountRepository)
        {
            this.uow = uow;
            this.studentRepository = studentRepository;
            this.accountRepository = accountRepository;
        }

        public List<Student> GetStudenten()
        {
            return studentRepository.All().ToList<Student>();
        }

        public List<Student> CreateStudenten(string csvData)
        {
            List<Student> studenten = new List<Student>();

            using (StringReader textReader = new StringReader(csvData))
            {
                string line = textReader.ReadLine();
                int skipCount = 0;

                while (line != null && skipCount < 1)
                {
                    line = textReader.ReadLine();

                    skipCount++;
                }

                while (line != null)
                {
                    string[] columns = line.Split(';');
                    Student student = new Student { Naam = columns[7], Trajecttype = columns[20], Opleiding = columns[1], Email = columns[15], StudentId = Int32.Parse(columns[14]), Geslacht = columns[18], Geboortedatum = columns[17] };

                    studentRepository.Insert(student);
                    uow.SaveChanges();

                    studenten.Add(student);

                    line = textReader.ReadLine();
                }
            }
            
            return studenten;
        }

        public List<String> GetOpleidingen()
        {
            return studentRepository.GetOpleidingen().ToList<String>();
        }

        public List<ApplicationUser> GetUsers()
        {
            return accountRepository.All().ToList();
        }

        public IHtmlString SerializeObject(object value)
        {
            using (var stringWriter = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                var serializer = new JsonSerializer
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                jsonWriter.QuoteName = false;
                serializer.Serialize(jsonWriter, value);

                return new HtmlString(stringWriter.ToString());
            }
        }
        public Student GetStudentByID(int id)
        {
            return studentRepository.GetByID(id);
        }
    }
}