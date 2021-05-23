using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace KralBot
{

    
    
    class DB
    {
        public static List<data> dataAZ = new List<data>();
        public static List<data> dataRU = new List<data>();

        static public Program main = new Program();

        static public SqlConnection sql = new SqlConnection("Data Source=kralbotuhoqqa.database.windows.net;Initial Catalog=kralhoqqadi;User ID=nuke;Password=Salamlar123;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        int az=0;
        int ru=0;
        public void azAddData(string _code,int _messageId)
        {
            sql.Open();
            SqlCommand oxu = new SqlCommand("Select *From KralDataAZ", sql);
            SqlDataReader read = oxu.ExecuteReader();

            while (read.Read())
            {
                az = int.Parse(read["id"].ToString());
            }
            az += 1;
            sql.Close();
            int code = int.Parse(_code);
            int messageId = _messageId;
            sql.Open();

            String query = "INSERT INTO KralDataAZ (id,MessageID,Code) VALUES ('" + az + "','" + messageId + "','" + code + "' );";
            az++;
            SqlCommand command = new SqlCommand(query, sql);
            command.ExecuteNonQuery();
            sql.Close();

        }
        public void ruAddData(string _code, int _messageId)
        {
            sql.Open();
            SqlCommand oxu = new SqlCommand("Select *From KralDataRU", sql);
            SqlDataReader read = oxu.ExecuteReader();

            while (read.Read())
            {
                ru = int.Parse(read["id"].ToString());
            }
            ru += 1;
            sql.Close();

            int code = int.Parse(_code);
            int messageId = _messageId;
            sql.Open();

            String query = "INSERT INTO KralDataRU (id,MessageID,Code) VALUES ('" + ru + "','" + messageId + "','" + code + "' );";
            ru++;
            SqlCommand command = new SqlCommand(query, sql);
            command.ExecuteNonQuery();
            sql.Close();

        }



        public void silMsjRu(string code)
        {
            int mainCode = int.Parse(code);
            int id=0;
            int messageid=0;
            sql.Open();
            SqlCommand command = new SqlCommand("Select *From KralDataRU", sql);
            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                int codeRU = int.Parse(read["Code"].ToString());
                if (mainCode==codeRU)
                {
                    id = int.Parse(read["id"].ToString());
                    messageid = int.Parse(read["MessageID"].ToString());
                }
               
            }
            sql.Close();
            if (id != 0)
            {
                sql.Open();
                SqlCommand command3 = new SqlCommand("DELETE FROM KralDataRU where id = " + id + "", sql);
                command3.ExecuteNonQuery();
                sql.Close();
                main.delMessageRU(messageid);

            }


        }





        public void yoxla()
        {
            dataAZ.Clear();
            dataRU.Clear();
            sql.Open();
            SqlCommand command = new SqlCommand("Select *From KralDataAZ", sql);
            
            SqlDataReader read = command.ExecuteReader();
            
            while (read.Read())
            {
                int id = int.Parse(read["id"].ToString());
                int messageidAZ = int.Parse(read["MessageID"].ToString());
                int codeAZ = int.Parse(read["Code"].ToString());
                dataAZ.Add(new data(id, messageidAZ, codeAZ));


            }
            sql.Close();
            sql.Open();
            SqlCommand command2 = new SqlCommand("Select *From KralDataRU", sql);
            SqlDataReader read2 = command2.ExecuteReader();
            while (read2.Read())
            {
                int id = int.Parse(read2["id"].ToString());
                int messageidRU = int.Parse(read2["MessageID"].ToString());
                int codeRU = int.Parse(read2["Code"].ToString());
                dataRU.Add(new data(id, messageidRU, codeRU));

            }
            sql.Close();

            

            for (int i = 0; i < dataAZ.Count; i++)
            {
                bool f = true;
                for (int j = 0; j < dataRU.Count; j++)
                {
                    
                    if (dataAZ[i].code == dataRU[j].code)
                    {
                        f = false;
                        break;
                    }

                }
                if (f)
                {
                    try
                    {
                        main.delMessage(dataAZ[i].messageid);
                    }
                    catch (Exception b)
                    {

                        Console.WriteLine(b);
                    }
                    sql.Open();
                    try
                    {
                        
                        SqlCommand command3 = new SqlCommand("DELETE FROM KralDataAZ where id = " + dataAZ[i].id + "", sql);
                        command3.ExecuteNonQuery();
                        

                    }
                    
                    catch (Exception z)
                    {

                        Console.WriteLine(z);
                    }
                    sql.Close();

                }


            }



        }


    }


    public struct data
    {
        public int id;
        public int messageid;
        public int code;
        public data(int _id,int _messageid,int _code)
        {
            id = _id;
            messageid = _messageid;
            code = _code;
        }
    }



}
