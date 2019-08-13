using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using LoyaltySignupAPISilpoAPP;
using System.Threading;

namespace LoyaltySignupAPISilpoAPPTest
{
    [TestClass]
    public class GenerateEmailAccessHashTests
    {
       [TestMethod]
            public void GenerateAccessHashEmpty() //Передаем пустые значения в полях
            {
                //arrange
                string email = InitialData.emailEmpty;
                string accessUrl = InitialData.accessUrlEmpty;
                string error = "";


                //expected
                string expected_error = InitialData.expectedError400;

                //Act
                try
                {
                    dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateEmailAccessHash(email, accessUrl));
                }
                catch (Exception e)
                {
                    error = e.Message;
                }

                //Assert
                Assert.AreEqual(expected_error, error);
            }

        [TestMethod]
        public void GenerateAccessHashIncorrect() //Передаем корявые значения в полях
        {
            //arrange
            string email = InitialData.emailIncorrect;
            string accessUrl = InitialData.accessUrlIncorrect;
            string error = "";


            //expected
            string expected_error = InitialData.expectedError400;

            //Act
            try
            {
                dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateEmailAccessHash(email, accessUrl));
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            //Assert
            Assert.AreEqual(expected_error, error);
        }

        [TestMethod]
        public void GenerateAccessHashEmailBanned() //Передаем email заблокированного на территории Украины почтового сервера
        {
            //arrange
            string email = InitialData.emailBanned;
            string accessUrl = InitialData.accessUrlCorrect;

            //expected
            Int32 expected_resultCode = 81;
            string expected_resultType = "RC_EMAIL_ERROR";
            string expected_resultStr = "Некоректний email";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateEmailAccessHash(email, accessUrl));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void GenerateAccessHashUrlFora() //Передаем урл Форы
        {
            //arrange
            string email = InitialData.emailNotVerified;
            string accessUrl = InitialData.accessUrlFora;

            //expected
            Int32 expected_resultCode = 87;
            string expected_resultType = "RC_URL_WRONG";
            string expected_resultStr = "Помилка в url";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateEmailAccessHash(email, accessUrl));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void GenerateAccessHashCorrect_OK() // Передаем корректный email и урл для генерации ссылки
        {
            //arrange
            String email = InitialData.emailNotVerified;
            string accessUrl = InitialData.accessUrlCorrect;


            //expected
            Int32 expected_resultCode = 0;
            string expected_resultType = "RC_OK";
            string expected_resultStr = "Лист відправлено на t.s******a@fozzy.ua";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateEmailAccessHash(email, accessUrl));
            
            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void GenerateAccessHashCorrect_Early() //Передаем корректный email и урл для генерации ссылки, когда ещё не прошло минуты
                                                       //и генерация новой ссылки запрещена
        {
            //arrange
            String email = InitialData.emailNotVerified;
            string accessUrl = InitialData.accessUrlCorrect;


            //expected
            Int32 expected_resultCode = 77;
            string expected_resultType = "RC_AC_OFTENTIMES_SENDING";
            string expected_resultStr = "Наступна відправка тимчасово заборонена";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateEmailAccessHash(email, accessUrl));


            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void GenerateGenerateAccessHashBlocked() // Превышено количество запросов на генерацию ссылки // На тесте установлено 5 попыток
        {
            //arrange
            String email = InitialData.emailBlocked;
            string accessUrl = InitialData.accessUrlCorrect;

            //expected
            Int32 expected_resultCode = 64;
            string expected_resultType = "RC_OFTENTIMES_SENDING";
            string expected_resultStr = "Перевищено кількість спроб відправки";

            //Act
            dynamic result = null;
            for (int i = 0; i < 6; i++)
            {
                Thread.Sleep(190000);
                result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateEmailAccessHash(email, accessUrl));
            }

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }
    }
}
