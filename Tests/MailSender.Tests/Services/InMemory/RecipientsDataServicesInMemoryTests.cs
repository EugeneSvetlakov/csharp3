using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailSender.Services;
using MailSender.Services.InMemory;
using System.Collections.ObjectModel;
using MailSender.Data.Linq2Sql;

namespace MailSender.Tests.Services.InMemory
{
    /// <summary>
    /// Проверка работы сервиса RecipientsDataServicesInMemory
    /// </summary>
    
    [TestClass]
    public class RecipientsDataServicesInMemoryTests
    {
        public RecipientsDataServicesInMemoryTests()
        {
            //
            // TODO: добавьте здесь логику конструктора
            //
        }

        private TestContext _TestContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _TestContextInstance;
            }
            set
            {
                _TestContextInstance = value;
            }
        }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void NotAddingExistingRecipient()
        {
            // Arrange
            IEnumerable<Recipient> expected_collection = new ObservableCollection<Recipient>
            {
                new Recipient() {id = 1, Name = "Валенков", MailAddr = "valenkov@localhost", Comment = ""},
                new Recipient() {id = 2, Name = "Ботинков", MailAddr = "botinkov@localhost", Comment = ""},
                new Recipient() {id = 3, Name = "Тапочков", MailAddr = "tapochkov@localhost", Comment = ""}
            };

            Recipient TryAddRecipient = 
                new Recipient() { id = 2, Name = "Ботинков", MailAddr = "botinkov@localhost", Comment = "" };

            RecipientsDataServicesInMemory _RecipientsDataServicesInMemory = new RecipientsDataServicesInMemory();

            var _collection = _RecipientsDataServicesInMemory.GetAll();
            // Act
            _RecipientsDataServicesInMemory.Create(TryAddRecipient);
            var actual_collection = _RecipientsDataServicesInMemory.GetAll();

            // Accert
            Assert.AreEqual(expected_collection, actual_collection);
        }


    }
}
