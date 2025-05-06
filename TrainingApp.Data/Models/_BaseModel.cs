using System;

namespace trainingapp.data.basemodels
{
    public abstract class _basemodel
    {
        #region fields

        [key]
        public Guid id { get; set; }

        public string iscreatedby { get; set; }
        public DateTime? createdon { get; set; }
        public string ismodifiedby { get; set; }
        public DateTime? modifiedon { get; set; }
        public bool isdeleted { get; set; }
        public string deletedby { get; set; }
        public DateTime? deletedon { get; set; }
        #endregion
    }


}