using trainingapp.data.basemodels;

namespace TrainingApp.Data.Models.Users
{
    public class PermissionType:_Basemodel
    {
        public SecurityModule Module { get; set; }
        public SecuritySubModule SubModule { get; set; }
        public SecuritySystemAction SystemAction { get; set; }
        
        public virtual List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
