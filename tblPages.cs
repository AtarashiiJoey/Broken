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
    
    public partial class tblPages
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPages()
        {
            this.tblUserAccess = new HashSet<tblUserAccess>();
        }
    
        public int iPageID { get; set; }
        public System.DateTime dtAdded { get; set; }
        public int iAddedBy { get; set; }
        public Nullable<System.DateTime> dtEdited { get; set; }
        public int iEditedBy { get; set; }
        public string strTitle { get; set; }
        public bool bIsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUserAccess> tblUserAccess { get; set; }
    }
}
