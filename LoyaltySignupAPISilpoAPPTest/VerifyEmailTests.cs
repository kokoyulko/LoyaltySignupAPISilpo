using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using LoyaltySignupAPISilpoAPP;
using System.Threading;

namespace LoyaltySignupAPISilpoAPPTest
{
    [TestClass]
    public class VerifyEmailTests
    {
        [TestMethod]
        public void VerifyEmptyEmail() //Передаем пустое значение в поле email
        {
            //arrange
            string email = InitialData.emailEmpty;
            string error = "";


            //expected
            string expected_error = InitialData.expectedError400;

            //Act
            try
            {
                dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyEmail(email));
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            //Assert
            Assert.AreEqual(expected_error, error);
        }

        [TestMethod]
        public void VerifyIncorrectemail() //Передаем корявое значение в поле email
        {
            //arrange
            string email = InitialData.emailIncorrect;
            string error = "";


            //expected
            string expected_error = InitialData.expectedError400;

            //Act
            try
            {
                dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyEmail(email));
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            //Assert
            Assert.AreEqual(expected_error, error);
        }

        [TestMethod]
        public void VerifyNotFoundEmail() //Передаем email, которого нет в анкетах пользователей
        {
            //arrange
            string email = InitialData.emailNotFound;

            //expected
            Int32 expected_resultCode = 78;
            string expected_resultType = "RC_EMAIL_NOT_FOUND";
            string expected_resultStr = "Email не знайдено";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyEmail(email));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void VerifyVerifiedEmail() //Передаем email, который в статусе Верифицирован
        {
            //arrange
            string email = InitialData.emailVerified;

            //expected
            Int32 expected_resultCode = 79;
            string expected_resultType = "RC_EMAIL_VERIFIED";
            string expected_resultStr = "Email верифицирован";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyEmail(email));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void VerifyNotVerifiedEmail() //Передаем email, который в статусе Не проходил верификацию
        {
            //arrange
            string email = InitialData.emailNotVerified;

            //expected
            Int32 expected_resultCode = 80;
            string expected_resultType = "RC_EMAIL_NOT_VERIFIED";
            string expected_resultStr = "Email не верифицирован";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyEmail(email));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }

        [TestMethod]
        public void VerifyUnVerifiedEmail() //Передаем email, который в статусе Не верифицирован
        {
            //arrange
            string email = InitialData.emailUnVerified;

            //expected
            Int32 expected_resultCode = 80;
            string expected_resultType = "RC_EMAIL_NOT_VERIFIED";
            string expected_resultStr = "Email не верифицирован";

            //Act
            dynamic result = JsonConvert.DeserializeObject(SwaggerMethods.VerifyEmail(email));

            //Assert
            Assert.AreEqual(expected_resultCode, (Int32)result.resultCode);
            Assert.AreEqual(expected_resultType, (string)result.resultType);
            Assert.AreEqual(expected_resultStr, (string)result.resultStr);
        }
    }
}
