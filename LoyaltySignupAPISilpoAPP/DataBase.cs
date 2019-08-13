using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LoyaltySignupAPISilpoAPP
{
    public class DataBase
    {
        public static string connectionString_X61PP = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=True", "S-KV-CENTER-X61", "PersonalPagesNew");

        // Получение хэша по ссылке для подтверждения имейла
        public static string GetHashStr(string email)
        {
            string hashStr = "";
            SqlConnection con = new SqlConnection(connectionString_X61PP);
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select top 1 convert(varchar(max),accessHash,2) as hashStr from [PersonalPagesNew].[security].[AccessEmail] where email ='" +email+"' and accessUrl like 'https://silpo%' order by created desc";

            cmd.ExecuteNonQuery();

            SqlDataReader thisReader = cmd.ExecuteReader();
            while (thisReader.Read())
            {
                hashStr = thisReader["hashStr"].ToString();
            }

            thisReader.Close();

            con.Close();

            return hashStr;

        }
    }
}
