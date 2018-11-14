using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colmart.View_Models;
using Colmart.Model_Manager;
using Colmart.Models;
using Colmart.Assistant_Classes;
using System.Net.Mail;
using System.Text;
using ColmartCMS.Assistant_Classes;

namespace Colmart.Controllers
{

    public class LoginController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            var clsUserLoginRegister = new clsUserLoginRegister();
            HttpCookie hcUserEmailCookie = Request.Cookies["strUserEmail"];
            if (hcUserEmailCookie != null)
            {
                clsUserLoginRegister.clsUserLogin.strEmail = hcUserEmailCookie.Value; //Get the email
                clsUserLoginRegister.clsUserLogin.bRememberMe = true;
            }

            if (TempData["RegisterToLogin"] != null)
            {
                ViewBag.Message = (string)TempData["RegisterToLogin"];
                ViewBag.Register = true;
            }
            else if (TempData["ConfirmToLogin"] != null)
            {
                ViewBag.Message = (string)TempData["ConfirmToLogin"];
                ViewBag.Register = true;
            }
            else if (TempData["bSendUserTempPasswordEmail"] != null)
            {
                ViewBag.Message = "Please check your email address to reset your password!";
                ViewBag.Register = true;
            }
            else if (TempData["bUserEmailnotFound"] != null)
            {
                ViewBag.Message = "Please check if you have entered the correct username/email!";
                ViewBag.Register = true;
            }
            return View(clsUserLoginRegister);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(clsUserLoginRegister clsUserLoginRegister)
        {
            //CMS Users Manager
            clsUsersManager clsUsersManager = new clsUsersManager();
            //CMS User
            clsUsers clsUser = clsUsersManager.getUserByEmail(clsUserLoginRegister.clsUserLogin.strEmail.ToLower());
            if (clsUser == null)
            {
                TempData["bUserEmailnotFound"] = true;
                return RedirectToAction("Login"); // login unsuccessful
            }
            else
            {
                if (clsUser.bIsConfirmed == false)
                {
                    ConfirmationEmail(clsUser.strFirstName, clsUser.strEmailAddress, clsUser.iUserID);
                    ModelState.AddModelError("loginError", "Email Address has not yet been confirmed. Please check your email to confirm the email address.");
                    clsUserLoginRegister.clsUserLogin.strPassword = "";
                    return View(clsUserLoginRegister);
                }
                //If User does not exist
                if (clsUser == null)
                {
                    ModelState.AddModelError("loginError", "Incorrect Login Credentials Entered");
                    clsUserLoginRegister.clsUserLogin.strPassword = "";
                    return View(clsUserLoginRegister);
                }

                string strPasswordHash = "";
                if (clsUserLoginRegister.clsUserLogin.strPassword != null && clsUserLoginRegister.clsUserLogin.strPassword != "")
                    strPasswordHash = clsCommonFunctions.GetMd5Sum(clsUserLoginRegister.clsUserLogin.strPassword);

                //If password is incorrect
                if (clsUser.strPassword != strPasswordHash)
                {
                    ModelState.AddModelError("loginError", "Incorrect Login Credentials Entered");
                    clsUserLoginRegister.clsUserLogin.strPassword = "";
                    return View(clsUserLoginRegister);
                }
                else
                {
                    //Set cookie
                    if (clsUserLoginRegister.clsUserLogin.bRememberMe == true)
                    {
                        HttpCookie hcUserEmailCookie = new HttpCookie("strUserEmail");
                        hcUserEmailCookie.Value = clsUserLoginRegister.clsUserLogin.strEmail;
                        hcUserEmailCookie.Expires = DateTime.Now.AddDays(30); // expires after 30 days
                        HttpContext.Response.Cookies.Add(hcUserEmailCookie);
                    }
                    else
                    {
                        if (Request.Cookies["strUserEmail"] != null)
                        {
                            Response.Cookies["strUserEmail"].Expires = DateTime.Now.AddDays(-1);
                        }
                    }

                    //Reset error message
                    ModelState.AddModelError("loginError", "");
                    Session["clsUser"] = clsUser;

                    return RedirectToAction("Index", "Home"); // login succeed 
                }
            }
        }

        [HttpPost]
        public ActionResult Register(clsUserLoginRegister clsUserLoginRegister)
        {
            if (ModelState.IsValid)
            {
                var clsUsersManager = new clsUsersManager();
                var clsUser = new clsUsers();
                string strPasswordHash = "";

                clsUser.iRoleTypeID = 1;
                clsUser.strFirstName = clsUserLoginRegister.strFirstName;
                clsUser.strSurname = clsUserLoginRegister.strSurname;
                clsUser.strBiographicalInfo = clsUserLoginRegister.strBiographicalInfo;
                clsUser.strContactNumber = clsUserLoginRegister.strContactNumber;
                clsUser.strEmailAddress = clsUserLoginRegister.strEmailAddress;
                clsUser.strCompanyName = clsUserLoginRegister.strCompanyName;
                clsUser.strArea = clsUserLoginRegister.strArea;
                clsUser.strVatNumber = clsUserLoginRegister.strVatNumber;
                clsUser.strBusinessPurpose = clsUserLoginRegister.strBusinessPurpose;

                if (clsUserLoginRegister.strPassword != null && clsUserLoginRegister.strPassword != "")
                    strPasswordHash = clsCommonFunctions.GetMd5Sum(clsUserLoginRegister.strPassword);

                clsUser.strPassword = strPasswordHash;
                clsUser.strPasswordConfirm = strPasswordHash;
                clsUser.strImagePath = clsUserLoginRegister.strImagePath;
                clsUser.strImageName = clsUserLoginRegister.strImageName;
                clsUser.bIsConfirmed = false;
                clsUser.bIsDeleted = false;

                int iUserID = clsUsersManager.registerUser(clsUser);

                HttpCookie hcUserEmailCookie = new HttpCookie("strUserEmail");
                hcUserEmailCookie.Value = clsUserLoginRegister.strEmailAddress;
                hcUserEmailCookie.Expires = DateTime.Now.AddDays(30); // expires after 30 days
                HttpContext.Response.Cookies.Add(hcUserEmailCookie);

                ConfirmationEmail(clsUser.strFirstName, clsUser.strEmailAddress, iUserID);
                TempData["RegisterToLogin"] = "Please check your email to complete the email confirmation.";
            }
            else
            {
                //Reset error message
                ModelState.AddModelError("RegisterError", "Please fill out all of the required fields.");
            }
            ViewBag.Register = true;
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.Remove("clsUser");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            var clsForgotPassword = new clsForgotPassword();
            return View(clsForgotPassword);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(clsForgotPassword clsForgotPassword)
        {
            //CMS Users Manager
            clsUsersManager clsUserManager = new clsUsersManager();
            //CMS User
            clsUsers clsUser = clsUserManager.getUserByEmail(clsForgotPassword.strEmail.ToLower());
            //If User does not exist
            if (clsUser == null)
            {
                TempData["bSendUserTempPasswordEmail"] = true;
                return RedirectToAction("Login", "Login");
            }

            string newPassword = clsCommonFunctions.strCreateRandomPassword(8);
            sendForgotPasswordEmail(clsUser.strEmailAddress, newPassword, clsUser.strFirstName);

            //Hash password
            string strPasswordHash = "";
            strPasswordHash = clsCommonFunctions.GetMd5Sum(newPassword);

            clsUser.strPassword = strPasswordHash;
            clsUserManager.resetUserPassword(clsUser);

            TempData["bSendUserTempPasswordEmail"] = true;
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public JsonResult checkIfEmailExists([Bind(Prefix = "strEmailAddress")] string strEmail)
        {
            bool bCanUseEmail = false;
            clsUsersManager clsUserManager = new clsUsersManager();
            if (strEmail != null && strEmail != "")
                strEmail = strEmail.ToLower();
            bool bDoesUserEmailExist = clsUserManager.checkIfUserExists(strEmail);
            if (bDoesUserEmailExist == false)
                bCanUseEmail = true;
            return Json(bCanUseEmail, JsonRequestBehavior.AllowGet);
        }

        public void ConfirmationEmail(string strFirstname, string strEmailAddress, int iUserID)
        {
            #region Mail
            Attachment[] emptyAttach = new Attachment[] { };
            string strEmail = StringCipher.Encrypt(strEmailAddress, "doIT");
            string iIDEncrypted = StringCipher.Encrypt(iUserID.ToString(), "doIT");
            string url = Url.Action("ConfirmEmailAddress", "Login",
            new System.Web.Routing.RouteValueDictionary(new { @em = strEmail, @userid = iIDEncrypted }),
            "http", Request.Url.Host);
            //Email Content
            clsUsers clsUser = (clsUsers)Session["clsUser"];
            StringBuilder strEmailContent = new StringBuilder();
            strEmailContent.AppendLine("<h3 class='pc-fallback-font'>");
            strEmailContent.AppendLine("Hi " + strFirstname + ",");
            strEmailContent.AppendLine("</h3>");
            strEmailContent.AppendLine("<br/>");
            strEmailContent.AppendLine("<p class='pc-fallback-font'>");
            strEmailContent.AppendLine("<span class='pc-bold-font'>Thank you for registering!</span>");
            strEmailContent.AppendLine("</p>");
            strEmailContent.AppendLine("<br/>");
            strEmailContent.AppendLine("<p class='pc-fallback-font'>");
            strEmailContent.AppendLine("<span class='pc-bold-font'>Please follow this link to confirm your email address and complete your signup:" + "<a href='" + url + "'>" + " Email Confirmation" + "</a></span>");
            strEmailContent.AppendLine("</p>");
            strEmailContent.AppendLine("<br/>");
            #endregion
            try
            {
                clsEmailComponent.SendMail("no-reply@rolando.co.za", strEmailAddress, "", "", "Email Confirmation", strEmailContent.ToString(), emptyAttach, true, true);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void sendForgotPasswordEmail(string strEmailAddress, string strNewPassword, string strFirstName)
        {
            #region Mail
            Attachment[] emptyAttach = new Attachment[] { };
            string url = Url.Action("Login", "Login",
            "http", Request.Url.Host);
            //Email Content
            clsUsers clsUser = (clsUsers)Session["clsUser"];
            StringBuilder strEmailContent = new StringBuilder();
            strEmailContent.AppendLine("<h3 class='pc-fallback-font'>");
            strEmailContent.AppendLine("Hi " + strFirstName);
            strEmailContent.AppendLine("</h3>");
            strEmailContent.AppendLine("<br/>");
            strEmailContent.AppendLine("<p class='pc-fallback-font'>");
            strEmailContent.AppendLine("Your temporary password as requested is: " + "<span class='pc-bold-font'>" + strNewPassword + "</span>");
            strEmailContent.AppendLine("</p>");
            strEmailContent.AppendLine("<br/>");
            strEmailContent.AppendLine("<p class='pc-fallback-font'>");
            strEmailContent.AppendLine("Please follow this link to login with the temporary password provided:" + "<span class='pc-bold-font'><a style='text-decoration:underline;' href='" + url + "'>" + " Login" + "</a></span>");
            strEmailContent.AppendLine("</p>");
            strEmailContent.AppendLine("<br/>");
            #endregion
            try
            {
                clsEmailComponent.SendMail("no-reply@rolando.co.za", strEmailAddress, "", "", "Password Reset", strEmailContent.ToString(), emptyAttach, true, false);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public ActionResult ConfirmEmailAddress(string em, string userid)
        {
            string strEmail = StringCipher.Decrypt(em, "doIT");
            string strID = StringCipher.Decrypt(userid, "doIT");
            int iUserID = Convert.ToInt32(strID);
            clsUsersManager clsUserManager = new clsUsersManager();
            bool bEmailExist = clsUserManager.checkIfUserExists(strEmail);
            var clsUser = new clsUsers();
            if (bEmailExist)
            {
                clsUser = clsUserManager.getUserByEmail(strEmail);
                if (!clsUser.bIsConfirmed)
                    clsUser.bIsConfirmed = true;
                clsUserManager.registerUser(clsUser);
                Session["iUserID"] = iUserID;
            }
            TempData["ConfirmToLogin"] = "Your registration has successfully been completed, you can now log in.";
            return RedirectToAction("Login", "Login");
        }
    }
}