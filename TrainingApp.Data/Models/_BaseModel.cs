namespace TrainingApp.Data.BaseModels
{
    public abstract class _BaseModel 
    {
        #region Fields

        [Key]
        public Guid Id { get; set; }

        public string IsCreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string IsModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        #endregion
    }

    
}