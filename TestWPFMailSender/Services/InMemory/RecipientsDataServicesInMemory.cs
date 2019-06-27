using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSender.Data.Linq2Sql;

namespace MailSender.Services.InMemory
{
    public class RecipientsDataServicesInMemory : IRecipientsDataService
    {
        private readonly ObservableCollection<Recipient> _RecipientsInMem = new ObservableCollection<Recipient>
        {
            new Recipient() {id = 1, Name = "Валенков", MailAddr = "valenkov@localhost", Comment = ""},
            new Recipient() {id = 2, Name = "Ботинков", MailAddr = "botinkov@localhost", Comment = ""},
            new Recipient() {id = 3, Name = "Тапочков", MailAddr = "tapochkov@localhost", Comment = ""}
        };

        public RecipientsDataServicesInMemory()
        {
        }

        public IEnumerable<Recipient> GetAll() => _RecipientsInMem;

        public Recipient GetById(int id) => _RecipientsInMem.FirstOrDefault(r => r.id == id);

        public void Create(Recipient item)
        {
            bool has_item = _RecipientsInMem.Contains(item);
            int ii = _RecipientsInMem.IndexOf(item);
            
            if (_RecipientsInMem.Contains(item)) return;
            item.id = _RecipientsInMem.Count == 0 ? 1 : _RecipientsInMem.Max(r => r.id) + 1;
            _RecipientsInMem.Add(item);
        }

        public void Update(Recipient item)
        {
            if (_RecipientsInMem.Contains(item)) return;
            var db_item = GetById(item.id);
            if (db_item is null) return;
            item.Name = db_item.Name;
            item.MailAddr = db_item.MailAddr;
            item.Comment = db_item.Comment;
        }

        public void Delete(Recipient item)
        {
            if (item is null) return;
            var db_item = GetById(item.id);
            if (db_item is null) return;
            _RecipientsInMem.Remove(db_item);
        }
    }
}
