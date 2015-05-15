using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Scenario.Entities
{
    public partial class testEntityFrameworkEntities : DbContext
    {
        internal System.Data.Entity.SqlServer.SqlProviderServices instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

        public testEntityFrameworkEntities()
            : base("name=testEntityFrameworkEntities")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public testEntityFrameworkEntities(string ConnectionString)
            : base(ConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public ObjectQuery<T> CreateNavigationSourceQuery<T>(object Entity, string NavigationProperty)
        {
            var ose = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(Entity);

            var rm = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetRelationshipManager(Entity);

            var entityType = (EntityType)ose.EntitySet.ElementType;
            var navigation = entityType.NavigationProperties[NavigationProperty];

            var relatedEnd = rm.GetRelatedEnd(navigation.RelationshipType.FullName, navigation.ToEndMember.Name);

            return ((dynamic)relatedEnd).CreateSourceQuery();
        }

        public void AddResultsToDatabase(Configuration Scenario)
        {
            testEntityFrameworkEntities context = this;

            foreach (IResult Result in Scenario.Results)
            {
                ((IIResult)Result).ScenarioId = Scenario.ID;
                if (Result is CURVES_MC_1)
                {
                    AddOrUpdateContext<CURVES_MC_1>(Scenario.CURVES_MC_1.Where<IResult>(r => r.Timestep.Equals(Result.Timestep) && r.Trial.Equals(Result.Trial)).ToList(), Result);
                }
                else if (Result is CURVES_MC_2)
                {
                    AddOrUpdateContext<CURVES_MC_2>(Scenario.CURVES_MC_2.Where<IResult>(r => r.Timestep.Equals(Result.Timestep) && r.Trial.Equals(Result.Trial)).ToList(), Result);
                }
                else if (Result is EXTRA)
                {
                    AddOrUpdateContext<EXTRA>(Scenario.EXTRAs.Where<IResult>(r => r.Timestep.Equals(Result.Timestep) && r.Trial.Equals(Result.Trial)).ToList(), Result);
                }
                else if (Result is SUMMARY)
                {
                    AddOrUpdateContext<SUMMARY>(Scenario.SUMMARies.Where<IResult>(r => r.Timestep.Equals(Result.Timestep) && r.Trial.Equals(Result.Trial)).ToList(), Result);
                }
                else if (Result is REALC)
                {
                    AddOrUpdateContext<REALC>(Scenario.REALCs.Where<IResult>(r => r.Timestep.Equals(Result.Timestep) && r.Trial.Equals(Result.Trial)).ToList(), Result);
                }
                else if (Result is TRANSITION_MATRIX)
                {
                    AddOrUpdateContext<TRANSITION_MATRIX>(Scenario.TRANSITION_MATRIX.Where<IResult>(r => r.Timestep.Equals(Result.Timestep) && r.Trial.Equals(Result.Trial)).ToList(), Result);
                }
            }

            context.SaveChanges();
        }

        private void AddOrUpdateContext<T>(IList<IResult> ObjectSet, IResult Result) where T : class
        {
            if (ObjectSet.Count() == 0)
                ((IObjectContextAdapter)this).ObjectContext.CreateObjectSet<T>().AddObject((T)Result);
            else
            {
                var entity = ObjectSet.First();
                entity = Result;
            }
        }
    }
}
