//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tools.AuxClasses
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tool
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ModelName { get; set; }
        public Nullable<System.DateTime> OperatingSince { get; set; }
        public Nullable<System.DateTime> DecomissionedSince { get; set; }
        public string SerialNumber { get; set; }
        public string Notes { get; set; }
    
        public virtual Category Category { get; set; }
    }
}
