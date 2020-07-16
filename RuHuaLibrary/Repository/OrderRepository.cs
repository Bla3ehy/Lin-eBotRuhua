using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using RuHuaLibrary.Models;

namespace RuHuaLibrary.Repository
{
    public class OrderRepository
    {
        readonly RuHuaContext _context = new RuHuaContext();
        private const string strConn = "Data Source = ruhua.database.windows.net; Initial Catalog = ruhua; User ID = ruhua; Password =!2Qwaszx";
        public void CreateOrder(Order input)
        {
                  
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                var result = new Order
                {
                    CompanyName = input.CompanyName,
                    StudentName = input.StudentName,
                    Order_Item = input.Order_Item,
                    Color = input.Color,
                    Count = input.Count,
                    User_Id = input.User_Id,
                    Status = input.Status
                };

                conn.Open();
                string sql = "Insert [dbo].[Order](CompanyName,StudentName,Color,Order_Item,Count,User_Id,Status) values(@CompanyName,@StudentName,@Color,@Order_Item,@Count,@User_Id,@Status)";
                conn.Execute(sql,result);
            }
        }

        public void Remove(Order input)
        {
            var find_id = _context.Order.Find(input);
            _context.Order.Remove(find_id);
            _context.SaveChanges();
        }

        public List<Order> GetOrder(string User_Id)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string sql = "select CompanyName,StudentName,Color,Order_Item,Count,Status from [dbo].[Order] where User_Id = @User_Id";
                var result = conn.Query<Order>(sql,new {User_Id }).ToList();
                return result;
            }
            
        }

        public List<Order> AdminGetOrder()
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                string sql = "select * from [dbo].[Order]";
                var result = conn.Query<Order>(sql).ToList();
                return result;
            }
        }

        //修改訂單狀態
        public List<Order> AdminUpdateOrder(string stat)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                
                conn.Open();
                var status = new Order() { Status = stat };
                string sql = "update [dbo].[Order] set Status = @stat";
                conn.Execute(sql,status);

                string _sql = "select * from [dbo].[Order]";
                var result = conn.Query<Order>(_sql).ToList();
                return result;
            }
           
        }
    }
}
