using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TrainingApp.Data.Repository
{
    public class DbRepository: IDbRepository
    {
        private readonly TrainingContextDb _database;

            
        public DbRepository(TrainingContextDb db)
        {
            this._database = db;
           

        }




        public DbSet<T> Set<T>() where T : class
        {
            return _database.Set<T>();
        }

        public async Task UpdateDatabaseModel<T>(T dbItem, T updatedItem) where T : class
        {
            _database.Entry(dbItem).CurrentValues.SetValues(updatedItem);
        }

       


        public async Task SaveChanges()
        {

            await _database.SaveChangesAsync();
        }

     

       
   public async Task Remove<T>(T model) where T : class
        {
            _database.Remove(model);
        }
    }
}
