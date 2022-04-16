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
            if ((type < 0) || (type > 2)) throw new Exception("Не существует тип");
            switch(type)
            {
                case 0://Инфиксная запись
                    infix = eq;
                    if (!OkInfix(infix)) throw new Exception("Не корректный ввод");
                    try
                    {
                        postfix = InfexToPostfix(infix);
                        prefix = PostfixToPrefix(postfix);
                    }
                    catch
                    {
                        throw new Exception("Не корректный ввод");
                    }
                    break;
                case 1: //Префиксная запись
                    prefix = eq;
                    if (!OkPrefix(prefix)) throw new Exception("Не корректный ввод");
                    postfix = PrefixToPostfix(prefix);
                    infix = PostfixToInfix(postfix);
                    break;
                case 2://Постфиксная запись
                    postfix = eq;
                    if(!OkPostfix(postfix)) throw new Exception("Не корректный ввод");
                    infix = PostfixToInfix(postfix);
                    prefix = PostfixToPrefix(postfix);
                    break;
            }
        }
        private static char[] sim = { '¬', '⋀', '⋁', '⊕', '(', ')', '→', '↔' };
        public static bool OkInfix(string infix)
        {
            if (sim.Contains(infix[infix.Length - 1])&&(infix[infix.Length - 1] != ')')) return false;
            if(sim.Contains(infix[0])&&(infix[0]!= '¬')&&(infix[0]!='(')) return false;
            Stack<char> st = new Stack<char>();
            foreach (char c in infix)
            {
                if (c == '(') st.Push('(');
                if (c == ')')
                {
                    if (st.Count == 0) return false;
                    else st.Pop();
                }
            }
            if (st.Count != 0) return false;
            for (int i = 0; i < infix.Length - 1; i++)
            {
                char now = infix[i];
                char next = infix[i + 1];
                if (char.IsLetter(now) && (!(sim.Contains(next)) || (next == '(')||(next == '¬'))) return false;
                if (sim.Contains(now) && (sim.Contains(next)) && (next != '(') && (next != '¬') && (now != ')')) return false;
                if ((now == ')') && ((char.IsLetter(next)) || (next == '¬'))) return false;
            }
            return true;
        }
        public static bool OkPostfix(string postfix)
        {
            if (sim.Contains(postfix[0])) return false;
            if (char.IsLetter(postfix[postfix.Length - 1])) return false;
            return RightSim(postfix);
        }
        public static bool OkPrefix(string prefix)
        {
            if (sim.Contains(prefix[prefix.Length - 1])) return false;
            if (char.IsLetter(prefix[0])) return false;
            return RightSim(prefix);
        }
        private static bool RightSim(string problem)
        {
            int sim_count = 0;
            int lit_count = 0;
            foreach (char c in problem)
            {
                if (char.IsLetter(c)) lit_count++;
                if (sim.Contains(c) && (c != '¬')) sim_count++;
            }
            return lit_count - 1 == sim_count;
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
            string result = "";
            int litter_count = 0;
            Stack<Char> st = new Stack<char>();
            Stack<Char> st2 = new Stack<char>();
            foreach (char c in prefix)
            {
                if (!char.IsLetter(c))
                {
                    if (st.Count != 0) st2.Push(st.Peek());
                    st.Push(c);
                    if(c != '¬') litter_count = 0;
                }
                else
                {
                    result += c.ToString();
                    litter_count++;
                    while (st.Peek() == '¬')
                    {
                        result += st.Pop();
                        if (st2.Count != 0) st2.Pop();
                        if (st.Count() == 0) break;
                    }
                    if (st2.Count != 0)
                    {
                        if ((priority(st.Peek()) > priority(st2.Peek()))&&(litter_count == 2))
                        {
                            result += st.Pop();
                            st2.Pop();
                        }
                    }
                }
            }
            
            while (st.Count != 0)
            {
                result += st.Pop();
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
                        if(st.Count != 0)
                        while (st.Peek() == '¬')
                        {
                            post += st.Pop();
                            if (st.Count == 0) break;
                        }

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
