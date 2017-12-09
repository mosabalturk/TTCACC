﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static Analyzer.MusComp;

namespace Analyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void clearBtn_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "";
            richTextBox2.Text = "";
            Program.cppFiles.Clear();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<result> rs = new List<result>();
            foreach (cppFile cq in Program.cppFiles)
                rs.Add(cq.result);
            choice c = new choice(rs);
            var result = c.ShowDialog();
            if (result == DialogResult.OK)
            {
                scopeTokens val = choice.ReturnValue;

                string s = "";
                s += "<br/>=========================================================<br/> " + val.scopeName;
                s += "<br/>========================================================<br/>";
                if (val != null)
                    foreach (token t in val.tokens)
                    {
                        if (t.isIdentifierObject())
                        {
                            identifier p = (identifier)t;
                            s += "<br/>";
                            s += "id: " + t.id + " , " + t.getLexeme() + " , DT: " + t.getType() + " , DATATYPE_LEXEME: " + p.dataType.getLexeme() + "<br/>";
                        }
                        else if (t.isPointer)
                        {
                            pointer p = (pointer)t;

                            s += "<br/>";
                            s += "id: " + t.id + " , " + t.getLexeme() + " , DT: " + t.getType() + " , DATATYPE_LEXEME: " + p.dataType.getLexeme()
                                 + " , pointer_level: " + p.pointerLevel.ToString() + "<br/>";
                        }
                        else if (t.isArray)
                        {
                            array p = (array)t;
                            s += "<br/>";
                            s += "id: " + t.id + " , " + t.getLexeme() + " , DT: " + t.getType() + " , DATATYPE_LEXEME: " + p.dataType.getLexeme()
                                 + " , array_indecies: <br/>";
                            foreach (token i in p.arrayIndices)
                                s += i.getLexeme().ToString() + "<br/>";
                            s += "<br/>";
                        }
                        else if (t.isFunctionCall)
                        {

                            s += "<br/>";
                            s += "id: " + t.id + " , " + t.getLexeme() + " , " + t.getType() + "<br/>";
                        }
                        else
                        {
                            s += "<br/>";
                            s += "id: " + t.id + " , " + t.getLexeme() + " , " + t.getType() + "<br/>";
                        }

                    }
                webBrowser1.DocumentText = s;
            }
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


        private void counters(object sender, EventArgs e)
        {
            richTextBox2.Text = "counters\n";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += code.spitYourCountersLan(cd.code);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (cppFile cd in Program.cppFiles)
            {
                code.spitYourClassesLan(cd.code);
                code.linecounter = 0;
                File.WriteAllLines(@"C:\Users\Mosab AlTurk\Desktop\TTCACC\Source\spitlantest" + i.ToString() + ".txt", code.lines);
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
            
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";
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
                        richTextBox2.Text += "==================================================================\n";
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";

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
                        richTextBox2.Text += "==================================================================\n";

                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";

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
                        richTextBox2.Text += "==================================================================\n";

                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";

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
                        richTextBox2.Text += "==================================================================\n";

                    }
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";

                if (cd.result != null)
                {
                    List<scopeVarCounter> res = cd.result.vars;
                    foreach (scopeVarCounter l in res)
                    {
                        richTextBox2.Text += " containId " + l.containId.ToString() + " scopeId " + l.scopeId.ToString() + "  " + l.scopeName + "\n";
                        foreach (variableCounter t in l.vars)
                        {
                            richTextBox2.Text += "id: " + t.id + "   " + t.getLexeme() + "   count:" + t.getCount() + "\n";
                        }
                        richTextBox2.Text += "==================================================================\n";

                    }
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";

                if (cd.result != null)
                {
                    List<scopeArrayCounter> res = cd.result.arrays;
                    foreach (scopeArrayCounter l in res)
                    {
                        richTextBox2.Text += " containId " + l.containId.ToString() + " scopeId " + l.scopeId.ToString() + "  " + l.scopeName + "\n";
                        foreach (arrayCounter t in l.arrayCounter)
                        {
                            richTextBox2.Text += t.getLexeme() + " count:" + t.getCount() + "\n";
                        }
                        richTextBox2.Text += "==================================================================\n";

                    }
                }
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";

                if (cd.result != null)
                {
                    List<scopePointersCounter> res = cd.result.pointrs;
                    foreach (scopePointersCounter l in res)
                    {
                        richTextBox2.Text += " containId " + l.containId.ToString() + " scopeId " + l.scopeId.ToString() + "  " + l.scopeName + "\n";
                        foreach (pointerCounter t in l.pointerCounter)
                        {
                            richTextBox2.Text += "id: " + t.id.ToString() + " lexeme: " + t.getLexeme() + " type:" + t.getType() + " pointer level :" + t.pointerLevel + " count:" + t.getCount() + "\n";
                        }
                        richTextBox2.Text += "==================================================================\n";

                    }
                }
            }
        }

        public void getCounters()
        {
        }
        private void button15_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";

                if (cd.result != null)
                {
                    List<scopefunctionCallCounter> res = cd.result.functionCalls;
                    foreach (scopefunctionCallCounter l in res)
                    {
                        richTextBox2.Text += " containId " + l.containId.ToString() + " scopeId " + l.scopeId.ToString() + "  " + l.scopeName + "\n";
                        foreach (functionCallCounter t in l.functionCalls)
                        {
                            richTextBox2.Text += t.getLexeme() + " count:" + t.getCount() + "\n";
                        }
                        richTextBox2.Text += "==================================================================\n";

                    }

                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                if (cd.result != null)
                {
                    List<scopeTokenCounter> res = cd.result.specialChar;
                    foreach (scopeTokenCounter l in res)
                    {
                        richTextBox2.Text += " containId " + l.containId.ToString() + " scopeId " + l.scopeId.ToString() + "  " + l.scopeName + "\n";
                        foreach (tokenCounter t in l.counter)
                        {
                            richTextBox2.Text += "id: " + t.id.ToString() + " lexeme: " + t.getLexeme() + " type:" + t.getType() + " count:" + t.getCount() + "\n";
                        }
                    }
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string tz;
            tz = "main tokens..  <br/>";
            foreach (cppFile cd in Program.cppFiles)
            {
                tz += "file name: " + cd.code.filename + "<br/>";
                foreach (token t in cd.code.main.getTokens)
                {
                    //richTextBox1.Text += "< " + t.getType() + " , " + t.getLexeme() + " >";
                    tz += t.id.ToString() + " " + t.getType() + " ," + t.getLexeme() + ",<br/>";
                }
            }
            webBrowser1.DocumentText = tz;


        }

        private void magicSauceIt_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            Comparison.CompareCppFiles(Program.cppFiles);
            //richTextBox2.Text += Comparison.StaticComparisonTable.Count.ToString();
            //richTextBox2.Text = Comparison.StaticComparisonTable.Count.ToString() + "\n";
            //foreach (ComparisonTable CTRow in Comparison.StaticComparisonTable)
            //{
            //    richTextBox2.Text += CTRow.GetFirstFileName() + "\nScope ID: " + CTRow.GetFirstScopeId() + "\n" + CTRow.GetSecondFileName() + "\nScope ID: " + CTRow.GetSecondScopeId() + "\n" + CTRow.GetCongruentTokenCounter().getType() + ": " + CTRow.GetCongruentTokenCounter().getLexeme() + "(" + CTRow.GetCongruentTokenCounter().getCount() + " times)\n============================\n";
            //}
            //foreach (ComparisonTable CTRow in Comparison.StaticComparisonTable)
            //{
            //    if (CTRow.GetCongruentTokenCounter() != null)
            //    {
            //        richTextBox2.Text += "Scope ID: " + CTRow.GetFirstScopeId() + "\nScope ID: " + CTRow.GetSecondScopeId() + "\n" + CTRow.GetCongruentTokenCounter().getType() + ": " + CTRow.GetCongruentTokenCounter().getLexeme() + "(" + CTRow.GetCongruentTokenCounter().getCount() + " times)\n============================\n";
            //    }
            //    else if (CTRow.GetCongruentPointerCounter() != null)
            //    {
            //        richTextBox2.Text += "Scope ID: " + CTRow.GetFirstScopeId() + "\nScope ID: " + CTRow.GetSecondScopeId() + "\n" + CTRow.GetCongruentPointerCounter().getType() + ": " + CTRow.GetCongruentPointerCounter().getLexeme() + "(" + CTRow.GetCongruentPointerCounter().getCount() + " times)\n============================\n";
            //    }
            //    else if (CTRow.GetCongruentArrayCounter() != null)
            //    {
            //        richTextBox2.Text += "Scope ID: " + CTRow.GetFirstScopeId() + "\nScope ID: " + CTRow.GetSecondScopeId() + "\n" + CTRow.GetCongruentArrayCounter().getType() + ": " + CTRow.GetCongruentArrayCounter().getLexeme() + "(" + CTRow.GetCongruentArrayCounter().getCount() + " times)\n============================\n";
            //    }
            //    //else if (CTRow.GetCongruentVariableCounter() != null)
            //    //{
            //    //    richTextBox2.Text += "Scope ID: " + CTRow.GetFirstScopeId() + "\nScope ID: " + CTRow.GetSecondScopeId() + "\n" + CTRow.GetCongruentVariableCounter().getType() + ": " + CTRow.GetCongruentVariableCounter().getLexeme() + "(" + CTRow.GetCongruentVariableCounter().getCount() + " times)\n============================\n";
            //    //}
            //    else
            //    {
            //        richTextBox2.Text += "Scope ID: " + CTRow.GetFirstScopeId() + "\nScope ID: " + CTRow.GetSecondScopeId() + "\n" + CTRow.GetCongruentFunctionCallCounterCounter().getType() + ": " + CTRow.GetCongruentFunctionCallCounterCounter().getLexeme() + "(" + CTRow.GetCongruentFunctionCallCounterCounter().getCount() + " times)\n============================\n";
            //    }
            //}
            ////////////////////////////////////////////////////////////////////////////////
            Comparison.StaticTemp.Sort((x, y) => x.comparisonResult.CompareTo(y.comparisonResult));
            foreach (Temp t in Comparison.StaticTemp)
            {
                richTextBox2.Text += t.fn1 + "\n" + t.fn2 + "\n" + t.comparisonResult.ToString() + "\n============================\n\n";
            }
            ////////////////////////////////////////////////////////////////////////////////
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }
        private void structsbtn_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            MusComp m = new MusComp();
            m.CompareCppFiles(Program.cppFiles,1);
            m.couples.Sort((x, y) => x.similarity.CompareTo(y.similarity));
            foreach (coupleComp c in m.couples)
            {
                richTextBox2.Text += "\n" + c.file1 + "\n" + c.file2 + "\n============================\n" + c.getLastResult().ToString() + "<br/>============================<br/>";
                foreach (myrow mr in c.towFiles)
                    richTextBox2.Text += mr.lexeme + " similarity " + mr.similarity + " credit " + mr.credit +"\n";
            }


        }

        private void keywordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "";
            MusComp m = new MusComp();
            m.CompareCppFiles(Program.cppFiles, 2);
            m.couples.Sort((x, y) => x.similarity.CompareTo(y.similarity));
            foreach (coupleComp c in m.couples)
            {
                s += "<br/>" + c.file1 + "<br/>" + c.file2 + "<br/>" + c.getLastResult().ToString() + "<br/>============================<br/>";
            }
            webBrowser1.DocumentText = s;

        }

        private void dataTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "";
            MusComp m = new MusComp();
            m.CompareCppFiles(Program.cppFiles, 3);
            m.couples.Sort((x, y) => x.similarity.CompareTo(y.similarity));
            foreach (coupleComp c in m.couples)
            {
                s += "<br/>" + c.file1 + "<br/>" + c.file2 + "<br/>" + c.getLastResult().ToString() + "<br/>============================<br/>";
            }
            webBrowser1.DocumentText = s;

        }

        private void operationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //richTextBox2.Text = "";
            string s = "";
            MusComp m = new MusComp();
            m.CompareCppFiles(Program.cppFiles, 1);
            m.couples.Sort((x, y) => x.similarity.CompareTo(y.similarity));
            foreach (coupleComp c in m.couples)
            {
                s += "<br/>" + c.file1 + "<br/>" + c.file2 + "<br/>" + c.getLastResult().ToString() + "<br/>============================<br/>";
            }
            webBrowser1.DocumentText = s;

        }

        private void valuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "";
            MusComp m = new MusComp();
            m.CompareCppFiles(Program.cppFiles, 4);
            m.couples.Sort((x, y) => x.similarity.CompareTo(y.similarity));
            foreach (coupleComp c in m.couples)
            {
                s += "<br/>" + c.file1 + "<br/>" + c.file2 + "<br/>" + c.getLastResult().ToString() + "<br/>============================<br/>";
            }
            webBrowser1.DocumentText = s;

        }
        private void allOfThemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "";
            MusComp m = new MusComp();
            m.CompareCppFiles(Program.cppFiles, 5);
            m.couples.Sort((x, y) => x.similarity.CompareTo(y.similarity));
            foreach (coupleComp c in m.couples)
            {
                s += "<br/>" + c.file1 + "<br/>" + c.file2 + "<br/>" + c.getLastResult().ToString() + "<br/>============================<br/>";
            }
            webBrowser1.DocumentText = s;


        }


        private void kwOpDtVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "";
            MusComp m = new MusComp();
            m.CompareCppFiles(Program.cppFiles, 6);
            m.couples.Sort((x, y) => x.similarity.CompareTo(y.similarity));
            foreach (coupleComp c in m.couples)
            {
                s += "<br/>" + c.file1 + "<br/>" + c.file2 + "<br/>" + c.getLastResult().ToString() + "<br/>============================<br/>";
            }
            webBrowser1.DocumentText = s;

        }

        private void نتيجةجمعكلالملفOperationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";

                foreach (var t in cd.result.operationsAllFile)
                {
                    richTextBox2.Text += t.getLexeme() + " count:" + t.getCount() + "\n";
                }

            }
        }

        private void نتيجةجمعكلالملفKeywordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";

                foreach (var t in cd.result.keyWordsAllFile)
                {
                    richTextBox2.Text += t.getLexeme() + " count:" + t.getCount() + "\n";
                }

            }
        }

        private void نتيجةجمعكلالملفValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";

                foreach (var t in cd.result.valuesAllFile)
                {
                    richTextBox2.Text += t.getLexeme() + " count:" + t.getCount() + "\n";
                }

            }
        }

        private void نتيجةجمعكلالملفDatatypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";

                foreach (var t in cd.result.dataTypesAllFile)
                {
                    richTextBox2.Text += t.getLexeme() + " count:" + t.getCount() + "\n";
                }

            }
        }

        private void librariesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                richTextBox2.Text += cd.name + "\n";
                richTextBox2.Text += "==================================================================\n";

                foreach (var t in cd.result.libraries)
                {
                    richTextBox2.Text += t + "\n";
                }

            }
        }

        private void عرضليستتالفاريابلزToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                s += cd.name + "<br/>";
                s += "==================================================================<br/>";
                foreach (var t in cd.result.vars)
                {
                    s += t.scopeName+" <br/>";
                    foreach (var id in t.vars)
                    {
                        s += "id: "+ id.id +" : "+id.getLexeme()+" data type : "+id.dataType.getLexeme()+ " count:" + id.getCount() + "<br/>";
                    }
                }
                webBrowser1.DocumentText = s;
            }
        }

        private void عرضليستتالبوينترزToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                s += cd.name + "<br/>";
                s += "==================================================================<br/>";
                foreach (var t in cd.result.pointrs)
                {
                    s += "<br/>";
                    foreach (var id in t.pointerCounter)
                    {
                        s += "id: " + id.id + " : " + id.getLexeme() + " data type : " + id.dataType.getLexeme() +" pointer level: "+id.pointerLevel.ToString()+ " count:" + id.getCount() + "<br/>";
                    }
                }
                webBrowser1.DocumentText = s;
            }
        }

        private void arraysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "";
            foreach (cppFile cd in Program.cppFiles)
            {
                s += cd.name + "<br/>";
                s += "==================================================================<br/>";
                foreach (var t in cd.result.arrays)
                {
                    s += "<br/>";
                    foreach (var id in t.arrayCounter)
                    {
                        s += "id: " + id.id + " : " + id.getLexeme() + " data type : " + id.dataType.getLexeme() + "  dimensions : " + id.arrayIndices.Count.ToString()+" indeces ";
                        foreach (token index in id.arrayIndices)
                            s += index.getLexeme().ToString() + " ";
                        s += " count:" + id.getCount() + "<br/>";
                    }
                }
                webBrowser1.DocumentText = s;
            }
        }
    }


}