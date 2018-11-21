using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Colmart.Model_Manager;
using Colmart.Models;
using ColmartCMS.View_Models;
using ColmartCMS.View_Models.Users;
using ColmartCMS.View_Models.CMSUsers;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ColmartCMS.Assistant_Classes;
using Colmart;

namespace ColmartCMS.Controllers
{
    public class CMSUsersController : Controller
    {
        ColmartDBContext db = new ColmartDBContext();
        // GET: Profile
        public ActionResult CMSUserProfile()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSUserProfile clsCMSUserProfile = new clsCMSUserProfile();
            clsCMSUserProfile.clsCMSUser = (clsCMSUsers)Session["clsCMSUser"];

            clsCMSUserProfile.strFullName = clsCMSUserProfile.clsCMSUser.strFirstName + " " + clsCMSUserProfile.clsCMSUser.strSurname;

            if (clsCMSUserProfile.clsCMSUser.strImagePath != null && clsCMSUserProfile.clsCMSUser.strImagePath != "" && clsCMSUserProfile.clsCMSUser.strImageName != null && clsCMSUserProfile.clsCMSUser.strImageName != "")
                clsCMSUserProfile.strFullImagePath = "/" + clsCMSUserProfile.clsCMSUser.strImagePath + "/" + clsCMSUserProfile.clsCMSUser.strImageName;

            return View(clsCMSUserProfile);
        }

        public ActionResult CMSUserProfileEdit()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSUserProfileEdit clsCMSUserProfileEdit = new clsCMSUserProfileEdit();
            clsCMSUserProfileEdit.clsCMSUser = (clsCMSUsers)Session["clsCMSUser"];

            clsCMSUserProfileEdit.strEmailAddress = clsCMSUserProfileEdit.clsCMSUser.strEmailAddress;
            clsCMSUserProfileEdit.strCMSRoleType = clsCMSUserProfileEdit.clsCMSUser.clsCMSRoleType.strTitle;

            if (clsCMSUserProfileEdit.clsCMSUser.strImagePath != null && clsCMSUserProfileEdit.clsCMSUser.strImagePath != "" && clsCMSUserProfileEdit.clsCMSUser.strImageName != null && clsCMSUserProfileEdit.clsCMSUser.strImageName != "")
                clsCMSUserProfileEdit.strFullImagePath = "/" + clsCMSUserProfileEdit.clsCMSUser.strImagePath + "/" + clsCMSUserProfileEdit.clsCMSUser.strImageName;

            clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager();
            clsCMSUserProfileEdit.lstCMSRoleTypes = clsCMSRoleTypesManager.getAllCMSRoleTypesList();

            return View(clsCMSUserProfileEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CMSUserProfileEdit(clsCMSUserProfileEdit clsCMSUserProfileEdit)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSUsers clsCMSUser = (clsCMSUsers)Session["clsCMSUser"];
            clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager();

            string strOldPasswordCheck = "";
            if (clsCMSUserProfileEdit.strOldPassword != null && clsCMSUserProfileEdit.strOldPassword != "")
            {
                strOldPasswordCheck = clsCommonFunctions.GetMd5Sum(clsCMSUserProfileEdit.strOldPassword);
                if (clsCMSUser.strPassword != strOldPasswordCheck)
                {
                    ModelState.AddModelError("passwordError", "Incorrect Password");

                    clsCMSUserProfileEdit.clsCMSUser = (clsCMSUsers)Session["clsCMSUser"];

                    clsCMSUserProfileEdit.strEmailAddress = clsCMSUserProfileEdit.clsCMSUser.strEmailAddress;

                    if (clsCMSUserProfileEdit.clsCMSUser.strImagePath != null && clsCMSUserProfileEdit.clsCMSUser.strImagePath != "" && clsCMSUserProfileEdit.clsCMSUser.strImageName != null && clsCMSUserProfileEdit.clsCMSUser.strImageName != "")
                        clsCMSUserProfileEdit.strFullImagePath = "/" + clsCMSUserProfileEdit.clsCMSUser.strImagePath + "/" + clsCMSUserProfileEdit.clsCMSUser.strImageName;

                    clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager();
                    clsCMSUserProfileEdit.lstCMSRoleTypes = clsCMSRoleTypesManager.getAllCMSRoleTypesList();

                    return View(clsCMSUserProfileEdit);
                }
            }

            ModelState.AddModelError("passwordError", "");
            if (strOldPasswordCheck != "")
            {
                if (clsCMSUserProfileEdit.strNewPassword != null && clsCMSUserProfileEdit.strNewPassword != "")
                    clsCMSUser.strPassword = clsCommonFunctions.GetMd5Sum(clsCMSUserProfileEdit.strNewPassword);
                else
                    clsCMSUser.strPassword = clsCommonFunctions.GetMd5Sum(clsCMSUser.strEmailAddress);
            }

            clsCMSUser.strFirstName = clsCMSUserProfileEdit.clsCMSUser.strFirstName;
            clsCMSUser.strSurname = clsCMSUserProfileEdit.clsCMSUser.strSurname;
            clsCMSUser.strContactNumber = clsCMSUserProfileEdit.clsCMSUser.strContactNumber;
            clsCMSUser.strBiographicalInfo = clsCMSUserProfileEdit.clsCMSUser.strBiographicalInfo;
            if (clsCMSUser.clsCMSRoleType.strTitle == "Super Admin")
                clsCMSUser.iCMSRoleTypeID = clsCMSUserProfileEdit.clsCMSUser.iCMSRoleTypeID;

            //Save image
            if (clsCMSUserProfileEdit.strCropImageData != null && clsCMSUserProfileEdit.strCropImageData != "")
            {
                string data = clsCMSUserProfileEdit.strCropImageData;
                data = data.Replace("data:image/png;base64,", "");
                string strImageName = "imgProfile_" + clsCMSUserProfileEdit.clsCMSUser.strFirstName + clsCMSUserProfileEdit.clsCMSUser.strSurname + ".png";
                string strSmallImageName = "sml_imgProfile_" + clsCMSUserProfileEdit.clsCMSUser.strFirstName + clsCMSUserProfileEdit.clsCMSUser.strSurname + ".png";

                string strDestPath = "";
                if (clsCMSUser.strImagePath != null && clsCMSUser.strImagePath != "")
                {
                    strDestPath = clsCMSUser.strImagePath.Replace("/", "\\");
                }
                else
                {
                    string newPath = GetUniquePath();
                    strDestPath = "images\\cmsUsers_Images\\" + newPath;
                    clsCMSUser.strImagePath = strDestPath.Replace("\\", "/");
                }

                string strFullDestPath = AppDomain.CurrentDomain.BaseDirectory + strDestPath;
                if (!System.IO.Directory.Exists(strFullDestPath))
                {
                    System.IO.Directory.CreateDirectory(strFullDestPath);
                }
                else
                {
                    //Clear files
                    string[] strAllFilePaths = Directory.GetFiles(strFullDestPath);
                    foreach (string strFilePath in strAllFilePaths)
                        System.IO.File.Delete(strFilePath);
                }

                clsCMSUser.strImageName = strImageName;

                string strImagePath = strFullDestPath + "\\" + strImageName;
                string strSmallImagePath = strFullDestPath + "\\" + strSmallImageName;

                byte[] bytes = Convert.FromBase64String(data);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                //Smaller Image
                Image smallImage = (Image)ResizeImage(image, 35, 35);
                //Save images
                image.Save(strImagePath, System.Drawing.Imaging.ImageFormat.Png);
                smallImage.Save(strSmallImagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            else //save default images
            {
                string strDefaultImagespath = AppDomain.CurrentDomain.BaseDirectory + "images\\defaultImages";

                string strDestPath = "";
                if (clsCMSUser.strImagePath != null && clsCMSUser.strImagePath != "")
                {
                    strDestPath = clsCMSUser.strImagePath.Replace("/", "\\");
                }
                else
                {
                    string newPath = GetUniquePath();
                    strDestPath = "images\\cmsUsers_Images\\" + newPath;
                    clsCMSUser.strImagePath = strDestPath.Replace("\\", "/");
                }

                string strFullDestPath = AppDomain.CurrentDomain.BaseDirectory + strDestPath;
                if (!System.IO.Directory.Exists(strFullDestPath))
                {
                    System.IO.Directory.CreateDirectory(strFullDestPath);
                }
                else
                {
                    //Clear files
                    string[] strAllFilePaths = Directory.GetFiles(strFullDestPath);
                    foreach (string strFilePath in strAllFilePaths)
                        System.IO.File.Delete(strFilePath);
                }

                clsCMSUser.strImageName = "default_image.png";

                string strImagePath = strFullDestPath + "\\" + "default_image.png";
                string strSmallImagePath = strFullDestPath + "\\" + "sml_default_image.png";

                //Copy small and default sizes
                System.IO.File.Copy(strDefaultImagespath + "\\default_image.png", strImagePath);
                System.IO.File.Copy(strDefaultImagespath + "\\sml_default_image.png", strSmallImagePath);
            }

            //Update CMS user
            clsCMSUsersManager.saveCMSUser(clsCMSUser);

            //Update Session
            clsCMSUsers clsNewCMSUser = clsCMSUsersManager.getCMSUserById(clsCMSUser.iCMSUserID);
            Session["clsCMSUser"] = null;
            Session["clsCMSUser"] = clsNewCMSUser;

            //Add successful / notification
            TempData["bIsCMSUserProfileUpdated"] = true;

            return RedirectToAction("CMSUserProfile", "CMSUsers");
        }

        //Get All CMS Users
        public ActionResult CMSUsersView()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSUsersView clsCMSUsersView = new clsCMSUsersView();
            clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager();
            clsCMSUsersView.lstCMSUsers = clsCMSUsersManager.getAllCMSUsersOnlyList();

            return View(clsCMSUsersView);
        }

        //Add CMS User
        public ActionResult CMSUserAdd()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSUsers clsCMSUser = (clsCMSUsers)Session["clsCMSUser"];
            clsCMSUserAdd clsCMSUserAdd = new clsCMSUserAdd();
            clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager();
            clsCMSUserAdd.lstCMSRoleTypes = clsCMSRoleTypesManager.getAllCMSRoleTypesList();
            clsCMSUserAdd.strCMSRoleType = clsCMSUser.clsCMSRoleType.strTitle;

            return View(clsCMSUserAdd);
        }
        //Add CMS User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CMSUserAdd(clsCMSUserAdd clsCMSUserAdd)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSUsers clsCMSUser = (clsCMSUsers)Session["clsCMSUser"];
            clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager();
            clsCMSUserAdd.clsCMSUser.strEmailAddress = clsCMSUserAdd.clsCMSUser.strEmailAddress.ToLower();
            if (clsCMSUser.clsCMSRoleType.strTitle != "Super Admin")
                clsCMSUserAdd.clsCMSUser.iCMSRoleTypeID = 2;

            //Save image
            if (clsCMSUserAdd.strCropImageData != null && clsCMSUserAdd.strCropImageData != "")
            {
                string data = clsCMSUserAdd.strCropImageData;
                data = data.Replace("data:image/png;base64,", "");
                string strImageName = "imgProfile_" + clsCMSUserAdd.clsCMSUser.strFirstName + clsCMSUserAdd.clsCMSUser.strSurname + ".png";
                string strSmallImageName = "sml_imgProfile_" + clsCMSUserAdd.clsCMSUser.strFirstName + clsCMSUserAdd.clsCMSUser.strSurname + ".png";
                string newPath = GetUniquePath();

                string strDestPath = "images\\cmsUsers_Images\\" + newPath;
                string strFullDestPath = AppDomain.CurrentDomain.BaseDirectory + "images\\cmsUsers_Images\\" + newPath;
                if (!System.IO.Directory.Exists(strFullDestPath))
                {
                    System.IO.Directory.CreateDirectory(strFullDestPath);
                }

                clsCMSUserAdd.clsCMSUser.strImagePath = strDestPath.Replace("\\", "/");
                clsCMSUserAdd.clsCMSUser.strImageName = strImageName;

                string strImagePath = strFullDestPath + "\\" + strImageName;
                string strSmallImagePath = strFullDestPath + "\\" + strSmallImageName;
               
                byte[] bytes = Convert.FromBase64String(data);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                //Smaller Image
                Image smallImage = (Image)ResizeImage(image, 35, 35);
                //Save images
                image.Save(strImagePath, System.Drawing.Imaging.ImageFormat.Png);
                smallImage.Save(strSmallImagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            else //save default images
            {
                string newPath = GetUniquePath();
                string strDestPath = "images\\cmsUsers_Images\\" + newPath;
                string strFullDestPath = AppDomain.CurrentDomain.BaseDirectory + "images\\cmsUsers_Images\\" + newPath;
                string strDefaultImagespath = AppDomain.CurrentDomain.BaseDirectory + "images\\defaultImages";
                if (!System.IO.Directory.Exists(strFullDestPath))
                {
                    System.IO.Directory.CreateDirectory(strFullDestPath);
                }

                clsCMSUserAdd.clsCMSUser.strImagePath = strDestPath.Replace("\\", "/");
                clsCMSUserAdd.clsCMSUser.strImageName = "default_image.png";

                string strImagePath = strFullDestPath + "\\" + "default_image.png";
                string strSmallImagePath = strFullDestPath + "\\" + "sml_default_image.png";

                //Copy small and default sizes
                System.IO.File.Copy(strDefaultImagespath + "\\default_image.png", strImagePath);
                System.IO.File.Copy(strDefaultImagespath + "\\sml_default_image.png", strSmallImagePath);
            }
            if (clsCMSUserAdd.strNewPassword != null && clsCMSUserAdd.strNewPassword != "")
                clsCMSUserAdd.clsCMSUser.strPassword = clsCommonFunctions.GetMd5Sum(clsCMSUserAdd.strNewPassword);
            else
                clsCMSUserAdd.clsCMSUser.strPassword = clsCommonFunctions.GetMd5Sum(clsCMSUserAdd.clsCMSUser.strEmailAddress.ToLower());

            //Add CMS user
            clsCMSUsersManager.saveCMSUser(clsCMSUserAdd.clsCMSUser);

            //Add successful / notification
            TempData["bIsCMSUserAdded"] = true;

            return RedirectToAction("CMSUsersView", "CMSUsers");
        }

        //Edit CMS User Info
        public ActionResult CMSUserEdit(int id)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            if (id == 0)
            {
                return RedirectToAction("CMSUsersView", "CMSUsers");
            }

            clsCMSUsers clsCMSUser = (clsCMSUsers)Session["clsCMSUser"];

            clsCMSUserEdit clsCMSUserEdit = new clsCMSUserEdit();
            clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager();
            clsCMSUserEdit.clsCMSUser = clsCMSUsersManager.getCMSUserById(id);

            clsCMSUserEdit.strEmailAddress = clsCMSUserEdit.clsCMSUser.strEmailAddress;
            clsCMSUserEdit.strCMSRoleType = clsCMSUser.clsCMSRoleType.strTitle;

            if (clsCMSUserEdit.clsCMSUser.strImagePath != null && clsCMSUserEdit.clsCMSUser.strImagePath != "" && clsCMSUserEdit.clsCMSUser.strImageName != null && clsCMSUserEdit.clsCMSUser.strImageName != "")
                clsCMSUserEdit.strFullImagePath = "/" + clsCMSUserEdit.clsCMSUser.strImagePath + "/" + clsCMSUserEdit.clsCMSUser.strImageName;

            clsCMSRoleTypesManager clsCMSRoleTypesManager = new clsCMSRoleTypesManager();
            clsCMSUserEdit.lstCMSRoleTypes = clsCMSRoleTypesManager.getAllCMSRoleTypesList();

            return View(clsCMSUserEdit);
        }

        //Edit CMS User Info
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CMSUserEdit(clsCMSUserEdit clsCMSUserEdit)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager();
            clsCMSUsers clsSessionCMSUser = (clsCMSUsers)Session["clsCMSUser"];
            clsCMSUsers clsCMSUser = clsCMSUsersManager.getCMSUserById(clsCMSUserEdit.clsCMSUser.iCMSUserID);

            //Set Role type and password if admin
            if (clsSessionCMSUser.clsCMSRoleType.strTitle == "Super Admin")
            {
                if (clsCMSUserEdit.bResetPassword == true)
                {
                    if (clsCMSUserEdit.strNewPassword != null && clsCMSUserEdit.strNewPassword != "")
                        clsCMSUser.strPassword = clsCommonFunctions.GetMd5Sum(clsCMSUserEdit.strNewPassword);
                    else
                        clsCMSUser.strPassword = clsCommonFunctions.GetMd5Sum(clsCMSUser.strEmailAddress.ToLower());
                }
                clsCMSUser.iCMSRoleTypeID = clsCMSUserEdit.clsCMSUser.iCMSRoleTypeID;
            }

            clsCMSUser.strFirstName = clsCMSUserEdit.clsCMSUser.strFirstName;
            clsCMSUser.strSurname = clsCMSUserEdit.clsCMSUser.strSurname;
            clsCMSUser.strContactNumber = clsCMSUserEdit.clsCMSUser.strContactNumber;
            clsCMSUser.strBiographicalInfo = clsCMSUserEdit.clsCMSUser.strBiographicalInfo;

            //Save image
            if (clsCMSUserEdit.strCropImageData != null && clsCMSUserEdit.strCropImageData != "")
            {
                string data = clsCMSUserEdit.strCropImageData;
                data = data.Replace("data:image/png;base64,", "");
                string strImageName = "imgProfile_" + clsCMSUserEdit.clsCMSUser.strFirstName + clsCMSUserEdit.clsCMSUser.strSurname + ".png";
                string strSmallImageName = "sml_imgProfile_" + clsCMSUserEdit.clsCMSUser.strFirstName + clsCMSUserEdit.clsCMSUser.strSurname + ".png";

                string strDestPath = "";
                if (clsCMSUser.strImagePath != null && clsCMSUser.strImagePath != "")
                {
                    strDestPath = clsCMSUser.strImagePath.Replace("/", "\\");
                }
                else
                {
                    string newPath = GetUniquePath();
                    strDestPath = "images\\cmsUsers_Images\\" + newPath;
                    clsCMSUser.strImagePath = strDestPath.Replace("\\", "/");
                }

                string strFullDestPath = AppDomain.CurrentDomain.BaseDirectory + strDestPath;
                if (!System.IO.Directory.Exists(strFullDestPath))
                {
                    System.IO.Directory.CreateDirectory(strFullDestPath);
                }
                else
                {
                    //Clear files
                    string[] strAllFilePaths = Directory.GetFiles(strFullDestPath);
                    foreach (string strFilePath in strAllFilePaths)
                        System.IO.File.Delete(strFilePath);
                }

                clsCMSUser.strImageName = strImageName;

                string strImagePath = strFullDestPath + "\\" + strImageName;
                string strSmallImagePath = strFullDestPath + "\\" + strSmallImageName;

                byte[] bytes = Convert.FromBase64String(data);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                //Smaller Image
                Image smallImage = (Image)ResizeImage(image, 35, 35);
                //Save images
                image.Save(strImagePath, System.Drawing.Imaging.ImageFormat.Png);
                smallImage.Save(strSmallImagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            else //save default images
            {
                string strDefaultImagespath = AppDomain.CurrentDomain.BaseDirectory + "images\\defaultImages";

                string strDestPath = "";
                if (clsCMSUser.strImagePath != null && clsCMSUser.strImagePath != "")
                {
                    strDestPath = clsCMSUser.strImagePath.Replace("/", "\\");
                }
                else
                {
                    string newPath = GetUniquePath();
                    strDestPath = "images\\cmsUsers_Images\\" + newPath;
                    clsCMSUser.strImagePath = strDestPath.Replace("\\", "/");
                }

                string strFullDestPath = AppDomain.CurrentDomain.BaseDirectory + strDestPath;
                if (!System.IO.Directory.Exists(strFullDestPath))
                {
                    System.IO.Directory.CreateDirectory(strFullDestPath);
                }
                else
                {
                    //Clear files
                    string[] strAllFilePaths = Directory.GetFiles(strFullDestPath);
                    foreach (string strFilePath in strAllFilePaths)
                        System.IO.File.Delete(strFilePath);
                }

                clsCMSUser.strImageName = "default_image.png";

                string strImagePath = strFullDestPath + "\\" + "default_image.png";
                string strSmallImagePath = strFullDestPath + "\\" + "sml_default_image.png";

                //Copy small and default sizes
                System.IO.File.Copy(strDefaultImagespath + "\\default_image.png", strImagePath);
                System.IO.File.Copy(strDefaultImagespath + "\\sml_default_image.png", strSmallImagePath);
            }

            //Update CMS user
            clsCMSUsersManager.saveCMSUser(clsCMSUser);

            //Update Session (Incase current CMS has been edited)
            if (clsSessionCMSUser.iCMSUserID == clsCMSUser.iCMSUserID)
            {
                clsCMSUsers clsNewCMSUser = clsCMSUsersManager.getCMSUserById(clsSessionCMSUser.iCMSUserID);
                Session["clsCMSUser"] = null;
                Session["clsCMSUser"] = clsNewCMSUser;
            }

            //Add successful / notification
            TempData["bIsCMSUserUpdated"] = true;

            return RedirectToAction("CMSUsersView", "CMSUsers");
        }

        //Delete CMS User
        [HttpPost]
        public ActionResult CMSUserDelete(int iCMSUserID)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            if (iCMSUserID == 0)
            {
                return RedirectToAction("CMSUsersView", "CMSUsers");
            }

            clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager();
            List<clsCMSUsers> lstCMSUsers = clsCMSUsersManager.getAllCMSUsersList();
            if (lstCMSUsers.Count > 1)
            {
                clsCMSUsersManager.removeCMSUserByID(iCMSUserID);
                bIsSuccess = true;
                //Delete successful / notification
                TempData["bIsCMSUserDeleted"] = true;

                //Update Session
                clsCMSUsers clsNewCMSUser = (clsCMSUsers)Session["clsCMSUser"];
                if (clsNewCMSUser.iCMSUserID == iCMSUserID)
                {
                    TempData["bIsCMSUserDeleted"] = null;
                    Session["clsCMSUser"] = null;
                }
            }
            else
            {
                bIsSuccess = false;
                TempData["bIsCMSUserDeleted"] = null;
            }            

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }

        //Check if email already exists
        [HttpPost]
        public JsonResult checkIfCMSUserExists([Bind(Prefix = "clsCMSUser.strEmailAddress")] string strEmailAddress)
        {
            bool bCanUseEmail = false;
            clsCMSUsersManager clsCMSUsersManager = new clsCMSUsersManager();
            if(strEmailAddress != null && strEmailAddress != "")
                strEmailAddress = strEmailAddress.ToLower();
            bool bDoesCMSUserEmailExist = clsCMSUsersManager.checkIfCMSUserExists(strEmailAddress);
            if (bDoesCMSUserEmailExist == false)
                bCanUseEmail = true;
            return Json(bCanUseEmail, JsonRequestBehavior.AllowGet);
        }

        #region IMAGE FUNCTIONS
        //Get unique user path
        private string GetUniquePath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "images\\cmsUsers_Images";
            int iCount = 1;
            //### First we need to get the path
            while (System.IO.Directory.Exists(path + "\\ProfileImage" + iCount) == true)
            {
                iCount++;
            }
            return "ProfileImage" + iCount;
        }
        //Resize image
        private Bitmap ResizeImage(Image image, int width, int height)
        {
            Rectangle recRectangle = new Rectangle(0, 0, width, height);
            Bitmap bitImage = new Bitmap(width, height);

            bitImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(bitImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapImageMode = new ImageAttributes())
                {
                    wrapImageMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, recRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapImageMode);
                }
            }

            return bitImage;
        }
        #endregion

    }
}