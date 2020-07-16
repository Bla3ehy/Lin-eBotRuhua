using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using RuHuaLibrary.Models;


namespace RuHuaLibrary.Repository
{
    public class ServiceRepository
    {
        public List<Service> GetService()
        {
            string strConn = "Data Source=ruhua.database.windows.net;Initial Catalog=ruhua;Persist Security Info=True;User ID=ruhua;Password=!2Qwaszx";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string sql = "select * from Service";
                var result = conn.Query<Service>(sql).ToList();
                return result;
            }
        }
    }
}
