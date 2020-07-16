using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RuHuaLibrary.Models;
using RuHuaLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuHuaLineBotApi.Services
{
    public class AdminService
    {
        private OrderRepository repo = new OrderRepository();

        public Order AdminGetOrder()
        {
            var order = repo.AdminGetOrder();

            var result = new Order();

            foreach (var o in order)
            {
                result.User_Id = o.User_Id;
                result.CompanyName = o.CompanyName;
                result.StudentName = o.StudentName;
                result.Order_Item = o.Order_Item;
                result.Color = o.Color;
                result.Count = o.Count;
                result.Status = o.Status;
            }

            return result;
        }



        public Order AdminUpdateOrder(string jdata)
        {
            var data = JsonConvert.DeserializeObject<Order>(jdata);

            var result = new Order();
            var order = repo.AdminUpdateOrder(data.Status);

            foreach (var o in order)
            {
                result.User_Id = o.User_Id;
                result.CompanyName = o.CompanyName;
                result.StudentName = o.StudentName;
                result.Order_Item = o.Order_Item;
                result.Color = o.Color;
                result.Count = o.Count;
                result.Status = o.Status;
            }

            return result;
        }
    }
}
