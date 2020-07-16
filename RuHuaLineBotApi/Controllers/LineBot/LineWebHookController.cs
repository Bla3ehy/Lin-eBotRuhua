using isRock.LineBot;
using Microsoft.AspNetCore.Mvc;
using RuHuaLibrary.Models;
using RuHuaLineBotApi.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace RuHuaLineBotApi.Controllers.LineBot
{
    public class LineWebHookController : LineWebHookControllerBase
    {
        private readonly LineBotService _service = new LineBotService();

        [Route("api/LineBotWebHook")]
        [HttpPost]
        public IActionResult POST()
        {
            string AdminUserId = "U6fa72a0d08801365a8f9cf58429795e3";
            var responseMsgs = new List<MessageBase>();


            try
            {
                //設定ChannelAccessToken
                this.ChannelAccessToken = "KA7w2j3+HPU/h0WuaV9OJLY/uXzWf9eds8RgdJxiIJ4LDYwnPxeKJu8rgcnsAA4f05dNJgzkPjESmBs9+hXkd4g132jnWHmqdp3QDvjfuS1EEXfE2o2AFnvVJTneIoouQ8MbYwJPBz9DJWUz5Qrc9wdB04t89/1O/w1cDnyilFU=";
                //取得Line Event
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //配合Line verify 
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();
                var responseMsg = "";
                var count = "";
                var companyName = "";
                var studentName = new ArrayList();
                var orderItem = "";
                var color = "";
                var colorList = new ArrayList();
                MessageBase response = null;

                //新用戶增加進資料庫
                if (ReceivedMessage.events.FirstOrDefault()?.type == "follow")
                {
                    var userId = this.GetUserInfo(ReceivedMessage.events.FirstOrDefault()?.source.userId);
                    var userName = userId.displayName;
                    _service.CreateUser(userId.ToString(), userName);
                }

                //TemplateMessage
                if (LineEvent.type.ToLower() == "message" && LineEvent.message.type == "text" && LineEvent.message.text.Contains("有什麼"))
                {
                    var act1 = new MessageAction()
                    { text = "我要預約", label = "我要預約" };
                    var act2 = new MessageAction()
                    { text = "我要查詢訂單", label = "我要查詢訂單" };

                    var tmp = new ButtonsTemplate()
                    {
                        text = "歡迎使用儒華官方line,在這裡可以預約跟查詢訂單",
                        title = "儒華文化事業社",
                        thumbnailImageUrl = new Uri("https://i.imgur.com/XUsnvLAb.jpg"),
                    };

                    tmp.actions.Add(act1);
                    tmp.actions.Add(act2);

                    responseMsgs.Add(new TemplateMessage(tmp));
                }


                switch (LineEvent.type.ToLower())
                {
                    case "message" when LineEvent.message.type == "text" && LineEvent.message.text.Contains("論文預約"):
                        {
                            //預約
                            response = new TextMessage("請告訴我以下資訊(可以直接複製以下句子) 公司名稱: 學生名稱: 種類: 論文份數: 顏色: ");
                            responseMsgs.Add(response);

                            //回覆訊息
                            this.ReplyMessage(LineEvent.replyToken, responseMsgs);
                            //response OK
                            return Ok();
                        }
                    case "message" when LineEvent.message.type == "text" && LineEvent.message.text.Contains("論文訂單查詢"):
                        {
                            var order = _service.GetOrder(LineEvent.source.userId);
                            string result = "";
                            foreach (var item in order)
                            {
                                result = $"公司名稱:{item.CompanyName} 學生名稱:{item.StudentName} 顏色:{item.Color} 預約項目:{item.Order_Item} 數量:{item.Count}";
                            }

                            response = new TextMessage(result);
                            responseMsgs.Add(response);
                            break;
                            //要先NEW一個東西 然後他的值 再去執行
                            //var order = new List<Order>();
                            //response = new TextMessage(order);
                            //responseMsgs.Add(response);
                            //break;


                            //response = new TextMessage("請告訴我以下資訊(可以直接複製以下句子)公司名稱: 學生名稱: ");
                            //responseMsgs.Add(response);

                            ////回覆訊息
                            //this.ReplyMessage(LineEvent.replyToken, responseMsgs);
                            ////response OK
                            //return Ok();
                        }
                    case "message" when LineEvent.message.type == "text" && LineEvent.message.text.Contains("價格查詢"):
                        {
                            response = new TextMessage("請告訴我以下資訊(例如:黑色跟黑色花紋共30本)");
                            responseMsgs.Add(response);

                            //回覆訊息
                            this.ReplyMessage(LineEvent.replyToken, responseMsgs);
                            //response OK
                            return Ok();
                        }
                    default:
                        break;

                }

                //Luis判斷指令
                if (LineEvent.type.ToLower() == "message" && LineEvent.message.type == "text")
                {
                    var LuisRet = CallLuis(LineEvent.message.text);
                    responseMsg = LuisRet.prediction.topIntent;
                    if (LuisRet.prediction.entities.Counts == null)
                    {
                        count = "1";
                    }
                    else
                    {
                        foreach (var c in LuisRet.prediction.entities.Counts)
                        {
                            count = c;
                        }
                    }

                    if (responseMsg == "Reserve_Intent")
                    {
                        foreach (var co in LuisRet.prediction.entities.CompanyName)
                        {
                            companyName = co;
                        }

                        foreach (var item in LuisRet.prediction.entities.StudentName)
                        {
                            studentName.Add(item);
                        }


                        foreach (var o in LuisRet.prediction.entities.Order_Item)
                        {
                            orderItem = o;
                        }

                        if (LuisRet.prediction.entities.Color == null)
                        {
                            color = "不需要顏色";
                            colorList.Add(color);
                        }
                        else
                        {
                            foreach (var _color in LuisRet.prediction.entities.Color)
                            {
                                colorList.Add(_color);
                            }
                        }
                    }
                }

                switch (LineEvent.type.ToLower())
                {
                    case "message" when LineEvent.message.type == "text" && responseMsg == "Reserve_Intent":
                        {
                            _service.CreateOrder(count, companyName, studentName, orderItem, colorList, LineEvent.source.userId);
                            response = new TextMessage("訂單新增完成!");
                            responseMsgs.Add(response);
                            break;
                        }
                    case "message" when LineEvent.message.type == "text" && responseMsg == "Calculate_Intent":
                        {
                            var ans = _service.CalculatePrice(LineEvent.message.text, count);
                            response = new TextMessage(ans);
                            responseMsgs.Add(response);
                            break;
                        }
                    case "message" when LineEvent.message.type == "text" && responseMsg == "Search_Intent":
                        {
                            var order = _service.GetOrder(LineEvent.source.userId);
                            string result = "";
                            foreach (var item in order)
                            {
                                result = $"公司名稱:{item.CompanyName} 學生名稱:{item.StudentName} 顏色:{item.Color} 預約項目:{item.Order_Item} 數量:{item.Count} 訂單狀態:{item.Status}";
                            }

                            response = new TextMessage(result);
                            responseMsgs.Add(response);
                            break;
                        }

                    default:
                        const string unknown = "抱歉我聽不懂你在說什麼...嘗試一下我們的指令";
                        response = new TextMessage(unknown);
                        responseMsgs.Add(response);
                        break;
                }


                //回覆訊息
                this.ReplyMessage(LineEvent.replyToken, responseMsgs);
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //回覆訊息
                this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }


        public dynamic CallLuis(string message)
        {
            const string endpoint = "https://westus.api.cognitive.microsoft.com/luis/prediction/v3.0/apps/ffbc7940-9c89-45cd-bbf1-f9efc5b1d795/slots/production/predict?subscription-key=8a64848d2d5341bc938b0487ad381d89&verbose=true&show-all-intents=true&log=true&query=";

            var client = new HttpClient();
            var endpointUri = endpoint + message;
            var response = client.GetAsync(endpointUri).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            return result;

        }

    }
}