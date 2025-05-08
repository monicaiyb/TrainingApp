using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TrainingApp.Data.Models.Users
{

        public class ApplicationUser : IdentityUser<Guid>
        {

            [Required, MaxLength(255), DisplayName("First Name")]
            public string FirstName { get; set; }
            [Required, MaxLength(255), DisplayName("Last Name")]
            public string LastName { get; set; }
            [MaxLength(255), DisplayName("Other Name")]
            public string OtherName { get; set; }
            [EmailAddress, MaxLength(255), DisplayName("Email")]
            public string UserEmail { get; set; }

            [RegularExpression(@"0[0-9]{9}", ErrorMessage = "Invalid Phone Number Format")]
            [DisplayName("TelePhone")]
            public new string PhoneNumber { get; set; }
            public bool IsProffessed { get; set; }
            public DateTime? LastPasswordChangeDate { get; set; }
            public DateTime? EmailActivationExpiryDate { get; set; }
            public string LoginToken { get; set; }



            /// <summary>
            /// NOT MAPPED
            /// </summary>

            #region NotMapped

            [NotMapped]
            public string ErrorMessage { get; set; }
            [NotMapped]
            [Required]
            public string Password { get; set; }
            [NotMapped]
            public string ConfirmPassWord { get; set; }

            #endregion


            #region Navigation Property

            public List<RoleMapping> UserRoleMappings { get; set; } = new List<RoleMapping>();

            #endregion

            public bool IsLockedOut()
            {
                return LockoutEnd == null ? false : LockoutEnd > DateTime.Now;
            }


        }
    
}
