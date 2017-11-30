using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Analyzer
{
    public partial class Form1 : Form
    {
        int gg = 1;
        public Form1()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            Program.codes.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                richTextBox1.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox1.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getAllTokens())
                {
                    //richTextBox1.Text += "< " + t.getType() + " , " + t.getLexeme() + " >";
                    richTextBox1.Text += t.id.ToString()+" "+ t.getType() + " , " + t.getLexeme() + "\n";
                }
            }
        }

            private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "cpp files|*.cpp";
            od.Multiselect = true;
            if (od.ShowDialog() == DialogResult.OK)
            {
                string tempFolder = Path.GetTempPath();

                foreach (string fileName in od.FileNames)
                {
                    string contents = File.ReadAllText(@fileName);
                    Program.addCodeToList(contents, fileName);
                    //System.IO.File.Copy(fileName, tempFolder + @"\" + System.IO.Path.GetFileName(fileName));

                }
            }
        }

        private void librariesbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n";
                foreach (string lib in cd.getLibraries)
                    richTextBox2.Text += lib + "\n";
            }
        }

        private void definesbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n";
                foreach (token def in cd.getDefines)
                    richTextBox2.Text += def.getLexeme() + "     " + def.GetCount().ToString()+" \n";
            }
        }

        private void mainbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n";
                richTextBox2.Text += "Data type: " + cd.main.funcDataType + "\n body :" + cd.main.funcAsStr;

            }
        }

        private void allFuncsbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n-----\n";
                foreach (function func in cd.Allfunctions)
                {
                    richTextBox2.Text += "parameters: ";
                    foreach (token t in func.funcParameters)
                        richTextBox2.Text +=  t.getType() + " " + t.getLexeme()+" , " ;
                    richTextBox2.Text += "\nfunction name:" + func.funcname + "\nData type: " + func.funcDataType + "\nbody :\n" + func.funcAsStr + "\n--------------\n--------------\n";
                    
                }
            }
        }

        private void classesbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n-----\n";
                foreach (code clas in cd.Allclasses)
                    richTextBox2.Text += "class name:" + clas.codeAsStr  + "\nbody :\n" + clas.codeAsStr + "\n--------------\n--------------\n";
            }
        }

        private void structsbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n-----\n";
                foreach (code clas in cd.Allstructs)
                    richTextBox2.Text += "class name:" + clas.filename + "\nbody :\n" + clas.codeAsStr + "\n--------------\n--------------\n";
            }

        }

        private void funcCompBtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "\n-----\n" + cd.filename + "\n";
                richTextBox2.Text +="functions count: "+ cd.Allfunctions.Count.ToString()+"\n";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "\n-----\n" + cd.filename+"\n";
                richTextBox2.Text += "functions count: " + cd.Allfunc_prototypes.Count.ToString() + "\n";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n-----\n";
                foreach (function func in cd.Allfunc_prototypes)
                {
                    foreach (token t in func.funcParameters)
                        richTextBox2.Text += "parameters:\n" + t.getType() + " " + t.getLexeme() + "\n";
                    richTextBox2.Text += "function name:" + func.funcname + "\nData type: " + func.funcDataType + "\nbody :\n" + func.funcAsStr + "\n--------------\n--------------\n";
                }
            }
        }

        private void showCodebtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n-----\nthe code:\n";
                richTextBox2.Text += cd.codeAsStr;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox1.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getGSTokens())
                {
                    richTextBox1.Text += "< "+t.id.ToString() + " " + t.getType() + " , " + t.getLexeme() + " >\n";
                }
            }
        }
    }
}
