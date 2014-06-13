using BeoordelingProject.DAL.Services;
using BeoordelingProject.Models;
using BeoordelingProject.ViewModel;
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
            accountbeheerVM.Studenten = studentService.GetStudenten();
            accountbeheerVM.Accounts = studentService.GetUsers();
            accountbeheerVM.Rollen = studentService.GetRoles();
            return View(accountbeheerVM);
        }

        [HttpPost]
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
                            if (model.SelectedRolId[i].Equals(studentrollen[s].Rollen[r].ID))
                            {
                                ViewBag.Error = "De rol voor een gekozen student dient uniek te zijn.";

                                var accountbeheerVM = new AccountbeheerVM();
                                accountbeheerVM.Studenten = studentService.GetStudenten();
                                accountbeheerVM.Accounts = studentService.GetUsers();
                                accountbeheerVM.Rollen = studentService.GetRoles();

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

            var user = new ApplicationUser() { UserName = model.Account.UserName };
            user.StudentRollen = studentrollen;
            var result = userService.Create(user, model.Account.PasswordHash);
            userService.AddUserToRoleUser(user.Id);

            return RedirectToAction("AddStudentRol", "Accountbeheer");
        }

        public ActionResult DeleteUser(string userId)
        {
            ApplicationUser tedeletenUser = studentService.GetUserById(userId);
            //List<StudentRollen> studentrollenVanUser = tedeletenUser.StudentRollen;
            
            

            studentService.DeleteUser(tedeletenUser);


            return RedirectToAction("AddStudentRol", "Accountbeheer");
        }
	}
}