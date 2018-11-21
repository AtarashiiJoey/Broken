using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Text;


namespace Colmart.Assistant_Classes
{
    public class clsEmailComponent
    {
        #region Mail Methods

        public static void SendMail(string strFrom, string strTo, string strCC, string strBCC, string strSubject, string strContent, Attachment[] atcAttatchedFiles, bool bUseGenericHTML, bool bRegister)
        {
            //### Create an instance of the MailMessage class 
            using (MailMessage myMail = new MailMessage())
            {
                //### Set the subject            
                myMail.Subject = strSubject;

                //### Send To / From / BCC
                myMail.From = new MailAddress(strFrom);
                myMail.To.Add(strTo);
                //ErrorLogging(strTo);
                if ((strCC != "") && (strCC != null))
                    myMail.CC.Add(strCC);
                if ((strBCC != "") && (strBCC != null))
                    myMail.Bcc.Add(strBCC);

                //### Add Attachments to message object
                if (atcAttatchedFiles != null)
                {
                    foreach (Attachment atcAttachedFile in atcAttatchedFiles)
                    {
                        myMail.Attachments.Add(atcAttachedFile);
                    }
                }

                //### Assign the content to the mail body
                myMail.IsBodyHtml = true;

                string strHtmlBody = "";

                if (bUseGenericHTML == true)
                    strHtmlBody = SendMailGenericHTML(strContent, strSubject);
                else
                    strHtmlBody += strContent;
                string strPathToLogo = "";
                if (bRegister)
                {
                    strPathToLogo = AppDomain.CurrentDomain.BaseDirectory + @"\images\Register-Header.jpg";
                }
                else
                {
                    strPathToLogo = AppDomain.CurrentDomain.BaseDirectory + @"\images\Password-Header.jpg";
                }

                AlternateView HTMLEmail = AlternateView.CreateAlternateViewFromString(strHtmlBody, null, "text/html");
                LinkedResource MyImage = new LinkedResource(strPathToLogo);

                //### <img src="cid:InlineImageID" />
                MyImage.ContentId = "logo";

                ////Other Resources
                //LinkedResource imgFacebook = new LinkedResource(AppDomain.CurrentDomain.BaseDirectory + @"Content\images\Social-Media\imgFacebook.png");
                //imgFacebook.ContentId = "imgFacebook";
                //LinkedResource imgGooglePlus = new LinkedResource(AppDomain.CurrentDomain.BaseDirectory + @"Content\images\Social-Media\imgGooglePlus.png");
                //imgGooglePlus.ContentId = "imgGooglePlus";
                //LinkedResource imgLinkdin = new LinkedResource(AppDomain.CurrentDomain.BaseDirectory + @"Content\images\Social-Media\imgLinkdin.png");
                //imgLinkdin.ContentId = "imgLinkdin";
                //LinkedResource imgYouTube = new LinkedResource(AppDomain.CurrentDomain.BaseDirectory + @"Content\images\Social-Media\imgYouTube.png");
                //imgYouTube.ContentId = "imgYouTube";
                //LinkedResource imgTwitter = new LinkedResource(AppDomain.CurrentDomain.BaseDirectory + @"Content\images\Social-Media\imgTwitter.png");
                //imgTwitter.ContentId = "imgTwitter";


                //### Add this linked resource to HTML view
                HTMLEmail.LinkedResources.Add(MyImage);
                //HTMLEmail.LinkedResources.Add(imgFacebook);
                //HTMLEmail.LinkedResources.Add(imgGooglePlus);
                //HTMLEmail.LinkedResources.Add(imgLinkdin);
                //HTMLEmail.LinkedResources.Add(imgYouTube);
                //HTMLEmail.LinkedResources.Add(imgTwitter);

                myMail.AlternateViews.Add(HTMLEmail);

                //### Password protected
                using (SmtpClient emailClient = new SmtpClient(ConfigurationManager.AppSettings["SMTP"]))
                {
                    emailClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                    emailClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["EmailAddress"], ConfigurationManager.AppSettings["EmailPassword"]);
                    //### Now, to send the message, use the Send method of the SmtpMail class 
                    emailClient.Send(myMail);


                }
            }
        }

        private static String SendMailGenericHTML(String strContent, String strSubject)
        {
            StringBuilder strbMailBuilder = new StringBuilder();

            strbMailBuilder.AppendLine("<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\"><title>" + strSubject + "</title>");

            strbMailBuilder.AppendLine("<!--[if (gte mso 9)|(IE)]>");
            strbMailBuilder.AppendLine("<style type='text/css'>");
            strbMailBuilder.AppendLine("[style*='Arial'] {font-family: 'Poppins',Century Gothic, sans-serif !important}");
            strbMailBuilder.AppendLine("</style>");
            strbMailBuilder.AppendLine("<![endif]-->");

            strbMailBuilder.AppendLine("<!--[if mso]>");
            strbMailBuilder.AppendLine("<style type='text/css'>");
            strbMailBuilder.AppendLine(".pc-fallback-font {font-family: 'Poppins',Century Gothic, sans-serif !important}");
            strbMailBuilder.AppendLine(".pc-bold-font {font-weight:bold;}");
            strbMailBuilder.AppendLine("</style>");
            strbMailBuilder.AppendLine("<![endif]-->");

            strbMailBuilder.AppendLine("<style type='text/css'>");
            strbMailBuilder.AppendLine("/* Client-specific Styles */");
            strbMailBuilder.AppendLine("#outlook a {padding:0;} /* Force Outlook to provide a 'view in browser' menu link. */");
            strbMailBuilder.AppendLine("body{width:100% !important; -webkit-text-size-adjust:100%; -ms-text-size-adjust:100%; margin:0; padding:0;}");
            strbMailBuilder.AppendLine("/* Prevent Webkit and Windows Mobile platforms from changing default font sizes, while not breaking desktop design. */");
            strbMailBuilder.AppendLine(".ExternalClass {width:100%;} /* Force Hotmail to display emails at full width */");
            strbMailBuilder.AppendLine(".ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height: 100%;} /* Force Hotmail to display normal line spacing.  More on that: http://www.emailonacid.com/forum/viewthread/43/ */");
            strbMailBuilder.AppendLine("#backgroundTable {margin:0; padding:0; width:100% !important; line-height: 100% !important;}");
            strbMailBuilder.AppendLine("img {outline:none; text-decoration:none;border:none; -ms-interpolation-mode: bicubic;}");
            strbMailBuilder.AppendLine("a img {border:none;}");
            strbMailBuilder.AppendLine(".image_fix {display:block;}");
            strbMailBuilder.AppendLine("p {margin: 0px 0px !important;}");
            strbMailBuilder.AppendLine("h1, h2, h3, h4, h5, h6 {color: #000000 !important;}");
            strbMailBuilder.AppendLine("h1 a, h2 a, h3 a, h4 a, h5 a, h6 a {color: #000000 !important;}");
            strbMailBuilder.AppendLine("h1 a:active, h2 a:active,  h3 a:active, h4 a:active, h5 a:active, h6 a:active {");
            strbMailBuilder.AppendLine("color: red !important; ");
            strbMailBuilder.AppendLine("}");
            strbMailBuilder.AppendLine("h1 a:visited, h2 a:visited,  h3 a:visited, h4 a:visited, h5 a:visited, h6 a:visited {");
            strbMailBuilder.AppendLine("color: purple !important; ");
            strbMailBuilder.AppendLine("}");
            strbMailBuilder.AppendLine("table td {border-collapse: collapse;}");
            strbMailBuilder.AppendLine("table { border-collapse:collapse; mso-table-lspace:0pt; mso-table-rspace:0pt; }");
            strbMailBuilder.AppendLine("a {color: #000000;text-decoration: underline;text-decoration:underline!important;}");
            strbMailBuilder.AppendLine("/*STYLES*/");
            strbMailBuilder.AppendLine("table[class=full] { width: 100%; clear: both; }");
            strbMailBuilder.AppendLine("table[class=button] {");
            strbMailBuilder.AppendLine("border-width:1px!important;border-style:solid!important;");
            strbMailBuilder.AppendLine("border-top-width: 1px!important;");
            strbMailBuilder.AppendLine("border-right-width: 1px!important;");
            strbMailBuilder.AppendLine("border-bottom-width: 1px!important;");
            strbMailBuilder.AppendLine("border-left-width: 1px!important;");
            strbMailBuilder.AppendLine("}");
            strbMailBuilder.AppendLine("/*IPAD STYLES*/");
            strbMailBuilder.AppendLine("@media only screen and (max-width: 640px) {");
            strbMailBuilder.AppendLine("a[href^='tel'], a[href^='sms'] {");
            strbMailBuilder.AppendLine("text-decoration: none;");
            strbMailBuilder.AppendLine("color: #2d98da; /* or whatever your want */");
            strbMailBuilder.AppendLine("pointer-events: none;");
            strbMailBuilder.AppendLine("cursor: default;");
            strbMailBuilder.AppendLine("}");
            strbMailBuilder.AppendLine(".mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {");
            strbMailBuilder.AppendLine("text-decoration: default;");
            strbMailBuilder.AppendLine("color: #2d98da !important;");
            strbMailBuilder.AppendLine("pointer-events: auto;");
            strbMailBuilder.AppendLine("cursor: default;");
            strbMailBuilder.AppendLine("}");
            strbMailBuilder.AppendLine("table[class=devicewidth] {width: 440px!important;text-align:center!important;}");
            strbMailBuilder.AppendLine("td[class=text-Center] {width: 100%;text-align: center!important;clear: both;}");
            strbMailBuilder.AppendLine("td[class=menu] {width: 100%;height:40px!important;text-align: center!important;clear: both;}");
            strbMailBuilder.AppendLine("img[class=banner] {width: 440px!important;height:191px!important;}");
            strbMailBuilder.AppendLine("td[class=w20]{width:10px!important}");
            strbMailBuilder.AppendLine("table[class=image-banner]{width:440px!important;height: 73px!important}");
            strbMailBuilder.AppendLine("td[class=image-banner]{width:440px!important;height: 73px!important}");
            strbMailBuilder.AppendLine("img[class=image-banner]{width:440px!important;height: 73px!important}");
            strbMailBuilder.AppendLine("table[class=emhide]{display:none!important;}");
            strbMailBuilder.AppendLine("}");
            strbMailBuilder.AppendLine("/*IPHONE STYLES*/");
            strbMailBuilder.AppendLine("@media only screen and (max-width: 480px) {");
            strbMailBuilder.AppendLine("a[href^='tel'], a[href^='sms'] {");
            strbMailBuilder.AppendLine("text-decoration: none;");
            strbMailBuilder.AppendLine("color: #2d98da; /* or whatever your want */");
            strbMailBuilder.AppendLine("pointer-events: none;");
            strbMailBuilder.AppendLine("cursor: default;");
            strbMailBuilder.AppendLine("}");
            strbMailBuilder.AppendLine(".mobile_link a[href^='tel'], .mobile_link a[href^='sms'] {");
            strbMailBuilder.AppendLine("text-decoration: default;");
            strbMailBuilder.AppendLine("color: #2d98da !important; ");
            strbMailBuilder.AppendLine("pointer-events: auto;");
            strbMailBuilder.AppendLine("cursor: default;");
            strbMailBuilder.AppendLine("}");
            strbMailBuilder.AppendLine("table[class=devicewidth] {width: 280px!important;text-align:center!important;}");
            strbMailBuilder.AppendLine("td[class=text-Center] {width: 100%;text-align: center!important;clear: both;}");
            strbMailBuilder.AppendLine("td[class=menu] {width: 100%;height:40px!important;text-align: center!important;clear: both;}");
            strbMailBuilder.AppendLine("img[class=banner] {width: 280px!important;height:121px!important;}");
            strbMailBuilder.AppendLine("td[class=w20]{width:10px!important}");
            strbMailBuilder.AppendLine("table[class=image-banner]{width:280px!important;height: 47px!important}");
            strbMailBuilder.AppendLine("td[class=image-banner]{width:280px!important;height: 47px!important}");
            strbMailBuilder.AppendLine("img[class=image-banner]{width:280px!important;height: 47px!important}");
            strbMailBuilder.AppendLine("table[class=emhide]{display:none!important;}");
            strbMailBuilder.AppendLine("}");

            strbMailBuilder.AppendLine("</style>");
            strbMailBuilder.AppendLine("</head>");

            strbMailBuilder.AppendLine("<body style='-webkit-user-select: none;'>");

            strbMailBuilder.AppendLine("<table style='width:100%; background-color:#f5f5f5;' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' movable=''>");
            strbMailBuilder.AppendLine("<tbody>");
            strbMailBuilder.AppendLine("<tr>");
            strbMailBuilder.AppendLine("<td>");

            strbMailBuilder.AppendLine("<table width='60%' bgcolor='#ffffff' cellpadding='0' cellspacing='0' border='0' align='center' class='devicewidth' nobg=''>");
            strbMailBuilder.AppendLine("<tbody>");

            //strbMailBuilder.AppendLine("<tr>");
            //strbMailBuilder.AppendLine("<td>");
            //strbMailBuilder.AppendLine("<table style='width:100%; background-color:#f5f5f5;' cellpadding='0' cellspacing='0' border='0' align='center' class='devicewidth' nobg=''>");
            //strbMailBuilder.AppendLine("<tbody>");
            //strbMailBuilder.AppendLine("<tr>");
            //strbMailBuilder.AppendLine("<td align='center' valign='middle' style='font-family: Helvetica, sans-serif;font-size: 13px;color: #666666' text=''>");
            //strbMailBuilder.AppendLine("Can't see this email? <a href='#' style='text-transform: uppercase; text-decoration:underline; color: #2d98da; font-size:12px;' link='#' hlite=''>View it Online</a>");
            //strbMailBuilder.AppendLine("</td>");
            //strbMailBuilder.AppendLine("</tr>");
            //strbMailBuilder.AppendLine("</tbody>");
            //strbMailBuilder.AppendLine("</table>");
            //strbMailBuilder.AppendLine("</td>");
            //strbMailBuilder.AppendLine("</tr>");

            strbMailBuilder.AppendLine("<tr>");
            strbMailBuilder.AppendLine("<td>");
            strbMailBuilder.AppendLine("<table style='width:100%; background-color:#fff;' cellpadding='0' cellspacing='0' border='0' align='center' class='devicewidth' nobg=''>");
            strbMailBuilder.AppendLine("<tbody>");
            strbMailBuilder.AppendLine("<tr>");
            strbMailBuilder.AppendLine("<td valign='middle' align='center'>");
            strbMailBuilder.AppendLine("<a href='#'><img src=\"cid:logo\" alt='logo' border='0' style='display:block; border:none; outline:none; text-decoration:none;'></a>");
            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("</tr>");
            strbMailBuilder.AppendLine("</tbody>");
            strbMailBuilder.AppendLine("</table>");
            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("</tr>");

            strbMailBuilder.AppendLine("<!-- Spacing -->");
            strbMailBuilder.AppendLine("<tr>");
            strbMailBuilder.AppendLine("<td>");
            strbMailBuilder.AppendLine("<table style='width:100%; background-color:#fff;' cellpadding='0' cellspacing='0' border='0' align='center' class='devicewidth' nobg=''>");
            strbMailBuilder.AppendLine("<tbody>");
            strbMailBuilder.AppendLine("<tr>");
            strbMailBuilder.AppendLine("<td height='35px' style='height:35px;' valign='middle' align='center'>");

            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("</tr>");
            strbMailBuilder.AppendLine("</tbody>");
            strbMailBuilder.AppendLine("</table>");
            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("</tr>");
            strbMailBuilder.AppendLine("<!-- End Spacing -->");

            strbMailBuilder.AppendLine("<tr style='PADDING-LEFT: 80px;PADDING-RIGHT: 80px;'>");
            strbMailBuilder.AppendLine("<td>");
            strbMailBuilder.AppendLine(strContent);
            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("</tr>");

            strbMailBuilder.AppendLine("<!-- Spacing -->");
            strbMailBuilder.AppendLine("<tr>");
            strbMailBuilder.AppendLine("<td>");
            strbMailBuilder.AppendLine("<table style='width:100%; background-color:#fff;' cellpadding='0' cellspacing='0' border='0' align='center' class='devicewidth' nobg=''>");
            strbMailBuilder.AppendLine("<tbody>");
            strbMailBuilder.AppendLine("<tr>");
            strbMailBuilder.AppendLine("<td height='35px' style='height:35px;' valign='middle' align='center'>");

            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("</tr>");
            strbMailBuilder.AppendLine("</tbody>");
            strbMailBuilder.AppendLine("</table>");
            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("</tr>");
            strbMailBuilder.AppendLine("<!-- End Spacing -->");

            strbMailBuilder.AppendLine("<tr>");
            strbMailBuilder.AppendLine("<td>");
            strbMailBuilder.AppendLine("<!-- html banner -->");
            strbMailBuilder.AppendLine("<table width='100%' bgcolor='#f5f5f5' cellpadding='0' cellspacing='0' border='0' id='backgroundTable' movable=''>");
            strbMailBuilder.AppendLine("<tbody>");
            strbMailBuilder.AppendLine("<tr>");
            strbMailBuilder.AppendLine("<td align='center' valign='top'>");
            strbMailBuilder.AppendLine("<!-- Start Module -->");
            strbMailBuilder.AppendLine("<table bgcolor='#000000' width='100%' cellpadding='0' cellspacing='0' border='0' align='center' class='devicewidth' options='' hlitebg=''>");
            strbMailBuilder.AppendLine("<tbody>");
            strbMailBuilder.AppendLine("<tr>");
            strbMailBuilder.AppendLine("<td>");
            strbMailBuilder.AppendLine("<!-- Left Column -->");
            strbMailBuilder.AppendLine("<table width='800' align='center' cellspacing='0' cellpadding='0' class='devicewidth'>");
            strbMailBuilder.AppendLine("<tbody>");
            strbMailBuilder.AppendLine("<tr>");
            strbMailBuilder.AppendLine("<td height='100'>");
            strbMailBuilder.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");
            strbMailBuilder.AppendLine("<tbody>");
            strbMailBuilder.AppendLine("<!-- content -->");
            strbMailBuilder.AppendLine("<tr>");
            strbMailBuilder.AppendLine("<td></td>");
            strbMailBuilder.AppendLine("<td class='pc-fallback-font' height='60' valign='middle' style='font-size: 16px; color: #ffffff; text-align:center;line-height: 30px; text-transform:uppercase;'>");
            strbMailBuilder.AppendLine("COPYRIGHT ROLANDO 2018. ALL RIGHTS RESERVED.");
            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("<td width='20'></td>");
            strbMailBuilder.AppendLine("</tr>");
            strbMailBuilder.AppendLine("<!-- end of content -->");
            strbMailBuilder.AppendLine("</tbody>");
            strbMailBuilder.AppendLine("</table>");
            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("</tr>");
            strbMailBuilder.AppendLine("</tbody>");
            strbMailBuilder.AppendLine("</table>");
            strbMailBuilder.AppendLine("<!-- Right Column -->");
            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("</tr>");
            strbMailBuilder.AppendLine("<!--  Spacing -->");
            strbMailBuilder.AppendLine("</tbody>");
            strbMailBuilder.AppendLine("</table>");
            strbMailBuilder.AppendLine("<!-- End Module-->");
            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("</tr>");
            strbMailBuilder.AppendLine("</tbody>");
            strbMailBuilder.AppendLine("</table>");
            strbMailBuilder.AppendLine("<!-- End of html banner -->");
            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("</tr>");

            //strbMailBuilder.AppendLine("<tr bgcolor='#ffffff'>");
            //strbMailBuilder.AppendLine("<td>");
            //strbMailBuilder.AppendLine("<table width='160' height='70' cellpadding='0' cellspacing='0' border='0' align='right' class='emhide'>");
            //strbMailBuilder.AppendLine("<tbody>");
            //strbMailBuilder.AppendLine("<tr>");
            //strbMailBuilder.AppendLine("<td height='70' align='center' valign='middle' class='devicewidth'>");
            //strbMailBuilder.AppendLine("<a href='https://www.facebook.com/softservedigitaldevelopment' style='text-decoration: none; color: #3e454c;'>");
            //strbMailBuilder.AppendLine("<img src=\"cid:imgFacebook\" alt='Facebook' border='0' style='display:block; border:none; outline:none; text-decoration:none;'/>");
            //strbMailBuilder.AppendLine("</a>");
            //strbMailBuilder.AppendLine("</td>");
            //strbMailBuilder.AppendLine("<td height='70' align='center' valign='middle' class='devicewidth'>");
            //strbMailBuilder.AppendLine("<a href='https://plus.google.com/+SoftservedigitalCoZa/about' style='text-decoration: none; color: #3e454c;'>");
            //strbMailBuilder.AppendLine("<img src=\"cid:imgGooglePlus\" alt='Google+' border='0' style='display:block; border:none; outline:none; text-decoration:none;'>");
            //strbMailBuilder.AppendLine("</a>");
            //strbMailBuilder.AppendLine("</td>");
            //strbMailBuilder.AppendLine("<td height='70' align='center' valign='middle' class='devicewidth'>");
            //strbMailBuilder.AppendLine("<a href='https://www.linkedin.com/company/softserve-digital-development' style='text-decoration: none; color: #3e454c;'>");
            //strbMailBuilder.AppendLine("<img src=\"cid:imgLinkdin\" alt='Linkdin' border='0' style='display:block; border:none; outline:none; text-decoration:none;'>");
            //strbMailBuilder.AppendLine("</a>");
            //strbMailBuilder.AppendLine("</td>");
            //strbMailBuilder.AppendLine("<td height='70' align='center' valign='middle' class='devicewidth'>");
            //strbMailBuilder.AppendLine("<a href='https://www.youtube.com/channel/UCCyqGtSxmvLDldp2EmJdujw' style='text-decoration: none; color: #3e454c;'>");
            //strbMailBuilder.AppendLine("<img src=\"cid:imgYouTube\" alt='YouTube' border='0' style='display:block; border:none; outline:none; text-decoration:none;'>");
            //strbMailBuilder.AppendLine("</a>");
            //strbMailBuilder.AppendLine("</td>");
            //strbMailBuilder.AppendLine("<td height='70' align='center' valign='middle' class='devicewidth'>");
            //strbMailBuilder.AppendLine("<a href='https://twitter.com/HelloSoftserve' style='text-decoration: none; color: #3e454c;'>");
            //strbMailBuilder.AppendLine("<img src=\"cid:imgTwitter\" alt='YouTube' border='0' style='display:block; border:none; outline:none; text-decoration:none;'>");
            //strbMailBuilder.AppendLine("</a>");
            //strbMailBuilder.AppendLine("</td>");
            //strbMailBuilder.AppendLine("</tr>");
            //strbMailBuilder.AppendLine("</tbody>");
            //strbMailBuilder.AppendLine("</table>");
            //strbMailBuilder.AppendLine("</td>");
            //strbMailBuilder.AppendLine("</tr>");

            //strbMailBuilder.AppendLine("<tr>");
            //strbMailBuilder.AppendLine("<td>");
            //strbMailBuilder.AppendLine("<table width='100%' bgcolor='#fff' cellpadding='0' cellspacing='0' border='0' align='center' class='devicewidth' nobg=''>");
            //strbMailBuilder.AppendLine("<tbody>");
            //strbMailBuilder.AppendLine("<tr>");
            //strbMailBuilder.AppendLine("<td align='center' valign='middle' style='font-family: Helvetica, Arial, sans-serif;font-size: 13px;color: #6c7480;line-height:20px;' text='postfooter'>");
            //strbMailBuilder.AppendLine("You're receiving this email because you subscribed for updates - <a href='#' style='text-transform: uppercase; text-decoration:underline; color: #2d98da; font-size:12px;' hlite=''>Unsubscribe</a>");
            //strbMailBuilder.AppendLine("</td>");
            //strbMailBuilder.AppendLine("</tr>");
            //strbMailBuilder.AppendLine("</tbody>");
            //strbMailBuilder.AppendLine("</table>");
            //strbMailBuilder.AppendLine("</td>");
            //strbMailBuilder.AppendLine("</tr>");

            strbMailBuilder.AppendLine("</tbody>");
            strbMailBuilder.AppendLine("</table>");

            strbMailBuilder.AppendLine("</td>");
            strbMailBuilder.AppendLine("</tr>");
            strbMailBuilder.AppendLine("</tbody>");
            strbMailBuilder.AppendLine("</table>");

            strbMailBuilder.AppendLine("</body>");

            strbMailBuilder.AppendLine("</html>");

            return strbMailBuilder.ToString();
        }

        #endregion
    }
}