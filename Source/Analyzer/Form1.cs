﻿using System;
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
        private void clearBtn_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "";
            richTextBox2.Text = "";
            Program.codes.Clear();
            token.zeroIdCounter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tz;
            tz = "All tokens..  <br/>";
            foreach (code cd in Program.codes)
            {
                tz += "file name: " + cd.filename + "<br/>";
                foreach (token t in cd.getAllTokens())
                {
                    //richTextBox1.Text += "< " + t.getType() + " , " + t.getLexeme() + " >";
                    tz += t.id.ToString() + " " + t.getType() + " ," + t.getLexeme() + ",<br/>";
                }
            }
            webBrowser1.DocumentText = tz;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void filesInputbtn_Click(object sender, EventArgs e)
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
            richTextBox2.Text = "Libraries\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n";
                foreach (string lib in cd.getLibraries)
                    richTextBox2.Text += lib + "\n";
            }
        }

        private void definesbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "defines:\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n";
                foreach (token def in cd.getDefines)
                    richTextBox2.Text += def.getLexeme() + "     " + def.getType().ToString() + " \n";
            }
        }

        private void mainbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "main function\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n";
                richTextBox2.Text += "Data type: " + cd.main.funcDataType + "\n body :" + cd.main.funcAsStr;

            }
        }

        private void allFuncsbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "All functions:\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n-----\n";
                foreach (function func in cd.Allfunctions)
                {
                    richTextBox2.Text += "\nfunction name:" + func.funcname + "\n";
                    richTextBox2.Text += "parameters: ";
                    foreach (token t in func.funcParameters)
                        richTextBox2.Text += t.getType() + " " + t.getLexeme() + " , ";
                    richTextBox2.Text += "\nData type: " + func.funcDataType + "\nbody :\n" + func.funcAsStr + "\n--------------\n--------------\n";

                }
            }
        }

        private void classesbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "classes:\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n-----\n";
                foreach (code clas in cd.Allclasses)
                    richTextBox2.Text += "class name:" + clas.codeAsStr + "\nbody :\n" + clas.codeAsStr + "\n--------------\n--------------\n";
            }
        }

        private void structsbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "structs:\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += cd.filename + "\n-----\n";
                foreach (code clas in cd.Allstructs)
                    richTextBox2.Text += "struct name:" + clas.filename + "\ntypedef name:" + clas.typedefName + "\nbody :\n" + clas.codeAsStr + "\n--------------\n--------------\n";
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
        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getGSTokens())
                {
                    richTextBox2.Text += "< " + t.id.ToString() + " " + t.getType() + " , " + t.getLexeme() + " >\n";
                }
            }
        }

        private void structObjects_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "struct objectcs\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.structObjectsGS)
                {
                    richTextBox2.Text += "< " + t.id.ToString() + " type: " + t.getType() + " , " + t.getLexeme() + " >\n";
                }
            }
        }

        private void opCntrbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "operations counter:\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getOperationsCounterGS)
                {
                    richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "data type counter:\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getDataTypesCounterGS)
                {
                    richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
                }
            }
        }

        private void kewWordsCntrbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "key words counter:\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getKeyWordsCounterGS)
                {
                    richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
                }
            }
        }

        private void valuesCntrbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "values counter:\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getValuesCounterGS)
                {
                    richTextBox2.Text += t.getType() + " count: " + t.getCount() + " >\n";
                }
            }
        }

        private void getArraysCntrbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "arrays counter:\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getArraysCounterGS)
                {
                    richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " array demintions: " + t.arrayIndexes.ToString() + " array boundaries "; ;
                    foreach (int i in t.arrayIndexes)
                        richTextBox2.Text += "[" + i.ToString() + "] ";
                    richTextBox2.Text += " >\n";
                }
            }
        }

        private void varCounterbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "variables counter:\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getVariablesCounterGS)
                {
                    richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
                }
            }
        }

        private void pointersCntrbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "pointers counter:\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getPointersCounterGS)
                {
                    richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
                }
            }
        }
        //h
        private void button2_Click_1(object sender, EventArgs e)
        {
            richTextBox2.Text = "counters:\noperations counter:\n";
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getOperationsCounterGS)
                {
                    richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
                }
            }
            foreach (code cd in Program.codes)
            {
                foreach (token t in cd.getDataTypesCounterGS)
                {
                    richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
                }
            }
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getKeyWordsCounterGS)
                {
                    richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
                }
            }
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getValuesCounterGS)
                {
                    richTextBox2.Text += t.getType() + " count: " + t.getCount() + " >\n";
                }
            }
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getArraysCounterGS)
                {
                    richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " array demintions: " + t.arrayIndexes.ToString() + " array boundaries "; ;
                    foreach (int i in t.arrayIndexes)
                        richTextBox2.Text += "[" + i.ToString() + "] ";
                    richTextBox2.Text += " >\n";
                }
            }
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getVariablesCounterGS)
                {
                    richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
                }

            }
            foreach (code cd in Program.codes)
            {
                richTextBox2.Text += "file name: " + cd.filename + "\n";
                foreach (token t in cd.getPointersCounterGS)
                {
                    richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (code cd in Program.codes)
            {
                code.spitYourClassesLan(cd);
                code.linecounter = 0;
                File.WriteAllLines(@"C:\Users\Mosab AlTurk\Desktop\TTCACC\Source\spitlantest"+i.ToString()+".txt", code.lines);
            }
        }
    }

}