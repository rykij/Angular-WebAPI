﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class testEntityFrameworkEntities : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Equity> Equities { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Weight> Weights { get; set; }
        public virtual DbSet<ModelParameter> ModelParameters { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<ScenarioType> ScenarioTypes { get; set; }
        public virtual DbSet<Credit> Credits { get; set; }
        public virtual DbSet<CreditTransitionMatrix> CreditTransitionMatrices { get; set; }
        public virtual DbSet<BhcTemplate> BhcTemplates { get; set; }
        public virtual DbSet<CURVES_MC_1> CURVES_MC_1 { get; set; }
        public virtual DbSet<EXTRA> EXTRAs { get; set; }
        public virtual DbSet<REALC> REALCs { get; set; }
        public virtual DbSet<SUMMARY> SUMMARies { get; set; }
        public virtual DbSet<TRANSITION_MATRIX> TRANSITION_MATRIX { get; set; }
        public virtual DbSet<CURVES_MC_2> CURVES_MC_2 { get; set; }
        public virtual DbSet<DefaultCredit> DefaultCredits { get; set; }
        public virtual DbSet<Real> Reals { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<ModelTemplate> ModelTemplate { get; set; }
        public virtual DbSet<Inflation> Inflations { get; set; }
        public virtual DbSet<NominalRate> NominalRates { get; set; }
        public virtual DbSet<Swaption> Swaptions { get; set; }
        public virtual DbSet<InflationCurve> InflationCurves { get; set; }
        public virtual DbSet<NominalCurve> NominalCurves { get; set; }
        public virtual DbSet<SwaptionCurve> SwaptionCurves { get; set; }
        public virtual DbSet<Factor> Factors { get; set; }
        public virtual DbSet<ModelsFactor> ModelsFactors { get; set; }
        public virtual DbSet<TestWeight> TestWeights { get; set; }
        public virtual DbSet<TestWeigthPoint> TestWeigthPoints { get; set; }
        public virtual DbSet<ScenarioSummary> ScenarioSummaries { get; set; }
    }
}
