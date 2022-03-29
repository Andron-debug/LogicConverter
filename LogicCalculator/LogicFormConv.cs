using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicCalculator
{
    public class LogicFormConv
    {
        private string inf;
        public LogicFormConv(string infex)
        {
            this.inf = infex;
        }
        public string ToPrexix()
        {
            // http://blog.kislenko.net/show.php?id=1927;
            return "";
        }
        public string ToPostfix()
        {
            string post = "";
            Stack<char> st = new Stack<char>();
            foreach (char c in inf)
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
        private int priority(char c)
        {
            int p = -1;
            switch (c)
            {
                case '(':
                    p = 0;
                    break;
                case '⋁':
                    p = 1;
                    break;
                case '⋀':
                    p = 2;
                    break;
                case '¬':
                    p = 4;
                    break;
                default:
                    p = 3;
                    break;
            }
            return p;
        }
    }
}
