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
        private void clearBtn_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "";
            richTextBox2.Text = "";
            Program.cppFiles.Clear();
            token.zeroIdCounter();
            code.zeroStatics();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tz;
            tz = "All tokens..  <br/>";
            foreach (cppFile cd in Program.cppFiles)
            {
                tz += "file name: " + cd.code.filename + "<br/>";
                foreach (token t in cd.code.getAllTokens())
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
                    Program.addCppFileToList(contents, fileName);
                    //System.IO.File.Copy(fileName, tempFolder + @"\" + System.IO.Path.GetFileName(fileName));

                }
            }
        }

        //private void librariesbtn_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "Libraries\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += cd.filename + "\n";
        //        foreach (string lib in cd.getLibraries)
        //            richTextBox2.Text += lib + "\n";
        //    }
        //}

        //private void definesbtn_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "defines:\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += cd.filename + "\n";
        //        foreach (token def in cd.getDefines)
        //            richTextBox2.Text += def.getLexeme() + "     " + def.getType().ToString() + " \n";
        //    }
        //}

        //private void mainbtn_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "main function\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += cd.filename + "\n";
        //        richTextBox2.Text += "Data type: " + cd.main.funcDataType + "\n body :" + cd.main.funcAsStr;

        //    }
        //}

        //private void allFuncsbtn_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "All functions:\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += cd.filename + "\n-----\n";
        //        foreach (function func in cd.Allfunctions)
        //        {
        //            richTextBox2.Text += "\nfunction name:" + func.funcname + "\n";
        //            richTextBox2.Text += "parameters: ";
        //            foreach (token t in func.funcParameters)
        //                richTextBox2.Text += t.getType() + " " + t.getLexeme() + " , ";
        //            richTextBox2.Text += "\nData type: " + func.funcDataType + "\nbody :\n" + func.funcAsStr + "\n--------------\n--------------\n";

        //        }
        //    }
        //}

        //private void classesbtn_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "classes:\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += cd.filename + "\n-----\n";
        //        foreach (code clas in cd.Allclasses)
        //            richTextBox2.Text += "class name:" + clas.codeAsStr + "\nbody :\n" + clas.codeAsStr + "\n--------------\n--------------\n";
        //    }
        //}

        private void structsbtn_Click(object sender, EventArgs e)
        {
            //richTextBox2.Text = "structs:\n";
            //foreach (code cd in Program.codes)
            //{
            //    richTextBox2.Text += cd.filename + "\n-----\n";
            //    foreach (code clas in cd.Allstructs)
            //        richTextBox2.Text += "struct name:" + clas.filename + "\ntypedef name:" + clas.typedefName + "\nbody :\n" + clas.codeAsStr + "\n--------------\n--------------\n";
            //}

        }

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += cd.filename + "\n-----\n";
        //        foreach (function func in cd.Allfunc_prototypes)
        //        {
        //            foreach (token t in func.funcParameters)
        //                richTextBox2.Text += "parameters:\n" + t.getType() + " " + t.getLexeme() + "\n";
        //            richTextBox2.Text += "function name:" + func.funcname + "\nData type: " + func.funcDataType + "\nbody :\n" + func.funcAsStr + "\n--------------\n--------------\n";
        //        }
        //    }
        //}
        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += "file name: " + cd.code.filename + "\n";
                foreach (token t in cd.code.getGSTokens())
                {
                    richTextBox2.Text += "< " + t.id.ToString() + " " + t.getType() + " , " + t.getLexeme() + " >\n";
                }
            }
        }

        //private void structObjects_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "struct objectcs\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += "file name: " + cd.filename + "\n";
        //        foreach (token t in cd.structObjectsGS)
        //        {
        //            richTextBox2.Text += "< " + t.id.ToString() + " type: " + t.getType() + " , " + t.getLexeme() + " >\n";
        //        }
        //    }
        //}

        //private void opCntrbtn_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "operations counter:\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += "file name: " + cd.filename + "\n";
        //        foreach (token t in cd.getOperationsCounterGS)
        //        {
        //            richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
        //        }
        //    }
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "data type counter:\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += "file name: " + cd.filename + "\n";
        //        foreach (token t in cd.getDataTypesCounterGS)
        //        {
        //            richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
        //        }
        //    }
        //}

        //private void kewWordsCntrbtn_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "key words counter:\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += "file name: " + cd.filename + "\n";
        //        foreach (token t in cd.getKeyWordsCounterGS)
        //        {
        //            richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
        //        }
        //    }
        //}

        //private void valuesCntrbtn_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "values counter:\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += "file name: " + cd.filename + "\n";
        //        foreach (token t in cd.getValuesCounterGS)
        //        {
        //            richTextBox2.Text += t.getType() + " count: " + t.getCount() + " >\n";
        //        }
        //    }
        //}

        //private void getArraysCntrbtn_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "arrays counter:\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += "file name: " + cd.filename + "\n";
        //        foreach (token t in cd.getArraysCounterGS)
        //        {
        //            richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " array demintions: " + t.arrayIndices.ToString() + " array boundaries "; ;
        //            foreach (int i in t.arrayIndices)
        //                richTextBox2.Text += "[" + i.ToString() + "] ";
        //            richTextBox2.Text += " >\n";
        //        }
        //    }
        //}

        //private void varCounterbtn_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "variables counter:\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += "file name: " + cd.filename + "\n";
        //        foreach (token t in cd.getVariablesCounterGS)
        //        {
        //            richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
        //        }
        //    }
        //}

        //private void pointersCntrbtn_Click(object sender, EventArgs e)
        //{
        //    richTextBox2.Text = "pointers counter:\n";
        //    foreach (code cd in Program.codes)
        //    {
        //        richTextBox2.Text += "file name: " + cd.filename + "\n";
        //        foreach (token t in cd.getPointersCounterGS)
        //        {
        //            richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
        //        }
        //    }
        //}
        ////h
        private void counters(object sender, EventArgs e)
        {
            richTextBox2.Text = "counters\n";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += code.spitYourCountersLan(cd.code);
            }
            //foreach (cppFile cd in Program.cppFiles)
            //{
            //    foreach (tokenCounter t in cd.code.getDataTypesCounterGS)
            //    {
            //        richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
            //    }
            //}
            //foreach (cppFile cd in Program.cppFiles)
            //{
            //    richTextBox2.Text += "file name: " + cd.code.filename + "\n";
            //    foreach (tokenCounter t in cd.code.getKeyWordsCounterGS)
            //    {
            //        richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
            //    }
            //}
            //foreach (cppFile cd in Program.cppFiles)
            //{
            //    richTextBox2.Text += "file name: " + cd.code.filename + "\n";
            //    foreach (tokenCounter t in cd.code.getValuesCounterGS)
            //    {
            //        richTextBox2.Text += t.getType() + " count: " + t.getCount() + " >\n";
            //    }
            //}
            //foreach (tokenCounter cd in Program.code.codes)
            //{
            //    richTextBox2.Text += "file name: " + cd.filename + "\n";
            //    foreach (token t in cd.getArraysCounterGS)
            //    {
            //        richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " array demintions: " + t.arrayIndices.ToString() + " array boundaries "; ;
            //        foreach (int i in t.arrayIndices)
            //            richTextBox2.Text += "[" + i.ToString() + "] ";
            //        richTextBox2.Text += " >\n";
            //    }
            //}
            //foreach (code cd in Program.codes)
            //{
            //    richTextBox2.Text += "file name: " + cd.filename + "\n";
            //    foreach (token t in cd.getVariablesCounterGS)
            //    {
            //        richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
            //    }

            //}
            //foreach (code cd in Program.codes)
            //{
            //    richTextBox2.Text += "file name: " + cd.filename + "\n";
            //    foreach (token t in cd.getPointersCounterGS)
            //    {
            //        richTextBox2.Text += t.getLexeme() + " count: " + t.getCount() + " >\n";
            //    }
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (cppFile cd in Program.cppFiles)
            {
                code.spitYourClassesLan(cd.code);
                code.linecounter = 0;
                File.WriteAllLines(@"C:\Users\Mosab AlTurk\Desktop\TTCACC\Source\spitlantest"+i.ToString()+".txt", code.lines);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (cppFile cd in Program.cppFiles)
                code.spitAllSnames(cd.code);

        }

        private void allFuncsbtn_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            foreach (cppFile cd in Program.cppFiles)
                cd.code.spitYourVariablesLn(cd.code);
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text+= cd.fname+"\n";
                if (cd.result != null)
                {
                    List<scopeTokenCounter> res = cd.result.operations;
                    foreach (scopeTokenCounter l in res)
                    {
                        richTextBox2.Text += " containId " + l.containId.ToString() + " scopeId " + l.scopeId.ToString() + "  " + l.scopeName + "\n";
                        foreach (tokenCounter t in l.counter)
                        {
                            richTextBox2.Text += t.getLexeme() + " count:" + t.getCount() + "\n";
                        }
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.fname + "\n";
                if (cd.result != null)
                {
                    List<scopeTokenCounter> res = cd.result.values;
                    foreach (scopeTokenCounter l in res)
                    {
                        richTextBox2.Text += " containId " + l.containId.ToString() + " scopeId " + l.scopeId.ToString() + "  " + l.scopeName + "\n";
                        foreach (tokenCounter t in l.counter)
                        {
                            richTextBox2.Text += t.getType() + " count:" + t.getCount() + "\n";
                        }
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.fname + "\n";
                if (cd.result != null)
                {
                    List<scopeTokenCounter> res = cd.result.datatypes;
                    foreach (scopeTokenCounter l in res)
                    {
                        richTextBox2.Text += " containId " + l.containId.ToString() + " scopeId " + l.scopeId.ToString() + "  " + l.scopeName + "\n";
                        foreach (tokenCounter t in l.counter)
                        {
                            richTextBox2.Text += t.getLexeme() + " count:" + t.getCount() + "\n";
                        }
                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.fname + "\n";
                if (cd.result != null)
                {
                    List<scopeTokenCounter> res = cd.result.keyWord;
                    foreach (scopeTokenCounter l in res)
                    {
                        richTextBox2.Text += " containId " + l.containId.ToString() + " scopeId " + l.scopeId.ToString() + "  " + l.scopeName + "\n";
                        foreach (tokenCounter t in l.counter)
                        {
                            richTextBox2.Text += t.getLexeme() + " count:" + t.getCount() + "\n";
                        }
                    }
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.fname + "\n";
                if (cd.result != null)
                {
                    List<scopeVarCounter> res = cd.result.vars;
                    foreach (scopeVarCounter l in res)
                    {
                        richTextBox2.Text += " containId " + l.containId.ToString() + " scopeId " + l.scopeId.ToString() + "  " + l.scopeName + "\n";
                        foreach (variableCounter t in l.vars)
                        {
                            richTextBox2.Text += t.getLexeme() + " count:" + t.getCount() + "\n";
                        }
                    }
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.fname + "\n";
                if (cd.result != null)
                {
                    List<scopeArrayCounter> res = cd.result.arrays;
                    foreach (scopeArrayCounter l in res)
                    {
                        richTextBox2.Text += " containId " + l.containId.ToString() + " scopeId " + l.scopeId.ToString() +"  " + l.scopeName + "\n";
                        foreach (arrayCounter t in l.arrayCounter)
                        {
                            richTextBox2.Text += t.getLexeme() + " count:" + t.getCount() + "\n";
                        }
                    }
                }
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.fname + "\n";
                if (cd.result != null)
                {
                    List<scopePointersCounter> res = cd.result.pointrs;
                    foreach (scopePointersCounter l in res)
                    {
                        richTextBox2.Text += " containId " + l.containId.ToString() + " scopeId " + l.scopeId.ToString() +"  "+l.scopeName+ "\n";
                        foreach (pointerCounter t in l.pointerCounter)
                        {
                            richTextBox2.Text +="id: "+t.id.ToString()+" lexeme: "+ t.getLexeme() + " type:" + t.getType() + " pointer level :" + t.pointerLevel+ " count:" + t.getCount() + "\n";
                        }
                    }
                }
            }
        }
    }

}