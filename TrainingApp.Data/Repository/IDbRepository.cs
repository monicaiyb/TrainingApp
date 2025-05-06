using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Data.Repository
{
    public interface IDbRepository
  
       
    {
     
        DbSet<T> Set<T>() where T : class;

        Task UpdateDatabaseModel<T>(T dbItem, T updatedItem) where T : class;


        Task UpdateCollections<T>(List<T> dbItems, List<T> updatedItems) where T : class, IGuidKey;

        Task SaveChanges();

       
        IEnumerable<T> SqlQuery<T>(string query, Func<DbDataReader, T> ExtractDataFtn, params SqlParameter[] parameters);

      
       
     Task Remove<T>(T model) where T : class;

     Task DeleteItems(Guid itemId, string userId);
     Task DeleteItem<T>(Guid itemId, string userId) where T : BaseModel;

 
    }
}
   

