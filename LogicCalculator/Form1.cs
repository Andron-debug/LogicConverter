using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogicCalculator
{
    public partial class Form1 : Form
    {
    //Написать программу, которая по формуле исчисления высказываний, представленной в инфиксной нотации, строит формулу в префиксной нотации.
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            eq_textBox.Text += bt.Text;
            pref_textBox.Text = "";
            postf_textBox.Text = "";
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            eq_textBox.Text = "";
            pref_textBox.Text = "";
            postf_textBox.Text = "";
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            if (eq_textBox.Text.Length != 0)
            eq_textBox.Text = eq_textBox.Text.Substring(0, eq_textBox.Text.Length - 1);
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            LogicFormConv pc = new LogicFormConv(eq_textBox.Text);
            pref_textBox.Text = pc.ToPrexix();
            postf_textBox.Text = pc.ToPostfix();
        }
    }
}
