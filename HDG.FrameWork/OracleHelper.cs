using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDG.FrameWork
{
    public class OracleHelper
    {
        public static string connectionString = ConfigurationManager.AppSettings["ConnBasic"];
        public static string err = string.Empty;

        public static int ExecuteNonQuery(string sql)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result;
                    }
                    catch (OracleException ex)
                    {
                        err = ex.Message;
                        return -1;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public static int ExecuteQuery(string sql)
        {
            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataReader reader = null;
                try
                {
                    conn.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return int.Parse(reader[0].ToString());
                    }
                    return 0;
                }
                catch (OracleException ex)
                {
                    err = ex.Message;
                    return -1;
                }
                finally
                {
                    reader.Close();
                    conn.Close();
                }
            }
            catch (OracleException ex)
            {
                err = ex.Message;
                return -1;
            }
        }
    }
}
