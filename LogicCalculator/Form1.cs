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
            inf_radioButton.Checked = true;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            input_textBox.Text += bt.Text;
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            inf_textBox.Text = "";
            pref_textBox.Text = "";
            postf_textBox.Text = "";
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            if (input_textBox.Text.Length != 0)
            input_textBox.Text = input_textBox.Text.Substring(0, input_textBox.Text.Length - 1);
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            try
            {
                LogicFormConv conv = new LogicFormConv(input_textBox.Text, type);
                inf_textBox.Text = conv.Infix;
                pref_textBox.Text = conv.Prefix;
                postf_textBox.Text = conv.Postfix;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int type = 0;
        private TextBox input_textBox;
        private void type_Changed(object sender, EventArgs e)
        {
            inf_textBox.BackColor = Color.FromArgb(240, 240, 240);
            pref_textBox.BackColor = Color.FromArgb(240, 240, 240);
            postf_textBox.BackColor = Color.FromArgb(240, 240, 240);
            if (inf_radioButton.Checked)
            {
                input_textBox = inf_textBox;
                type = 0;
            }
            if(pref_radioButton.Checked)
            {
                input_textBox = pref_textBox;
                type = 1;
            }
            if(postf_radioButton.Checked)
            {
                input_textBox = postf_textBox;
                type = 2;
            }
            input_textBox.BackColor = Color.White;
        }

        private void about_button_Click(object sender, EventArgs e)
        {
            Form f = new About_form();
            f.Show();
        }
    }
}
