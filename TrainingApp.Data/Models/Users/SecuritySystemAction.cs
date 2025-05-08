using System.ComponentModel;

namespace TrainingApp.Data.Models.Users{
    public enum SecuritySystemAction
    {
        //generic actions under 1000
        [Description("Create and Edit")]
        CreateAndEdit = 1,
        Delete = 2,
        [Description("View List")]
        ViewList = 3,
        Review = 10,
        UpdatePermissions = 11,
        [Description("Submit For Approval")]
        SubmitForApproval = 12,
        //security specific actions 1000-9999
        [Description("Impersonate Users")]
        Impersonate = 1003,
        Login = 1004,
        LogOff = 1005,
        [Description("Change Password")]
        ChangePassword = 1006,
        [Description("Reset Password")]
        ResetPassword = 1007,
        Activate = 1008,
        Deactivate = 1009,
        [Description("View Details")]
        View_Details = 1010,
        [Description("View Approved Profiles")]
        View_Approved_Profiles = 1011,
        [Description("Approve Profiles")]
        Approve_Profiles = 1012,
        [Description("View Dashboard")]
        View_Dashboard = 1013,
       
        View_All_Items = 1022,
        Match_Account=1023,
        Microsite_Orders=1024,
        Microsite_Payments = 1025,


    }
}
