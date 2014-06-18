using BeoordelingProject.DAL.Services;
using BeoordelingProject.Models;
using BeoordelingProject.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.Controllers
{
    public class AccountbeheerController : Controller
    {
        private IStudentService studentService = null;
        private IUserManagementService userService = null;
        private IStudentrolService studentrolService = null;
        
        public AccountbeheerController()
        {

        }

        public AccountbeheerController(IStudentService studentService, IUserManagementService userService, IStudentrolService studentrolService)
        {
            this.studentService = studentService;
            this.userService = userService;
            this.studentrolService = studentrolService;
        }

        [Authorize(Roles="Admin")]
        public ActionResult AddStudentRol()
        {
            var accountbeheerVM = new AccountbeheerVM();
            
            accountbeheerVM.Studenten = new SelectList(studentService.GetStudenten(), "ID", "Naam");
            accountbeheerVM.Accounts = studentService.GetUsers();
            
            accountbeheerVM.Rollen = new SelectList(studentService.GetRoles(), "ID", "Naam");

            List<StudentKeuzeVM> studentKeuzes = new List<StudentKeuzeVM>();

            foreach (ApplicationUser beoordelaar in accountbeheerVM.Accounts) {
                StudentKeuzeVM vm = new StudentKeuzeVM();

                vm.Studenten = studentService.GetStudentenByStudentRollen(beoordelaar.StudentRollen);
                vm.RollenPerStudent = studentService.GetRollenByStudent(beoordelaar.StudentRollen);
                vm.Aantal = studentService.GetAantalTeTonenStudenten(beoordelaar.StudentRollen);
                vm.StudentenString = studentService.SerializeObject(vm.Studenten, vm.RollenPerStudent, beoordelaar.Id);

                studentKeuzes.Add(vm);
            }

            accountbeheerVM.studentKeuzesVM = studentKeuzes;
            accountbeheerVM.StudentenString = studentService.SerializeObject(accountbeheerVM.Accounts);

            return View(accountbeheerVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddStudentRol(AccountbeheerVM model)
        {
            bool inArray = false;
            List<StudentRollen> studentrollen = new List<StudentRollen>();
            for (int i = 0; i < model.SelectedRolId.Count;i++ )
            {
                Rol selectedRol = new Rol();
                for (int s = 0; s < studentrollen.Count;s++ )
                {
                    if (model.SelectedStudentId[i].Equals(studentrollen[s].Student.ID))
                    {
                        for (int r = 0; r < studentrollen[s].Rollen.Count; r++)
                        {
                            //unieke studentrol?
                            if (model.SelectedRolId[i].Equals(studentrollen[s].Rollen[r].ID))
                            {
                                ViewBag.Error = "De rol voor een gekozen student dient uniek te zijn.";

                                var accountbeheerVM = new AccountbeheerVM();
                                
                                accountbeheerVM.Studenten = new SelectList(studentService.GetStudenten(), "ID", "Naam");
                                accountbeheerVM.Accounts = studentService.GetUsers();
       
                                accountbeheerVM.Rollen = new SelectList(studentService.GetRoles(), "ID", "Naam");

                                List<StudentKeuzeVM> studentKeuzes = new List<StudentKeuzeVM>();

                                foreach (ApplicationUser beoordelaar in accountbeheerVM.Accounts)
                                {
                                    StudentKeuzeVM vm = new StudentKeuzeVM();

                                    vm.Studenten = studentService.GetStudentenByStudentRollen(beoordelaar.StudentRollen);
                                    vm.RollenPerStudent = studentService.GetRollenByStudent(beoordelaar.StudentRollen);
                                    vm.Aantal = studentService.GetAantalTeTonenStudenten(beoordelaar.StudentRollen);
                                    vm.StudentenString = studentService.SerializeObject(vm.Studenten, vm.RollenPerStudent, beoordelaar.Id);

                                    studentKeuzes.Add(vm);
                                }

                                accountbeheerVM.studentKeuzesVM = studentKeuzes;
                                accountbeheerVM.StudentenString = studentService.SerializeObject(accountbeheerVM.Accounts);

                                accountbeheerVM.SelectedStudentId = model.SelectedStudentId;
                                accountbeheerVM.SelectedRolId = model.SelectedRolId;

                                return View(accountbeheerVM);

                            }
                        }
                        selectedRol = studentService.GetRolById(model.SelectedRolId[i]);
                        studentrollen[s].Rollen.Add(selectedRol);
                        inArray = true;
                    }
                }
                if (!inArray)
                {
                    Student selectedStudent = studentService.GetStudentByID(model.SelectedStudentId[i]);
                    List<Rol> selectedRollen = new List<Rol>();
                    selectedRollen.Add(studentService.GetRolById(model.SelectedRolId[i]));
                    StudentRollen studentrol = studentrolService.CreateStudentrol(selectedStudent, selectedRollen);
                    studentrollen.Add(studentrol);
                }
            }

            var user = new ApplicationUser() { UserName = model.registerVM.UserName };
            user.StudentRollen = studentrollen;

            var users = studentService.GetUsers();
            foreach (ApplicationUser gebruiker in users)
            {
                //unieke gebruiker?
                if (gebruiker.UserName.ToLower() == user.UserName.ToLower())
                {
                    ViewBag.Error = "De gebruiker bestaat al.";

                    var accountbeheerVM = new AccountbeheerVM();

                    accountbeheerVM.Studenten = new SelectList(studentService.GetStudenten(), "ID", "Naam");
                    accountbeheerVM.Accounts = studentService.GetUsers();

                    accountbeheerVM.Rollen = new SelectList(studentService.GetRoles(), "ID", "Naam");

                    List<StudentKeuzeVM> studentKeuzes = new List<StudentKeuzeVM>();

                    foreach (ApplicationUser beoordelaar in accountbeheerVM.Accounts)
                    {
                        StudentKeuzeVM vm = new StudentKeuzeVM();

                        vm.Studenten = studentService.GetStudentenByStudentRollen(beoordelaar.StudentRollen);
                        vm.RollenPerStudent = studentService.GetRollenByStudent(beoordelaar.StudentRollen);
                        vm.Aantal = studentService.GetAantalTeTonenStudenten(beoordelaar.StudentRollen);
                        vm.StudentenString = studentService.SerializeObject(vm.Studenten, vm.RollenPerStudent, beoordelaar.Id);

                        studentKeuzes.Add(vm);
                    }

                    accountbeheerVM.studentKeuzesVM = studentKeuzes;
                    accountbeheerVM.StudentenString = studentService.SerializeObject(accountbeheerVM.Accounts);

                    accountbeheerVM.SelectedStudentId = model.SelectedStudentId;
                    accountbeheerVM.SelectedRolId = model.SelectedRolId;

                    return View(accountbeheerVM);
                }
            }
            var result = userService.Create(user, model.registerVM.Password);
            userService.AddUserToRoleUser(user.Id);
            return RedirectToAction("AddStudentRol", "Accountbeheer");
        }

        public ActionResult DeleteUser(string userID)
        {
            ApplicationUser tedeletenUser = studentService.GetUserById(userID);
           
            studentService.DeleteUser(tedeletenUser);


            return RedirectToAction("AddStudentRol", "Accountbeheer");
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public ActionResult EditUser(string userId)
        {
            if (userId != null)
            {
                var accountbeheerVM = new AccountbeheerVM();

                accountbeheerVM.Studenten = new SelectList(studentService.GetStudenten(), "ID", "Naam");

                List<ApplicationUser> accounts = new List<ApplicationUser>();
                ApplicationUser user = studentService.GetUserById(userId);
                accounts.Add(user);
                accountbeheerVM.Accounts = accounts;

                accountbeheerVM.Rollen = new SelectList(studentService.GetRoles(), "ID", "Naam");

                List<StudentKeuzeVM> vms = new List<StudentKeuzeVM>();
                StudentKeuzeVM vm = new StudentKeuzeVM();
                vm.Studenten = studentService.GetStudentenByStudentRollen(user.StudentRollen);
                vm.RollenPerStudent = studentService.GetRollenByStudent(user.StudentRollen);
                vms.Add(vm);

                accountbeheerVM.studentKeuzesVM = vms;

                List<int> selectedStudentIds = new List<int>();
                List<int> selectedRolIds = new List<int>();

                foreach(StudentRollen studentrol in user.StudentRollen)
                {
                    
                    foreach(Rol rol in studentrol.Rollen)
                    {
                        selectedStudentIds.Add(studentrol.Student.ID);
                        selectedRolIds.Add(rol.ID);
                    }
                }

                accountbeheerVM.SelectedAccountId = user.Id;
                accountbeheerVM.SelectedStudentId = selectedStudentIds;
                accountbeheerVM.SelectedRolId = selectedRolIds;

                return View(accountbeheerVM);
            }
            else
            {
                return RedirectToAction("AddStudentRol", "Accountbeheer");
            }
        }

        [HttpPost]
        public ActionResult EditUser(AccountbeheerVM model)
        {
            //user editen
            ApplicationUser user = studentService.GetUserById(model.SelectedAccountId);
            user.UserName = model.registerVM.UserName;
            user.StudentRollen.Clear();

            //studentrollen editen
            bool inArray = false;
            List<StudentRollen> studentrollen = new List<StudentRollen>();
            for (int i = 0; i < model.SelectedRolId.Count; i++)
            {
                Rol selectedRol = new Rol();
                for (int s = 0; s < studentrollen.Count; s++)
                {
                    inArray = false;
                    if (model.SelectedStudentId[i].Equals(studentrollen[s].Student.ID))
                    {
                        for (int r = 0; r < studentrollen[s].Rollen.Count; r++)
                        {
                            if (model.SelectedRolId[i].Equals(studentrollen[s].Rollen[r].ID))
                            {
                                ViewBag.Error = "De rol voor een gekozen student dient uniek te zijn.";

                                var accountbeheerVM = new AccountbeheerVM();

                                accountbeheerVM.Studenten = new SelectList(studentService.GetStudenten(), "ID", "Naam");
                                accountbeheerVM.Accounts = studentService.GetUsers();

                                accountbeheerVM.Rollen = new SelectList(studentService.GetRoles(), "ID", "Naam");

                                List<StudentKeuzeVM> studentKeuzes = new List<StudentKeuzeVM>();

                                foreach (ApplicationUser beoordelaar in accountbeheerVM.Accounts)
                                {
                                    StudentKeuzeVM vm = new StudentKeuzeVM();

                                    vm.Studenten = studentService.GetStudentenByStudentRollen(beoordelaar.StudentRollen);
                                    vm.RollenPerStudent = studentService.GetRollenByStudent(beoordelaar.StudentRollen);
                                    vm.Aantal = studentService.GetAantalTeTonenStudenten(beoordelaar.StudentRollen);
                                    vm.StudentenString = studentService.SerializeObject(vm.Studenten, vm.RollenPerStudent, beoordelaar.Id);

                                    studentKeuzes.Add(vm);
                                }

                                accountbeheerVM.studentKeuzesVM = studentKeuzes;
                                accountbeheerVM.StudentenString = studentService.SerializeObject(accountbeheerVM.Accounts);

                                accountbeheerVM.SelectedStudentId = model.SelectedStudentId;
                                accountbeheerVM.SelectedRolId = model.SelectedRolId;

                                return View(accountbeheerVM);

                            }
                        }
                        selectedRol = studentService.GetRolById(model.SelectedRolId[i]);
                        studentrollen[s].Rollen.Add(selectedRol);
                        inArray = true;
                    }
                }
                if (!inArray)
                {
                    Student selectedStudent = studentService.GetStudentByID(model.SelectedStudentId[i]);
                    List<Rol> selectedRollen = new List<Rol>();
                    selectedRollen.Add(studentService.GetRolById(model.SelectedRolId[i]));
                    StudentRollen studentrol = studentrolService.CreateStudentrol(selectedStudent, selectedRollen);
                    studentrollen.Add(studentrol);
                }
            }

            user.StudentRollen = studentrollen;

            var users = studentService.GetUsers();
            foreach (ApplicationUser gebruiker in users)
            {
                //unieke gebruiker?
                if (gebruiker.UserName.ToLower() == user.UserName.ToLower())
                {
                    ViewBag.Error = "De gebruiker bestaat al.";

                    var accountbeheerVM = new AccountbeheerVM();

                    accountbeheerVM.Studenten = new SelectList(studentService.GetStudenten(), "ID", "Naam");
                    accountbeheerVM.Accounts = studentService.GetUsers();

                    accountbeheerVM.Rollen = new SelectList(studentService.GetRoles(), "ID", "Naam");

                    List<StudentKeuzeVM> studentKeuzes = new List<StudentKeuzeVM>();

                    foreach (ApplicationUser beoordelaar in accountbeheerVM.Accounts)
                    {
                        StudentKeuzeVM vm = new StudentKeuzeVM();

                        vm.Studenten = studentService.GetStudentenByStudentRollen(beoordelaar.StudentRollen);
                        vm.RollenPerStudent = studentService.GetRollenByStudent(beoordelaar.StudentRollen);
                        vm.Aantal = studentService.GetAantalTeTonenStudenten(beoordelaar.StudentRollen);
                        vm.StudentenString = studentService.SerializeObject(vm.Studenten, vm.RollenPerStudent, beoordelaar.Id);

                        studentKeuzes.Add(vm);
                    }

                    accountbeheerVM.studentKeuzesVM = studentKeuzes;
                    accountbeheerVM.StudentenString = studentService.SerializeObject(accountbeheerVM.Accounts);

                    accountbeheerVM.SelectedStudentId = model.SelectedStudentId;
                    accountbeheerVM.SelectedRolId = model.SelectedRolId;

                    return View(accountbeheerVM);
                }
            }

            userService.EditUser(user);

            return RedirectToAction("AddStudentRol","Accountbeheer");
        }
	}
}