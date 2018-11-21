using Colmart.Model_Manager;
using Colmart.Models;
using ColmartCMS.Assistant_Classes;
using ColmartCMS.View_Models.Users;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

namespace ColmartCMS.Controllers
{
    public class UsersController : Controller
    {
        // GET: User

        //Get All Users
        public ActionResult UsersView()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsUsersView clsUsersView = new clsUsersView();
            clsUsersManager clsUsersManager = new clsUsersManager();
            clsUsersView.lstUsers = clsUsersManager.getAllUsersOnlyList();

            return View(clsUsersView);
        }

        //Add User
        public ActionResult UserAdd()
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsUserAdd clsUserAdd = new clsUserAdd();
            clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager();
            clsUserAdd.lstRoleTypes = clsRoleTypesManager.getAllRoleTypesList();

            return View(clsUserAdd);
        }

        //Add User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserAdd(clsUserAdd clsUserAdd)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsUsersManager clsUsersManager = new clsUsersManager();
            clsUserAdd.clsUser.strEmailAddress = clsUserAdd.clsUser.strEmailAddress.ToLower();

            //Save image
            if (clsUserAdd.strCropImageData != null && clsUserAdd.strCropImageData != "")
            {
                string data = clsUserAdd.strCropImageData;
                data = data.Replace("data:image/png;base64,", "");
                string strImageName = "imgProfile_" + clsUserAdd.clsUser.strFirstName + clsUserAdd.clsUser.strSurname + ".png";
                string strSmallImageName = "sml_imgProfile_" + clsUserAdd.clsUser.strFirstName + clsUserAdd.clsUser.strSurname + ".png";
                string newPath = GetUniquePath();

                string strDestPath = "images\\users_Images\\" + newPath;
                string strFullDestPath = AppDomain.CurrentDomain.BaseDirectory + "images\\users_Images\\" + newPath;
                if (!System.IO.Directory.Exists(strFullDestPath))
                {
                    System.IO.Directory.CreateDirectory(strFullDestPath);
                }

                clsUserAdd.clsUser.strImagePath = strDestPath.Replace("\\", "/");
                clsUserAdd.clsUser.strImageName = strImageName;

                string strImagePath = strFullDestPath + "\\" + strImageName;
                string strSmallImagePath = strFullDestPath + "\\" + strSmallImageName;

                byte[] bytes = Convert.FromBase64String(data);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                //Smaller Image
                Image smallImage = ResizeImage(image, 35, 35);
                //Save images
                image.Save(strImagePath, System.Drawing.Imaging.ImageFormat.Png);
                smallImage.Save(strSmallImagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            else //save default images
            {
                string newPath = GetUniquePath();
                string strDestPath = "images\\users_Images\\" + newPath;
                string strFullDestPath = AppDomain.CurrentDomain.BaseDirectory + "images\\users_Images\\" + newPath;
                string strDefaultImagespath = AppDomain.CurrentDomain.BaseDirectory + "images\\defaultImages";
                if (!System.IO.Directory.Exists(strFullDestPath))
                {
                    System.IO.Directory.CreateDirectory(strFullDestPath);
                }

                clsUserAdd.clsUser.strImagePath = strDestPath.Replace("\\", "/");
                clsUserAdd.clsUser.strImageName = "default_image.png";

                string strImagePath = strFullDestPath + "\\" + "default_image.png";
                string strSmallImagePath = strFullDestPath + "\\" + "sml_default_image.png";

                //Copy small and default sizes
                System.IO.File.Copy(strDefaultImagespath + "\\default_image.png", strImagePath);
                System.IO.File.Copy(strDefaultImagespath + "\\sml_default_image.png", strSmallImagePath);
            }
            if (clsUserAdd.strNewPassword != null && clsUserAdd.strNewPassword != "")
                clsUserAdd.clsUser.strPassword = clsCommonFunctions.GetMd5Sum(clsUserAdd.strNewPassword);
            else
                clsUserAdd.clsUser.strPassword = clsCommonFunctions.GetMd5Sum(clsUserAdd.clsUser.strEmailAddress.ToLower());

            //Add  user
            clsUsersManager.saveUser(clsUserAdd.clsUser);

            //Add successful / notification
            TempData["bIsUserAdded"] = true;

            return RedirectToAction("UsersView", "Users");
        }

        //Edit User Info
        public ActionResult UserEdit(int id)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            if (id == 0)
            {
                return RedirectToAction("UsersView", "Users");
            }

            clsUserEdit clsUserEdit = new clsUserEdit();
            clsUsersManager clsUsersManager = new clsUsersManager();
            clsUserEdit.clsUser = clsUsersManager.getUserById(id);

            clsUserEdit.strEmailAddress = clsUserEdit.clsUser.strEmailAddress;

            if (clsUserEdit.clsUser.strImagePath != null && clsUserEdit.clsUser.strImagePath != "" && clsUserEdit.clsUser.strImageName != null && clsUserEdit.clsUser.strImageName != "")
                clsUserEdit.strFullImagePath = "/" + clsUserEdit.clsUser.strImagePath + "/" + clsUserEdit.clsUser.strImageName;

            clsRoleTypesManager clsRoleTypesManager = new clsRoleTypesManager();
            clsUserEdit.lstRoleTypes = clsRoleTypesManager.getAllRoleTypesList();

            return View(clsUserEdit);
        }

        //Edit User Info
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(clsUserEdit clsUserEdit)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            clsUsersManager clsUsersManager = new clsUsersManager();
            clsUsers clsSessionUser = (clsUsers)Session["clsUser"];
            clsUsers clsUser = clsUsersManager.getUserById(clsUserEdit.clsUser.iUserID);

            //Set Role type and password
            if (clsUserEdit.bResetPassword == true)
            {
                if (clsUserEdit.strNewPassword != null && clsUserEdit.strNewPassword != "")
                    clsUser.strPassword = clsCommonFunctions.GetMd5Sum(clsUserEdit.strNewPassword);
                else
                    clsUser.strPassword = clsCommonFunctions.GetMd5Sum(clsUser.strEmailAddress.ToLower());
            }
            clsUser.iRoleTypeID = clsUserEdit.clsUser.iRoleTypeID;

            clsUser.strFirstName = clsUserEdit.clsUser.strFirstName;
            clsUser.strSurname = clsUserEdit.clsUser.strSurname;
            clsUser.strPrimaryContactNumber = clsUserEdit.clsUser.strPrimaryContactNumber;

            //Save image
            if (clsUserEdit.strCropImageData != null && clsUserEdit.strCropImageData != "")
            {
                string data = clsUserEdit.strCropImageData;
                data = data.Replace("data:image/png;base64,", "");
                string strImageName = "imgProfile_" + clsUserEdit.clsUser.strFirstName + clsUserEdit.clsUser.strSurname + ".png";
                string strSmallImageName = "sml_imgProfile_" + clsUserEdit.clsUser.strFirstName + clsUserEdit.clsUser.strSurname + ".png";

                string strDestPath = "";
                if (clsUser.strImagePath != null && clsUser.strImagePath != "")
                {
                    strDestPath = clsUser.strImagePath.Replace("/", "\\");
                }
                else
                {
                    string newPath = GetUniquePath();
                    strDestPath = "images\\users_Images\\" + newPath;
                    clsUser.strImagePath = strDestPath.Replace("\\", "/");
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

                clsUser.strImageName = strImageName;

                string strImagePath = strFullDestPath + "\\" + strImageName;
                string strSmallImagePath = strFullDestPath + "\\" + strSmallImageName;

                byte[] bytes = Convert.FromBase64String(data);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

                //Smaller Image
                Image smallImage = ResizeImage(image, 35, 35);
                //Save images
                image.Save(strImagePath, System.Drawing.Imaging.ImageFormat.Png);
                smallImage.Save(strSmallImagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            else //save default images
            {
                string strDefaultImagespath = AppDomain.CurrentDomain.BaseDirectory + "images\\defaultImages";

                string strDestPath = "";
                if (clsUser.strImagePath != null && clsUser.strImagePath != "")
                {
                    strDestPath = clsUser.strImagePath.Replace("/", "\\");
                }
                else
                {
                    string newPath = GetUniquePath();
                    strDestPath = "images\\users_Images\\" + newPath;
                    clsUser.strImagePath = strDestPath.Replace("\\", "/");
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

                clsUser.strImageName = "default_image.png";

                string strImagePath = strFullDestPath + "\\" + "default_image.png";
                string strSmallImagePath = strFullDestPath + "\\" + "sml_default_image.png";

                //Copy small and default sizes
                System.IO.File.Copy(strDefaultImagespath + "\\default_image.png", strImagePath);
                System.IO.File.Copy(strDefaultImagespath + "\\sml_default_image.png", strSmallImagePath);
            }

            //Update  user
            clsUsersManager.saveUser(clsUser);

            //Add successful / notification
            TempData["bIsUserUpdated"] = true;

            return RedirectToAction("UsersView", "Users");
        }

        //Delete User
        [HttpPost]
        public ActionResult UserDelete(int iUserID)
        {
            //Redirect to login if null session exists
            if (Session["clsCMSUser"] == null)
                return RedirectToAction("Login", "Account");

            bool bIsSuccess = false;

            if (iUserID == 0)
            {
                return RedirectToAction("UsersView", "Users");
            }

            clsUsersManager clsUsersManager = new clsUsersManager();
            clsUsersManager.removeUserByID(iUserID);

            bIsSuccess = true;

            //Delete successful / notification
            TempData["bIsUserDeleted"] = true;

            return Json(new { bIsSuccess = bIsSuccess }, JsonRequestBehavior.AllowGet);
        }

        //Check if email already exists
        [HttpPost]
        public JsonResult checkIfUserExists([Bind(Prefix = "clsUser.strEmailAddress")] string strEmailAddress)
        {
            bool bCanUseEmail = false;
            clsUsersManager clsUsersManager = new clsUsersManager();
            if (strEmailAddress != null && strEmailAddress != "")
                strEmailAddress = strEmailAddress.ToLower();
            bool bDoesUserEmailExist = clsUsersManager.checkIfUserExists(strEmailAddress);
            if (bDoesUserEmailExist == false)
                bCanUseEmail = true;
            return Json(bCanUseEmail, JsonRequestBehavior.AllowGet);
        }

        #region IMAGE FUNCTIONS
        //Get unique user path
        private string GetUniquePath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "images\\users_Images";
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