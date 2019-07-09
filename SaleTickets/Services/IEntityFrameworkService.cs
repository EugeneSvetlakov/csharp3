using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleTickets.Services
{
    public interface IEntityFrameworkService<T>
    {
        /// <summary>
        /// Получить всех
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Получить представителя класса по его id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Создать (зарегистрировать)
        /// </summary>
        /// <param name="item">представитель класса</param>
        void Add(T item);

        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="item">представитель класса</param>
        void Edit(T item);

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="item">представитель класса</param>
        void Delete(T item);
    }
}
