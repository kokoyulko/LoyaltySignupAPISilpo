using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using LoyaltySignupAPISilpoAPP;
using System.Threading;

namespace LoyaltySignupAPISilpoAPPTest
{
    [TestClass]
    public class VerifyPhoneTests
    {
        [TestMethod]
        public void VerifyEmptyPhoneNumber() //Передаем пустое значение в поле телефона
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberEmpty;
            string error = "";


            //expected
            string expected_error = InitialData.expectedError400;

            //Act
            try
            {
                dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyPhone(phoneNumber));
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            //Assert
            Assert.AreEqual(expected_error, error);
        }

        [TestMethod]
        public void VerifyIncorrectPhoneNumber() //Передаем корявое значение в поле телефона
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberIncorrect;
            string error = "";


            //expected
            string expected_error = InitialData.expectedError400;

            //Act
            try
            {
                dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyPhone(phoneNumber));
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            //Assert
            Assert.AreEqual(expected_error, error);
        }

        [TestMethod]
        public void VerifyCityPhoneNumber() //Передаем городской код в номере
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberCity;

            //expected
            Int32 expected_resultCode = -1;
            string expected_resultType = "RC_DBMS";
            string expected_resultStr = "Помилка сервісу Помилка коду мобільного оператора";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyPhone(phoneNumber));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void VerifyNotFoundPhoneNumber() //Передаем номер телефона, которого нет в анкетах пользователей
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberNotFound;

            //expected
            Int32 expected_resultCode = 40;
            string expected_resultType = "RC_MOBILEPHONE_NOT_FOUND";
            string expected_resultStr = "Немає мобільного номера телефону";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyPhone(phoneNumber));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void VerifyVerifiedPhoneNumber() //Передаем номер телефона, который в статусе Верифицирован
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberVerified;

            //expected
            Int32 expected_resultCode = 70;
            string expected_resultType = "RC_VERIFIED";
            string expected_resultStr = "Телефон верифицирован";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyPhone(phoneNumber));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void VerifyNotVerifiedPhoneNumber() //Передаем номер телефона, который в статусе Не проходил верификацию
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberNotVerified;

            //expected
            Int32 expected_resultCode = 71;
            string expected_resultType = "RC_NOT_VERIFIED";
            string expected_resultStr = "Телефон не верифицирован";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyPhone(phoneNumber));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void VerifyUnVerifiedPhoneNumber() //Передаем номер телефона, который в статусе Неверифицирован
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberUnVerified;

            //expected
            Int32 expected_resultCode = 71;
            string expected_resultType = "RC_NOT_VERIFIED";
            string expected_resultStr = "Телефон не верифицирован";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyPhone(phoneNumber));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void VerifyPhoneNumberFora() //Передаем номер телефона в статусе Верифицирован из анкеты пользователя Форы (в Сильпо нет)
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberFora;

            //expected
            Int32 expected_resultCode = 40;
            string expected_resultType = "RC_MOBILEPHONE_NOT_FOUND";
            string expected_resultStr = "Немає мобільного номера телефону";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyPhone(phoneNumber));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }


    }
}
