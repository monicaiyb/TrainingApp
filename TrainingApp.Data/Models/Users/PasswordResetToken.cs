namespace TrainingApp.Data.Models.Users
{
    public class PasswordResetToken
    {
        public string Token { get; set; }
        public DateTime DateGenerated { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsValid { get; set; }
        public string ApplicationUserId { get; set; }

        #region Navigation Properties
        public ApplicationUser ApplicationUser { get; set; }
        #endregion
    }
}
