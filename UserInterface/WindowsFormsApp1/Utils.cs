using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager
{
    class ConnectionUtils
    {
        private static readonly string conn = ConfigurationManager.ConnectionStrings["SuperMercadoConnection"].ToString();
        private static SqlConnection cn = new SqlConnection(conn);

        public static SqlConnection GetConnection()
        {
            verifySGBDConnection();
            return cn;
        }

        public static bool verifySGBDConnection()
        {
            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        public static void Release()
        {
            cn.Close();
        }
    }
    public class TransactionManager
    {
        private  SqlTransaction transaction;
        private  SqlCommand cmd;

        public TransactionManager() { }

        public SqlCommand GetCmd() {

            this.cmd = new SqlCommand();
            this.cmd.Connection = ConnectionUtils.GetConnection();
            this.cmd.CommandType = CommandType.Text;
            this.cmd.Transaction = this.transaction;
            return this.cmd;
        }

        public void BeginTransaction()
        {
            if (!ConnectionUtils.verifySGBDConnection())
                throw new Exception("Ligação a Base de dados falhou!");

            if (this.cmd == null)
                GetCmd();

            this.transaction = cmd.Connection.BeginTransaction();
            this.cmd.Transaction = transaction;
        }

        public void Commit()
        {
            this.transaction.Commit();
        }

        public void Rollback()
        {
            this.transaction.Rollback();
            this.transaction = null;
            this.cmd = null;
        }

        public void Release()
        {
            this.transaction = null;
            this.cmd = null;
            ConnectionUtils.Release();
        }

    }

    class Cipher
    {
        public static string byteToHexStr(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X2"));
                }
            }
            return sb.ToString();
        }
    }
}
