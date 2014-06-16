using BeoordelingProject.DAL.Repositories;
using BeoordelingProject.DAL.UnitOfWork;
using BeoordelingProject.Models;
using BeoordelingProject.ViewModel;
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
        IGenericRepository<Rol> rolRepository = null;
        IResultaatRepository resultaatRepository = null;

        public StudentService()
        {

        }

        public StudentService(IUnitOfWork uow, IStudentRepository studentRepository, IAccountRepository accountRepository, IGenericRepository<Rol> rolRepository, IResultaatRepository resultaatRepository)
        {
            this.uow = uow;
            this.studentRepository = studentRepository;
            this.accountRepository = accountRepository;
            this.rolRepository = rolRepository;
            this.resultaatRepository = resultaatRepository;
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

        public void DeleteUser(ApplicationUser user)
        {
            accountRepository.DeleteGebruiker(user);

        }

        public List<Rol> GetRoles()
        {
            return studentRepository.GetRoles().ToList();
        }

        public List<Resultaat> GetResultaat()
        {
            return resultaatRepository.All().ToList<Resultaat>();
        }

        public IHtmlString SerializeObject(object beoordelaarslijst) {
            StudentService studentService = new StudentService();

            List<ApplicationUser> beoordelaars = (List<ApplicationUser>)beoordelaarslijst;

            string jsonString = "{Studenten:[";

            foreach (ApplicationUser beoordelaar in beoordelaars) {
                List<Student> studenten = studentService.GetStudentenByStudentRollen(beoordelaar.StudentRollen);
                List<List<Rol>> studentPerRollen = studentService.GetRollenByStudent(beoordelaar.StudentRollen);

                jsonString += "{beoordelaar: \"" + beoordelaar.UserName + "\", ";
                jsonString += "beoordelaarID: \"" + beoordelaar.Id + "\", ";
                jsonString += "leerlingen:[";

                for (int i = 0; i < studenten.Count; i++) {
                    for (int j = 0; j < studentPerRollen[i].Count; j++) {
                        jsonString += "{";
                        jsonString += "studentRol: \"" + studenten[i].ID + "." + studentPerRollen[i][j].ID + "\", ";
                        jsonString += "naam: \"" + studenten[i].Naam + "\", ";
                        jsonString += "opleiding: \"" + studenten[i].Opleiding + "\", ";
                        jsonString += "userName: \"" + beoordelaar.UserName + "\", ";
                        jsonString += "userID: \"" + beoordelaar.Id + "\", ";
                        jsonString += "rol: \"" + studentPerRollen[i][j].Naam + "\"";
                        jsonString += "},";
                    }
                }

                //laatste komma wissen, deze is niet nodig
                jsonString = jsonString.Remove(jsonString.Length - 1);

                jsonString += "]},";
            }

            //laatste komma wissen, deze is niet nodig
            jsonString = jsonString.Remove(jsonString.Length - 1);

            jsonString += "]}";

            return new HtmlString(jsonString);
        }

        public IHtmlString SerializeObject(object studentenlijst, object studentPerRollenlijst, object userID) {
            List<Student> studenten = (List<Student>)studentenlijst;
            List<List<Rol>> studentPerRollen = (List<List<Rol>>)studentPerRollenlijst;

            string jsonString = "{Studenten:[";

            for (int i = 0; i < studenten.Count; i++) {
                for (int j = 0; j < studentPerRollen[i].Count; j++) {
                    jsonString += "{";
                    jsonString += "studentRol: \"" + studenten[i].ID + "." + studentPerRollen[i][j].ID + "\", ";
                    jsonString += "naam: \"" + studenten[i].Naam + "\", ";
                    jsonString += "opleiding: \"" + studenten[i].Opleiding + "\", ";
                    jsonString += "userID: \"" + userID + "\", ";
                    jsonString += "rol: \"" + studentPerRollen[i][j].Naam + "\"";
                    jsonString += "},";
                }
            }

            //laatste komma wissen, deze is niet nodig
            jsonString = jsonString.Remove(jsonString.Length - 1);

            jsonString += "]}";

            return new HtmlString(jsonString);
        }

        public IHtmlString MaakStudentString(object studentlijst, object resultaatlijst)
        {
            List<Student> studenten = (List<Student>)studentlijst;
            List<Resultaat> resultaten = (List<Resultaat>)resultaatlijst;
            string jsonString = "{Studenten:[";

            foreach (Student student in studenten)
            {
                bool heeftScore = false;
                jsonString += "{";
                jsonString += "naam: \"" + student.Naam + "\",";
                jsonString += "academiejaar: \"" + "2014-2015" + "\",";
                jsonString += "trajecttype: \"" + student.Trajecttype + "\",";
                foreach (Resultaat resultaat in resultaten)
                {
                    if (resultaat.StudentId.Equals(student.ID))
                    {
                        heeftScore = true;
                        if(!resultaat.TotaalTussentijdResultaat.Equals(0) && !resultaat.TotaalEindresultaat.Equals(0))
                        {
                            jsonString += "tussentijdse: \"" + resultaat.TotaalTussentijdResultaat + "\",";
                            jsonString += "eind: \"" + resultaat.TotaalEindresultaat + "\",";
                        }else if(!resultaat.TotaalTussentijdResultaat.Equals(0) && resultaat.TotaalEindresultaat.Equals(0))
                        {
                            jsonString += "tussentijdse: \"" + resultaat.TotaalTussentijdResultaat + "\",";
                            jsonString += "eind: \"" + "-" + "\",";
                        }else if(resultaat.TotaalTussentijdResultaat.Equals(0) && !resultaat.TotaalEindresultaat.Equals(0))
                        {
                            jsonString += "tussentijdse: \"" + "-" + "\",";
                            jsonString += "eind: \"" + resultaat.TotaalEindresultaat + "\",";
                        }
                        else
                        {
                            jsonString += "tussentijdse: \"" + "-" + "\",";
                            jsonString += "eind: \"" + "-" + "\",";
                        }
                    }
                    if (!heeftScore)
                    {
                        jsonString += "tussentijdse: \"" + "-" + "\",";
                        jsonString += "eind: \"" + "-" + "\",";
                    }
                }
                

                jsonString += "id: " + student.ID + ",";
                jsonString += "opleiding: \"" + student.Opleiding + "\",";
                jsonString += "studentId: " + student.StudentId + "";
                jsonString += "},";
            }

            //laatste komma wissen, deze is niet nodig
            jsonString = jsonString.Remove(jsonString.Length - 1);

            jsonString += "]}";

            return new HtmlString(jsonString);
        }

        public Student GetStudentByID(int id)
        {
            return studentRepository.GetByID(id);
        }

        public Rol GetRolById(int id)
        {
            return rolRepository.GetByID(id);
        }

        public List<Student> GetStudentenByStudentRollen(List<StudentRollen> studentRollen) {
            List<Student> studenten = new List<Student>();
            
            foreach (StudentRollen studentRol in studentRollen) {
                studenten.Add(studentRol.Student);
            }

            return studenten;
        }

        public List<List<Rol>> GetRollenByStudent(List<StudentRollen> studentRollen) {
            List<List<Rol>> rollen = new List<List<Rol>>();

            for (int i = 0; i < studentRollen.Count; i++) {
                rollen.Add(studentRollen[i].Rollen);
            }

            return rollen;
        }

        public int GetAantalTeTonenStudenten(List<StudentRollen> studentRollen) {
            int counter = 0;

            for (int i = 0; i < GetStudentenByStudentRollen(studentRollen).Count; i++) {
                for (int j = 0; j < GetRollenByStudent(studentRollen)[i].Count; j++) {
                    counter++;
                }
            }

            return counter;
        }

        public ApplicationUser GetUserById(string userId)
        {
            return accountRepository.GetByID(userId);
        }

        public int GetAantalTussentijds(string opleiding, int minimum, int maximum)
        {
            return studentRepository.AantalStudentenTussentijds(opleiding,minimum,maximum);
        }

        public int GetAantalEind(string opleiding, int minimum, int maximum)
        {
            return studentRepository.AantalStudentenEind(opleiding, minimum, maximum);
        }
    }
}
