using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ClientLogin.SQLHelper
{
    public class SQLHelper
    {

        public static DataTable GetAllUserData()
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                using (var sqlComm = new SqlCommand("SELECT * FROM User_List"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sqlComm.Connection = con;
                        sda.SelectCommand = sqlComm;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            return dt;
                        }
                    }
                }
            }
        }

        //public static DataTable GetUserDetailsByUserName(string Username,string Password)
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
        //    {
        //        using (SqlCommand sqlComm = new SqlCommand("User_List_GetDetailsByUsernamePassword", con))
        //        {
        //            using (SqlDataAdapter sda = new SqlDataAdapter())
        //            {
        //                sqlComm.Connection = con;
        //                sqlComm.Parameters.AddWithValue("@Email", Username);
        //                sqlComm.Parameters.AddWithValue("@Password", Password);
        //                sqlComm.CommandType = CommandType.StoredProcedure;
        //                sda.SelectCommand = sqlComm;
        //                using (DataTable dt = new DataTable())
        //                {
        //                    sda.Fill(dt);

        //                    return dt;
        //                }
        //            }
        //        }

        //    }
        //}
        public static string GetUserDetailsByUserName(string Username, string Password)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            using (var sqlComm = new SqlCommand("User_List_GetDetailsByUsernamePassword", con))
            {
                sqlComm.Connection = con;
                sqlComm.Parameters.AddWithValue("@Email", Username);
                sqlComm.Parameters.AddWithValue("@Password", Password);
                sqlComm.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();
                while (reader.Read())
                {
                    return reader.GetString(0);
                }
                con.Close();
            }
            return null;
        }

        public static int User_List_Register_NewAccount(string Username, string Password)
        {
            string cmdText = "User_List_Register_NewAccount";
            SqlCommand sqlCmd = new SqlCommand(cmdText);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            AddSqlParameter(sqlCmd, "@Email", Username, SqlDbType.NVarChar, ParameterDirection.Input);
            AddSqlParameter(sqlCmd, "@PasswordHash", Password, SqlDbType.NVarChar, ParameterDirection.Input);
            AddSqlParameter(sqlCmd, "@ReturnValue", 0, SqlDbType.Int, ParameterDirection.InputOutput);

            return ExecuteInsert(sqlCmd);
        }

        public static int ExecuteInsert(SqlCommand sqlCmd)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                try
                {
                    sqlCmd.Connection = connection;
                    connection.Open();
                    sqlCmd.ExecuteScalar();
                    return (int)sqlCmd.Parameters["@ReturnValue"].Value;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static void AddSqlParameter(SqlCommand sqlCmd, string paramId, object paramValue,
            SqlDbType sqlDbType, ParameterDirection paramDirection)
        {
            if (sqlCmd == null) throw new ArgumentNullException("sqlCmd");
            if (string.IsNullOrEmpty(paramId)) throw new ArgumentOutOfRangeException("paramId");

            SqlParameter sqlParam = new SqlParameter(paramId, sqlDbType);
            sqlParam.Direction = paramDirection;

            if (sqlDbType == SqlDbType.DateTime)
            {

                sqlParam.Value = (paramValue == null || DateTime.Parse(paramValue.ToString()) == DateTime.MinValue) ? DateTime.Parse("1900-01-01").ToString() : ((DateTime)paramValue).ToString("yyyy-MM-dd HH:mm:ss");

            }
            else if (paramValue == null)
            {
                if (sqlDbType == SqlDbType.Char ||
                    sqlDbType == SqlDbType.NChar ||
                    sqlDbType == SqlDbType.NText ||
                    sqlDbType == SqlDbType.NVarChar ||
                    sqlDbType == SqlDbType.Text ||
                    sqlDbType == SqlDbType.VarChar)
                {
                    sqlParam.Value = string.Empty;
                }
                else if (sqlDbType == SqlDbType.BigInt ||
                    sqlDbType == SqlDbType.Decimal ||
                    sqlDbType == SqlDbType.Float ||
                    sqlDbType == SqlDbType.Int ||
                    sqlDbType == SqlDbType.Money ||
                    sqlDbType == SqlDbType.Real ||
                    sqlDbType == SqlDbType.SmallInt ||
                    sqlDbType == SqlDbType.SmallMoney ||
                    sqlDbType == SqlDbType.TinyInt)
                {
                    sqlParam.Value = 0;
                }
                else if (sqlDbType == SqlDbType.Image)
                {
                    sqlParam.Value = new byte[0];

                }
            }

            else
            {
                sqlParam.Value = paramValue;
            }
            sqlCmd.Parameters.Add(sqlParam);
        }
    }
}
    