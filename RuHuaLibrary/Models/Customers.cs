using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RuHuaLibrary.Models
{
    public class Customers
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string User_Id { get; set; }
        

    }
}
