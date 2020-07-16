using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuHuaLineBotApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RuHuaLibrary.Models;
using System.Collections;

namespace RuHuaLineBotApi.Services.Tests
{
    [TestClass()]
    public class LineBotServiceTests
    {
        [TestMethod()]
        public void CalculatePriceTest()
        {
            string input = "黑色共30";
            string end_input;
            var num = input.IndexOf("共");

            if (num == -1)
            {
                end_input = input;
            }
            else
            {
                end_input = input.Substring(0, num);

            }


            string count = "30";

            var ary = end_input.Split("跟");
            List<Service> list = new List<Service>
            {
                new Service() {ServiceId = 1, Color = "黑色", Price = 200,platemakingcharge=300},
                new Service() {ServiceId = 2, Color = "紅色", Price = 200,platemakingcharge=300},
                new Service() {ServiceId = 3, Color = "黑色加花紋", Price = 200,platemakingcharge=300}
            };

            decimal temp = 0;
            foreach (var item in ary)
            {
                foreach (var i in list)
                {
                    if (item == i.Color)
                    {
                        temp += i.Price + i.platemakingcharge;
                    }
                }
            }
            var cou = Convert.ToDecimal(count);
            var ans = temp * cou;


            Assert.AreEqual(ans, 15000);


        }

        [TestMethod()]
        public void CreateOrderTest()
        {
            
            ArrayList studentName = new ArrayList{ "陳思庭", "DACV"};

            var s_Name = String.Join(",", studentName.ToArray());
           
            var target = "陳思庭,DACV";


            Assert.AreEqual(s_Name,target);
        }
    }
}