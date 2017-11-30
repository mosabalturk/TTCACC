using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{

    public class code
    {
        List<token> allCodeTokens;//done
        List<token> globalScobeTokens;//no
        private string cStr;//done
        private string fname;//done
        private List<string> libraries = new List<string>();//done
        private List<token> defines = new List<token>();//done
        private List<function> func_prototypes = new List<function>();//done
        private List<code> classes = new List<code>();//done
        private List<code> structes = new List<code>();//done
        private List<token> GlobalScobeGlobalVariablesCounter = new List<token>();
        private List<token> GlobalScobeKeyWordsCounter = new List<token>();
        private List<token> GlobalScobeDataTypesCounter = new List<token>();
        private List<token> GlobalScobeValuesCounter = new List<token>();
        private List<token> GlobalScobeOperationsCounter = new List<token>();
        private List<function> functions = new List<function>();//done
        public function main { get; set; }//done?
        public List<function> Allfunctions { get { return functions; } }
        public List<function> Allfunc_prototypes { get { return func_prototypes; } }
        public List<code> Allclasses { get { return classes; } }
        public List<code> Allstructs { get { return structes; } }
        public List<string> getLibraries { get { return libraries; } }
        public List<token> getDefines { get { return defines; } }
        public void findValDataTypesKeywsGlblVarOp()
        {

        }
        public code(string codestr, string filename)
        {
            codestr = Program.RemoveCommentsAndSpaces(codestr);
            this.cStr = codestr;
            this.fname = filename;
            Analyzer temp = new Analyzer();
            allCodeTokens = temp.Result2(codestr);
            allCodeTokens = temp.PAanlysiss(allCodeTokens);
            findLibrariesAndDefines();
            //globalScobeTokens = allCodeTokens.Select(a=>a.Copy()).ToList();
            globalScobeTokens = new List<token>(allCodeTokens); // copy allCodeTokens list
            findClassesAndStructs();
            Analyzer.structAsIdentifiers(allCodeTokens, globalScobeTokens, structes.Select(a => a.filename).ToList());
            //globalScobeTokens = Analyzer.newIdanalysiss(globalScobeTokens, structes.Select(a => a.filename).ToList());
            findMain();
            findFunctionsAndPrototypes();
            
            
        }


        public void findMain()
        {
            int parentheses = 0;
            string datatype = "";
            List<token> funcTokens = new List<token>();
            string funcBody = "";
            for (int i = 0; i < allCodeTokens.Count; i++)
            {
                if (allCodeTokens[i].getLexeme() == "main")
                {

                    {
                        globalScobeTokens.Remove(allCodeTokens[i]);
                        if (allCodeTokens[i - 1].getType() == "datatype")
                        {
                            datatype = allCodeTokens[i - 1].getLexeme();
                            globalScobeTokens.Remove(allCodeTokens[i-1]);
                        }
                    }
                    i++;
                    globalScobeTokens.Remove(allCodeTokens[i]);
                    while (allCodeTokens[i].getLexeme() != "{")
                    {
                        globalScobeTokens.Remove(allCodeTokens[i]);
                        i++;
                    }
                    globalScobeTokens.Remove(allCodeTokens[i]);
                    i++;
                    while ((allCodeTokens[i].getLexeme() != "}") || (parentheses != 0))
                    {
                        funcBody += allCodeTokens[i].getLexeme();
                        funcTokens.Add(allCodeTokens[i]);
                        globalScobeTokens.Remove(allCodeTokens[i]);
                        if (allCodeTokens[i].getLexeme() == "{")
                            parentheses++;
                        if (allCodeTokens[i].getLexeme() == "}")
                            parentheses--;
                        i++;
                    }
                    globalScobeTokens.Remove(allCodeTokens[i]);
                    main = new function(funcBody, funcTokens,new List<token>(), datatype, "main");
                    main.funcDataType = datatype;
                    break;
                }
            }
        }
        public void findFunctionsAndPrototypes()
        {
            for (int i = 0; i < allCodeTokens.Count; i++)
            {
                int parentheses = 0;
                int j=0;
                
                List<token> parameters=new List<token>();
                bool func= ((allCodeTokens[i].isDatatype()) && (allCodeTokens[i + 1].isIdentifier())&&  (allCodeTokens[i + 2].getLexeme() == "("));
                bool funcWithoutDATATYPE = ((allCodeTokens[i].isIdentifier()) && (allCodeTokens[i + 1].getLexeme() == "("));
                //bool funcprotoType = ((tokens[i].getType() == "datatype") && (tokens[i + 1].getType() == "identifier") && (tokens[j = i + 2].getLexeme() == "("));
                // bool funcAsterisk = ((tokens[i].getType() == "datatype") && (tokens[i+1].getLexeme() == "*") && (tokens[i + 2].getType() == "identifier") && (tokens[j = i + 3].getLexeme() == "("));


                if (func || funcWithoutDATATYPE)
                {
                    globalScobeTokens.Remove(allCodeTokens[i]);
                    globalScobeTokens.Remove(allCodeTokens[i + 1]);
                    if (func)
                    { j = i + 3; globalScobeTokens.Remove(allCodeTokens[i + 2]); }
                    else
                        j = i + 2;
                    bool prototype = false;
                    int k = j;
                    while (allCodeTokens[k].getLexeme() != ")")
                    {
                        globalScobeTokens.Remove(allCodeTokens[k]);
                        k++;
                    }
                    globalScobeTokens.Remove(allCodeTokens[k]);// deleting )
                    if ((k + 1 < allCodeTokens.Count))
                        if (allCodeTokens[k + 1].getLexeme() == ";")// void func (int blabla);
                        {
                            prototype = true;
                            globalScobeTokens.Remove(allCodeTokens[k + 1]);
                        }
                    //parameters are frome j to k
                    for (int s = j; s < k; s++)
                    {//خطأ عندما يكون الباراميتر ستركت أو بوينتر او مصفوفة
                        if ((allCodeTokens[s].isDatatype() && (allCodeTokens[s + 1].getType() == "identifier")))
                        {
                            parameters.Add(new token(allCodeTokens.Count + 1, allCodeTokens[s + 1].getLexeme(), 0));
                        }
                    }
                    string datatype;
                    string funcName;
                    if (func)
                    { datatype = allCodeTokens[i].getLexeme(); funcName = allCodeTokens[i + 1].getLexeme(); }
                    else
                    {
                        datatype = "void"; funcName = allCodeTokens[i].getLexeme();
                    }

                    if (!prototype)//function
                    {
                        string funcBody = "";
                        List<token> funcTokens = new List<token>();
                        while (allCodeTokens[j].getLexeme() != "{")
                            j++;
                        globalScobeTokens.Remove(allCodeTokens[j]);
                        j++;
                        while ((allCodeTokens[j].getLexeme() != "}") || (parentheses != 0))
                        {
                            
                            funcBody += allCodeTokens[j].getLexeme()+" ";
                            funcTokens.Add(allCodeTokens[j]);
                            globalScobeTokens.Remove(allCodeTokens[j]);
                            if (allCodeTokens[j].getLexeme() == "{")
                                parentheses++;
                            if (allCodeTokens[j].getLexeme() == "}")
                                parentheses--;
                            j++;
                        }
                        globalScobeTokens.Remove(allCodeTokens[j]);
                        i = j-1;
                        functions.Add(new function(funcBody, funcTokens, parameters,datatype,funcName));

                    }
                    else
                    {
                        func_prototypes.Add(new function(parameters, datatype, funcName, true));
                    }
                }
            }
        }
        public List<token> getAllTokens() { return allCodeTokens; }
        public List<token> getGSTokens() { return globalScobeTokens; }
        public string codeAsStr { get { return cStr; } set { cStr = value; } }
        public string filename { get { return fname; } set { fname = value; } }
        public void findLibrariesAndDefines()
        {
            if (allCodeTokens!=null)
                for (int i=0;i<allCodeTokens.Count;i++)
                {
                    if ((allCodeTokens[i].getLexeme() == "#") && (allCodeTokens[i + 1].getLexeme() == "include"))
                    {
                        string lib = "#include";
                        int cntr = 1;
                        while ((allCodeTokens[i+cntr].getLexeme()!=">")&&(allCodeTokens[i + cntr].getType() != "vstring"))
                        { lib += allCodeTokens[i + cntr+1].getLexeme(); cntr++; }
                        cntr++;
                        for (int j = i; j < i + cntr-1; j++)
                            allCodeTokens.RemoveAt(i+1);
                        allCodeTokens[i].setLexeme(lib);
                        allCodeTokens[i].setType("library");
                        libraries.Add(lib);
                    }
                    if ((allCodeTokens[i].getLexeme() == "#") && (allCodeTokens[i + 1].getLexeme() == "define"))
                    {
                        
                        defines.Add(new token(allCodeTokens.Count+1,allCodeTokens[i + 2].getLexeme(), 0));
                        allCodeTokens.RemoveAt(i);
                        allCodeTokens.RemoveAt(i);
                        allCodeTokens.RemoveAt(i);//error it could be operation like (2/2)
                        allCodeTokens.RemoveAt(i);
                    }
                }
        }
        public void findClassesAndStructs()
        {
            for (int i = 0; i < allCodeTokens.Count; i++)
            {
                int parentheses = 0;
                List<token> parameters = new List<token>();
                bool Isclass =(allCodeTokens[i].getLexeme() == "class") && (allCodeTokens[i + 1].getType() == "identifier") && (allCodeTokens[i + 2].getLexeme() == "{");
                bool Isstruct = (allCodeTokens[i].getLexeme() == "struct") && (allCodeTokens[i + 1].getType() == "identifier") && (allCodeTokens[i + 2].getLexeme() == "{");
                if (Isclass|| Isstruct)
                {
                    globalScobeTokens.Remove(allCodeTokens[i]);
                    globalScobeTokens.Remove(allCodeTokens[i+1]);
                    globalScobeTokens.Remove(allCodeTokens[i+2]);
                    string Name = allCodeTokens[i + 1].getLexeme();
                    i += 3;
                    string Body = "";
                    List<token> Tokens = new List<token>();
                    while ((allCodeTokens[i].getLexeme() != "}") || (parentheses != 0))
                    {
                        Body += allCodeTokens[i].getLexeme()+" ";
                        Tokens.Add(allCodeTokens[i]);
                        globalScobeTokens.Remove(allCodeTokens[i]);
                        if (allCodeTokens[i].getLexeme() == "{")
                            parentheses++;
                        if (allCodeTokens[i].getLexeme() == "}")
                            parentheses--;
                        i++;
                    }
                    globalScobeTokens.Remove(allCodeTokens[i]);
                    globalScobeTokens.Remove(allCodeTokens[i+1]);
                    i += 1;
                    if (Isclass)
                        classes.Add(new code(Body, Name));
                    else
                        structes.Add(new code(Body, Name));
                }
            }
        }
        //public static void findClassesOrStructs(string strSource, string strStart)
        //{
        //    string strEnd = "};";
        //    int Start, End;
        //    bool f = false;
        //    if (strSource.Contains(strStart) )
        //    {
        //        f = true;
        //        Start = strSource.IndexOf(strStart, 0)+6;
        //        End = strSource.IndexOf("};", Start)+2;
        //        int tEnd;
        //        while (((tEnd = strSource.IndexOf(strStart, End)) != -1))
        //            if (tEnd > End)
        //                break;
        //            else
        //                End = strSource.IndexOf(strEnd, tEnd)+2;
        //        string get = strSource.Substring(Start, End - Start);//CLASS 
        //        System.Windows.Forms.MessageBox.Show(get);
        //        if (!f)
        //            return ;
        //        else findClassesOrStructs(strSource.Replace(get,""), strStart);
        //    }
        //    else
        //    {
        //        return ;
        //    }
        //}
    }
    public class function 
    {
        bool protoType = false;
        public bool recursive { get; set; }
        private string fname;
        private string funcBody;
        public string funcDataType { get; set; }
        List<token> tokens;
        public void setTokens(List<token> funcTokens) { tokens = funcTokens; }
        private List<token> parameters = new List<token>();
        private List<token> global_vars = new List<token>();
        private List<token> identifiers = new List<token>();
        private List<token> operations = new List<token>();
        private List<token> keyWords = new List<token>();
        private List<token> DataTypes = new List<token>();
        private List<token> values = new List<token>();
        private List<token> funcCalls = new List<token>();
        public string funcAsStr { get { return funcBody; } set { funcBody = value; } }
        public List<token> funcParameters { get { return parameters; }  }
        public function(string code, List<token> tokens,List<token> parameters, string datatype, string funcName)
        {
            this.funcBody = code;
            this.tokens = tokens;
            this.parameters = parameters;
            this.funcDataType = datatype;
            this.fname = funcName;
            for (int j=0;j<tokens.Count();j++)
                if ((tokens[j].getLexeme() == "return") && (tokens[j + 1].getLexeme() == funcName))
                    this.recursive = true;

        }
        public function(List<token> parameters, string datatype, string funcName, bool protoType = true)
        {
            this.protoType = protoType;
            this.parameters = parameters;
            this.funcDataType = datatype;
            this.fname = funcName;
        }
        public string funcname { get { return fname; } set { fname = value; } }

    }
}
