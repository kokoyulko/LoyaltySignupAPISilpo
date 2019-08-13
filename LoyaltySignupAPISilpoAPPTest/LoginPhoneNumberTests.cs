using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using LoyaltySignupAPISilpoAPP;
using System.Threading;

namespace LoyaltySignupAPISilpoAPPTest
{
    [TestClass]
    public class LoginPhoneNumberTests
    {
        [TestMethod]
        public void LoginPhoneNumberEmpty() //Передаем пустые значения в полях
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberEmpty;
            string password = InitialData.passwordEmpty;
            string error = "";

            //expected
            string expected_error = InitialData.expectedError400;

            //Act
            try
            {
                dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.LoginPhoneNumber(phoneNumber, password));
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            //Assert
            Assert.AreEqual(expected_error, error);
        }

        [TestMethod]
        public void LoginPhoneNumberIncorrect() //Передаем корявое значение в поле телефона
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberIncorrect;
            string password = InitialData.passwordCorrect;
            string error = "";

            //expected
            string expected_error = InitialData.expectedError400;

            //Act
            try
            {
                dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.LoginPhoneNumber(phoneNumber, password));
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            //Assert
            Assert.AreEqual(expected_error, error);
        }

        [TestMethod]
        public void LoginPhoneNumberCityPhoneNumber() //Передаем городской код в номере
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberCity;
            string password = InitialData.passwordCorrect;

            //expected
            Int32 expected_resultCode = 71;
            string expected_resultType = "RC_NOT_VERIFIED";
            string expected_resultStr = "Телефон не верифицирован";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.LoginPhoneNumber(phoneNumber, password));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void LoginPhoneNumberNotVerifiedPhoneNumber() //Передаем номер телефона, который в статусе Не проходил верификацию
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberNotVerified;
            string password = InitialData.passwordCorrect;

            //expected
            Int32 expected_resultCode = 71;
            string expected_resultType = "RC_NOT_VERIFIED";
            string expected_resultStr = "Телефон не верифицирован";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.LoginPhoneNumber(phoneNumber, password));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void LoginPhoneNumberUnVerifiedPhoneNumber() //Передаем номер телефона, который в статусе Неверифицирован
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberUnVerified;
            string password = InitialData.passwordCorrect;

            //expected
            Int32 expected_resultCode = 71;
            string expected_resultType = "RC_NOT_VERIFIED";
            string expected_resultStr = "Телефон не верифицирован";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.LoginPhoneNumber(phoneNumber, password));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void LoginPhoneNumberPasswordMatch() //Передаем правильный пароль
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberCorrect;
            string password = InitialData.passwordCorrect;

            //expected
            Int32 expected_resultCode = 0;
            string expected_resultType = "RC_OK";
            string expected_resultStr = "ОК";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.LoginPhoneNumber(phoneNumber, password));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }            
        

        [TestMethod]
        public void LoginPhoneNumberPasswordWrong() //Передаем неправильный пароль
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberCorrect;
            string password = InitialData.passwordWrong;

            //expected
            Int32 expected_resultCode = 14;
            string expected_resultType = "RC_PASSWORD_NOT_MATCH";
            string expected_resultStr = "Невірний пароль";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.LoginPhoneNumber(phoneNumber, password));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void LoginPhoneNumberPasswordWrongLoginBlocked() //Передаем неправильный пароль 3 раза и блокируем на ПС на 10 минут
        {
            //arrange
            string phoneNumber = InitialData.phoneNumberCorrect;
            string password = InitialData.passwordWrong;
            DateTime date1 = DateTime.Now.AddMinutes(10);            
                        
            //expected
            Int32 expected_resultCode = 45;
            string expected_resultType = "RC_LOGIN_BLOCKED";
            string expected_resultStr = "Ваша персональна сторінка заблокована до " + date1.ToShortTimeString() + ". Якщо Ви забули пароль, будь ласка, скористайтесь функцією «забув пароль», або зателефонуйте на Гарячу Лінію – <br/>0 800 301 707";

            //Act
            dynamic result = null;
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(3000);
                result = JsonConvert.DeserializeObject(SwaggerMethods.LoginPhoneNumber(phoneNumber, password));
            }

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }
    }
    
}
