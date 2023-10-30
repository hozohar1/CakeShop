using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal static class Config
    {
        static string s_config = "Configuration";

        //for order
        public static int GetNextOrderIDfromXMLConfig()
        {
            return XMLTools.ToIntNullable(XMLTools.LoadListFromXMLElement(s_config),"NextOrderNumber")?? 0;
        }
        public static void saveListToXMLElementOrders(int ID)
            {
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            root.Element("NextOrderNumber")?.SetValue(ID.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
        }


        //for orderitem
        public static int GetNextOrderItemIDfromXMLConfig()
        {
            return XMLTools.ToIntNullable(XMLTools.LoadListFromXMLElement(s_config), "NextOrderItem") ?? 0  ;//XMLTools.LoadListFromXMLElement(s_config).Element("NextOrderItem");
        }
        public static void saveListToXMLElementOrderItem(int ID)
        {
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            root.Element("NextOrderItem")?.SetValue(ID.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
        }
    }
}
