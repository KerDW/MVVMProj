//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVVMPractica2.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class telefon
    {
        public int telId { get; set; }
        public string telefon1 { get; set; }
        public string tipus { get; set; }
        public Nullable<int> contacteId { get; set; }
    
        public virtual contacte contacte { get; set; }
    }
}
