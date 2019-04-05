using System;
using System.Diagnostics;

namespace Idax.Api.Csharp.Client
{
    public class MatchStack
    {
        private string strStack;
        public bool IsEmpty() { return string.IsNullOrEmpty(strStack); }
        public void Push(char ch)
        {
            if (ch != '\"' && ch != '{' && ch != '}' && ch != '[' && ch != ']')
                return;

            Debug.Assert(!string.IsNullOrEmpty(strStack) || ch != '}' && ch != ']');
            if (string.IsNullOrEmpty(strStack) && ch != '}' && ch != ']')
            {
                strStack += ch;
                return;
            }

            if (ch == '\"')
            {
                if (strStack[strStack.Length - 1] == '\"')
                    strStack = strStack.Substring(0, strStack.Length - 1);
                else
                    strStack += ch;
            }
            if (ch == '{' || ch == '[')
            {
                strStack += ch;
            }
            if (ch == '}')
            {
                Debug.Assert(strStack[strStack.Length - 1] == '{');
                if (strStack[strStack.Length - 1] == '{')
                    strStack = strStack.Substring(0, strStack.Length - 1);
            }
            if (ch == ']')
            {
                Debug.Assert(strStack[strStack.Length - 1] == '[');
                if (strStack[strStack.Length - 1] == '[')
                    strStack = strStack.Substring(0, strStack.Length - 1);
            }
        }
    }
}
