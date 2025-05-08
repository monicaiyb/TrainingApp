using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Data.DTOs
{
    public class APIResponse
    {
        public string Message { get; set; }

        public object Data { get; set; }
        public int Code { get; set; }
        public bool IsSuccess { get; set; }
        public string Status { get; set; }

    }
    public class ApiResponse<TData>
    {
        public string Message { get; set; }
        public TData Data { get; set; }

    }
}
