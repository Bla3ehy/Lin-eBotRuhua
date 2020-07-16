using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuisJSONClass
{
    public class Rootobject
    {
        public string query { get; set; }
        public Prediction prediction { get; set; }
    }

    public class Prediction
    {
        public string topIntent { get; set; }
        public Intents intents { get; set; }
        public Entities entities { get; set; }
    }

    public class Intents
    {
        public Reserve_Intent Reserve_Intent { get; set; }
        public None None { get; set; }
    }

    public class Reserve_Intent
    {
        public float score { get; set; }
    }

    public class None
    {
        public float score { get; set; }
    }

    public class Entities
    {
        public string[] Color { get; set; }
        public string[] Order_Item { get; set; }
        public string[] Counts { get; set; }
        public string[] Date { get; set; }
        public Instance instance { get; set; }
    }

    public class Instance
    {
        public Color[] Color { get; set; }
        public Order_Item[] Order_Item { get; set; }
        public Count[] Counts { get; set; }
        public Date[] Date { get; set; }
    }

    public class Color
    {
        public string type { get; set; }
        public string text { get; set; }
        public int startIndex { get; set; }
        public int length { get; set; }
        public float score { get; set; }
        public int modelTypeId { get; set; }
        public string modelType { get; set; }
        public string[] recognitionSources { get; set; }
    }

    public class Order_Item
    {
        public string type { get; set; }
        public string text { get; set; }
        public int startIndex { get; set; }
        public int length { get; set; }
        public float score { get; set; }
        public int modelTypeId { get; set; }
        public string modelType { get; set; }
        public string[] recognitionSources { get; set; }
    }

    public class Count
    {
        public string type { get; set; }
        public string text { get; set; }
        public int startIndex { get; set; }
        public int length { get; set; }
        public float score { get; set; }
        public int modelTypeId { get; set; }
        public string modelType { get; set; }
        public string[] recognitionSources { get; set; }
    }

    public class Date
    {
        public string type { get; set; }
        public string text { get; set; }
        public int startIndex { get; set; }
        public int length { get; set; }
        public float score { get; set; }
        public int modelTypeId { get; set; }
        public string modelType { get; set; }
        public string[] recognitionSources { get; set; }
    }
}
