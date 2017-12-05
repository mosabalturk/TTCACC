using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class cppFile
    {


        public result result { get; set; }

        public code code { set; get; }
        public List<token> holeCodeTokens;//done
        private string cStr;//done
        public string fname;//done
        private List<token> defines = new List<token>();
        //# define varName value
        // define.type = value of define 
        // defin.lexeme = varName
        private List<string> libraries = new List<string>();//done
        public List<string> getLibraries { get { return libraries; } }
        public List<token> getDefines { get { return defines; } }//ساويه من نوع ايدنتيفير
        
        public cppFile(string codestr, string filename)
        {
            codestr = Program.RemoveCommentsAndSpaces(codestr);//delete comments

            this.cStr = codestr;//add hole code to cStr string

            this.fname = filename;//filename or class or struct name

            Analyzer temp = new Analyzer(); // anlyzer class is the class that analyze code to tokens and lexemes

            holeCodeTokens = temp.Result2(codestr);//analyze code to tokens and lexemes to this list

            findLibrariesAndDefines();

            pointersArraysAnalzer();

            code = new code(codestr, filename, ++code.idno, holeCodeTokens,  this,"GS");
            
            code.spitYourVariablesLn(code);
            code.countAllSubScopes(code);
            setResults(code);
        }
        
        public void findLibrariesAndDefines()
        {
            if (holeCodeTokens != null)
                for (int i = 0; i < holeCodeTokens.Count; i++)
                {
                    if ((holeCodeTokens[i].getLexeme() == "#") && (holeCodeTokens[i + 1].getLexeme() == "include"))
                    {
                        string lib = "#include";
                        int cntr = 1;
                        while ((holeCodeTokens[i + cntr].getLexeme() != ">") && (holeCodeTokens[i + cntr].getType() != "vstring"))
                        { lib += holeCodeTokens[i + cntr + 1].getLexeme(); cntr++; }
                        cntr++;
                        for (int j = i; j < i + cntr - 1; j++)
                            holeCodeTokens.RemoveAt(i + 1);
                        holeCodeTokens[i].setLexeme(lib);
                        holeCodeTokens[i].setType("library");
                        libraries.Add(lib);
                    }
                    if ((holeCodeTokens[i].getLexeme() == "#") && (holeCodeTokens[i + 1].getLexeme() == "define"))
                    {
                        holeCodeTokens[i + 2].setType(holeCodeTokens[i + 3].getLexeme());
                        defines.Add(holeCodeTokens[i + 2]);
                        holeCodeTokens.RemoveAt(i);
                        holeCodeTokens.RemoveAt(i);
                        holeCodeTokens.RemoveAt(i);//error it could be operation like (2/2)
                        holeCodeTokens.RemoveAt(i);
                        i--;
                    }
                }
        }
        public void pointersArraysAnalzer()
        {
            List<token> remove = new List<token>();
            for (int i = 0; i < holeCodeTokens.Count - 1; i++)
            {

                bool structPtr = i > 1 ? ((holeCodeTokens[i - 1].getLexeme() == "struct") && (holeCodeTokens[i].isIdentifierTokenObject()) && (holeCodeTokens[i + 1].getLexeme() == "*")) : (false);
                if ((holeCodeTokens[i].isDatatype() || (structPtr)) && holeCodeTokens[i + 1].getLexeme() == "*")
                {
                    int k = 0;//  * counter
                    int j = i; //  i is data type index
                    while (holeCodeTokens[++j].getLexeme() == "*")// j is  * index
                        k++;
                    // now j is identifier index
                    if (holeCodeTokens[j].isIdentifierTokenObject())
                    {// modifiye porpreties of identifier
                        pointer p = new pointer(holeCodeTokens[j], k);
                        p.dataType = holeCodeTokens[i];
                        holeCodeTokens[j] = p;
                    }
                    i++;
                    int howManyPointers = j - i;
                    while (howManyPointers > 0)
                    {
                        token t = holeCodeTokens[i];
                        //remove all *s from result list
                        holeCodeTokens.Remove(t);
                        howManyPointers--;
                    }
                    i--;
                    continue;
                }

                bool IsarrayInit = i < holeCodeTokens.Count - 1 ? holeCodeTokens[i].getType() == "identifier" && holeCodeTokens[i + 1].getLexeme() == "[" : false;
                if (IsarrayInit)
                {
                    int j = i + 1;
                    array newArray = new array(holeCodeTokens[i]);
                    List<int> arr = new List<int>();
                    if ((i > 0) && (holeCodeTokens[i - 1].isDatatype()))
                        newArray.dataType = holeCodeTokens[i - 1];
                    while (holeCodeTokens[j].getLexeme() == "[")
                    {
                        bool arrayWithoutBoundaies = (holeCodeTokens[j].getLexeme() == "[") && (holeCodeTokens[j + 1].getLexeme() == "]");
                        if (arrayWithoutBoundaies)
                        {

                            int valu = holeCodeTokens[j + 3].getLexeme().Length - 2;
                            remove.Add(holeCodeTokens[j]);
                            remove.Add(holeCodeTokens[j + 1]);
                            newArray.arrayIndices.Add(valu);
                            j += 2;
                        }
                        else if ((holeCodeTokens[j].getLexeme() == "[") && (holeCodeTokens[j + 2].getLexeme() == "]"))
                        {

                            int valu = 0;
                            if (holeCodeTokens[j + 1].isValue())
                                valu = Convert.ToInt32(holeCodeTokens[j + 1].getLexeme());

                            if (holeCodeTokens[j + 1].isIdentifierTokenObject())
                                foreach (token t in defines)
                                    if (t.getLexeme() == holeCodeTokens[j + 1].getLexeme())
                                        valu = Convert.ToInt32(t.getType());
                            newArray.arrayIndices.Add(valu);
                            remove.Add(holeCodeTokens[j]);
                            remove.Add(holeCodeTokens[j + 1]);
                            remove.Add(holeCodeTokens[j + 2]);
                            j += 3;
                        }
                        else
                        {
                            int k = j + 1;
                            remove.Add(holeCodeTokens[j]);
                            int paran = 0;
                            while ((holeCodeTokens[k].getLexeme() != "]") || (paran != 0))
                            {
                                if (holeCodeTokens[k].getLexeme() == "[")
                                {
                                    paran++;
                                }
                                if (holeCodeTokens[k].getLexeme() == "]")
                                {
                                    paran--;
                                }
                                remove.Add(holeCodeTokens[k++]);
                            }
                            j = k;
                            remove.Add(holeCodeTokens[j]);
                            newArray.arrayIndices.Add(0);
                            j = ++k;
                        }

                    }
                    holeCodeTokens[i] = newArray;
                    i = j;
                    continue;
                }

            }
            foreach (token t in remove)
            {
                ///System.Windows.Forms.MessageBox.Show(t.id.ToString());
                holeCodeTokens.Remove(t);

            }
            //defect of this function
            // when deleting [] the code coulde be like this :
            //	   setup_duration[values[0]][values[1]]=values[2];
            //     setup_duration[values[1]][values[0]] = values[2];
            // and the result will be 
            // setup_duration = values 
            // setup_duration = values
        }

        
        public void setResults( code t)
        {
            result = new result();
            result.keyWord = t.keywordsLL1(t);
            result.operations = t.operatorsLL1(t);
            result.datatypes = t.datatypesLL1(t);
            result.values = t.ValuesLL1(t);
            result.vars = t.VarsLL1(t);
            result.pointrs = t.pointersLL1(t);
            result.arrays = t.ArraysLL1(t);
            result.libraries = libraries;
            result.specialChar = t.specialCharList(t);
            code.zeroStatics();
        }

    }

}
