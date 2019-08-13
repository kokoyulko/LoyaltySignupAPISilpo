using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltySignupAPISilpoAPP
{
    class SwaggerHelper
    {
        public static string serviceUrl = "https://s-kv-center-q09:8181"; //LoyaltySignupAPIAPP

        public static String CommitPostRequest(String url, string postData)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.UseDefaultCredentials = true;
            request.Method = "Post";

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            request.ContentType = "application/json";
            StreamWriter dataStream = new StreamWriter(request.GetRequestStream());
            dataStream.Write(postData);
            dataStream.Close();

            String resp = "";

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    resp = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {

                throw e;

            }
            return resp;
        }


    }
}


