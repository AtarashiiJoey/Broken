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
    
    public partial class tblProductAssociationLinkTable
    {
        public int iProductLinkID { get; set; }
        public string iMainProductCode { get; set; }
        public string iAssociatedProductCode { get; set; }
        public int iAssociationID { get; set; }
        public bool bIsDeleted { get; set; }
    
        public virtual tblAssociations tblAssociations { get; set; }
    }
}
