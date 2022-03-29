using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicCalculator
{
    public class LogicFormConv
    {
        private string infix;
        public string Infix => infix;
        private string postfix;
        public string Postfix => postfix;
        private string prefix;
        public string Prefix => prefix;


        public LogicFormConv(string eq, int type)
        {
            switch(type)
            {
                case 0://Инфиксная запись
                    infix = eq;
                    postfix = InfexToPostfix(infix);
                    prefix = PostfixToPrefix(postfix);
                    break;
                case 1: //Префиксная запись
                    prefix = eq;
                    postfix = PrefixToPostfix(prefix);
                    infix = PostfixToInfix(postfix);
                    break;
                case 2://Постфиксная запись
                    postfix = eq;
                    infix = PostfixToInfix(postfix);
                    prefix = PostfixToPrefix(postfix);
                    break;
            }
        }
        private static string PostfixToPrefix(string postfix)
        {
            Stack<string> st = new Stack<string>();
            foreach(char c in postfix)
            {
                if (!char.IsLetter(c))
                {
                    if(c != '¬') { 
                    string op1 = st.Pop();
                    string op2 = st.Pop();
                    string temp = c.ToString() + op2 + op1;
                    st.Push(temp);
                    }
                    else
                    {
                        string temp = c.ToString() + st.Pop();
                        st.Push(temp);
                    }
                }
                else
                {
                    st.Push(c.ToString());
                }
            }
            string ans = "";
            while (st.Count != 0)
            {
                ans += st.Pop();
            }
            return ans;
        }
        private static string PostfixToInfix(string postfix)
        {
            Stack<string> st = new Stack<string>();
            foreach (char c in postfix)
            {
                if (!char.IsLetter(c))
                {
                    if (c != '¬')
                    {
                        string op1 = st.Pop();
                        string op2 = st.Pop();
                        string temp =  "("+ op2 + c.ToString() + op1 + ")";
                        st.Push(temp);
                    }
                    else
                    {
                        string temp = c.ToString() + st.Pop();
                        st.Push(temp);
                    }
                }
                else
                {
                    st.Push(c.ToString());
                }
            }
            return st.Pop();
        }
        private static string PrefixToPostfix(string prefix)
        {
            Stack<string> l_st = new Stack<string>();
            Stack<string> o_st = new Stack<string>();
            int letter_count = 0;
            string result = "";
            foreach (char c in prefix)
            {
                if (char.IsLetter(c))
                {
                    l_st.Push(c.ToString());
                    letter_count++;
                    if (letter_count == 2)
                    {
                        string op1 = l_st.Pop();
                        string op2 = l_st.Pop();
                        result += op2 + op1 + o_st.Pop();
                        letter_count = 0;
                    }
                    if((letter_count == 1)&&(o_st.Peek() == "¬"))
                    {
                        result += l_st.Pop() + o_st.Pop();
                        letter_count = 0;
                    }
                }
                else
                {
                    o_st.Push(c.ToString());
                    letter_count = 0;
                }
            }
            while (l_st.Count != 0) result += l_st.Pop();
            while (o_st.Count != 0)
            {
                result += o_st.Pop();
            }
            return result;
        }
        private static string InfexToPostfix(string infix)
        {
            string post = "";
            Stack<char> st = new Stack<char>();
            foreach (char c in infix)
            {
                if (Char.IsLetter(c)) post += c.ToString();
                else
                {
                    if ((c != '(') && (c != ')'))
                        if (st.Count == 0) st.Push(c);
                        else
                        {
                            if (priority(st.Peek()) > priority(c))
                            {
                                post += st.Pop();
                                st.Push(c);
                            }
                            else st.Push(c);
                        }
                    else if (c == '(')
                        st.Push(c);
                    else if (c == ')')
                    {
                        while (st.Peek() != '(') post += st.Pop();
                        st.Pop();
                    }
                }
            }
            while (st.Count != 0) post += st.Pop();
            return post;
        }
        private static int priority(char c)
        {
            int p = -1;
            switch(c)
            {
                case '¬':
                    p = 3;
                    break;
                case '⋀':
                    p = 2;
                    break;
                case '⋁': case '⊕':
                    p = 1;
                    break;
                default:
                    p = 0;
                    break;
                case '(':
                    p = -1;
                    break;
            }
            return p;
        }
    }
}
