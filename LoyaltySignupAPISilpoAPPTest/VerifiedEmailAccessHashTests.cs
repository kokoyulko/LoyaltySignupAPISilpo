using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using LoyaltySignupAPISilpoAPP;
using System.Threading;

namespace LoyaltySignupAPISilpoAPPTest
{
    [TestClass]
    public class VerifiedEmailAccessHashTests
    {
        [TestMethod]
        public void VerifiedEmailAccessHashEmpty() //Передаем пустые значения в полях
        {
            //arrange
            string hashStr = InitialData.hashStrEmpty;
            string error = "";


            //expected
            string expected_error = InitialData.expectedError400;

            //Act
            try
            {
                dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifiedEmailAccessHash(hashStr));
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            //Assert
            Assert.AreEqual(expected_error, error);
        }

        [TestMethod]
        public void VerifiedEmailAccessHashIncorrect() //Передаем корявые значения в полях
        {
            //arrange
            string hashStr = InitialData.hashStrIncorrect;
            string error = "";


            //expected
            string expected_error = InitialData.expectedError400;

            //Act
            try
            {
                dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifiedEmailAccessHash(hashStr));
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            //Assert
            Assert.AreEqual(expected_error, error);
        }

        [TestMethod]
        public void VerifiedEmailAccessHashFora() //Передаем хэш сгенерированной ссылки для Форы
        {
            //arrange
            string hashStr = InitialData.hashStrFora;
            
            //expected
            Int32 expected_resultCode = 78;
            string expected_resultType = "RC_EMAIL_NOT_FOUND";
            string expected_resultStr = "Email не знайдено";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifiedEmailAccessHash(hashStr));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void VerifiedEmailAccessHashCorrect() // Передаем правильную ссылку
        {
            //arrange
                 
            String email = InitialData.emailCorrect;
            string accessUrl = InitialData.accessUrlCorrect;

            //Сначала генерируем ссылку для подтверждения
            //expected
            Int32 expected_resultCodeGenerate = 0;
            string expected_resultTypeGenerate = "RC_OK";
            string expected_resultStrGenerate = "Лист відправлено на koy***o@ukr.net";

            //Act
            dynamic resultGenerate = JsonConvert.DeserializeObject(SwaggerMethods.GenerateEmailAccessHash(email, accessUrl));

            //Assert
            Assert.AreEqual(expected_resultCodeGenerate, (Int32)resultGenerate.resultCode);
            Assert.AreEqual(expected_resultTypeGenerate, (string)resultGenerate.resultType);
            Assert.AreEqual(expected_resultStrGenerate, (string)resultGenerate.resultStr);

            //Затем проверяем переход по ссылке, берем из базы хэш по номеру, по которому прошла генерация ссылки
            //expected

            String hashStr = DataBase.GetHashStr(InitialData.emailCorrect);
            Console.WriteLine("HASH:" + hashStr);

            Int32 expected_resultCodeVerify = 0;
            string expected_resultTypeVerify = "RC_OK";
            string expected_resultStrVerify = "OK";

            //Act
            dynamic resultVerify = JsonConvert.DeserializeObject(SwaggerMethods.VerifiedEmailAccessHash(hashStr));

            //Assert
            Assert.AreEqual(expected_resultCodeVerify, (Int32)resultVerify.resultCode);
            Assert.AreEqual(expected_resultTypeVerify, (string)resultVerify.resultType);
            Assert.AreEqual(expected_resultStrVerify, (string)resultVerify.resultStr);

        }

        [TestMethod]
        public void VerifiedEmailAccessHashExpired() // Ссылка просрочилась, на тесте срок жизни ссылки 3 минуты
        {
            //arrange
            String hashStr = DataBase.GetHashStr(InitialData.emailCorrect);          
            String email = InitialData.emailCorrect;
            string accessUrl = InitialData.accessUrlCorrect;


            //Сначала генерируем ссылку для подтверждения
            //expected
            Int32 expected_resultCodeGenerate = 0;
            string expected_resultTypeGenerate = "RC_OK";
            string expected_resultStrGenerate = "Лист відправлено на koy***o@ukr.net";

            //Act
            dynamic resultGenerate = JsonConvert.DeserializeObject(SwaggerMethods.GenerateEmailAccessHash(email, accessUrl));

            //Assert
            Assert.AreEqual(expected_resultCodeGenerate, (Int32)resultGenerate.resultCode);
            Assert.AreEqual(expected_resultTypeGenerate, (string)resultGenerate.resultType);
            Assert.AreEqual(expected_resultStrGenerate, (string)resultGenerate.resultStr);

            ////Задаем время, чтоб ссылка просрочилась и вытаскиваем хэш из базы по имейлу, по которому сгенерировалась ссылка
            Thread.Sleep(180000);

            //expected
            Int32 expected_resultCodeVerify = 82;
            string expected_resultTypeVerify = "RC_HASH_EXPIRED";
            string expected_resultStrVerify = "Термін дії посилання закінчився. Виконайте запит на отримання нового посилання.";

            //Act
            dynamic resultVerify = JsonConvert.DeserializeObject(SwaggerMethods.VerifiedEmailAccessHash(hashStr));

            //Assert
            Assert.AreEqual(expected_resultCodeVerify, (Int32)resultVerify.resultCode);
            Assert.AreEqual(expected_resultTypeVerify, (string)resultVerify.resultType);
            Assert.AreEqual(expected_resultStrVerify, (string)resultVerify.resultStr);

        }
    }
}
