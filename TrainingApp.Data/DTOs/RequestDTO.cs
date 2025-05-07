using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Data.DTOs
{
   
    
    public class RequestDTO
    {
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
       
    }
}
