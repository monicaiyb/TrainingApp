using System.ComponentModel.DataAnnotations.Schema;
using trainingapp.data.basemodels;

namespace TrainingApp.Data.Models.Users
{
    public class RoleMapping : _Basemodel
    {
        #region Fields

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("SecurityRole")]
        public Guid SecurityRoleId { get; set; }
        #endregion

        #region Navigation Properties

        public virtual ApplicationUser User { get; set; }
        public virtual Role SecurityRole { get; set; }

        #endregion
    }
}
