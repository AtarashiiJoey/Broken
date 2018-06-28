using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ColmartCMS.View_Models.Account;
using Colmart.Model_Manager;
using Colmart.Models;
using System.Web.Security;
using ColmartCMS.Assistant_Classes;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Net.Mail;
using Colmart;

namespace ColmartCMS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        ColmartDBContext db = new ColmartDBContext();
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            clsLogin clsLogin = new clsLogin();

            HttpCookie hcCMSUserEmailCookie = Request.Cookies["strCMSUserEmail"];
            if (hcCMSUserEmailCookie != null)
            {
                clsLogin.strEmail = hcCMSUserEmailCookie.Value; //Get the email
                clsLogin.bRememberMe = true;
            }

            return View(clsLogin);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(clsLogin clsLogin)
        {
            //CMS Users Manager
            clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager();
            //CMS User
            clsCMSUsers clsCMSUser = clsCMSUsersManager.getCMSUserByEmail(clsLogin.strEmail.ToLower());
            //If User does not exist
            if (clsCMSUser == null)
            {
                ModelState.AddModelError("loginError", "Incorrect Login Credentials Entered");
                clsLogin.strPassword = "";
                return View(clsLogin);
            }

            string strPasswordHash = "";
            if (clsLogin.strPassword != null && clsLogin.strPassword != "")
                strPasswordHash = clsCommonFunctions.GetMd5Sum(clsLogin.strPassword);

            //If password is incorrect
            if (clsCMSUser.strPassword != strPasswordHash)
            {
                ModelState.AddModelError("loginError", "Incorrect Login Credentials Entered");
                clsLogin.strPassword = "";
                return View(clsLogin);
            }
            else
            {
                //Set cookie
                if (clsLogin.bRememberMe == true)
                {
                    HttpCookie hcCMSUserEmailCookie = new HttpCookie("strCMSUserEmail");
                    hcCMSUserEmailCookie.Value = clsLogin.strEmail;
                    hcCMSUserEmailCookie.Expires = DateTime.Now.AddDays(30); // expires after 30 days
                    HttpContext.Response.Cookies.Add(hcCMSUserEmailCookie);
                }
                else
                {
                    if (Request.Cookies["strCMSUserEmail"] != null)
                    {
                        Response.Cookies["strCMSUserEmail"].Expires = DateTime.Now.AddDays(-1);
                    }
                }

                //Reset error message
                ModelState.AddModelError("loginError", "");
                Session["clsCMSUser"] = clsCMSUser;

                return RedirectToAction("Dashboard", "CMSHome"); // login succeed 
            }
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            clsForgotPassword clsForgotPassword = new clsForgotPassword();

            return View(clsForgotPassword);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(clsForgotPassword clsForgotPassword)
        {
            //CMS Users Manager
            clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager();
            //CMS User
            clsCMSUsers clsCMSUser = clsCMSUsersManager.getCMSUserByEmail(clsForgotPassword.strEmail.ToLower());
            //If User does not exist
            if (clsCMSUser == null)
            {
                TempData["bSendUserTempPasswordEmail"] = true;
                return RedirectToAction("Login", "Account");
            }

            string newPassword = clsCommonFunctions.strCreateRandomPassword(8);
            //Send Email
            sendForgotPasswordEmail(clsCMSUser.strEmailAddress, newPassword);

            //Hash password
            string strPasswordHash = "";
            strPasswordHash = clsCommonFunctions.GetMd5Sum(newPassword);

            clsCMSUser.strPassword = strPasswordHash;
            clsCMSUsersManager.resetCMSUserPassword(clsCMSUser);

            TempData["bSendUserTempPasswordEmail"] = true;
            return RedirectToAction("Login", "Account");
        }

        private void sendForgotPasswordEmail(string strEmail, string strNewPassword)
        {
            StringBuilder strContent = new StringBuilder();

            Attachment[] emptyAttach = new Attachment[] { };

            strContent.AppendLine("<!-- Start of textbanner -->");
            strContent.AppendLine("<table width='100%' bgcolor='#fff' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' movable=''>");
            strContent.AppendLine("<tbody>");
            strContent.AppendLine("<tr>");
            strContent.AppendLine("<td>");
            strContent.AppendLine("<table bgcolor='#ffffff' width='650' cellpadding='0' cellspacing='0' border='0' align='center' class='devicewidth' options=''>"); //style='border-left:1px solid #333; border-right:1px solid #333;'
            strContent.AppendLine("<tbody>");
            strContent.AppendLine("<!-- Spacing -->");
            strContent.AppendLine("<tr>");
            strContent.AppendLine("<td height='50'></td>");
            strContent.AppendLine("</tr>");
            strContent.AppendLine("<!-- End of Spacing -->");
            strContent.AppendLine("<tr>");
            strContent.AppendLine("<td>");
            strContent.AppendLine("<table width='100%' cellspacing='0' cellpadding='0'>");
            strContent.AppendLine("<tbody>");

            strContent.AppendLine("<!-- Content -->");
            strContent.AppendLine("<tr>");
            strContent.AppendLine("<td width='60'></td>");
            strContent.AppendLine("<td valign='top' style='font-family:Arial;font-size: 15px; color: #3b3b3b; text-align:left;line-height: 22px;' text=''>");
            strContent.AppendLine("Good Day," + "<br /><br />");
            strContent.AppendLine("Your password has been successfully reset.<br />");
            strContent.AppendLine("Please use the <b>Temporary Password: </b>" + strNewPassword + "<br />");
            strContent.AppendLine("Please reset your password as soon as you log in.<br /><br />");
            strContent.AppendLine("<b>The Softserve Team</b>" + "<br /><br />");
            strContent.AppendLine("</td>");
            strContent.AppendLine("<td width='60'></td>");
            strContent.AppendLine("</tr>");
            strContent.AppendLine("<!-- End of Content -->");

            strContent.AppendLine("</td>");
            strContent.AppendLine("<td width='20'></td>");
            strContent.AppendLine("</tr>");
            strContent.AppendLine("</tbody>");
            strContent.AppendLine("</table>");
            strContent.AppendLine("</td>");
            strContent.AppendLine("</tr>");
            strContent.AppendLine("<!-- Spacing -->");
            strContent.AppendLine("<tr>");
            strContent.AppendLine("<td height='50'></td>");
            strContent.AppendLine("</tr>");
            strContent.AppendLine("<!-- End of Spacing -->");
            strContent.AppendLine("</tbody>");
            strContent.AppendLine("</table>");
            strContent.AppendLine("</td>");
            strContent.AppendLine("</tr>");
            strContent.AppendLine("</tbody>");
            strContent.AppendLine("</table>");
            strContent.AppendLine("<!-- End of textbanner -->");

            try
            {
                //TEST
                clsEmailComponent.SendMail("noreply@softservedigital.co.za", strEmail, "", "", "Password Reset", strContent.ToString(), emptyAttach, true);

                //clsEmailComponent.SendMail("noreply@careerwiz.co.za", strEmail, "", "hello@softservedigital.co.za", "Password Reset", strContent.ToString(), emptyAttach, true);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}