using RuHuaLibrary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using RuHuaLibrary.Repository;

namespace RuHuaLineBotApi.Services
{
    public class LineBotService
    {
        private OrderRepository repo = new OrderRepository();
        private ServiceRepository s_repo = new ServiceRepository();
        private CustomerRepository c_repo = new CustomerRepository();

        public string CalculatePrice(string input, string count)
        {
            var num = input.IndexOf("共", StringComparison.Ordinal);
            var endInput = num == -1 ? input : input.Substring(0, num);

            try
            {
                var _list = s_repo.GetService();

                var ary = endInput.Split("跟");

                decimal temp = 0;
                foreach (var item in ary)
                {
                    foreach (var i in _list)
                    {
                        if (item == i.Color)
                        {
                            temp += i.Price + i.platemakingcharge;
                        }
                    }
                }

                var cou = Convert.ToDecimal(count);
                var ans = temp * cou;
                return ans.ToString(CultureInfo.InvariantCulture);

            }
            catch (Exception)
            {
                const string sorry = "抱歉出錯了喔,請確認物品輸入正確,或是輸入 例: 黑色跟黑色花紋共30 !";
                return sorry;
            }

        }

        public void CreateOrder(string count, string companyName, ArrayList studentName, string orderItem, ArrayList colorList,string User_Id)
        {
            var s_Name = String.Join(",", studentName.ToArray());
            var c_color = String.Join(",", colorList.ToArray());

            var result = new Order
            {
                CompanyName = companyName,
                StudentName = s_Name,
                Order_Item = orderItem,
                Color = c_color,
                Count = count,
                User_Id = User_Id,
                Status = "未完成"
            };

            repo.CreateOrder(result);

        }

        public List<Order> GetOrder(string userId)
        {
            var order = repo.GetOrder(userId);
            return order;
        }

        public void CreateUser(string userId,string userName)
        {
            var cus = new Customers
            {
                User_Id = userId,
                Name = userName
            };
            c_repo.CreateUser(cus);
        }

    }
}
