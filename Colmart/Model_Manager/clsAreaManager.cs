using Colmart.Models;
using System.Collections.Generic;
using System.Linq;

namespace Colmart.Model_Manager
{
    public class clsAreaManager
    {
        ColmartDBContext db = new ColmartDBContext();

        //Get All
        public List<clsAreas> getAllAreasList()
        {
            List<clsAreas> lstAreas = new List<clsAreas>();
            var lstGetAreasList = db.tblAreas.Where(Area => Area.bIsDeleted == false).ToList();

            if (lstGetAreasList.Count > 0)
            {
                foreach (var item in lstGetAreasList)
                {
                    clsAreas clsArea = new clsAreas
                    {
                        iAreaID = item.iAreaID,
                        iAddedBy = item.iAddedBy,
                        dtAdded = item.dtAdded,
                        iEditedBy = item.iEditedBy,
                        dtEdited = item.dtEdited,
                        strAreaAbbreviation = item.strAreaAbbreviation,
                        strAreaName = item.strAreaName,
                        bIsDeleted = item.bIsDeleted
                    };

                    lstAreas.Add(clsArea);
                }
            }

            return lstAreas;
        }
    }
}