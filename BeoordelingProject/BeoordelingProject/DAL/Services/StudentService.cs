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

        private Dictionary<string, int> getKolommen(string[] columnHeaders) {
            Dictionary<string, int> kolommen = new Dictionary<string, int>();

            // index: 7 20 1 15 14 18 17
            string[] kolomNamen = new string[] {
                "student", "trajecttype", "opleiding", "howest_email", "studentid", "geslacht", "geboordat"
            };

            for (int i = 0; i < columnHeaders.Length; i++) {
                foreach (string kolomNaam in kolomNamen) {
                    if (kolomNaam.Equals(columnHeaders[i].ToLower())) {
                        kolommen.Add(kolomNaam, i);
                    }
                }
            }

            return kolommen;
        }

        public List<Student> CreateStudenten(string csvData, string academiejaar)
        {
            List<Student> studenten = new List<Student>();

            using (StringReader textReader = new StringReader(csvData))
            {
                string line = textReader.ReadLine();

                string[] columnHeaders = line.Split(';');
                Dictionary<string, int> kolommen = getKolommen(columnHeaders);

                line = textReader.ReadLine();

                while (line != null)
                {
                    string[] columns = line.Split(';');

                    Student student = new Student() {
                        Naam = columns[kolommen["student"]],
                        Trajecttype = columns[kolommen["trajecttype"]],
                        Opleiding = columns[kolommen["opleiding"]],
                        Email = columns[kolommen["howest_email"]],
                        StudentId = int.Parse(columns[kolommen["studentid"]]),
                        Geslacht = columns[kolommen["geslacht"]],
                        Geboortedatum = columns[kolommen["geboordat"]],
                        academiejaar = academiejaar
                    };

                    /*studentRepository.Insert(student);
                    uow.SaveChanges();*/

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

        public Student GetUserById(int Id)
        {
            return studentRepository.GetByID(Id);
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
                jsonString += "academiejaar: \"" + student.academiejaar + "\",";
                jsonString += "trajecttype: \"" + student.Trajecttype + "\",";
                foreach (Resultaat resultaat in resultaten)
                {
                    if (resultaat.StudentId.Equals(student.ID))
                    {
                        heeftScore = true;
                        if (!resultaat.TotaalTussentijdResultaat.Equals(-1) && !resultaat.TotaalEindresultaat.Equals(-1))
                        {
                            jsonString += "tussentijdse: \"" + Math.Round(resultaat.TotaalTussentijdResultaat, 0) + "\",";
                            jsonString += "eind: \"" + Math.Round(resultaat.TotaalEindresultaat, 0) + "\",";
                        }
                        else if (!resultaat.TotaalTussentijdResultaat.Equals(-1) && resultaat.TotaalEindresultaat.Equals(-1))
                        {
                            jsonString += "tussentijdse: \"" + Math.Round(resultaat.TotaalTussentijdResultaat, 0) + "\",";
                            jsonString += "eind: \"" + "-" + "\",";
                        }
                        else if (resultaat.TotaalTussentijdResultaat.Equals(-1) && !resultaat.TotaalEindresultaat.Equals(-1))
                        {
                            jsonString += "tussentijdse: \"" + "-" + "\",";
                            jsonString += "eind: \"" + Math.Round(resultaat.TotaalEindresultaat, 0) + "\",";
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
