using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ngetu20108278Library;
using PROG6212_POE_Mkhuseli_Ngetu_20108278__V4_.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SemesterRegistrationPOE.Controllers
{
    public class Home : Controller
    {
        private readonly ILogger<Home> _logger;

        Classes All = new Classes();

        public Home(ILogger<Home> logger)
        {
            _logger = logger;

            All.InstanciateAll();
            

        }
       
        public IActionResult Index()
        {
          
            
            if (Request.Cookies["LoggedInUser"] == null)
            {
                ViewData["LoginStatus"] = "You Are Not Logged In";///(Trivedi, 2021).
                ViewBag.ModulesLoadedMessage = "Modules have been loaded from the database successfully.";///(TutorialsTeacher, 2021).
            }
            else
            {
              
                ViewData["LoginStatus"] = "Current User : " + Request.Cookies["LoggedInUser"];///(Trivedi, 2021).
                ViewBag.Message4 = "You have successfully logged in";///(TutorialsTeacher, 2021).
                ViewBag.ModulesLoadedMessage = "Modules stored in your user account have been loaded from the database successfully";///(TutorialsTeacher, 2021).
            }

            All.GetReminder();

            try
            {
             

                if (DateTime.Today.Date == Convert.ToDateTime(All[2]))
                {

                    ViewData["StudyReminder1"] = "Reminder: Study " + All[1] + "(" + All[0] + ")" + " !";///(Trivedi, 2021).

                    ViewData["StudyReminder2"] = "Orignal Date Set For Studying: " + All[2];///(Trivedi, 2021).

                }
                else
                {
                    ViewData["StudyReminder1"] = "You have not set any reminders for today: " + DateTime.Today;///(Trivedi, 2021).




                }


            }catch{



            }
           
            return View(All);///(Jon Hilton, 2017).
        }

        [HttpGet]
        public IActionResult CreateModules()///(Jon Hilton, 2017).
        {


                All.ModuleDetails[0] = "";
                All.ModuleDetails[1] = "";
                All.ModuleDetails[2] = "";
                All.ModuleDetails[3] = "";
                All.ModuleDetails[4] = "";

           
            if (Request.Cookies["LoggedInUser"] == null)
            {


                ViewData["LoginStatus"] = "You Are Not Logged In";///(Trivedi, 2021).


            }
            else
            {

                ViewData["LoginStatus"] = "Current User : " + Request.Cookies["LoggedInUser"];///(Trivedi, 2021).


            }

            if (Request.Cookies["SemesterStartDate"] == null)
            {


                ViewData["StartDateOfSemester"] = " Has not been set set";///(Trivedi, 2021).
                ViewData["NumberOfWeeksInASemester"] = " Has not been set yet";///(Trivedi, 2021).
            }
            else
            {

                ViewData["StartDateOfSemester"] =  Request.Cookies["SemesterStartDate"];///(Trivedi, 2021).
                ViewData["NumberOfWeeksInASemester"] = Request.Cookies["WeeksInSemester"];///(Trivedi, 2021).
            }



            return View(All);///(Jon Hilton, 2017).

        }
        [HttpPost]
        public IActionResult CreateModules(string NumberOfWeeks, string StartDateOfSemester ,string ModuleCode, string ModuleName, string ModuleCredits, string RecommendedClassHours,string SubmitButton,string StartDateButton)///(Jon Hilton, 2017).
        {
           
            if (Request.Cookies["LoggedInUser"] == null)
            {
                ViewData["LoginStatus"] = "You Are Not Logged In";///(Trivedi, 2021).
            }
            else
            {
                ViewData["LoginStatus"] = "Current User : " + Request.Cookies["LoggedInUser"];///(Trivedi, 2021).
            }


            if (StartDateButton == "Set Start Date & Number Of Weeks" && Request.Cookies["SemesterStartDate"]==null && Request.Cookies["WeeksInSemester"]==null)
            {

                    Response.Cookies.Append("SemesterStartDate", StartDateOfSemester);///(Learn Razor Pages, 2020).
                    Response.Cookies.Append("WeeksInSemester", NumberOfWeeks);///(Learn Razor Pages, 2020).

                    ViewData["StartDateOfSemester"] =  StartDateOfSemester;///(Trivedi, 2021).
                    ViewData["NumberOfWeeksInASemester"] = NumberOfWeeks;///(Trivedi, 2021).

                    ViewBag.Message0 = " The number of weeks and start date of the semester has been successfully set";///(TutorialsTeacher, 2021).
                    ViewBag.Message1 = "";///(TutorialsTeacher, 2021).
                    ViewBag.Message2 = "";///(TutorialsTeacher, 2021).
            }
            else
            {
                ViewData["StartDateOfSemester"] = Request.Cookies["SemesterStartDate"];///(Trivedi, 2021).
                ViewData["NumberOfWeeksInASemester"] = Request.Cookies["WeeksInSemester"];///(Trivedi, 2021).


                ViewBag.Message0 = "ERROR: The number of weeks and start date of the semester have already been set";///(TutorialsTeacher, 2021).
                ViewBag.Message1 ="";///(TutorialsTeacher, 2021).
                ViewBag.Message2 = "";///(TutorialsTeacher, 2021).

            }


            if (SubmitButton == "Submit" && Request.Cookies["SemesterStartDate"] != null && Request.Cookies["WeeksInSemester"] != null)
            {
                All.ModuleDetails[0] = ModuleCode;
                All.ModuleDetails[1] = ModuleName;
                All.ModuleDetails[2] = ModuleCredits;
                All.ModuleDetails[3] = RecommendedClassHours;
                All.ModuleDetails[4] = Request.Cookies["WeeksInSemester"].ToString();

              
                All.Tables.CalculateSelfStudyHours();
                All.Tables.CalculateRemainingSelfStudyHours();

                if (Request.Cookies["LoggedInUser"] != null)
                {
                    All.TheDatabases[0] = Request.Cookies["LoggedInUser"].ToString();
                    All.TheDatabases.AddModulesToLoggedInUser();
                    ViewBag.Message0 = "";///(TutorialsTeacher, 2021).
                    ViewBag.Message1 = ModuleCode + " has been created successfully";///(TutorialsTeacher, 2021).
                    ViewBag.Message2 = ModuleCode + " has been saved successfully to " + Request.Cookies["LoggedInUser"] + " 's profile within the database";///(TutorialsTeacher, 2021).
                   
                }
                else
                {
                    All.TheDatabases.AddModulesToDatabase();
                    ViewBag.Message0 = "";///(TutorialsTeacher, 2021).
                    ViewBag.Message1 = ModuleCode + " has been created successfully";///(TutorialsTeacher, 2021).
                    ViewBag.Message2 = ModuleCode + " has been saved successfully to the database";///(TutorialsTeacher, 2021).
                   
                }


            }
            else
            {

                if(SubmitButton == "Submit" && ModuleCode == null)
                {

                    ViewBag.Message1 = "You have not entered a code for your module. Please re-enter a code for your module";///(TutorialsTeacher, 2021).
                }
                else
                {

                    if (SubmitButton == "Submit" && ModuleName == null)
                    {


                        ViewBag.Message1 = "You have not entered a name for your module. Please re-enter a code for your module";///(TutorialsTeacher, 2021).

                    }
                    else
                    {


                       
                    }
                }
            }
         
            return View(All);///(Jon Hilton, 2017).

        }


        [HttpGet]
        public IActionResult RecordSelfStudyHours()///(Jon Hilton, 2017).
        {

            if (Request.Cookies["LoggedInUser"] == null)
            {
                ViewData["LoginStatus"] = "You Are Not Logged In";///(Trivedi, 2021).
            }
            else
            {
                ViewData["LoginStatus"] = "Current User : " + Request.Cookies["LoggedInUser"];///(Trivedi, 2021).
            }

            All.TheDatabases[2] = "";
            All.TheDatabases[3] = "";

          
            return View(All);///(Jon Hilton, 2017).
        }


        [HttpPost]
        public IActionResult RecordSelfStudyHours(string SelfStudyModuleCode, string SelfStudyDate, string SelfStudyHours,string SubmitButton)///(Jon Hilton, 2017).
        {
            if (Request.Cookies["LoggedInUser"] == null)
            {


                ViewData["LoginStatus"] = "You Are Not Logged In";///(Trivedi, 2021).


            }
            else
            {

                ViewData["LoginStatus"] = "Current User : " + Request.Cookies["LoggedInUser"];///(Trivedi, 2021).


            }

            if (SubmitButton == "Submit" && SelfStudyModuleCode !=null && SelfStudyDate!= null && SelfStudyHours!= null)
            {
                All.TheDatabases[2] = SelfStudyModuleCode;
                All.TheDatabases[3] = SelfStudyHours;

                All[0] = All.TheDatabases[2];
                All[1] = SelfStudyDate;
                All[2] = All.TheDatabases[3];

                

                if (Request.Cookies["LoggedInUser"] != null)
                {
                     All.TheDatabases.UpdateSelfStudyHoursLoggedInUser();
                  
                }
            
                else
                {
                    All.TheDatabases.UpdateSelfStudyHours();
                  
                }

                ViewBag.Message3 = SelfStudyModuleCode + " has been updated successfully";///(TutorialsTeacher, 2021).


            }
            else
            {

                if(SubmitButton == "Submit" && SelfStudyModuleCode == null)
                {

                    ViewBag.Message3 =  "The module code you have entered is invalid. Please re-enter a valid module code";///(TutorialsTeacher, 2021).

                }
                else
                {

                    if(SubmitButton == "Submit" && SelfStudyDate == null)
                    {


                        ViewBag.Message3 = "The date you have entered is invalid. Please re-enter a valid date";///(TutorialsTeacher, 2021).

                    }
                    
                }
            }


            return View(All);///(Jon Hilton, 2017).
        }

        [HttpGet]
        public IActionResult Login()///(Jon Hilton, 2017).
        {

            if (Request.Cookies["LoggedInUser"] == null)
            {
                ViewData["LoginStatus"] = "You Are Not Logged In";///(Trivedi, 2021).
            }
            else
            {
                ViewData["LoginStatus"] = "Current User : " + Request.Cookies["LoggedInUser"];///(Trivedi, 2021).
            }

            All.TheDatabases[0] = "";
            All.TheDatabases[1] = "";

            
            return View(All);///(Jon Hilton, 2017).
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Login(string Username, string Password,string LoginButton)///(Jon Hilton, 2017).
        {
            if (Username != null && Password != null && LoginButton=="Login")
            {
                All.TheDatabases[0] = Username;
                All.TheDatabases[1] = Password;

                All.TheDatabases.StudentLogin();

                Response.Cookies.Append("LoggedInUser", Username);///(Learn Razor Pages, 2020).

                ViewBag.Message4 = Username + " has logged in successfully";///(TutorialsTeacher, 2021).

                return RedirectToAction("Index","Home");


            }
            else
            {
                if (LoginButton == "Login" && Username == null)
                {


                    ViewBag.Message4 = "You have not entered a username. Please-re-enter your username";///(TutorialsTeacher, 2021).

                }
                else
                {

                    if (LoginButton == "Login" && Password == null)
                    {


                        ViewBag.Message4 = "You have not entered a password. Please-re-enter your password";///(TutorialsTeacher, 2021).

                    }


                }



            }

            return View(All);///(Jon Hilton, 2017).
        }

        [HttpGet]
        public IActionResult Registration()///(Jon Hilton, 2017).
        {
            if (Request.Cookies["LoggedInUser"] == null)
            {
                ViewData["LoginStatus"] = "You Are Not Logged In";///(Trivedi, 2021).
            }
            else
            {
                ViewData["LoginStatus"] = "Current User : " + Request.Cookies["LoggedInUser"];///(Trivedi, 2021).
            }

            ViewData["PasswordAdvice1"] = "Passwords must be between 5 to 10 characters long";///(Trivedi, 2021).
            ViewData["PasswordAdvice2"] = "Passwords must contain at least 1 uppercase letter(A-Z)";///(Trivedi, 2021).
            ViewData["PasswordAdvice3"] = "Passwords must contain at more that 1 lower case letter(a - z)";///(Trivedi, 2021).
            ViewData["PasswordAdvice4"] = "Passwords must contain 1 special character";///(Trivedi, 2021).
            ViewData["PasswordAdvice5"] = "Passwords must contain 2 or less numbers";///(Trivedi, 2021).
            ViewData["PasswordAdvice6"] = "Do not create password around names of other individuals, pets, or locations";///(Trivedi, 2021).
            ViewData["PasswordAdvice7"] = "Please keep this password safe";///(Trivedi, 2021).
            ViewData["PasswordAdvice8"] = "Do not share your password with other users / individuals";///(Trivedi, 2021).

            All.TheDatabases[0] = "";
            All.TheDatabases[1] = "";

            return View(All);
        }

        [HttpPost]
        public IActionResult Registration(string RegistrationButton,string NewUserName,string NewPassword)///(Jon Hilton, 2017).
        {
            ViewData["PasswordAdvice1"] = "Passwords must be between 5 to 10 characters long";///(Trivedi, 2021).
            ViewData["PasswordAdvice2"] = "Passwords must contain at least 1 uppercase letter(A-Z)";///(Trivedi, 2021).
            ViewData["PasswordAdvice3"] = "Passwords must contain at more that 1 lower case letter(a - z)";///(Trivedi, 2021).
            ViewData["PasswordAdvice4"] = "Passwords must contain 1 special character";///(Trivedi, 2021).
            ViewData["PasswordAdvice5"] = "Passwords must contain 2 or less numbers";///(Trivedi, 2021).
            ViewData["PasswordAdvice6"] = "Do not create password around names of other individuals, pets, or locations";///(Trivedi, 2021).
            ViewData["PasswordAdvice7"] = "Please keep this password safe";///(Trivedi, 2021).
            ViewData["PasswordAdvice8"] = "Do not share your password with other users / individuals";///(Trivedi, 2021).

            if (RegistrationButton=="Submit" && NewUserName !=null && NewPassword !=null)
            {
                All.TheDatabases[0] = NewUserName;
                All.TheDatabases[1] = NewPassword;

                All.TheDatabases.StudentRegistrationDetails();
                ViewBag.Message5 = NewUserName + " has been created successfully";///(TutorialsTeacher, 2021).

            }
            else
            {
                if (RegistrationButton == "Submit" && NewUserName == null) 
                {

                    ViewBag.Message5 = "You have not entered a username. Please-re-enter a username";///(TutorialsTeacher, 2021).

                }
                else
                {


                    if(RegistrationButton == "Submit" && NewPassword == null)
                    {

                        ViewBag.Message5 = "You have not entered a passsword. Please-re-enter a password";///(TutorialsTeacher, 2021).

                    }
                }
            }


            return View(All);///(Jon Hilton, 2017).
        }

        [HttpGet]
        public IActionResult Logout()
        {
           
            if (Request.Cookies["LoggedInUser"] == null)
            {
                ViewData["LoginStatus"] = "You Are Not Logged In";///(Trivedi, 2021).
            }
            else
            {
                ViewData["LoginStatus"] = "Current User : " + Request.Cookies["LoggedInUser"];///(Trivedi, 2021).
            }

            return View(All);///(Jon Hilton, 2017).
        }
        [HttpPost]
        public IActionResult Logout(string LogoutButton)///(Jon Hilton, 2017).
        {
            if(LogoutButton == "Logout")
            {
                if (Request.Cookies["LoggedInUser"] == null)
                {
                    ViewData["LoginStatus"] = "You cannot log out. You have not logged in yet";///(Trivedi, 2021).
                }
                else
                {
                    Response.Cookies.Delete("LoggedInUser");
                    ViewData["LoginStatus"] = "You Are Not Logged In";///(Trivedi, 2021).
                   
                    All.MainDataTableDetails().Clear();

                    ViewBag.Message6 = "You have successfully logged out";///(TutorialsTeacher, 2021).
                    ViewBag.Message7 = "Goodbye";///(TutorialsTeacher, 2021).

                }

            }
            return View(All);
        }
        [HttpGet]
        public IActionResult StudyReminder()///(Jon Hilton, 2017).
        {
            if (Request.Cookies["LoggedInUser"] == null)
            {


                ViewData["LoginStatus"] = "You Are Not Logged In";///(Trivedi, 2021).


            }
            else
            {

                ViewData["LoginStatus"] = "Current User : " + Request.Cookies["LoggedInUser"];///(Trivedi, 2021).


            }



            return View(All);///(Jon Hilton, 2017).
        }

        [HttpPost]
        public IActionResult StudyReminder(string StudyModuleCode,string StudyModuleName,string StudyDate,string SetReminder)///(Jon Hilton, 2017).
        {
            
            if (Request.Cookies["LoggedInUser"] == null)
            {


                ViewData["LoginStatus"] = "You Are Not Logged In";///(Trivedi, 2021).


            }
            else
            {

                ViewData["LoginStatus"] = "Current User : " + Request.Cookies["LoggedInUser"];///(Trivedi, 2021).


            }

            if (SetReminder=="Set Reminder" && StudyModuleCode != null && StudyModuleName != null && StudyDate !=null)
            {
                All[0] = StudyModuleCode;
                All[1] = StudyModuleName;
                All[2] = StudyDate;

                All.AddReminderData();

                ViewBag.Message7 = "A Reminder for "+ StudyModuleName + " has been created successfully";///(TutorialsTeacher, 2021).




            }
            else
            {


                if (SetReminder == "Set Reminder" && StudyModuleCode == null)
                {



                    ViewBag.Message7 = "You have not entered the code for the module. Please re-enter a code for the module";///(TutorialsTeacher, 2021).

                }
                else
                {


                    if(SetReminder == "Set Reminder" && StudyModuleName == null)
                    {

                        ViewBag.Message7 = "You have not entered a name for the module. Please re-renter a name for the module";///(TutorialsTeacher, 2021).

                    }
                    else
                    {


                        if(SetReminder == "Set Reminder" && StudyDate == null)
                        {



                            ViewBag.Message7 = "You have not entered a date for the module. Please re-enter a date for your reminder";///(TutorialsTeacher, 2021).
                        }
                    }
                }


            }




            return View(All);///(Jon Hilton, 2017).
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
