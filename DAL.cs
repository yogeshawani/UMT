using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.IO;
namespace UMT
{
    class DAL
    {
        public static DataTable GetAllDataParameterized(string Conn, string ProcName, SqlParameter[] p)
        {

            DataTable dt = new DataTable();
            try
            {
                SqlConnection cn = new SqlConnection(Conn);
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Open();
                SqlCommand cmd = new SqlCommand(ProcName, cn);

                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in p)
                {
                    cmd.Parameters.Add(param);
                }

                SqlDataReader dr = cmd.ExecuteReader();

                dt.Load(dr);
            }
            catch (Exception ex)
            {
                dt.Columns.Add(new DataColumn("Error", typeof(string)));
                DataRow dr = dt.NewRow();

                dr["Error"] = "Error :" + ex.Message;
                dt.Rows.Add(dr);
            }
            return dt;
        }


        public static string GetMaxCodeDal(string Conn, string TblName, string Field)
        {
            string Code = "";
            SqlConnection cn = new SqlConnection(Conn);

            string qry = "select Isnull(max(cast(RIGHT(" + Field + ",4) as numeric(10))),0) + 1 " + Field + " from " + TblName;
            SqlCommand cmd = new SqlCommand(qry, cn);

            if (cn.State == ConnectionState.Open)
                cn.Close();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            Code = Field.Substring(0, 2).ToUpper() + String.Format("{0:0000}", int.Parse(dr[0].ToString()));

            cn.Close();
            return Code;
        }

        public static DataTable GetAllData(string Conn, string ProcName)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cn = new SqlConnection(Conn);
                SqlDataAdapter da = new SqlDataAdapter(ProcName, Conn);

                da.Fill(dt);
            }
            catch (Exception ex)
            {
                dt.Columns.Add(new DataColumn("Error", typeof(string)));
                DataRow dr = dt.NewRow();

                dr["Error"] = "Error :" + ex.Message;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataTable GetAllDataByQuery(string Conn,string strQry)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cn = new SqlConnection(Conn);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQry;
                cmd.Connection = cn;
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                dt.Columns.Add(new DataColumn("Error", typeof(string)));
                DataRow dr = dt.NewRow();
                dr["Error"] = "Error :" + ex.Message;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static string InsUpdDel(string Conn, string ProcName, SqlParameter[] p)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Conn);
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Open();
                SqlCommand cmd = new SqlCommand(ProcName, cn);

                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in p)
                {
                    cmd.Parameters.Add(param);
                }
                cmd.ExecuteNonQuery();
                cn.Close();
                return "1";
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message.ToString();
            }
        }

        public static DataTable GetColumnNames(string Conn, string ProcName, SqlParameter[] p)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cn = new SqlConnection(Conn);
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Open();
                SqlCommand cmd = new SqlCommand(ProcName, cn);

                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in p)
                {
                    cmd.Parameters.Add(param);
                }
                SqlDataReader dr = cmd.ExecuteReader();

                dt.Load(dr);
            }
            catch (Exception ex)
            {
                dt.Columns.Add(new DataColumn("Error", typeof(string)));
                DataRow dr = dt.NewRow();


                dr["Error"] = "Error :" + ex.Message;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataTable GetReferenceTables(string Conn, string ProcName, SqlParameter[] p)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cn = new SqlConnection(Conn);
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Open();
                SqlCommand cmd = new SqlCommand(ProcName, cn);

                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in p)
                {
                    cmd.Parameters.Add(param);
                }
                SqlDataReader dr = cmd.ExecuteReader();

                dt.Load(dr);
            }
            catch (Exception ex)
            {
                dt.Columns.Add(new DataColumn("Error", typeof(string)));
                DataRow dr = dt.NewRow();
                dr["Error"] = "Error :" + ex.Message;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static int CheckInwardStatus(String Conn, String ProcName, SqlParameter[] p)
        {
            try
            {
                SqlConnection cn = new SqlConnection(Conn);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = ProcName;
                foreach (SqlParameter param in p)
                {
                    cmd.Parameters.Add(param);
                }

                cn.Open();
                int val = int.Parse(cmd.ExecuteScalar().ToString());
                cn.Close();

                return val;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static DataSet GetAllDataParameterizedDATASET(string Conn, string ProcName, SqlParameter[] p)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlConnection cn = new SqlConnection(Conn);
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Open();
                SqlCommand cmd = new SqlCommand(ProcName, cn);

                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in p)
                {
                    cmd.Parameters.Add(param);
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd); //= cmd.ExecuteReader();
                adp.Fill(ds);

            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public static string BulkCopyPMDetails(DataTable dt,string strConn,string strTableName)
        {
            try
            {
                using (SqlConnection Conn = new SqlConnection(strConn))
                {
                    Conn.Open();
                    SqlBulkCopy SqlBCopy = new SqlBulkCopy(strConn);
                    SqlBCopy.DestinationTableName = strTableName;                    
                    SqlBCopy.WriteToServer(dt);
                    Conn.Close();
                    return "1";
                }
            }
            catch (Exception Ex)
            {
                return "0";
            }
        }
        public static DataSet GetAllDataDATASET(string Conn, string ProcName)
        {
            DataSet ds = new DataSet();
            try
            {

                SqlConnection cn = new SqlConnection(Conn);

                SqlDataAdapter da = new SqlDataAdapter(ProcName, Conn);

                da.Fill(ds);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public static DataTable BindDropdown(SqlParameter[] p, string ProcName, string Conn)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection cn = new SqlConnection(Conn);
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Open();
                SqlCommand cmd = new SqlCommand(ProcName, cn);

                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in p)
                {
                    cmd.Parameters.Add(param);
                }
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }
        }
        //public static string sendSMS(string to, string OTP)
        //{
        //    string message = "Dear User your OTP for mobile validation is " + OTP + " by InternetID";
        //    string SAPI = "http://alerts.sinfini.com/api/web2sms.php?workingkey=5pskr4wx1dd661rwwy9341yv3w3ez20&to=" + to + "&sender=BRYANI&message=" + message + "";
        //    try
        //    {
        //        WebRequest req = WebRequest.Create(SAPI);
        //        result = req.GetResponse();
        //        Stream ReceiveStream = result.GetResponseStream();
        //        Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
        //        StreamReader sr = new StreamReader(ReceiveStream, encode);
        //        Char[] read = new Char[256];
        //        int count = sr.Read(read, 0, read.Length);
        //        while (count > 0)
        //        {
        //            String str = new String(read, 0, count);
        //            output += str;
        //            count = sr.Read(read, 0, read.Length);
        //        }
        //        return "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Failure";
        //    }
        //}
    }
}
