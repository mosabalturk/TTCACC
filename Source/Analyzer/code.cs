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


        List<identifier> thisScopeVars = new List<identifier>();
        List<identifier> upperLevelVar = new List<identifier>();
        List<pointer> upperLevelPointers = new List<pointer>();
        List<pointer> thisScopePointers = new List<pointer>();
        List<array> upperLevelArray = new List<array>();
        List<array> thisScopeArray = new List<array>();


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


        public code(string codestr, string filename,List<token> thisCodeToken, ref List<token> holeCodeTokens, List<identifier> upperLevelVar, List<pointer> upperLevelPointers, List<array> upperLevelArray)
        {
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
            Analyzer.structAsDatatype(this.thisCodeToken, globalScobeTokens, structes.Select(a => a.filename).ToList(), structes.Select(a => a.typedefName).ToList());

            findMain();
            findFunctionsAndPrototypes();
            count();
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

                    main = new function(funcBody, ref funcTokens, new List<identifier>(), datatype, "main",ref thisCodeToken, thisScopeVars, thisScopePointers.Concat(upperLevelPointers).ToList(), thisScopeArray.Concat(upperLevelArray).ToList());
                    main.funcDataType = datatype;
                    functions.Add(main);
                    break;
                }
            }
            
        }
        public void findFunctionsAndPrototypes()
        {
            for (int i = 0; i < thisCodeToken.Count - 2; i++)
            {
                int parentheses = 0;
                int j = 0;
                if (thisCodeToken[i].getLexeme() == "main")
                {
                    while (thisCodeToken[i].getLexeme() != "{")
                    {
                        i++;
                    }
                    i++;
                    while ((thisCodeToken[i].getLexeme() != "}") || (parentheses != 0))
                    {
                        if (thisCodeToken[i].getLexeme() == "{")
                            parentheses++;
                        if (thisCodeToken[i].getLexeme() == "}")
                            parentheses--;
                        i++;
                    }
                    continue;
                }
                List<identifier> parameters = new List<identifier>();
                bool id = thisCodeToken[i + 1].isPointer || thisCodeToken[i + 1].isArray || thisCodeToken[i + 1].isIdentifierTokenObject() || thisCodeToken[i + 1].isIdentifierTokenObject();
                bool func = ((thisCodeToken[i].isDatatype()) && id && (thisCodeToken[i + 2].getLexeme() == "("));
                bool funcWithoutDATATYPE = (id) && (thisCodeToken[i + 1].getLexeme() == "(");// like balabla(int bla)
                //bool funcprotoType = ((tokens[i].getType() == "datatype") && (tokens[i + 1].getType() == "identifier") && (tokens[j = i + 2].getLexeme() == "("));
                // bool funcAsterisk = ((tokens[i].getType() == "datatype") && (tokens[i+1].getLexeme() == "*") && (tokens[i + 2].getType() == "identifier") && (tokens[j = i + 3].getLexeme() == "("));
                if (funcWithoutDATATYPE)
                {
                    int y = i + 2;
                    while (thisCodeToken[y].getLexeme() != ")")
                        y++;
                    if (thisCodeToken[y + 1].getLexeme() == ";")
                        funcWithoutDATATYPE = false;
                }
                if (func || funcWithoutDATATYPE)
                {
                    string datatype;
                    string funcName;
                    if (func)
                    { datatype = thisCodeToken[i].getLexeme(); funcName = thisCodeToken[i + 1].getLexeme(); }
                    else
                    {
                        datatype = "void"; funcName = thisCodeToken[i].getLexeme();
                    }

                    globalScobeTokens.Remove(thisCodeToken[i]);
                    globalScobeTokens.Remove(thisCodeToken[i + 1]);
                    if (func)
                    { j = i + 3; globalScobeTokens.Remove(thisCodeToken[i + 2]); }
                    else
                        j = i + 2;
                    bool prototype = false;
                    int k = j;
                    while (thisCodeToken[k].getLexeme() != ")")
                    {
                        globalScobeTokens.Remove(thisCodeToken[k]);
                        k++;
                    }
                    globalScobeTokens.Remove(thisCodeToken[k]);// deleting )
                    if ((k + 1 < thisCodeToken.Count))
                        if (thisCodeToken[k + 1].getLexeme() == ";")// void func (int blabla);
                        {
                            prototype = true;
                            globalScobeTokens.Remove(thisCodeToken[k + 1]);
                        }
                    //parameters are frome j to k
                    for (int s = j; s < k; s++)
                    {//خطأ عندما يكون الباراميتر ستركت أو بوينتر او مصفوفة
                        if ((thisCodeToken[s].isDatatype() && (thisCodeToken[s + 1].getType() == "identifier")))
                        {
                            identifier p1 = new identifier(thisCodeToken[s + 1]);
                            if (thisCodeToken[s].isDatatype())
                                p1.dataType = thisCodeToken[s];
                            parameters.Add(p1);
                        }
                    }

                    if (!prototype)//function
                    {
                        string funcBody = "";
                        List<token> funcTokens = new List<token>();
                        while (thisCodeToken[j].getLexeme() != "{")
                            j++;
                        globalScobeTokens.Remove(thisCodeToken[j]);
                        j++;
                        while ((thisCodeToken[j].getLexeme() != "}") || (parentheses != 0))
                        {

                            funcBody += thisCodeToken[j].getLexeme() + " ";
                            funcTokens.Add(thisCodeToken[j]);
                            globalScobeTokens.Remove(thisCodeToken[j]);
                            if (thisCodeToken[j].getLexeme() == "{")
                                parentheses++;
                            if (thisCodeToken[j].getLexeme() == "}")
                                parentheses--;
                            j++;
                        }
                        globalScobeTokens.Remove(thisCodeToken[j]);
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
                        functions.Add(new function(funcBody, ref funcTokens, parameters, datatype, funcName,ref thisCodeToken, thisScopeVars, temp2,temp3));

                    }
                    else
                    {
                        func_prototypes.Add(new function(parameters, datatype, funcName, true));
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
                        classes.Add(new code(Body, Name, Tokens,ref holeCodeTokens ,thisScopeVars, thisScopePointers, thisScopeArray));
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
                        code temp = new code(Body, Name, Tokens,ref holeCodeTokens, thisScopeVars, thisScopePointers, thisScopeArray);
                        temp.typedefName = Name;
                        structes.Add(temp);
                    }
                    else if (Isstruct)
                    {
                        code temp = new code(Body, Name, Tokens,ref holeCodeTokens, thisScopeVars, thisScopePointers, thisScopeArray);
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
        public static void spitAllGS(code something)
        {
            string s="";
            foreach (token t in something.globalScobeTokens)
                s += t.getLexeme() + " ";
            System.Windows.Forms.MessageBox.Show(s);
            s = "";
            foreach (var dodo in something.structes)
            {
                spitAllGS(dodo);
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
        public static List<List<tokenCounter>> res = new List<List<tokenCounter>>();
        public List<List<tokenCounter>> KwOpDtTC( code st)
        {
            
            res.Add(KeyWordsCounterGS.Concat(operationsCounterGS).Concat(dataTypesCounterGS).ToList());
            foreach (code cd in st.structes)
            { res.Concat(cd.KwOpDtTC(cd)).ToList();}
            return res;
        }
    }
}
