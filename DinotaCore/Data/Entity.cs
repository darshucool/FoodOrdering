using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dinota.Core.Data.Context;

namespace Dinota.Core.Data
{
    /// <summary>
    /// Base class for all entities. Change tracking and other data services are handled by the ORM.
    /// </summary>
    public abstract class Entity : IValidatableObject
    {
        protected Entity()
        {
            IsTrackingEnable = true;
        }
        /// <summary>
        /// Is called just before the new entity is inserted to the DB.
        /// Default implementation does not do anything.
        /// <remarks>Is called if the entity has no validation errors.</remarks>
        /// </summary>
        public virtual void OnBeforeInsert(IInsertionContext insertionContext) { }

        /// <summary>
        /// Is called just before the changes made to the entity is persisted.
        /// Default implementation does not do anything.
        /// <remarks>Is called if the entity has no validation errors.</remarks>
        /// </summary>
        public virtual void OnBeforeUpdate(IUpdationContext updationContext) { }

        /// <summary>
        /// Is called just before entity is deleted from DB.
        /// Default implementation does not do anything.
        /// <remarks>Is called if the entity has no validation errors.</remarks>
        /// </summary>
        public virtual void OnBeforeDelete(IDeletionContext deletionContext) { }

        [Display(AutoGenerateField = false, AutoGenerateFilter = false)]
        [ScaffoldColumn(false)]
        public DateTime CreationDate { get; set; }

        [Display(AutoGenerateField = false, AutoGenerateFilter = false, Name = "Last Updated")]
        [ScaffoldColumn(false)]
        public DateTime LastModifiedDate { get; set; }

        [Display(AutoGenerateField = false, AutoGenerateFilter = false)]
        [ScaffoldColumn(false)]
        public string LastModifiedBy { get; set; }

        [Display(AutoGenerateField = false, AutoGenerateFilter = false)]
        [ScaffoldColumn(false)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Indicated whether the entity should be expired or deleted when asked to delete.
        /// </summary>
        [NotMapped]
        [Display(AutoGenerateField = false, AutoGenerateFilter = false)]
        [ScaffoldColumn(false)]
        public virtual bool ExpireOnDelete { get { return true; } }

        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <returns>
        /// A collection that holds failed-validation information.
        /// </returns>
        /// <param name="validationContext">The validation context.</param>
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }

        /// <summary>
        /// Disable tracking of entities at some times only.
        /// </summary>
        /// <remarks>Tracking is enabled for all entites by default</remarks>
        [NotMapped]
        public bool IsTrackingEnable { get; set; }
    }
}
