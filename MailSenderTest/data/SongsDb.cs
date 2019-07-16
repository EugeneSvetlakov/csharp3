using MailSenderTest.Migrations;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderTest.data
{
    class SongsDb : DbContext
    {
        #region Установка инициализатора БД

        static SongsDb()
        {
            // Отладочный инициализатор
            // При каждом запуске приложения полностью удаляет и пересоздает БД со срабатыванием 
            // метода Seed, который наполняет БД начальными данными
            //Database.SetInitializer(new DropCreateDatabaseAlways<SongsDb>());

            // Если структура классов программы отличается от структуры таблиц в БД
            // БД пересоздается
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SongsDb>());

            // Создает БД, если ее не существует
            Database.SetInitializer(new CreateDatabaseIfNotExists<SongsDb>());

            // Версия БД обновится до последней версии
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SongsDb, Configuration>());
        }

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public SongsDb() : this("Name=SongsDb") { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ConnectionString">Имя строки подключения из App.config или строка подключенияк БД</param>
        public SongsDb(string ConnectionString) : base(ConnectionString)
        {

        }

        public DbSet<Song> Songs { get; set; }

        public DbSet<Artist> Artists { get; set; }
    }
}
