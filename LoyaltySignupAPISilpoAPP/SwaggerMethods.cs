using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltySignupAPISilpoAPP
{
    public class SwaggerMethods
    {
        // Проверка телефона (статус верификации в системе)
        public static string VerifyPhone(string phoneNumber)
        {

            string relativeUrl = "/api/Silpo/VerifyPhone";

            var v = new

            {
                phoneNumber
            };

            return SwaggerHelper.CommitPostRequest(SwaggerHelper.serviceUrl + relativeUrl, JsonConvert.SerializeObject(v));
        }
        
        // Генерация кода доступа для проверки телефона
        public static string GenerateAccessCode(string phoneNumber)
        {

            string relativeUrl = "/api/Silpo/GenerateAccessCode";

            var v = new

            {
                phoneNumber
            };

            return SwaggerHelper.CommitPostRequest(SwaggerHelper.serviceUrl + relativeUrl, JsonConvert.SerializeObject(v));
        }

        // Проверка кода доступа (для подтверждения телефона)
        //Нет возможности написать тесты из-за того что код доступа приходит на телефон.
        //В базе код доступа захэширован
        public static string CheckAccessCode(int accessCodeId, int accessCode)
        {

            string relativeUrl = "/api/Silpo/CheckAccessCode";

            var v = new

            {
                accessCodeId,
                accessCode
            };

            return SwaggerHelper.CommitPostRequest(SwaggerHelper.serviceUrl + relativeUrl, JsonConvert.SerializeObject(v));
        }

        // Проверка e-mail (статус верификации в системе)
        public static string VerifyEmail(string email)
        {

            string relativeUrl = "/api/Silpo/VerifyEmail";

            var v = new

            {
                email
            };

            return SwaggerHelper.CommitPostRequest(SwaggerHelper.serviceUrl + relativeUrl, JsonConvert.SerializeObject(v));
        }


        // Генерация ссылки для подтверждения e-mail
        public static string GenerateEmailAccessHash(string email, string accessUrl)
        {

            string relativeUrl = "/api/Silpo/GenerateEmailAccessHash";

            var v = new

            {
                email,
                accessUrl,
            };

            return SwaggerHelper.CommitPostRequest(SwaggerHelper.serviceUrl + relativeUrl, JsonConvert.SerializeObject(v));
        }

        // Проверка перехода по ссылке (для подтверждения e-mail)
        public static string VerifiedEmailAccessHash(string hashStr)
        {

            string relativeUrl = "/api/Silpo/VerifiedEmailAccessHash";

            var v = new

            {
                hashStr,                
            };

            return SwaggerHelper.CommitPostRequest(SwaggerHelper.serviceUrl + relativeUrl, JsonConvert.SerializeObject(v));
        }

        //Авторизация по номеру телефона
        public static string LoginPhoneNumber(string phoneNumber, string password)
        {

            string relativeUrl = "/api/Silpo/LoginPhoneNumber";

            var v = new

            {
                phoneNumber,
                password
            };

            return SwaggerHelper.CommitPostRequest(SwaggerHelper.serviceUrl + relativeUrl, JsonConvert.SerializeObject(v));
        }
    }
}
