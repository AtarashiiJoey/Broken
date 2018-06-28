//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Colmart
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCMSUsers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCMSUsers()
        {
            this.tblCMSUserAccess = new HashSet<tblCMSUserAccess>();
        }
    
        public int iCMSUserID { get; set; }
        public System.DateTime dtAdded { get; set; }
        public int iAddedBy { get; set; }
        public Nullable<System.DateTime> dtEdited { get; set; }
        public int iEditedBy { get; set; }
        public int iCMSRoleTypeID { get; set; }
        public string strFirstName { get; set; }
        public string strSurname { get; set; }
        public string strBiographicalInfo { get; set; }
        public string strContactNumber { get; set; }
        public string strEmailAddress { get; set; }
        public string strPassword { get; set; }
        public string strImagePath { get; set; }
        public string strImageName { get; set; }
        public bool bIsDeleted { get; set; }
    
        public virtual tblCMSRoleTypes tblCMSRoleTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCMSUserAccess> tblCMSUserAccess { get; set; }
    }
}
