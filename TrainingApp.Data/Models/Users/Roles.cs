using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using trainingapp.data.basemodels;

namespace TrainingApp.Data.Models.Users
{
    public class Role : IdentityRole<Guid>
    {
        public virtual List<RolePermission> Permissions { get; set; } = new List<RolePermission>();

        public Role() :   base() { }
        public Role(string roleName) : base(roleName) { }

        [NotMapped]
        public string Enc_Id { get; set; }
    }

    public class RolePermission : _Basemodel
    {
        #region Fields

        //[ForeignKey("Permission")]
        //public long PermissionId { get; set; }
        [ForeignKey("Role"), Required]
        public Guid RoleId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual PermissionType Permission { get; set; }
        public virtual Role Role { get; set; }

        #endregion
    }
}
