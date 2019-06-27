using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailSender.Data.Encryptor;
using System.Text.RegularExpressions;

namespace MailSender.Tests.Data.Encryptor
{
    /// <summary>
    /// Класс модульных тестов для StringEncryptor
    /// </summary>
    [TestClass]
    public class StringEncryptorTests
    {
        public StringEncryptorTests()
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
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testcontext)
        {

        }
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        [ClassInitialize()]
        public static void myclassinitialize(TestContext testcontext)
        {

        }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        [ClassCleanup()]
        public static void MyClassCleanup()
        {

        }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        [TestInitialize()]
        public void MyTestInitialize()
        {

        }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        [TestCleanup()]
        public void MyTestCleanup()
        {

        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {

        }
        //
        #endregion

        [TestMethod]
        public void EncryptMethod_On_ASD_Return_BTE_WithKey_1()
        {
            //AAA
            // Arrange (организовать)
            const string str = "ASD";
            const string expected_result = "BTE";
            const int key = 1;

            // Act (действие)
            var actual_result = StringEncryptor.Encrypt(str, key);

            //Assert (утверждение, проверка)
            Assert.AreEqual(expected_result, actual_result);
        }

        [TestMethod]
        public void DecryptMethod_On_BTE_Return_ASD_WithKey_1()
        {
            //AAA
            // Arrange (организовать)
            const string str = "BTE";
            const string expected_result = "ASD";
            const int key = 1;

            // Act (действие)
            var actual_result = StringEncryptor.Decrypt(str, key);

            //Assert (утверждение, проверка)
            Assert.AreEqual(expected_result, actual_result);
            //Проверка строки на соответствие регулярному выражению
            //StringAssert.Matches("проверяемая строка", new Regex("регулярное выражение для проверки строки"));
            //Сравнение коллекций
            //CollectionAssert.
            //Ручное формирование проверки
            //if (expected_result != actual_result)
            //    throw new AssertFailedException("Ошибка в методе кодирования");
        }
    }
}
