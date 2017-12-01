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
        public string typedefName { get; set; }
        private List<string> libraries = new List<string>();//done
        private List<token> defines = new List<token>();
        //# define varName value
        // define.type = value of define 
        // defin.lexeme = varName
        private List<function> func_prototypes = new List<function>();//done
        private List<code> classes = new List<code>();//done
        private List<code> structes = new List<code>();//done
        private List<token> classObjects = new List<token>();
        private List<token> structObjects = new List<token>();
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
        public List<token> structObjectsGS { get { return structObjects; } }
        public List<code> Allstructs { get { return structes; } }
        public List<string> getLibraries { get { return libraries; } }
        public List<token> getDefines { get { return defines; } }
    
        public code(string codestr, string filename)
        {
            codestr = Program.RemoveCommentsAndSpaces(codestr);//delete comments

            this.cStr = codestr;//add hole code to cStr string

            this.fname = filename;//filename or class or struct name

            Analyzer temp = new Analyzer(); // anlyzer class is the class that analyze code to tokens and lexemes

            allCodeTokens = temp.Result2(codestr);//analyze code to tokens and lexemes to this list

            findLibrariesAndDefines();

            temp.pointersArraysAnalzer(allCodeTokens, getDefines,null);
            //remove *s pointers and make pointer true to the identifier that defined as pointer
            // remove [ , ] and the value between them from arrays and add them to the propreties of identifier that defined as an array
            globalScobeTokens = new List<token>(allCodeTokens); // copy allCodeTokens list
            findClassesAndStructs();
            Analyzer.structAsDatatype(allCodeTokens, globalScobeTokens, structes.Select(a => a.filename).ToList(),structes.Select(a => a.typedefName).ToList());
            temp.pointersArraysAnalzer(allCodeTokens, getDefines, globalScobeTokens);
            findMain();
            findFunctionsAndPrototypes();
        }
        public void findValDataTypesKeywsGlblVarOp()
        {
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
                        funcBody += allCodeTokens[i].getLexeme()+" ";
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
            for (int i = 0; i < allCodeTokens.Count-2; i++)
            {
                int parentheses = 0;
                int j=0;
                if (allCodeTokens[i].getLexeme() == "main")
                {
                    while (allCodeTokens[i].getLexeme() != "{")
                    {
                        i++;
                    }
                    i++;
                    while ((allCodeTokens[i].getLexeme() != "}") || (parentheses != 0))
                    {
                        if (allCodeTokens[i].getLexeme() == "{")
                            parentheses++;
                        if (allCodeTokens[i].getLexeme() == "}")
                            parentheses--;
                        i++;
                    }
                    continue;
                }
                List<token> parameters=new List<token>();
                bool func= ((allCodeTokens[i].isDatatype()) && (allCodeTokens[i + 1].isIdentifier())&&  (allCodeTokens[i + 2].getLexeme() == "("));
                bool funcWithoutDATATYPE = ((allCodeTokens[i].isIdentifier()) && (allCodeTokens[i + 1].getLexeme() == "("));// like balabla(int bla)
                //bool funcprotoType = ((tokens[i].getType() == "datatype") && (tokens[i + 1].getType() == "identifier") && (tokens[j = i + 2].getLexeme() == "("));
                // bool funcAsterisk = ((tokens[i].getType() == "datatype") && (tokens[i+1].getLexeme() == "*") && (tokens[i + 2].getType() == "identifier") && (tokens[j = i + 3].getLexeme() == "("));
                if (funcWithoutDATATYPE)
                {
                    int y = i + 2;
                    while (allCodeTokens[y].getLexeme() != ")")
                        y++;
                    if (allCodeTokens[y + 1].getLexeme() == ";")
                        funcWithoutDATATYPE = false;
                }
                if (func || funcWithoutDATATYPE)
                {
                    string datatype;
                    string funcName;
                    if (func)
                    { datatype = allCodeTokens[i].getLexeme(); funcName = allCodeTokens[i + 1].getLexeme(); }
                    else
                    {
                        datatype = "void"; funcName = allCodeTokens[i].getLexeme();
                    }

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
                            parameters.Add(new token( allCodeTokens[s + 1].getLexeme(), 0));
                        }
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
                        allCodeTokens[i + 2].setType(allCodeTokens[i + 3].getLexeme());
                        defines.Add(allCodeTokens[i + 2]);
                        allCodeTokens.RemoveAt(i);
                        allCodeTokens.RemoveAt(i);
                        allCodeTokens.RemoveAt(i);//error it could be operation like (2/2)
                        allCodeTokens.RemoveAt(i);
                        i--;
                    }
                }
            //foreach (token tt in defines)
            //    foreach (token t in allCodeTokens)
            //    {
            //        if (t.isIdentifier())
            //        {
            //            if (tt.getLexeme() == t.getLexeme())
            //            {
            //                t.setType("");
            //                t.setLexeme(tt.arrayDimensions.ToString());
            //            }
            //        }
            //    }
        }

        public void findClassesAndStructs()
        {
            for (int i = 0; i < allCodeTokens.Count-2; i++)
            {
                int parentheses = 0;
                List<token> parameters = new List<token>();
                bool Isclass =(allCodeTokens[i].getLexeme() == "class") && (allCodeTokens[i + 1].getType() == "identifier") && (allCodeTokens[i + 2].getLexeme() == "{");
                bool Isstruct = (allCodeTokens[i].getLexeme() == "struct") && (allCodeTokens[i + 1].getType() == "identifier") && (allCodeTokens[i + 2].getLexeme() == "{");

                bool typedefStruct = (allCodeTokens[i].getLexeme() == "typedef") && (allCodeTokens[i + 1].getLexeme() == "struct") && (allCodeTokens[i + 2].getLexeme() == "{");
                bool typedefStructName = i>0? (allCodeTokens[i-1].getLexeme() == "typedef") &&(allCodeTokens[i].getLexeme() == "struct") && (allCodeTokens[i + 1].getType() == "identifier") && (allCodeTokens[i + 2].getLexeme() == "{"):false;
                if (typedefStructName)
                    globalScobeTokens.Remove(allCodeTokens[i-1]);
                if (Isclass|| Isstruct|| typedefStruct)
                {
                    globalScobeTokens.Remove(allCodeTokens[i]);
                    globalScobeTokens.Remove(allCodeTokens[i+1]);
                    globalScobeTokens.Remove(allCodeTokens[i+2]);
                    string Name = allCodeTokens[i + 1].getLexeme();//it will change for typedefStruct down
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
                    //now i reffer to }
                    globalScobeTokens.Remove(allCodeTokens[i]);
                    if (Isclass)
                    {
                        classes.Add(new code(Body, Name));
                        globalScobeTokens.Remove(allCodeTokens[++i]);
                    }
                    else if (typedefStruct)
                    {
                        if ((allCodeTokens[++i].isIdentifier()) && (typedefStruct))
                        {
                            Name = allCodeTokens[i].getLexeme();
                        }
                        globalScobeTokens.Remove(allCodeTokens[i]);
                        globalScobeTokens.Remove(allCodeTokens[++i]);
                        code temp = new code(Body, Name);
                        temp.typedefName = Name;
                        structes.Add(temp);
                    }
                    else if(Isstruct)
                    {
                        code temp = new code(Body, Name);
                        structes.Add(temp);
                        if (!typedefStructName)
                            while (allCodeTokens[++i].isIdentifier())
                            {
                                structObjects.Add(new token(allCodeTokens[i].getLexeme(), "identifier"));
                                globalScobeTokens.Remove(allCodeTokens[i]);
                            }
                        else
                        {
                            if (allCodeTokens[++i].isIdentifier())
                            {
                                temp.typedefName = allCodeTokens[i].getLexeme();
                                globalScobeTokens.Remove(allCodeTokens[i++]);//i++ not ++i they are different ! MUS
                            }
                        }
                        globalScobeTokens.Remove(allCodeTokens[i]);
                    }
                    
                }
            }
        }
        
    }

    // The class Tibi is working on
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
