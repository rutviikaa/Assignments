//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompanyApplication
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProjectManager
    {
        public int ProjectManagerId { get; set; }
        public int ProjectEmployeeId { get; set; }
        public int ProjectId { get; set; }
    
        public virtual Project_Employees Project_Employees { get; set; }
        public virtual Project Project { get; set; }
    }
}
