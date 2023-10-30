using System.Collections;
using System.Reflection;

namespace BO;
public static class Tools
{
    //public static string ToStringProperty<T>(this T t) //get order for exp
    //{
    //    string str = "";

    //    foreach (PropertyInfo item in t.GetType().GetProperties())
    //    {
    //        str += "\n" + item.Name + ": ";
    //        var val = item.GetValue(t, null);  

    //        if (val is  IEnumerable && !(val is string))
    //        {
    //            foreach (var i  in  (IEnumerable)val)
    //            {
    //                str += (i + " ");
    //            }
    //        }
    //        else
    //            str += val;
    //        //str += "\n" + item.Name + ": " + val;
    //    }

    //    str += "\n";



    //    return str;
    //}


    //}
    public static string ToStringProperty<T>(this T t) //get order for exp
    {
        string str = "";

        foreach (PropertyInfo item in t.GetType().GetProperties())
        {
            str += "\n" + item.Name + ": ";
            if (item.GetValue(t, null) is IEnumerable<object>)
            {
                IEnumerable<object> lst = (IEnumerable<object>)item.GetValue(obj: t, null);
                string s = String.Join("  ", lst);
                str += s;
            }
            else
                str += item.GetValue(t, null);
        }
        return str + "\n";
    }
}