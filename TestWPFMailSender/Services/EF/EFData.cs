using MailSender.Data.BaseEntityes;
using MailSender.Data.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services.EF
{
    public abstract class EFData<T> : IDataService<T> where T : Entity
    {
        protected readonly MailSenderDB _Db;
        protected readonly DbSet<T> _Set;

        public EFData(MailSenderDB Db)
        {
            _Db = Db;
            _Set = Db.Set<T>();
        }

        public IEnumerable<T> GetAll() => _Set.AsEnumerable();

        public async Task<IEnumerable<T>> GetAllAsync() => await _Set.ToArrayAsync().ConfigureAwait(false);

        public T GetById(int id) => _Set.FirstOrDefault(item => item.id == id);

        public async Task<T> GetByIdAsync(int id) => await _Set.FirstOrDefaultAsync(item => item.id == id).ConfigureAwait(false);

        public int Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _Set.Add(item);
            Commit();
            return item.id;
        }

        public async Task<int> AddAsync(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _Set.Add(item);
            await CommitAsync();
            return item.id;
        }

        public abstract T Edit(int id, T item);

        public abstract Task<T> EditAsync(int id, T item);

        public bool Delete(int id)
        {
            var db_item = GetById(id);
            if (db_item is null) return false;
            _Set.Remove(db_item);
            return Commit();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var db_item = await GetByIdAsync(id).ConfigureAwait(false);
            if (db_item is null) return false;
            _Set.Remove(db_item);
            return await CommitAsync();
        }

        public bool Commit() => _Db.SaveChanges() > 0;

        public async Task<bool> CommitAsync() => await _Db.SaveChangesAsync().ConfigureAwait(false) > 0;
    }
}
