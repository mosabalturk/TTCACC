using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    class MusComp
    {
        public List<coupleComp> couples = new List<coupleComp>();

        public void CompareCppFiles(List<cppFile> allFilesList,int x)
        {
            for (int i = 0; i < allFilesList.Count - 1; i++)
            {
                for (int j = i + 1; j < allFilesList.Count; j++)
                {
                    couples.Add(compare(allFilesList[i], allFilesList[j],x));
                }
            }
        }
        public coupleComp compare(cppFile f1, cppFile f2,int x)
        {
            coupleComp c = new coupleComp();
            List<myrow> l = new List<myrow>();
            c.file1 = f1.name;
            c.file2 = f2.name;
            List<tokenCounter> temp1 = new List<tokenCounter>(f1.result.operationsAllFile);
            List<tokenCounter> temp2 = new List<tokenCounter>(f2.result.operationsAllFile);
            switch (x)
            {
                case 1:
                    break;
                case 2:
                     temp1 = new List<tokenCounter>(f1.result.keyWordsAllFile);
                     temp2 = new List<tokenCounter>(f2.result.keyWordsAllFile);
                    break;
                case 3:
                    temp1 = new List<tokenCounter>(f1.result.dataTypesAllFile);
                    temp2 = new List<tokenCounter>(f2.result.dataTypesAllFile);
                    break;
                //case 4:
                //    temp1 = new List<tokenCounter>(f1.result.valuesAllFile);
                //    temp2 = new List<tokenCounter>(f2.result.valuesAllFile);
                //    break;
                case 5:
                    temp1 = temp1.Concat(f1.result.keyWordsAllFile).Concat(f1.result.dataTypesAllFile).ToList()/*Concat(f1.result.valuesAllFile)*/;
                    temp2 = temp2.Concat(f2.result.keyWordsAllFile).Concat(f2.result.dataTypesAllFile).ToList()/*Concat(f2.result.valuesAllFile)*/;
                    break;
            }
            foreach (tokenCounter tt in temp1)
                if (!temp2.Select(a => a.getLexeme()).Contains(tt.getLexeme()))
                {
                    tokenCounter tc = new tokenCounter(new token(tt.getLexeme()));
                    tc.setCount(0);
                    temp2.Add(tc);

                }
            foreach (tokenCounter tt in temp2)
                if (!temp1.Select(a => a.getLexeme()).Contains(tt.getLexeme()))
                {
                    tokenCounter tc = new tokenCounter(new token(tt.getLexeme()));
                    tc.setCount(0);
                    temp1.Add(tc);
                }

            foreach (tokenCounter t2 in temp2)
            {
                if (temp1.Select(a => a.getLexeme()).Contains(t2.getLexeme()))
                {

                    int a = temp1.Find(aa => aa.getLexeme() == t2.getLexeme()).getCount();
                    int b = t2.getCount();
                    
                        l.Add(new myrow(a, b, t2.getLexeme(), f1.name, f2.name));

                }

            }
                c.towFiles = l;
                return c;
            
        }
        public class myrow
        {
            public int first_;
            public int seconde;
            public string firstfile;
            public string secondfile;
            public float similarity { get { if (seconde == 0) return 0; else if (first_ < seconde) return (float)first_ / seconde; else return (float)seconde / first_; } }
            public int credit { get { if (first_ > seconde) return first_; else return seconde; } }
            public string lexeme;
            public myrow(int first, int seconde, string lexeme, string firstfile, string secondfile)
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
            public string file1;
            public string file2;

            public List<myrow> towFiles = new List<myrow>();
            public float similarity { get { return getLastResult(); } }
            public float getLastResult()
            {
                string s = "";
                float ratio = 0;
                int credit = 0;
                foreach (myrow m in towFiles)
                {
                    s += m.first_.ToString() + " " + m.seconde.ToString() + "  " + m.credit.ToString() + "   " + m.similarity.ToString("R") + " " + m.lexeme + "\n";
                    ratio += (float)m.similarity * (float)m.credit;
                    credit += m.credit;
                }
                //System.Windows.Forms.MessageBox.Show(s);
                ratio /= credit;
                return ratio;
            }
        }
    }
}
