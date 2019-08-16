using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using LoyaltySignupAPISilpoAPP;
using System.Threading;

namespace LoyaltySignupAPISilpoAPPTest
{
    [TestClass]
    public class GenerateAccessCodeTests
    {
        [TestMethod]
        public void GenerateEmptyPhoneNumber() //Передаем пустое значение в поле телефона
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberEmpty;


            string error = "";


            //expected
            string expected_error = InitialData.expectedError400;

            //Act
            try
            {
                dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateAccessCode(phoneNumber));
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            //Assert
            Assert.AreEqual(expected_error, error);
        }

        [TestMethod]
        public void GenerateIncorrectPhoneNumber() //Передаем корявое значение в поле телефона
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberIncorrect;
            string error = "";


            //expected
            string expected_error = InitialData.expectedError400;

            //Act
            try
            {
                dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateAccessCode(phoneNumber));
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            //Assert
            Assert.AreEqual(expected_error, error);
        }

        [TestMethod]
        public void GenerateCityPhoneNumber() //Передаем городской код в номере
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberCity;

            //expected
            Int32 expected_resultCode = 35;
            string expected_resultType = "RC_MOBILEPHONECODE_ERROR";
            string expected_resultStr = "Помилка коду мобільного оператора";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateAccessCode(phoneNumber));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void GeneratePhoneNumberCorrect_OK() // Передаем корректный номер телефона для генерации кода
        {
            //arrange
            String phoneNumber = InitialData.phoneNumberCorrect;


            //expected
            Int32 expected_resultCode = 0;
            string expected_resultType = "RC_OK";
            string expected_resultStr = "Тимчасовий пароль відправлено.";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateAccessCode(phoneNumber));


            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void GeneratePhoneNumberCorrect_Early() //Передаем корректный номер телефона для генерации кода, когда ещё не прошло минуты
                                                       //и генерация нового кода запрещена
        {
            //arrange
            String phoneNumber = InitialData.phoneNumberCorrect;


            //expected
            Int32 expected_resultCode = 77;
            string expected_resultType = "RC_AC_OFTENTIMES_SENDING";
            string expected_resultStr = "Наступна відправка тимчасово заборонена";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateAccessCode(phoneNumber));


            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void GeneratePhoneNumberBlocked() // Превышено количество запросов на генерацию кода // На тесте установлено 5 попыток
        {
            //arrange
            String phoneNumber = InitialData.phoneNumberBlocked;

            //expected
            Int32 expected_resultCode = 64;
            string expected_resultType = "RC_OFTENTIMES_SENDING";
            string expected_resultStr = "Перевищено кількість спроб відправки";

            //Act
            dynamic result = null;
            for (int i = 0; i < 6; i++)
            {
                Thread.Sleep(180000);
                result = JsonConvert.DeserializeObject(SwaggerMethods.GenerateAccessCode(phoneNumber));

            }

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }
    }
}
