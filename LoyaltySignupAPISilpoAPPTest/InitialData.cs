using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltySignupAPISilpoAPPTest
{
    class InitialData
    {
        //Примеры телефонов
        public static string phoneNumberEmpty = "";
        public static string phoneNumberIncorrect = "yulko";
        public static string phoneNumberCity = "+380445081788";
        public static string phoneNumberNotFound = "+380637772300";
        public static string phoneNumberVerified = "+380933336699";
        public static string phoneNumberNotVerified = "+380508529696";
        public static string phoneNumberUnVerified = "+380985081788";
        public static string phoneNumberFora = "+380509653668";
        public static string phoneNumberCorrect = "+380999463352";
        public static string phoneNumberBlocked = "+380673339185";


        //Примеры почты
        public static string emailEmpty = "";
        public static string emailIncorrect = "yulko";
        public static string emailNotFound = "takor@adamant.net";
        public static string emailVerified = "a.berkuta@fozzy.ua";
        public static string emailNotVerified = "t.shumkova@fozzy.ua";
        public static string emailUnVerified = "g.felinska@temabit.com";
        public static string emailBanned = "koyulko@mail.ru";
        public static string emailBlocked = "koyulko@gmail.com";
        public static string emailCorrect = "koyulko@ukr.net";

        //Урлы для ссылок
        public static string accessUrlEmpty = "";
        public static string accessUrlCorrect = "https://silpo.ua";
        public static string accessUrlFora = "https://fora.ua";
        public static string accessUrlIncorrect = "youtube.com";

        //Хэши ссылок для подтверждения
        public static string hashStrEmpty = "";
        public static string hashStrIncorrect = "123456789";
        public static string hashStrFora = "A3B3E99146D3D5D71DE766EBA9143542";

        //Пароли
        public static string passwordEmpty = "";
        public static string passwordWrong = "123";
        public static string passwordCorrect = "6U1y3E";
        

        // Ошибки
        public static string expectedError400 = "Удаленный сервер возвратил ошибку: (400) Недопустимый запрос.";

    }
}
