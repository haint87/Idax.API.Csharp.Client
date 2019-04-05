using System;
using System.Diagnostics;

namespace Idax.Api.Csharp.Client
{
    public static class Json
    {
        public class Dictionary : System.Collections.Generic.Dictionary<string, string> { };
        public static Dictionary ToDictionary(string strText)
        {
            strText = strText.Trim();
            Debug.Assert(!string.IsNullOrEmpty(strText));
            if (strText[0] != '{' || strText[strText.Length - 1] != '}')
                return null;

            Dictionary dict = new Dictionary();
            strText = strText.Substring(1, strText.Length - 2); // remove '{' & '}'
            int begin = 0;
            int end = strText.Length;
            while (begin < end)
            {
                int beginKey = begin;
                int endKey = beginKey;
                MatchStack stackKey = new MatchStack();
                while (endKey < end && (!stackKey.IsEmpty() || strText[endKey] != ':'))
                {
                    stackKey.Push(strText[endKey]);
                    endKey++;
                }
                Debug.Assert(endKey < end);
                string key = strText.Substring(beginKey, endKey - beginKey).Trim();
                Debug.Assert(key.Length < 2 || (key[0] == '"') == (key[key.Length - 1] == '"'));
                if (key.Length >= 2 && key[0] == '"' && key[key.Length - 1] == '"')
                    key = key.Substring(1, key.Length - 2);

                int beginValue = endKey + 1;
                int endValue = beginValue;
                MatchStack stackValue = new MatchStack();
                while (endValue < end && (!stackValue.IsEmpty() || strText[endValue] != ','))
                {
                    stackValue.Push(strText[endValue]);
                    endValue++;
                }
                string value = strText.Substring(beginValue, endValue - beginValue).Trim();
                Debug.Assert(value.Length < 2 || (value[0] == '"') == (value[value.Length - 1] == '"'));
                if (value.Length >= 2 && value[0] == '"' && value[value.Length - 1] == '"')
                    value = value.Substring(1, value.Length - 2);

                begin = endValue + 1;

                Debug.Assert(!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value));
                dict.Add(key, value);
            }

            return dict;
        }
    }
}