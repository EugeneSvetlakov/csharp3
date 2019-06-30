using System;
using System.Collections.Generic;
using MailSender.Data;

namespace MailSender.Services.InMemory
{
    public class RecipientsDataInMemory : DataInMemory<Recipient>,IRecipientsDataService
    {
        public RecipientsDataInMemory()
        {
            var test_data = new List<Recipient>
            {
                new Recipient{ id = 1, Name = "Деревянкин", Address = "derevyankin@localhost", Comment = ""},
                new Recipient{ id = 2, Name = "Вишенкин", Address = "vishenkin@localhost", Comment = ""},
                new Recipient{ id = 3, Name = "Ольховкин", Address = "olkhovkin@localhost", Comment = ""}
            };

            _Items.AddRange(test_data);
        }

        public override void Edit(Recipient item)
        {
            //todo: проверить исправность работы сравнивалки _Items.Contains(item)
            if (item is null) throw new ArgumentNullException(nameof(item));

            var db_item = GetById(item.id);

            if (db_item is null) return;

            db_item.Name = item.Name;
            db_item.Address = item.Address;
            db_item.Comment = item.Comment;
        }
    }
}
