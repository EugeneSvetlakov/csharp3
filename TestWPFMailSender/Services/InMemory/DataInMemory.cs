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

        public T GetById(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), id, "Значенеи id должно быть больше 0");
            return _Items.FirstOrDefault(item => item.id == id);
        }

        public void Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            if (_Items.Any(i => i.id == item.id)) return;
            item.id = _Items.Count == 0 ? 1 : _Items.Max(i => i.id) + 1;
            _Items.Add(item);
        }

        public abstract void Edit(T item);

        public void Delete(T item)
        {
            _Items.Remove(item);
        }
    }
}
