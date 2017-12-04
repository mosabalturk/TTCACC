using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Analyzer
{

    public class code
    {
        public static string[] lines = new string[100];
        public static int linecounter = 0;

        public string type { get; set; }
        public bool isStruct { get { if (this.type == "struct") return true; else return false; } }
        public bool isGS { get { if (this.type == "GS") return true; else return false; } }
        public bool isclass { get { if (this.type == "class") return true; else return false; } }

        List<identifier> thisScopeVars = new List<identifier>();
        List<identifier> upperLevelVar = new List<identifier>();
        List<pointer> upperLevelPointers = new List<pointer>();
        List<pointer> thisScopePointers = new List<pointer>();
        List<array> upperLevelArray = new List<array>();
        List<array> thisScopeArray = new List<array>();
        static List<string> AllstructesClassesNames = new List<string>();
        static List<string> AlltypdefNames = new List<string>();


        public static List<scopeTokenCounter> scopeTokenCounterList1 = new List<scopeTokenCounter>();
        public static List<scopeTokenCounter> scopeTokenCounterList2 = new List<scopeTokenCounter>();
        public static List<scopeTokenCounter> scopeTokenCounterList3 = new List<scopeTokenCounter>();


        public static void zeroStatics() {
            AlltypdefNames = new List<string>();
            AllstructesClassesNames = new List<string>();
            idno = 0;
            scopeTokenCounterList1.Clear();
            scopeTokenCounterList2.Clear();
            scopeTokenCounterList3.Clear();
        }


        List<token> thisCodeToken;//done
        public List<token> holeCodeTokens;
        List<token> globalScobeTokens;//no
        private string cStr;//done
        private string fname;//done
        public string typedefName { get; set; }

        private List<function> func_prototypes = new List<function>();//done
        public List<code> classes = new List<code>();//done
        private List<code> structes = new List<code>();//done
        private List<token> classObjects = new List<token>();
        private List<token> structObjects = new List<token>();

        private List<variableCounter> variablesCounterGS = new List<variableCounter>();
        private List<pointerCounter> pointersCounterGS = new List<pointerCounter>();
        private List<arrayCounter> arraysCounterGS = new List<arrayCounter>();
        private List<tokenCounter> KeyWordsCounterGS = new List<tokenCounter>();
        private List<tokenCounter> dataTypesCounterGS = new List<tokenCounter>();
        private List<tokenCounter> valuesCounterGS = new List<tokenCounter>();
        private List<tokenCounter> operationsCounterGS = new List<tokenCounter>();
        private List<function> functions = new List<function>();//done
        public function main { get; set; }//done?
        public List<function> Allfunctions { get { return functions; } }
        public List<function> Allfunc_prototypes { get { return func_prototypes; } }
        public List<code> Allclasses { get { return classes; } }
        public List<token> structObjectsGS { get { return structObjects; } }
        public List<code> Allstructs { get { return structes; } }

        #region get lists
        public List<tokenCounter> getOperationsCounterGS { get { return operationsCounterGS; } }
        public List<tokenCounter> getDataTypesCounterGS { get { return dataTypesCounterGS; } }
        public List<tokenCounter> getValuesCounterGS { get { return valuesCounterGS; } }
        public List<tokenCounter> getKeyWordsCounterGS { get { return KeyWordsCounterGS; } }
        public List<arrayCounter> getArraysCounterGS { get { return arraysCounterGS; } }
        public List<pointerCounter> getPointersCounterGS { get { return pointersCounterGS; } }
        public List<variableCounter> getVariablesCounterGS { get { return variablesCounterGS; } }
        public List<List<token>> allGlobalVariables = new List<List<token>>();

        #endregion




        public static int idno = 0;
        public int ScopeId;
        public int containingScopeId;


        
        public code(string codestr, string filename,int containingScopeId, List<token> thisCodeToken, ref List<token> holeCodeTokens, List<identifier> upperLevelVar, List<pointer> upperLevelPointers, List<array> upperLevelArray,string type)
        {
            this.ScopeId = idno++;
            this.containingScopeId = containingScopeId;

            this.type = type;

            this.thisCodeToken = thisCodeToken;
            this.holeCodeTokens = holeCodeTokens;
            this.upperLevelVar = upperLevelVar;
            this.upperLevelPointers = upperLevelPointers;
            this.upperLevelArray = upperLevelArray;

            codestr = Program.RemoveCommentsAndSpaces(codestr);//delete comments

            this.cStr = codestr;//add hole code to cStr string

            this.fname = filename;//filename or class or struct name

            Analyzer temp = new Analyzer(); // anlyzer class is the class that analyze code to tokens and lexemes
            globalScobeTokens = new List<token>(this.thisCodeToken); // copy allCodeTokens list

            findClassesAndStructs();
            Analyzer.structAsDatatype(this.thisCodeToken, AllstructesClassesNames, AlltypdefNames);

            findMain();
            findFunctionsAndPrototypes();
            
        }
        public void count()
        {
            foreach (token t in globalScobeTokens)
            {
                if (t.isOperation())
                    tokenCounter.AddOneByLexeme(t, operationsCounterGS);
                else if (t.isKeyword())
                { tokenCounter.AddOneByLexeme(t, KeyWordsCounterGS);}
                else if (t.isDatatype())
                    tokenCounter.AddOneByLexeme(t, dataTypesCounterGS);
                else if (t.isValue())
                    tokenCounter.AddOneValueByDataType(t, valuesCounterGS);
                //else if (t.isIdentifier())
                //    token.addOne(t, variablesCounterGS, true);
                //else if (t.isPointer)
                //    token.addOne(t, pointersCounterGS, true);
                //else if (t.isArray)
                //    token.addOne(t, arraysCounterGS, true);
            }
        }
        public void findVar()
        {
            Analyzer.structAsDatatype( this.globalScobeTokens, AllstructesClassesNames, AlltypdefNames);
            //foreach(string f in AllstructesClassesNames)
            //    System.Windows.Forms.MessageBox.Show(f);
            for (int i = 0; i < globalScobeTokens.Count; i++)
            {
                if ((globalScobeTokens[i].isIdentifierTokenObject()) && !globalScobeTokens[i].isPointer && !globalScobeTokens[i].isArray)
                {
                    identifier id;

                    if ((i>0)&&globalScobeTokens[i].isIdentifierTokenObject()&&(globalScobeTokens[i-1].isDatatype()))
                    {

                        id = new identifier(globalScobeTokens[i]);
                        if ((i > 0) && (globalScobeTokens[i - 1].isDatatype()))
                            id.dataType = globalScobeTokens[i - 1];
                        thisScopeVars.Add(id);

                        for (int q = 0; q < thisCodeToken.Count; q++)
                            if (thisCodeToken[q].id == globalScobeTokens[i].id)
                                thisCodeToken[q] = id;

                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count) ; q++)
                            if (holeCodeTokens[q].id == globalScobeTokens[i].id)
                                holeCodeTokens[q] = id;

                        globalScobeTokens[i] = id;
                    }
                    else if (thisScopeVars.Select(a => a.getLexeme()).ToList().Contains(globalScobeTokens[i].getLexeme()))
                    {
                        id = thisScopeVars.Find(a => a.getLexeme() == globalScobeTokens[i].getLexeme ());

                        for (int q = 0; q < thisCodeToken.Count; q++)
                            if (thisCodeToken[q].id == globalScobeTokens[i].id)
                                thisCodeToken[q] = id;
                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count) ; q++)
                            if (holeCodeTokens[q].id == globalScobeTokens[i].id)
                                holeCodeTokens[q] = id;
                        globalScobeTokens[i] = id;
                    }
                    else if (upperLevelVar.Select(a => a.getLexeme()).ToList().Contains(globalScobeTokens[i].getLexeme()))
                    {
                        id = upperLevelVar.Find(a => a.getLexeme() == globalScobeTokens[i].getLexeme());
                        for (int q = 0; q < thisCodeToken.Count; q++)
                            if (thisCodeToken[q].id == globalScobeTokens[i].id)
                                thisCodeToken[q] = id;
                        for (int q = 0;  (holeCodeTokens != null) && (q < holeCodeTokens.Count) ; q++)
                            if (holeCodeTokens[q].id == globalScobeTokens[i].id)
                                holeCodeTokens[q] = id;
                        globalScobeTokens[i] = id;

                    }

                    else 
                    {
                        id = new identifier(globalScobeTokens[i]);
                        if ((i > 0) && (globalScobeTokens[i - 1].isDatatype()))
                            id.dataType = globalScobeTokens[i - 1];
                        thisScopeVars.Add(id);
                        for (int q = 0; q < thisCodeToken.Count; q++)
                            if (thisCodeToken[q].id == globalScobeTokens[i].id)
                                thisCodeToken[q] = id;
                        for (int q = 0; (q < holeCodeTokens.Count) && (holeCodeTokens != null); q++)
                            if (holeCodeTokens[q].id == globalScobeTokens[i].id)
                                holeCodeTokens[q] = id;
                        globalScobeTokens[i] = id;

                    }
                }
            }
        }
        List<identifier> getHoleVars { get { return upperLevelVar.Concat(thisScopeVars).ToList(); } }
        public void findMain()
        {
            int parentheses = 0;
            string datatype = "";
            List<token> funcTokens = new List<token>();
            string funcBody = "";
            for (int i = 0; i < thisCodeToken.Count; i++)
            {
                if (thisCodeToken[i].getLexeme() == "main")
                {

                    {
                        globalScobeTokens.Remove(thisCodeToken[i]);
                        if (thisCodeToken[i - 1].getType() == "datatype")
                        {
                            datatype = thisCodeToken[i - 1].getLexeme();
                            globalScobeTokens.Remove(thisCodeToken[i - 1]);
                        }
                    }
                    i++;
                    globalScobeTokens.Remove(thisCodeToken[i]);
                    while (thisCodeToken[i].getLexeme() != "{")
                    {
                        globalScobeTokens.Remove(thisCodeToken[i]);
                        i++;
                    }
                    globalScobeTokens.Remove(thisCodeToken[i]);
                    i++;
                    while ((thisCodeToken[i].getLexeme() != "}") || (parentheses != 0))
                    {
                        funcBody += thisCodeToken[i].getLexeme() + " ";
                        funcTokens.Add(thisCodeToken[i]);
                        globalScobeTokens.Remove(thisCodeToken[i]);
                        if (thisCodeToken[i].getLexeme() == "{")
                            parentheses++;
                        if (thisCodeToken[i].getLexeme() == "}")
                            parentheses--;
                        i++;
                    }

                    main = new function(funcBody,this.ScopeId, ref funcTokens, new List<identifier>(), datatype, "main",ref thisCodeToken, thisScopeVars, thisScopePointers.Concat(upperLevelPointers).ToList(), thisScopeArray.Concat(upperLevelArray).ToList());
                    main.funcDataType = datatype;
                    functions.Add(main);
                    break;
                }
            }
            
        }
        //public void findFunctionsAndPrototypes()
        //{
        //    for (int i = 0; i < thisCodeToken.Count - 2; i++)
        //    {
        //        int parentheses = 0;
        //        int j = 0;
        //        if (thisCodeToken[i].getLexeme() == "main")
        //        {
        //            while (thisCodeToken[i].getLexeme() != "{")
        //            {
        //                i++;
        //            }
        //            i++;
        //            while ((thisCodeToken[i].getLexeme() != "}") || (parentheses != 0))
        //            {
        //                if (thisCodeToken[i].getLexeme() == "{")
        //                    parentheses++;
        //                if (thisCodeToken[i].getLexeme() == "}")
        //                    parentheses--;
        //                i++;
        //            }
        //            continue;
        //        }
        //        List<identifier> parameters = new List<identifier>();
        //        bool id = thisCodeToken[i + 1].isPointer || thisCodeToken[i + 1].isArray || thisCodeToken[i + 1].isIdentifierTokenObject() || thisCodeToken[i + 1].isIdentifierTokenObject();
        //        bool func = ((thisCodeToken[i].isDatatype()) && id && (thisCodeToken[i + 2].getLexeme() == "("));
        //        bool funcWithoutDATATYPE = (id) && (thisCodeToken[i + 1].getLexeme() == "(");// like balabla(int bla)
        //        //bool funcprotoType = ((tokens[i].getType() == "datatype") && (tokens[i + 1].getType() == "identifier") && (tokens[j = i + 2].getLexeme() == "("));
        //        // bool funcAsterisk = ((tokens[i].getType() == "datatype") && (tokens[i+1].getLexeme() == "*") && (tokens[i + 2].getType() == "identifier") && (tokens[j = i + 3].getLexeme() == "("));
        //        if (funcWithoutDATATYPE)
        //        {
        //            int y = i + 2;
        //            while (thisCodeToken[y].getLexeme() != ")")
        //                y++;
        //            if (thisCodeToken[y + 1].getLexeme() == ";")
        //                funcWithoutDATATYPE = false;
        //        }
        //        if (func || funcWithoutDATATYPE)
        //        {
        //            string datatype;
        //            string funcName;
        //            if (func)
        //            { datatype = thisCodeToken[i].getLexeme(); funcName = thisCodeToken[i + 1].getLexeme(); }
        //            else
        //            {
        //                datatype = "void"; funcName = thisCodeToken[i].getLexeme();
        //            }

        //            globalScobeTokens.Remove(thisCodeToken[i]);
        //            globalScobeTokens.Remove(thisCodeToken[i + 1]);
        //            if (func)
        //            { j = i + 3; globalScobeTokens.Remove(thisCodeToken[i + 2]); }
        //            else
        //                j = i + 2;
        //            bool prototype = false;
        //            int k = j;
        //            while (thisCodeToken[k].getLexeme() != ")")
        //            {
        //                globalScobeTokens.Remove(thisCodeToken[k]);
        //                k++;
        //            }
        //            globalScobeTokens.Remove(thisCodeToken[k]);// deleting )
        //            if ((k + 1 < thisCodeToken.Count))
        //                if (thisCodeToken[k + 1].getLexeme() == ";")// void func (int blabla);
        //                {
        //                    prototype = true;
        //                    globalScobeTokens.Remove(thisCodeToken[k + 1]);
        //                }
        //            //parameters are frome j to k
        //            for (int s = j; s < k; s++)
        //            {//خطأ عندما يكون الباراميتر ستركت أو بوينتر او مصفوفة
        //                if ((thisCodeToken[s].isDatatype() && (thisCodeToken[s + 1].getType() == "identifier")))
        //                {
        //                    identifier p1 = new identifier(thisCodeToken[s + 1]);
        //                    if (thisCodeToken[s].isDatatype())
        //                        p1.dataType = thisCodeToken[s];
        //                    parameters.Add(p1);
        //                }
        //            }

        //            if (!prototype)//function
        //            {
        //                string funcBody = "";
        //                List<token> funcTokens = new List<token>();
        //                while (thisCodeToken[j].getLexeme() != "{")
        //                    j++;
        //                globalScobeTokens.Remove(thisCodeToken[j]);
        //                j++;
        //                while ((thisCodeToken[j].getLexeme() != "}") || (parentheses != 0))
        //                {

        //                    funcBody += thisCodeToken[j].getLexeme() + " ";
        //                    funcTokens.Add(thisCodeToken[j]);
        //                    globalScobeTokens.Remove(thisCodeToken[j]);
        //                    if (thisCodeToken[j].getLexeme() == "{")
        //                        parentheses++;
        //                    if (thisCodeToken[j].getLexeme() == "}")
        //                        parentheses--;
        //                    j++;
        //                }
        //                globalScobeTokens.Remove(thisCodeToken[j]);
        //                i = j - 1;
        //                List<identifier> temp1 = new List<identifier>(thisScopeVars);
        //                foreach (identifier t in upperLevelVar)
        //                    temp1.Add(t);
        //                List<pointer> temp2 = new List<pointer>(thisScopePointers);
        //                foreach (pointer t in upperLevelPointers)
        //                    temp2.Add(t);
        //                List<array> temp3 = new List<array>(thisScopeArray);
        //                foreach (array t in upperLevelArray)
        //                    temp3.Add(t);
        //                functions.Add(new function(funcBody, this.ScopeId, ref funcTokens, parameters, datatype, funcName, ref thisCodeToken, thisScopeVars, temp2, temp3));

        //            }
        //            else
        //            {
        //                func_prototypes.Add(new function(parameters, this.ScopeId, datatype, funcName, true));
        //            }
        //        }
        //    }
        //}

        public void findFunctionsAndPrototypes()
        {
            List<token> remove = new List<token>();
            for (int i = 0; i < globalScobeTokens.Count - 2; i++)
            {
                int parentheses = 0;
                int j = 0;
                if (globalScobeTokens[i].getLexeme() == "main")
                {
                    while (globalScobeTokens[i].getLexeme() != "{")
                    {
                        i++;
                    }
                    i++;
                    while ((globalScobeTokens[i].getLexeme() != "}") || (parentheses != 0))
                    {
                        if (globalScobeTokens[i].getLexeme() == "{")
                            parentheses++;
                        if (globalScobeTokens[i].getLexeme() == "}")
                            parentheses--;
                        i++;
                    }
                    continue;
                }
                List<identifier> parameters = new List<identifier>();
                bool id = globalScobeTokens[i + 1].isPointer || globalScobeTokens[i + 1].isArray || globalScobeTokens[i + 1].isIdentifierTokenObject() || globalScobeTokens[i + 1].isIdentifierTokenObject();
                bool id2 = globalScobeTokens[i].isPointer || globalScobeTokens[i].isArray || globalScobeTokens[i ].isIdentifierTokenObject() || globalScobeTokens[i].isIdentifierTokenObject();
                bool func = ((globalScobeTokens[i].isDatatype()) && id && (globalScobeTokens[i + 2].getLexeme() == "("));
                bool funcWithoutDATATYPE = (id2) && (globalScobeTokens[i + 1].getLexeme() == "(");// like balabla(int bla)
                //bool funcprotoType = ((tokens[i].getType() == "datatype") && (tokens[i + 1].getType() == "identifier") && (tokens[j = i + 2].getLexeme() == "("));
                // bool funcAsterisk = ((tokens[i].getType() == "datatype") && (tokens[i+1].getLexeme() == "*") && (tokens[i + 2].getType() == "identifier") && (tokens[j = i + 3].getLexeme() == "("));
                if (funcWithoutDATATYPE)
                {
                    int y = i + 2;
                    while (globalScobeTokens[y].getLexeme() != ")")
                        y++;
                    if (globalScobeTokens[y + 1].getLexeme() == ";")
                        funcWithoutDATATYPE = false;
                }
                if (func || funcWithoutDATATYPE)
                {
                    string datatype;
                    string funcName;
                    if (func)
                    { datatype = globalScobeTokens[i].getLexeme(); funcName = globalScobeTokens[i + 1].getLexeme(); }
                    else
                    {
                        datatype = "void"; funcName = globalScobeTokens[i].getLexeme();
                    }
                    remove.Add(globalScobeTokens[i]);
                    remove.Add(globalScobeTokens[i+1]);
                    if (func)
                    {
                        j = i + 3;;
                        remove.Add(globalScobeTokens[i + 2]);

                    }
                    else
                        j = i + 2;
                    bool prototype = false;
                    int k = j;
                    while ((k< globalScobeTokens.Count) &&(globalScobeTokens[k].getLexeme() != ")"))
                    {
                        remove.Add(globalScobeTokens[k]);
                        k++;
                    }
                    remove.Add(globalScobeTokens[k]);
                    // deleting )
                    if ((k + 1 < globalScobeTokens.Count))
                        if (globalScobeTokens[k + 1].getLexeme() == ";")// void func (int blabla);
                        {
                            prototype = true;
                            remove.Add(globalScobeTokens[k+1]);
                        }
                    //parameters are frome j to k
                    for (int s = j; (s < k)&&((k + 1 < globalScobeTokens.Count)); s++)
                    {//خطأ عندما يكون الباراميتر ستركت أو بوينتر او مصفوفة
                        if ((globalScobeTokens[s].isDatatype() && (globalScobeTokens[s + 1].getType() == "identifier")))
                        {
                            identifier p1 = new identifier(globalScobeTokens[s + 1]);
                            if (globalScobeTokens[s].isDatatype())
                                p1.dataType = globalScobeTokens[s];
                            parameters.Add(p1);
                        }
                    }

                    if (!prototype)//function
                    {
                        string funcBody = "";
                        List<token> funcTokens = new List<token>();
                        while (globalScobeTokens[j].getLexeme() != "{")
                            j++;
                        remove.Add(globalScobeTokens[j]);
                        j++;
                        while ((globalScobeTokens[j].getLexeme() != "}") || (parentheses != 0))
                        {

                            funcBody += globalScobeTokens[j].getLexeme() + " ";
                            funcTokens.Add(globalScobeTokens[j]);
                            remove.Add(globalScobeTokens[j]);
                            if (globalScobeTokens[j].getLexeme() == "{")
                                parentheses++;
                            if (globalScobeTokens[j].getLexeme() == "}")
                                parentheses--;
                            j++;
                        }
                        remove.Add(globalScobeTokens[j]);
                        i = j - 1;
                        List<identifier> temp1 = new List<identifier>(thisScopeVars);
                        foreach (identifier t in upperLevelVar)
                            temp1.Add(t);
                        List<pointer> temp2 = new List<pointer>(thisScopePointers);
                        foreach (pointer t in upperLevelPointers)
                            temp2.Add(t);
                        List<array> temp3 = new List<array>(thisScopeArray);
                        foreach (array t in upperLevelArray)
                            temp3.Add(t);
                        functions.Add(new function(funcBody, this.ScopeId, ref funcTokens, parameters, datatype, funcName,ref thisCodeToken, thisScopeVars, temp2,temp3));

                    }
                    else
                    {
                        func_prototypes.Add(new function(parameters, this.ScopeId, datatype, funcName, true));
                    }
                }
            }
        }
        public List<token> getAllTokens() { return thisCodeToken; }
        public List<token> getGSTokens() { return globalScobeTokens; }
        public string codeAsStr { get { return cStr; } set { cStr = value; } }
        public string filename { get { return fname; } set { fname = value; } }

        public void findClassesAndStructs()
        {
            for (int i = 0; i < thisCodeToken.Count - 2; i++)
            {
                int parentheses = 0;
                List<token> parameters = new List<token>();
                bool id = thisCodeToken[i + 1].isPointer || thisCodeToken[i + 1].isArray || thisCodeToken[i + 1].isIdentifierTokenObject() || thisCodeToken[i + 1].isIdentifierTokenObject();
                bool Isclass = (thisCodeToken[i].getLexeme() == "class") && (id) && (thisCodeToken[i + 2].getLexeme() == "{");
                bool Isstruct = (thisCodeToken[i].getLexeme() == "struct") && (id) && (thisCodeToken[i + 2].getLexeme() == "{");

                bool typedefStruct = (thisCodeToken[i].getLexeme() == "typedef") && (thisCodeToken[i + 1].getLexeme() == "struct") && (thisCodeToken[i + 2].getLexeme() == "{");
                bool typedefStructName = i > 0 ? (thisCodeToken[i - 1].getLexeme() == "typedef") && (thisCodeToken[i].getLexeme() == "struct") && (id) && (thisCodeToken[i + 2].getLexeme() == "{") : false;
                if (typedefStructName)
                    globalScobeTokens.Remove(thisCodeToken[i - 1]);
                if (Isclass || Isstruct || typedefStruct)
                {
                    globalScobeTokens.Remove(thisCodeToken[i]);
                    globalScobeTokens.Remove(thisCodeToken[i + 1]);
                    globalScobeTokens.Remove(thisCodeToken[i + 2]);
                    string Name = thisCodeToken[i + 1].getLexeme();//it will change for typedefStruct down
                    i += 3;
                    string Body = "";
                    List<token> Tokens = new List<token>();
                    int kk = i;
                    while ((thisCodeToken[i].getLexeme() != "}") || (parentheses != 0))
                    {
                        Body += thisCodeToken[i].getLexeme() + " ";
                        Tokens.Add(thisCodeToken[i]);
                        globalScobeTokens.Remove(thisCodeToken[i]);
                        if (thisCodeToken[i].getLexeme() == "{")
                            parentheses++;
                        if (thisCodeToken[i].getLexeme() == "}")
                            parentheses--;
                        i++;
                    }
                    //now i reffer to }
                    globalScobeTokens.Remove(thisCodeToken[i]);
                    if (Isclass)
                    {
                        classes.Add(new code(Body, Name, this.ScopeId, Tokens,ref holeCodeTokens ,thisScopeVars, thisScopePointers, thisScopeArray,"class"));
                        AllstructesClassesNames.Add(Name);
                        globalScobeTokens.Remove(thisCodeToken[++i]);
                    }
                    else if (typedefStruct)
                    {
                        if ((thisCodeToken[++i].isIdentifierTokenObject()) && (typedefStruct))
                        {
                            Name = thisCodeToken[i].getLexeme();
                        }
                        globalScobeTokens.Remove(thisCodeToken[i]);
                        globalScobeTokens.Remove(thisCodeToken[++i]);
                        code temp = new code(Body, Name, this.ScopeId, Tokens,ref holeCodeTokens, thisScopeVars, thisScopePointers, thisScopeArray,"struct");
                        AllstructesClassesNames.Add(Name);

                        temp.typedefName = Name;
                        structes.Add(temp);
                    }
                    else if (Isstruct)
                    {
                        code temp = new code(Body, Name, this.ScopeId, Tokens,ref holeCodeTokens, thisScopeVars, thisScopePointers, thisScopeArray,"struct");
                        AllstructesClassesNames.Add(Name);
                        structes.Add(temp);
                        if (!typedefStructName)
                            while (thisCodeToken[++i].isIdentifierTokenObject())
                            {
                                structObjects.Add(new token(thisCodeToken[i].getLexeme(), "object"));
                                globalScobeTokens.Remove(thisCodeToken[i]);
                            }
                        else
                        {
                            if (thisCodeToken[++i].isIdentifierTokenObject())
                            {
                                temp.typedefName = thisCodeToken[i].getLexeme();
                                AlltypdefNames.Add(Name);
                                globalScobeTokens.Remove(thisCodeToken[i++]);//i++ not ++i they are different ! MUS
                            }
                        }
                        globalScobeTokens.Remove(thisCodeToken[i]);
                    }

                }
            }
        }
        public static void spitYourClassesLan(code something)
        {
            foreach (var dodo in something.structes)
            {
                spitYourClassesLan(dodo);
                code.lines[linecounter++] = dodo.fname;
            }

        }
        public static void spitAllSnames(code something)
        {
            string s= something.filename+ "\n ScopeId " + something.ScopeId.ToString()+ "\n containingScopeId " + something.containingScopeId.ToString();
            System.Windows.Forms.MessageBox.Show(s);
            s = "";
            foreach (var dodo in something.functions)
            {
                 s = dodo.funcname + "\n ScopeId " + dodo.ScopeId.ToString() + "\n containingScopeId " + dodo.containingScopeId.ToString();
                System.Windows.Forms.MessageBox.Show(s);
                s = "";
            }
            foreach (var dodo in something.structes)
            {
                spitAllSnames(dodo);
            }
            foreach (var dodo in something.classes)
            {
                spitAllSnames(dodo);
            }

        }
        public void spitYourVariablesLn(code something)
        {
            something.findVar();
            foreach (function f in something.functions)
                f.findVar();
            foreach (var strct in something.Allstructs)
            {
                    spitYourVariablesLn(strct);
            }
            foreach (var cls in something.classes)
            {

                spitYourVariablesLn(cls);
            }

        }
        public void countAllSubScopes(code something)
        {
            something.count();
            foreach (function f in something.functions)
                f.count();
            foreach (var strct in something.Allstructs)
            {
                countAllSubScopes(strct);
            }
            foreach (var cls in something.classes)
            {

                countAllSubScopes(cls);
            }

        }
        public static void spitYourstructsLan(code something)
        {
            string s = "";
            foreach (var strct in something.structes)
            {
                foreach (var t in strct.globalScobeTokens)
                    s += t.getLexeme() + " ";
                s = "";
                spitYourClassesLan(strct);
            }

        }
        public static string spitYourCountersLan(code something)
        {
            string s = something.filename + "\n";
            foreach (tokenCounter tk in something.operationsCounterGS)
                s += tk.getLexeme() + " Count:" + tk.getCount()+"\n";
            s += "\n";
            foreach (code srct in something.structes)
                return s + spitYourCountersLan(srct);
            return s;
        }
        public static string spitYourfnames(code something)
        {
            string s = something.filename + "\n";
            foreach (code srct in something.structes)
                return s + spitYourCountersLan(srct);
            return s;
        }

        #region counters

        public static List<List<tokenCounter>> res1 = new List<List<tokenCounter>>();
        public static List<List<tokenCounter>> res2 = new List<List<tokenCounter>>();
        public static List<List<tokenCounter>> res3 = new List<List<tokenCounter>>();

        public List<List<tokenCounter>> keywordsLL( code st)
        {
            
            res1.Add(KeyWordsCounterGS);
            foreach (code cd in st.structes)
            { res1.Concat(cd.keywordsLL(cd)).ToList();}
            return res1;
        }



        public List<List<tokenCounter>> operatorsLL(code st)
        {

            res2.Add(operationsCounterGS);
            foreach (code cd in st.structes)
            { res2.Concat(cd.operatorsLL(cd)).ToList(); }
            return res2;
        }
        public List<List<tokenCounter>> datatypesLL(code st)
        {

            res3.Add(dataTypesCounterGS);
            foreach (code cd in st.structes)
            { res3.Concat(cd.datatypesLL(cd)).ToList(); }
            return res3;
        }


        //public static List<scopeTokenCounter> res11 = new List<scopeTokenCounter>();
        //public static List<scopeTokenCounter> res12 = new List<scopeTokenCounter>();
        //public static List<scopeTokenCounter> res13 = new List<scopeTokenCounter>();


        public List<scopeTokenCounter> keywordsLL1(code st)
        {
            scopeTokenCounter temp = new scopeTokenCounter(st.ScopeId, st.containingScopeId);
            scopeTokenCounterList1.Add(temp);
            temp.counter = KeyWordsCounterGS;

            foreach (code cd in st.structes)
            { scopeTokenCounterList1.Concat(cd.keywordsLL1(cd)).ToList(); }

            return scopeTokenCounterList1;
        }

        public List<scopeTokenCounter> operatorsLL1(code st)
        {
            scopeTokenCounter temp = new scopeTokenCounter(st.ScopeId, st.containingScopeId);
            scopeTokenCounterList2.Add(temp);
            temp.counter = operationsCounterGS;

            foreach (code cd in st.structes)
            { scopeTokenCounterList2.Concat(cd.operatorsLL1(cd)).ToList(); }

            return scopeTokenCounterList2;
        }
        public List<scopeTokenCounter> datatypesLL1(code st)
        {
            scopeTokenCounter temp = new scopeTokenCounter(st.ScopeId, st.containingScopeId);
            scopeTokenCounterList3.Add(temp);
            temp.counter = dataTypesCounterGS;

            foreach (code cd in st.structes)
            { scopeTokenCounterList3.Concat(cd.datatypesLL1(cd)).ToList(); }

            return scopeTokenCounterList3;
        }
        public result result { get; set; }
        public void setResults()
        {
            result = new result();
            result.keyWord = keywordsLL1(this);
            result.operations = operatorsLL1(this);
            result.datatypes = datatypesLL1(this);
        }
        #endregion

    }
    public class result {
        public List<scopeTokenCounter> keyWord = new List<scopeTokenCounter>();
        public List<scopeTokenCounter> operations = new List<scopeTokenCounter>();
        public List<scopeTokenCounter> datatypes = new List<scopeTokenCounter>();
    }

    public class scopeTokenCounter
    {
        public int scopeId;
        public int containId;
        public List<tokenCounter> counter = new List<tokenCounter>();
        public scopeTokenCounter(int scopeId,int containId)
        {
            this.scopeId = scopeId;
            this.containId = containId;
        }

    }
}
