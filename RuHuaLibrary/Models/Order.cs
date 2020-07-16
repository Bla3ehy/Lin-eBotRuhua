using System;
using System.Collections.Generic;
using System.Text;

namespace RuHuaLibrary.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string StudentName { get; set; }
        public string Color { get; set; }
        public string Order_Item { get; set; }
        public string Count { get; set; }
        public string User_Id { get; set; }
        public string Status { get; set; }

    }
}
