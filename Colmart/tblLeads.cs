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
    
    public partial class tblLeads
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblLeads()
        {
            this.tblLeadWishlists = new HashSet<tblLeadWishlists>();
        }
    
        public int iLeadID { get; set; }
        public Nullable<System.DateTime> dtAdded { get; set; }
        public Nullable<int> iAddedBy { get; set; }
        public Nullable<System.DateTime> dtEdited { get; set; }
        public Nullable<int> iEditedBy { get; set; }
        public string strFirstName { get; set; }
        public string strEmail { get; set; }
        public string strPhone { get; set; }
        public bool bIsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLeadWishlists> tblLeadWishlists { get; set; }
    }
}
