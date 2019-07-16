using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.Services
{
    public interface IDataService<T>
    {
        /// <summary>
        /// Получить всех
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Получить всех Асинхронно
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Получить представителя класса по его id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Получить представителя класса по его id Асинхронно
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Создать (зарегистрировать)
        /// </summary>
        /// <param name="item">представитель класса</param>
        int Add(T item);

        /// <summary>
        /// Создать (зарегистрировать) Асинхронно
        /// </summary>
        /// <param name="item">представитель класса</param>
        Task<int> AddAsync(T item);

        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="item">представитель класса</param>
        T Edit(int id, T item);

        /// <summary>
        /// Обновить Асинхронно
        /// </summary>
        /// <param name="item">представитель класса</param>
        Task<T> EditAsync(int id, T item);

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="item">представитель класса</param>
        bool Delete(int id);

        /// <summary>
        /// Удалить Асинхронно
        /// </summary>
        /// <param name="item">представитель класса</param>
        Task<bool> DeleteAsync(int id);
    }

    public interface ISenderService<T>
    {
        void Send(T item);
    }
}
