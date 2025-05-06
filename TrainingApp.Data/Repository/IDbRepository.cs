using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TrainingApp.Data.Repository
{
    public interface IDbRepository
  
       
    {
     
        DbSet<T> Set<T>() where T : class;

        Task UpdateDatabaseModel<T>(T dbItem, T updatedItem) where T : class;


        Task SaveChanges();

       
              
       
     Task Remove<T>(T model) where T : class;

     

 
    }
}
   

