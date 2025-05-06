using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Data.Repository
{
    public class DbRepository
    {
        private readonly TrainingDbContext _database;

        public DbSet<BaseModel> MyBaseEntities { get; set; }
        private static IHttpContextAccessor _httpContextAccessor;



        public DbRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this._database = db;
            _httpContextAccessor = httpContextAccessor;

        }




        public DbSet<T> Set<T>() where T : class
        {
            return _database.Set<T>();
        }

        public async Task UpdateDatabaseModel<T>(T dbItem, T updatedItem) where T : class
        {
            _database.Entry(dbItem).CurrentValues.SetValues(updatedItem);
        }

        public async Task UpdateCollections<T>(List<T> dbItems, List<T> updatedItems) where T : class, IGuidKey
        {
            var newListIds = updatedItems.Select(r => r.Id).ToList();

            //Delete removed entries
            foreach (var entry in dbItems.Where(a => newListIds.Contains(a.Id) == false).ToList())
            {
                this.Set<T>().Remove(entry);
            }

            await this.SaveChanges();

            //Update existing
            foreach (var origEntry in dbItems.Where(r => newListIds.Contains(r.Id)).ToList())
            {
                var updatedEntry = updatedItems.Where(r => r.Id == origEntry.Id).Single();

                this.UpdateDatabaseModel(origEntry, updatedEntry);
            }

            await this.SaveChanges();

            //Add new entries

            foreach (var entry in updatedItems.Where(r => r.Id == Guid.Empty).ToList())
            {
                this.Set<T>().Add(entry);
            }

            this.SaveChanges();
        }


        public async Task SaveChanges()
        {

            await _database.SaveChangesAsync();
        }

        public async Task LoadReference<T>(T model, Expression<Func<T, object>> Property) where T : class
        {
            _database.Entry(model).Reference(Property).Load();
        }


        public async Task DeleteItems(Guid itemId, string userId)
        {
            var item = await MyBaseEntities.FindAsync(itemId);

            if (item != null)
            {
                item.IsDeleted = true;
                item.DeletedBy = userId;
                item.DeletedOn = DateTime.Now;

            }
            // You might want to handle the case when the item is not found.
        }

        public async Task DeleteItem<T>(Guid itemId, string userId) where T : BaseModel
        {
            var item = await Set<T>().FindAsync(itemId);

            if (item != null)
            {
                item.IsDeleted = true;
                item.DeletedBy = userId;
                item.DeletedOn = DateTime.Now;

            }
            // You might want to handle the case when the item is not found.
        }
        public async Task Remove<T>(T model) where T : class
        {
            _database.Remove(model);
        }
    }
}
