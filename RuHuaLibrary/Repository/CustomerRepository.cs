using System;
using System.Collections.Generic;
using System.Text;
using RuHuaLibrary.Models;

namespace RuHuaLibrary.Repository
{
    public class CustomerRepository
    {
        private RuHuaContext _context = new RuHuaContext();
        public void CreateUser(Customers UserInfo)
        {
            _context.Customer.Add(UserInfo);
            _context.SaveChanges();
        }
    }
}
