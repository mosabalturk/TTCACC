using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Analyzer
{
    public partial class choice : Form
    {
        List<int> id = new List<int>();
        List<int> cid = new List<int>();
        List<string> name = new List<string>();
        List<scopeTokens> scopes = new List<scopeTokens>();
        List<token> alltokens;
        List<result> results;
        public choice(List<result> results)
        {
            
            InitializeComponent();
            this.results = results;
            this.alltokens = results[0].allTokens;
        }

        private void choice_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = new BindingSource { DataSource = results };
            comboBox1.DisplayMember = "filename";

        }
        public static List<token> ReturnValue { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedItem != null) && (comboBox1.SelectedItem != null))
            {
                ReturnValue = results[comboBox1.SelectedIndex].tokens[comboBox2.SelectedIndex].tokens;
                this.DialogResult = DialogResult.OK;
            }
            this.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<scopeTokens> ls = results[comboBox1.SelectedIndex].tokens;
            comboBox2.DataSource = new BindingSource { DataSource = ls };
            comboBox2.DisplayMember = "scopeName";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedItem != null))
            {
                ReturnValue = results[comboBox1.SelectedIndex].allTokens;
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
        }
    }
}
