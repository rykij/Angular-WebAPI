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
    
    public partial class CreditTransitionMatrix
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CreditTransitionMatrix()
        {
            this.Credits = new HashSet<Credit>();
        }
    
        public int ID { get; internal set; }
        public string StartRating { get; internal set; }
        public string EndRating { get; internal set; }
        public string Type { get; internal set; }
        public System.DateTime Date { get; internal set; }
        public double Value { get; internal set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Credit> Credits { get; set; }
    }
}
