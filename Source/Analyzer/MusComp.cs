using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    class MusComp
    {
        public List<coupleComp> couples = new List<coupleComp>();

        public void CompareCppFilesTokenCounter(List<cppFile> allFilesList,params int[] x)
        {
            for (int i = 0; i < allFilesList.Count - 1; i++)
            {
                for (int j = i + 1; j < allFilesList.Count; j++)
                {
                    couples.Add(compare(allFilesList[i], allFilesList[j], x));
                }
            }
        }
        public void keyowrdComp(coupleComp c)
        {
            List<myrow> l = new List<myrow>();
            List<tokenCounter>  temp1 = new List<tokenCounter>(c.f1.result.keyWordsAllFile);
            List<tokenCounter>  temp2 = new List<tokenCounter>(c.f2.result.keyWordsAllFile);
            foreach (tokenCounter tt in temp1)
                if (!temp2.Select(a => a.getLexeme()).Contains(tt.getLexeme()))
                {
                    tokenCounter tc = tt.copy();
                    tc.setCount(0);
                    temp2.Add(tc);

                }
            foreach (tokenCounter tt in temp2)
                if (!temp1.Select(a => a.getLexeme()).Contains(tt.getLexeme()))
                {
                    tokenCounter tc = tt.copy();
                    tc.setCount(0);
                    temp1.Add(tc);
                }

            foreach (tokenCounter t2 in temp2)
            {

                int a = temp1.Find(aa => aa.getLexeme() == t2.getLexeme()).getCount();
                int b = t2.getCount();
                if ((a != 0) || (b != 0))
                    l.Add(new myrow(a, b, t2.getLexeme(), c.f1.name, c.f2.name));

            }
            c.refreshResult(l);
        }
        public void operationsComp(coupleComp c)
        {
            List<myrow> l = new List<myrow>();
            List<tokenCounter> temp1 = new List<tokenCounter>(c.f1.result.operationsAllFile);
            List<tokenCounter> temp2 = new List<tokenCounter>(c.f2.result.operationsAllFile);
            foreach (tokenCounter tt in temp1)
                if (!temp2.Select(a => a.getLexeme()).Contains(tt.getLexeme()))
                {
                    tokenCounter tc = tt.copy();
                    tc.setCount(0);
                    temp2.Add(tc);

                }
            foreach (tokenCounter tt in temp2)
                if (!temp1.Select(a => a.getLexeme()).Contains(tt.getLexeme()))
                {
                    tokenCounter tc = tt.copy();
                    tc.setCount(0);
                    temp1.Add(tc);
                }

            foreach (tokenCounter t2 in temp2)
            {

                int a = temp1.Find(aa => aa.getLexeme() == t2.getLexeme()).getCount();
                int b = t2.getCount();
                if ((a != 0) || (b != 0))
                    l.Add(new myrow(a, b, t2.getLexeme(), c.f1.name, c.f2.name));

            }
            c.refreshResult(l);
        }
        public void dataTypesComp(coupleComp c)
        {
            List<myrow> l = new List<myrow>();
            List<tokenCounter> temp1 = new List<tokenCounter>(c.f1.result.dataTypesAllFile);
            List<tokenCounter> temp2 = new List<tokenCounter>(c.f2.result.dataTypesAllFile);
            foreach (tokenCounter tt in temp1)
                if (!temp2.Select(a => a.getLexeme()).Contains(tt.getLexeme()))
                {
                    tokenCounter tc = tt.copy();
                    tc.setCount(0);
                    temp2.Add(tc);

                }
            foreach (tokenCounter tt in temp2)
                if (!temp1.Select(a => a.getLexeme()).Contains(tt.getLexeme()))
                {
                    tokenCounter tc = tt.copy();
                    tc.setCount(0);
                    temp1.Add(tc);
                }

            foreach (tokenCounter t2 in temp2)
            {

                int a = temp1.Find(aa => aa.getLexeme() == t2.getLexeme()).getCount();
                int b = t2.getCount();
                if ((a != 0) || (b != 0))
                {
                    l.Add(new myrow(a, b, t2.getLexeme(), c.f1.name, c.f2.name));
                }
            }
            c.refreshResult(l);
        }
        public void funcsComp(coupleComp c)
        {
            List<myrow> l = new List<myrow>();
            List<functionCallCounter> temp1 = new List<functionCallCounter>(c.f1.result.allFrequentlyUsedFunctionCalls);
            List<functionCallCounter> temp2 = new List<functionCallCounter>(c.f2.result.allFrequentlyUsedFunctionCalls);

            foreach (functionCallCounter t2 in temp2)
            {

                int a = temp1.Find(aa => aa.getLexeme() == t2.getLexeme()).getCount();
                int b = t2.getCount();
                if ((a != 0) || (b != 0))
                {
                    l.Add(new myrow(a, b, t2.getLexeme(), c.f1.name, c.f2.name));
                }
            }
            c.refreshResult(l);
        }
        public void varsComp(coupleComp c)
        {
            List<myrow> l = new List<myrow>();
            List<variableCounter> temp1 = new List<variableCounter>(c.f1.result.varsAllFile);
            List<variableCounter> temp2 = new List<variableCounter>(c.f2.result.varsAllFile);

            int temp1funcs = temp1.Count();
            int temp2funcs = temp2.Count();
            int temp1funcsCount = 0;
            int temp2funcsCount = 0;
            foreach (variableCounter tt in temp1)
                temp1funcsCount += tt.getCount();
            foreach (variableCounter tt in temp2)
                temp2funcsCount += tt.getCount();
            l.Add(new myrow(temp1funcs, temp2funcs, "functions count", c.f1.name, c.f2.name));
            if (temp1funcsCount != 0 || temp2funcsCount != 0)
                l.Add(new myrow(temp1funcsCount, temp2funcsCount, "functions call count", c.f1.name, c.f2.name));
            c.refreshResult(l);
        }

        public void valuesComp(coupleComp c)
        {
            List<myrow> l = new List<myrow>();
            List<tokenCounter> temp1 = new List<tokenCounter>(c.f1.result.valuesAllFile);
            List<tokenCounter> temp2 = new List<tokenCounter>(c.f2.result.valuesAllFile);
            foreach (tokenCounter tt in temp1)
                if (!temp2.Select(a => a.getType()).Contains(tt.getType()))
                {
                    tokenCounter tc = tt.copy();
                    tc.setCount(0);
                    temp2.Add(tc);

                }
            foreach (tokenCounter tt in temp2)
                if (!temp1.Select(a => a.getType()).Contains(tt.getType()))
                {
                    tokenCounter tc = tt.copy();
                    tc.setCount(0);
                    temp1.Add(tc);
                }

            foreach (tokenCounter t2 in temp2)
            {

                int a = temp1.Find(aa => aa.getType() == t2.getType()).getCount();
                int b = t2.getCount();
                if ((a != 0) || (b != 0))
                {
                    l.Add(new myrow(a, b, t2.getType(), c.f1.name, c.f2.name));
                }
            }
            c.refreshResult(l);
        }
        public void librariesComp(coupleComp c)
        {
            List<myrow> l = new List<myrow>();
            List<tokenCounter> temp1 = new List<tokenCounter>(c.f1.result.libraries);
            List<tokenCounter> temp2 = new List<tokenCounter>(c.f2.result.libraries);
            foreach (tokenCounter tt in temp1)
                if (!temp2.Select(a => a.getLexeme()).Contains(tt.getLexeme()))
                {
                    tokenCounter tc = tt.copy();
                    tc.setCount(0);
                    temp2.Add(tc);

                }
            foreach (tokenCounter tt in temp2)
                if (!temp1.Select(a => a.getLexeme()).Contains(tt.getLexeme()))
                {
                    tokenCounter tc = tt.copy();
                    tc.setCount(0);
                    temp1.Add(tc);
                }

            foreach (tokenCounter t2 in temp2)
            {

                int a = temp1.Find(aa => aa.getLexeme() == t2.getLexeme()).getCount();
                int b = t2.getCount();
                if ((a != 0) || (b != 0))
                    l.Add(new myrow(a, b, t2.getLexeme(), c.f1.name, c.f2.name));

            }
            c.refreshResult(l);
        }
        public void funcsCountAndCodeFuncCallsComp(coupleComp c)
        {
            List<myrow> l = new List<myrow>();
            List<functionCallCounter> temp1 = new List<functionCallCounter>(c.f1.result.allCodeFunctionCalls);
            List<functionCallCounter> temp2 = new List<functionCallCounter>(c.f2.result.allCodeFunctionCalls);

            int temp1funcs = temp1.Count();
            int temp2funcs = temp2.Count();
            int temp1funcsCount = 0;
            int temp2funcsCount = 0;
            foreach (functionCallCounter tt in temp1)
                temp1funcsCount += tt.getCount();
            foreach (functionCallCounter tt in temp2)
                temp2funcsCount += tt.getCount();
            //System.Windows.Forms.MessageBox.Show(temp1funcs.ToString()+" "+ temp2funcs.ToString());
            //System.Windows.Forms.MessageBox.Show(temp1funcsCount.ToString() + " " + temp2funcsCount.ToString());
            l.Add(new myrow(temp1funcs, temp2funcs, "functions count", c.f1.name, c.f2.name));
            if (temp1funcsCount != 0 || temp2funcsCount!=0)
                l.Add(new myrow(temp1funcsCount, temp2funcsCount, "functions call count", c.f1.name, c.f2.name));
            c.refreshResult(l);
        }
        public void detrminMethod(coupleComp c, int x)
        {
            switch (x)
            {
                case 1:
                    operationsComp(c);
                    break;
                case 2:
                    keyowrdComp(c);
                    break;
                case 3:
                    dataTypesComp(c);
                    break;
                case 4:
                    valuesComp(c);
                    break;
                case 5:
                    funcsComp(c);
                    break;
                case 6:
                    librariesComp(c);
                    break;
                case 7:
                    funcsCountAndCodeFuncCallsComp(c);
                    break;
                case 8://not used XXX
                    varsComp(c);
                    break;
            }
        }
        public coupleComp compare(cppFile f1, cppFile f2, params int[] x)
        {
            coupleComp c = new coupleComp();
            c.f1 = f1;
            c.f2 = f2;
            c.file1 = f1.name;
            c.file2 = f2.name;

            foreach (int d in x)
                detrminMethod(c,d);
            return c;

        }

    }
    public class myrow
    {
        public float first_;
        public float seconde;
        public string firstfile;
        public string secondfile;
        public float similarity { get { if (seconde == 0) return 0; else if (first_ < seconde) return (float)first_ / seconde; else return (float)seconde / first_; } }
        public float credit { get { if (first_ > seconde) return first_; else return seconde; } }
        public string lexeme;
        public myrow(float first, float seconde, string lexeme, string firstfile, string secondfile)
        {
            this.firstfile = firstfile;
            this.secondfile = secondfile;
            this.first_ = first;
            this.seconde = seconde;
            this.lexeme = lexeme;
        }
    }
    public class coupleComp
    {
        public cppFile f1, f2;
        public string file1;
        public string file2;
        float ratio = 0;
        float credit = 0;
        public float similarity { get { return ratio; } }
        public float refreshResult(List<myrow> towFiles)
        {
            float ratio1 = 0;
            float credit1 = 0;
            foreach (myrow m in towFiles)
            {
                ratio1 += (float)m.similarity * (float)m.credit;
                credit1 += m.credit;
            }
            float ratio2 = this.ratio * this.credit;
            this.credit += credit1;
            this.ratio = (ratio1 + ratio2)/this.credit;
            return ratio1 / credit1;
        }
        public float getLastResult()
        { return ratio; }
    }
}
