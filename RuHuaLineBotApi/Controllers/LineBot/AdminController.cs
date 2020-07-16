using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using isRock.LIFF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RuHuaLineBotApi.Services;
using Newtonsoft.Json;
using RuHuaLibrary.Models;
using Microsoft.Extensions.Logging;

namespace RuHuaLineBotApi.Controllers.LineBot
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private AdminService _service = new AdminService();
        

        [HttpGet]
        public ActionResult<Order> AdminGetOrder()
        {
            var order = _service.AdminGetOrder();
            return order;
        }

        [HttpPost]
        public ActionResult<Order> PostOrder(string jdata)
        {
            var order = _service.AdminUpdateOrder(jdata);
            return order;
        }
    }
}
