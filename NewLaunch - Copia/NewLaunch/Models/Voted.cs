//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewLaunch.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Voted
    {
        public int Id { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<int> RestaurantID { get; set; }
        public Nullable<System.DateTime> DateVoted { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
