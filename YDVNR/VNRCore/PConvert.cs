using System;

namespace YDVNR.VNRCore
{
    public static class ObjectExtend
    {
        public static string ToStr(this Object Any)
        {
            if (Any != null)
            { 
               return Any.ToString();
            }
           
            return string.Empty;
        }
        public static int ToInt(this Object Any,int NullValue = -1)
        {
            if (Any != null)
            {
                int Number = 0;
                if(int.TryParse(Any.ToString(),out Number)) return Number;
            }

            return NullValue;
        }
        public static long ToLong(this Object Any, int NullValue = -1)
        {
            if (Any != null)
            {
                long Number = 0;
                if (long.TryParse(Any.ToString(), out Number)) return Number;
            }

            return NullValue;
        }
    }

    public static class StringExtend
    {
        public static string StringDivision(this string Message, string Left, string Right)
        {
            if (Message.Contains(Left) && Message.Contains(Right))
            {
                string GetLeftString = Message.Substring(Message.IndexOf(Left) + Left.Length);
                string GetRightString = GetLeftString.Substring(0, GetLeftString.IndexOf(Right));
                return GetRightString;
            }
            else
            {
                return string.Empty;
            }
        }
        public static int ToInt(this string Any,int NullValue = -1)
        {
            int Number = 0;
            if (int.TryParse(Any, out Number)) return Number;

            return NullValue;
        }
        public static long ToLang(this string Any, int NullValue = -1)
        {
            long Number = 0;
            if (long.TryParse(Any, out Number)) return Number;

            return NullValue;
        }
    }
}
