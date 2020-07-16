using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace RuHuaLibrary.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string Name { get; set; }
        //裝書費
        public decimal Price { get; set; }
        public string Color { get; set; }
        //版費
         public decimal  platemakingcharge { get; set; }
    }
}
