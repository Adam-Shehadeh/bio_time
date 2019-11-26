using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using bio_time.Models;

namespace bio_time.Helpers
{
    public class SQLHelper
    {
        private static string ConnectionString = "Data Source=den1.mssql7.gear.host;Initial Catalog=bio;Persist Security Info=True;User ID=bio;Password=lu(kyGoose18";
        public static List<TimeLogFile> GetLogFiles()
        {
            List<TimeLogFile> local = new List<TimeLogFile>();
            string sql = @"SELECT * FROM dbo.bio_timelogs";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = CommandType.Text;
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        TimeLogFile model = new TimeLogFile();
                        model.ID = Convert.ToInt32(rdr["ID"]);
                        model.FileName = Convert.ToString(rdr["FileName"]);
                        model.FileContent = Convert.ToString(rdr["FileContent"]);
                        model.LastWrite = Convert.ToDateTime(rdr["LastWrite"]);
                        local.Add(model);
                    }
                }
            }
            return local;
        }
        public static bool SaveLogFile(TimeLogFile model)
        {
            bool local = false;
            string insertSql = $@"  INSERT INTO [dbo].[bio_timelogs]
                                           ([FileName]
                                           ,[FileContent]
                                           ,[LastWrite])
                                     VALUES
                                           (@FileName,
                                           @FileContent,
                                           CURRENT_TIMESTAMP)";
            string updateSql = @" UPDATE dbo.bio_timelogs 
                                  SET FileContent = @FileContent 
                                  WHERE [FileName] = @FileName";
            string sql;
            if (SQLHelper.GetLogFiles().Where(r => r.FileName == model.FileName).Count() > 0)
                sql = updateSql;
            else
                sql = insertSql;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@FileName", model.FileName);
                cmd.Parameters.AddWithValue("@FileContent", model.FileContent);
                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                    local = true;
            }
            return local;
        }
    }
}
