//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Scenario.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class TestWeigthPoint
    {
        public double Value { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> GID { get; set; }
    
        public virtual TestWeight TestWeight { get; set; }
    }
}
