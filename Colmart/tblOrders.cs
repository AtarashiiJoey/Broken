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
    
    public partial class tblOrders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblOrders()
        {
            this.tblOrderItems = new HashSet<tblOrderItems>();
        }
    
        public int iOrderID { get; set; }
        public System.DateTime dtAdded { get; set; }
        public int iAddedBy { get; set; }
        public Nullable<System.DateTime> dtEdited { get; set; }
        public Nullable<int> iEditedBy { get; set; }
        public int iUserID { get; set; }
        public int iOrderStatusID { get; set; }
        public bool bIsConfirmed { get; set; }
        public bool bIsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOrderItems> tblOrderItems { get; set; }
        public virtual tblOrderStatuses tblOrderStatuses { get; set; }
        public virtual tblUsers tblUsers { get; set; }
    }
}