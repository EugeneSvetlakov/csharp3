using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Services;
using MailSender.Data.BaseEntityes;
using System.Collections.ObjectModel;

namespace MailSender.Services.InMemory
{
    public abstract class DataInMemory<T> : IDataService<T> where T : Entity
    {
        protected readonly List<T> _Items = new List<T>();

        public IEnumerable<T> GetAll() => _Items;

        public Task<IEnumerable<T>> GetAllAsync() => Task.FromResult((IEnumerable<T>)_Items);

        public T GetById(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Значенеи id должно быть больше 0");
            return _Items.FirstOrDefault(item => item.id == id);
        }

        public async Task<T> GetByIdAsync(int id) => await Task.Run(() => GetById(id));

        public int Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            if (_Items.Any(i => i.id == item.id)) return -1;
            item.id = _Items.Count == 0 ? 1 : _Items.Max(i => i.id) + 1;
            _Items.Add(item);
            return item.id;
        }

        public async Task<int> AddAsync(T item) => await Task.Run(() => Add(item));

        public abstract T Edit(int id, T item);

        public virtual async Task<T> EditAsync(int id, T item) => await Task.Run(() => Edit(id, item));

        public bool Delete(int id)
        {
            try
            {
                _Items.RemoveAt(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id) => await Task.Run(() => Delete(id));
    }
}
