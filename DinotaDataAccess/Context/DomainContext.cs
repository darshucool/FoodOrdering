using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Objects;
using System.Linq;
using System.Web;
using Dinota.Core.Data;
using Dinota.Core.Specification;
using Dinota.Core.Xml;
using Dinota.DataAccces.Context.Internal;
using Dinota.DataAccces.Tracking;
using Dinota.DataAccces.User;
using Dinota.Domain;
using Dinota.Domain.User;
using Dinota.Security.Tracking;

namespace Dinota.DataAccces.Context
{
    [System.Diagnostics.DebuggerDisplay("UserName = {UserName}")]
    public partial class DomainContext : DbContext, IDomainContext
    {
        public DomainContext(string connectionString, string userName)
            : base(connectionString)
        {
            UserName = userName;
        }
      
        public string UserName
        {
            get;
            private set;
        }

        private readonly HashSet<Entity> _expiredEntities = new HashSet<Entity>();

        internal void ExpireEntity(Entity entity)
        {
            _expiredEntities.Add(entity);
        }

        public override int SaveChanges()
        {
            //we are validating entities manually
            Configuration.ValidateOnSaveEnabled = false;

            //todo: check whether DetectChanges() call is required
          
            var validationResults = new List<DbEntityValidationResult>();

            var changedEntities = ChangeTracker.Entries();
            var trackingEntityRecords = new List<TrackingEntityRecord>();
            var trackingService = new TrackingService(new TrackingRepo(this));

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is Entity)
                {
                    var entity = (Entity)changedEntity.Entity;
                    
                    //ignore the types that are marked as NotTracked
                    if (Attribute.IsDefined(entity.GetType(), typeof(NotTrackedAttribute)))
                    {
                        changedEntity.State = EntityState.Unchanged;
                        continue;
                    }

                    if (_expiredEntities.Contains(entity))
                    {
                        DoExpire(entity);
                        continue;
                    }

                    if(!entity.IsTrackingEnable)
                    {
                        continue;
                    }

                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            DoAdd(entity, validationResults, changedEntity);
                            AddTrackingEntityRecords(trackingService, EntityState.Added, entity, trackingEntityRecords);
                            break;

                        case EntityState.Modified:
                            DoUpdate(entity, validationResults, changedEntity);
                            AddTrackingEntityRecords(trackingService, EntityState.Modified, entity, trackingEntityRecords);
                            break;

                        case EntityState.Deleted:
                            DoDelete(entity);
                            AddTrackingEntityRecords(trackingService, EntityState.Deleted, entity, trackingEntityRecords);
                            break;
                    }
                }
            }

            if (validationResults.Count > 0)
                throw new DbEntityValidationException("Validation Failed. See EntityValidationErrors for more details.", validationResults);

            var count = base.SaveChanges();

            EntityStateTracking(trackingEntityRecords);

            _expiredEntities.Clear();

            return count;
        }

        private void AddTrackingEntityRecords(TrackingService trackingService, EntityState state, Entity entity, List<TrackingEntityRecord> trackingEntities)
        {
            List<KeyValue> listKeyValue;
           
            switch (state)
            {
                case EntityState.Added:
                    trackingEntities.Add(new TrackingEntityRecord { Entity = entity, KeyValues = null, EntityState = EntityTraceTypeEnum.Added.ToString() });
                    break;

                case EntityState.Modified:
                    listKeyValue = trackingService.GetKeyValueList(UnderlyingObjectContext, entity);
                    trackingEntities.Add(new TrackingEntityRecord { Entity = entity, KeyValues = listKeyValue, EntityState = EntityTraceTypeEnum.Modified.ToString() });
                    break;

                case EntityState.Deleted:
                    listKeyValue = trackingService.GetKeyValueList(UnderlyingObjectContext, entity);
                    trackingEntities.Add(new TrackingEntityRecord { Entity = entity, KeyValues = listKeyValue, EntityState = EntityTraceTypeEnum.Deleted.ToString() });
                    break;
            }
            
        }

        public void EntityStateTracking(List<TrackingEntityRecord> trackingEntityRecords)
        {
            var domainContext = (DomainContext) GetDomainContext();

            using (domainContext)
            {
                var userBaseService = new UserBaseService(new UserBaseRepo(domainContext));
                var filter = userBaseService.GetDefaultSpecification();
                filter = filter.And(user => user.UserName == HttpContext.Current.User.Identity.Name);
                var theUser = userBaseService.GetBy(filter);

                if (theUser == null) return;
                var trackingService = new TrackingService(new TrackingRepo(this));

                foreach (var trackingEntityRecord in trackingEntityRecords)
                {
                    var newTracking = trackingService.Create();
                    newTracking.UserId = theUser.Id;
                    newTracking.CreationDate = DateTime.Now;

                    if (trackingEntityRecord != null)
                    {
                        newTracking = trackingEntityRecord.EntityState == "Added"
                                          ? trackingService.GenerateTrackingXmlField(UnderlyingObjectContext,
                                                                                     trackingEntityRecord.Entity,
                                                                                     newTracking,
                                                                                     trackingEntityRecord.EntityState,
                                                                                     theUser.UserName)
                                          : trackingService.GenerateTrackingXmlField(trackingEntityRecord.KeyValues,
                                                                                     trackingEntityRecord.Entity,
                                                                                     newTracking,
                                                                                     trackingEntityRecord.EntityState,
                                                                                     theUser.UserName);
                    }
                    trackingService.Add(newTracking);
                }

                base.SaveChanges();
            }
        }

        protected static IDomainContext GetDomainContext()
        {
            return new DomainContext("Alfasi", "System");
        }

        private void DoAdd(Entity entity, ICollection<DbEntityValidationResult> validationResults, DbEntityEntry changedEntity)
        {
            entity.LastModifiedBy = UserName;
            entity.CreationDate = entity.LastModifiedDate = DateTime.Now;

            if (ValidateEntity(changedEntity, validationResults))
            {
                entity.OnBeforeInsert(new InsertionContext(this, entity));
            }
        }

        private void DoUpdate(Entity entity, ICollection<DbEntityValidationResult> validationResults, DbEntityEntry changedEntity)
        {
            entity.LastModifiedBy = UserName;
            entity.LastModifiedDate = DateTime.Now;

            if (ValidateEntity(changedEntity, validationResults))
            {
                entity.OnBeforeUpdate(new UpdationContext(this, entity));
            }
        }

        private void DoExpire(Entity entity)
        {
            entity.OnBeforeDelete(new DeletionContext(this, entity));

            entity.ExpiryDate = DateTime.Now;
            entity.LastModifiedBy = UserName;
            entity.LastModifiedDate = DateTime.Now;
        }

        private void DoDelete(Entity entity)
        {
            entity.OnBeforeDelete(new DeletionContext(this, entity));
        }

        private bool ValidateEntity(DbEntityEntry changedEntity, ICollection<DbEntityValidationResult> validationResults)
        {
            var validationResult = base.ValidateEntity(changedEntity, null);
            if (validationResult != null && !validationResult.IsValid)
            {
                validationResults.Add(validationResult);
                return false;
            }

            return true;
        }

        internal virtual ObjectContext UnderlyingObjectContext
        {
            get
            {
                return ((IObjectContextAdapter)this).ObjectContext;
            }
        }

        internal DateTime? GetLastSyncTime()
        {
            var lastSyncDate = Database.SqlQuery<DateTime?>("SELECT MAX(end_time) AS SyncDateTime " +
                                                            "FROM distribution.dbo.MSmerge_sessions S " +
                                                            "INNER JOIN distribution.dbo.MSmerge_agents A ON A.id = S.agent_id " +
                                                            "WHERE A.publication = 'EMC' ").FirstOrDefault();

            return lastSyncDate;
        }
    }
}
