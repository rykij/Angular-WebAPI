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
    
    public partial class Configuration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Configuration()
        {
            this.Children = new HashSet<Configuration>();
            this.Weights = new HashSet<Weight>();
            this.Equities = new HashSet<Equity>();
            this.Properties = new HashSet<Property>();
            this.Credits = new HashSet<Credit>();
            this.BhcTemplates = new HashSet<BhcTemplate>();
            this.DerivedFrom = new HashSet<Configuration>();
            this.CURVES_MC_1 = new HashSet<CURVES_MC_1>();
            this.EXTRAs = new HashSet<EXTRA>();
            this.REALCs = new HashSet<REALC>();
            this.SUMMARies = new HashSet<SUMMARY>();
            this.TRANSITION_MATRIX = new HashSet<TRANSITION_MATRIX>();
            this.CURVES_MC_2 = new HashSet<CURVES_MC_2>();
            this.Reals = new HashSet<Real>();
        }
    
        public int ID { get; internal set; }
        internal int status { get; set; }
        public string Economy { get; set; }
        internal Nullable<int> ParentID { get; set; }
        public int Version { get; set; }
        public Nullable<int> TypeId { get; set; }
        public double Seed { get; set; }
        public string ElaboratingMachine { get; set; }
        public Nullable<int> Simulated { get; set; }
        public Nullable<int> BaseScenarioID { get; set; }
        public string Notes { get; set; }
        public int Trials { get; set; }
        internal Nullable<int> NYCGID { get; set; }
        internal Nullable<int> NYCLP0GID { get; set; }
        internal Nullable<int> INFGID { get; set; }
        internal Nullable<int> INFCEGID { get; set; }
        internal Nullable<int> SWAPTIONGID { get; set; }
        internal Nullable<int> SWAPTIONMGID { get; set; }
        public int Timestep { get; set; }
        public string CalibratePath { get; set; }
        public string CalibrationDescription { get; set; }
        public Nullable<int> TestWeight { get; set; }
        public bool InfiniteBonds { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configuration> Children { get; set; }
        public virtual Configuration Parent { get; set; }
        public virtual ModelParameter ModelParameter { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Weight> Weights { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equity> Equities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ScenarioType ScenarioType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Credit> Credits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BhcTemplate> BhcTemplates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configuration> DerivedFrom { get; set; }
        public virtual Configuration BaseScenario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<CURVES_MC_1> CURVES_MC_1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<EXTRA> EXTRAs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<REALC> REALCs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<SUMMARY> SUMMARies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<TRANSITION_MATRIX> TRANSITION_MATRIX { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        internal virtual ICollection<CURVES_MC_2> CURVES_MC_2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Real> Reals { get; set; }
        public virtual InflationCurve InflationCurve { get; set; }
        public virtual InflationCurve InflationCurveCe { get; set; }
        public virtual SwaptionCurve SwaptionMarketCurve { get; set; }
        public virtual NominalCurve NominalCurve { get; set; }
        public virtual NominalCurve NominalCurveLp0 { get; set; }
        public virtual SwaptionCurve SwaptionCurve { get; set; }
        public virtual TestWeight TestWeight1 { get; set; }
    }
}
