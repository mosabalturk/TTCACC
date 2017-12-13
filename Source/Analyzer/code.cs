using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Analyzer
{

    public class code
    {

        #region class code main proprites
        cppFile cppfile;
        private string cStr;//done
        private string name;//done
        public static int idno = 0;
        public  bool error;
        public  string errormsg = "";
        public int ScopeId;
        public int containingScopeId;
        public List<code> classes = new List<code>();//done
        private List<function> functions = new List<function>();//done
        public function main { get; set; }//done?
        public List<function> Allfunctions { get { return functions; } }
        public List<function> Allfunc_prototypes { get { return func_prototypes; } }
        public List<code> Allclasses { get { return classes; } }
        public static List<token> holeCodeTokens;
        List<token> globalScobeTokens;//no
        private List<function> func_prototypes = new List<function>();//done
        private List<code> structes = new List<code>();//done

        #endregion

        public static string[] lines = new string[100];
        public static int linecounter = 0;


        public List<token> structObjectsGS { get { return structObjects; } }
        private List<token> classObjects = new List<token>();
        private List<token> structObjects = new List<token>();


        List<identifier> thisScopeVars = new List<identifier>();
        List<identifier> upperLevelVar = new List<identifier>();
        List<pointer> upperLevelPointers = new List<pointer>();
        List<pointer> thisScopePointers = new List<pointer>();
        List<array> upperLevelArray = new List<array>();
        List<array> thisScopeArray = new List<array>();

        static List<string> AllstructesClassesNames = new List<string>();
        static List<string> AlltypdefNames = new List<string>();
        public static void zeroStatics()
        {
            AlltypdefNames = new List<string>();
            AllstructesClassesNames = new List<string>();
            code.idno = 0;
            scopeTokenCounterList1 = new List<scopeTokenCounter>();
            scopeTokenCounterList2 = new List<scopeTokenCounter>();
            scopeTokenCounterList3 = new List<scopeTokenCounter>();
            scopeTokenCounterList4 = new List<scopeTokenCounter>();
            scopeTokenCounterList5 = new List<scopeVarCounter>();
            scopeTokenCounterList6 = new List<scopeArrayCounter>();
            scopeTokenCounterList7 = new List<scopePointersCounter>();
            scopeTokenCounterList8 = new List<scopeTokenCounter>();
            scopeTokenCounterList9 = new List<scopefunctionCallCounter>();
            scopeTokenCounterList10 = new List<scopeTokens>();
            scopeTokenCounterList11 = new List<scopefunctionCallCounter>();
        }
        public string typedefName { get; set; }

        public List<token> getGSTokens() { return globalScobeTokens; }
        public List<token> getHoleCodeTokens() { return holeCodeTokens; }
        public string codeAsStr { get { return cStr; } set { cStr = value; } }
        public string filename { get { return name; } set { name = value; } }

        #region static lists for recursive functions using
        public static List<scopeTokenCounter> scopeTokenCounterList1 = new List<scopeTokenCounter>();
        public static List<scopeTokenCounter> scopeTokenCounterList2 = new List<scopeTokenCounter>();
        public static List<scopeTokenCounter> scopeTokenCounterList3 = new List<scopeTokenCounter>();
        public static List<scopeTokenCounter> scopeTokenCounterList4 = new List<scopeTokenCounter>();
        public static List<scopeVarCounter> scopeTokenCounterList5 = new List<scopeVarCounter>();
        public static List<scopeArrayCounter> scopeTokenCounterList6 = new List<scopeArrayCounter>();
        public static List<scopePointersCounter> scopeTokenCounterList7 = new List<scopePointersCounter>();
        public static List<scopeTokenCounter> scopeTokenCounterList8 = new List<scopeTokenCounter>();
        public static List<scopefunctionCallCounter> scopeTokenCounterList9 = new List<scopefunctionCallCounter>();
        public static List<scopeTokens> scopeTokenCounterList10 = new List<scopeTokens>();
        public static List<scopefunctionCallCounter> scopeTokenCounterList11 = new List<scopefunctionCallCounter>();
        #endregion

        #region type of code
        public string type { get; set; }
        public bool isStruct { get { if (this.type == "struct") return true; else return false; } }
        public bool isGS { get { if (this.type == "GS") return true; else return false; } }
        public bool isclass { get { if (this.type == "class") return true; else return false; } }

        #endregion

        #region COUNTER LISTS
        public List<tokenCounter> specialCharGS = new List<tokenCounter>();
        private List<variableCounter> variablesCounterGS = new List<variableCounter>();
        private List<pointerCounter> pointersCounterGS = new List<pointerCounter>();
        private List<arrayCounter> arraysCounterGS = new List<arrayCounter>();
        private List<functionCallCounter> functionCallsCounterGS = new List<functionCallCounter>();
        private List<tokenCounter> KeyWordsCounterGS = new List<tokenCounter>();
        private List<tokenCounter> dataTypesCounterGS = new List<tokenCounter>();
        private List<tokenCounter> valuesCounterGS = new List<tokenCounter>();
        private List<tokenCounter> operationsCounterGS = new List<tokenCounter>();

        #endregion

        #region get lists
        public List<tokenCounter> getOperationsCounterGS { get { return operationsCounterGS; } }
        public List<tokenCounter> getDataTypesCounterGS { get { return dataTypesCounterGS; } }
        public List<tokenCounter> getValuesCounterGS { get { return valuesCounterGS; } }
        public List<tokenCounter> getKeyWordsCounterGS { get { return KeyWordsCounterGS; } }
        public List<arrayCounter> getArraysCounterGS { get { return arraysCounterGS; } }
        public List<pointerCounter> getPointersCounterGS { get { return pointersCounterGS; } }
        public List<variableCounter> getVariablesCounterGS { get { return variablesCounterGS; } }
        public List<code> Allstructs { get { return structes; } }

        #endregion
        public void setDownLevel()
        {
            foreach (code c in this.structes)
            {
                c.upperLevelVar = this.thisScopeVars.Concat(upperLevelVar).ToList();
                c.upperLevelPointers = this.thisScopePointers.Concat(upperLevelPointers).ToList();
                c.upperLevelArray = this.thisScopeArray.Concat(thisScopeArray).ToList();
            }
            foreach (code c in this.classes)
            {
                c.upperLevelVar = this.thisScopeVars.Concat(upperLevelVar).ToList();
                c.upperLevelPointers = this.thisScopePointers.Concat(thisScopePointers).ToList();
                c.upperLevelArray = this.thisScopeArray.Concat(thisScopeArray).ToList();
            }
            foreach (function c in this.Allfunctions)
            {
                c.upperLevelVar = this.thisScopeVars.Concat(upperLevelVar).ToList();
                c.upperLevelPointers = this.thisScopePointers.Concat(thisScopePointers).ToList();
                c.upperLevelArray = this.thisScopeArray.Concat(thisScopeArray).ToList();
            }
        }
        public code(string codestr, string filename, int containingScopeId, List<token> globalScobeTokens, cppFile cppfile, string type)
        {
            this.cppfile = cppfile;
            this.ScopeId = idno++;
            this.containingScopeId = containingScopeId;

            this.type = type;

            this.globalScobeTokens = new List<token>(globalScobeTokens);
            holeCodeTokens = cppfile.holeCodeTokens;




            this.cStr = codestr;//add hole code to cStr string

            this.name = filename;//filename or class or struct name

            Analyzer temp = new Analyzer(); // anlyzer class is the class that analyze code to tokens and lexemes
                                            // globalScobeTokens = new List<token>(this.thisCodeToken); // copy allCodeTokens list

            findClassesAndStructs();



            findMain();
            findFunctionsAndPrototypes();

        }
        public void pointersArraysAnalzer(List<token> holeCodeTokens)
        {
            List<token> remove = new List<token>();
            for (int i = 0; i < holeCodeTokens.Count - 1; i++)
            {

                bool structPtr = i > 1 ? ((holeCodeTokens[i - 1].getLexeme() == "struct") && (holeCodeTokens[i].isIdentifierTokenObject()) && (holeCodeTokens[i + 1].getLexeme() == "*")) : (false);
                if ((holeCodeTokens[i].isDatatype() || holeCodeTokens[i].getLexeme() == "," || (structPtr)) && holeCodeTokens[i + 1].getLexeme() == "*")
                {
                    int k = 0;//  * counter
                    int j = i; //  i is data type index
                    while (holeCodeTokens[++j].getLexeme() == "*")// j is  * index
                        k++;
                    // now j is identifier index
                    if (holeCodeTokens[j].isIdentifierTokenObject())
                    {// modifiye porpreties of identifier
                        pointer p = new pointer(holeCodeTokens[j], k);
                        if (holeCodeTokens[i].isDatatype())
                            p.dataType = holeCodeTokens[i];
                        else if (holeCodeTokens[i].getLexeme() == ",")
                        {
                            int kk = i;
                            while ((i > 0) && (!holeCodeTokens[kk].isDatatype()))
                                kk--;
                            if(kk!=0)
                                p.dataType = holeCodeTokens[kk];
                        }
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
                            token valu = new token((0).ToString(), "vint");
                            try
                            {
                                valu = new token((holeCodeTokens[j + 3].getLexeme().Length - 2).ToString(), "vint");
                            }
                            catch
                            { }
                            remove.Add(holeCodeTokens[j]);
                            remove.Add(holeCodeTokens[j + 1]);
                            newArray.arrayIndices.Add(valu);
                            j += 2;
                        }
                        else if ((holeCodeTokens[j].getLexeme() == "[") && (holeCodeTokens[j + 2].getLexeme() == "]"))
                        {
                            token valu = new token((0).ToString(), "vint");
                            if (holeCodeTokens[j + 1].isValue())
                                valu = holeCodeTokens[j + 1];
                            if (holeCodeTokens[j + 1].isIdentifierTokenObject())
                                foreach (token t in cppfile.defines)
                                    if (t.getLexeme() == holeCodeTokens[j + 1].getLexeme())
                                        valu = holeCodeTokens[j + 1];
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
                            newArray.arrayIndices.Add(new token((0).ToString(), "vint"));
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

        public void count()//this functions fills counter lists  that exist in counter lists region
        {
            for (int ii = 0; ii < globalScobeTokens.Count; ii++)
            {
                if (globalScobeTokens[ii].getType() == "library")
                { }
                else if (globalScobeTokens[ii].isOperation())
                    tokenCounter.AddOneByLexeme(globalScobeTokens[ii], operationsCounterGS);
                else if (globalScobeTokens[ii].isKeyword())
                { tokenCounter.AddOneByLexeme(globalScobeTokens[ii], KeyWordsCounterGS); }
                else if (globalScobeTokens[ii].isDatatype())
                    tokenCounter.AddOneByLexeme(globalScobeTokens[ii], dataTypesCounterGS);
                else if (globalScobeTokens[ii].isValue())
                    tokenCounter.AddOneValueByDataType(globalScobeTokens[ii], valuesCounterGS);
                else if (globalScobeTokens[ii].isIdentifierObject())
                {
                    variableCounter.AddOneByToken(globalScobeTokens[ii], ((identifier)globalScobeTokens[ii]).dataType, variablesCounterGS);
                    if ((ii > 0) && ((globalScobeTokens[ii - 1].getLexeme() == ",") || (globalScobeTokens[ii - 1].isDatatype())) && (!globalScobeTokens[ii + 1].isOperation()))
                        variablesCounterGS[variablesCounterGS.Count - 1].decCount();// we want count to be zero at initilization unless there is assignment afetr it like int b = 5; or init a, b=10,c;  a and c count equal zero but b count is 1

                }
                else if (globalScobeTokens[ii].isPointer)
                    pointerCounter.AddOneByToken((pointer)globalScobeTokens[ii], pointersCounterGS);
                else if (globalScobeTokens[ii].isArray)
                    arrayCounter.AddOneByToken((array)globalScobeTokens[ii], arraysCounterGS);
                else if(globalScobeTokens[ii].isFunctionCall)
                    functionCallCounter.AddOneByLexeme(globalScobeTokens[ii], functionCallsCounterGS);
                else
                    tokenCounter.AddOneByLexeme(globalScobeTokens[ii], specialCharGS);
            }
        }
        public void structAsDatatype() // in case like "struct struct_name object_name" the function merge struct struct_name into one token and set type of this token as datatype
        {//    or "typedef struct operations{...}opr;" opr now is datatype


            List<int> remove = new List<int>();



            for (int i = 0; i < globalScobeTokens.Count - 1; i++)
            {

                bool structPtr = (globalScobeTokens[i].getLexeme() == "struct") && (AllstructesClassesNames.Contains(globalScobeTokens[i + 1].getLexeme()));
                bool structInitFromTypedefNames = AlltypdefNames.Contains(globalScobeTokens[i].getLexeme());
                if (structPtr)
                {
                    string temp = globalScobeTokens[i].getLexeme() + " " + globalScobeTokens[i + 1].getLexeme();
                    globalScobeTokens[i + 1].setType("datatype");
                    globalScobeTokens[i + 1].setLexeme(temp);
                    remove.Add(globalScobeTokens[i].id);

                }
                if (structInitFromTypedefNames)
                {
                    //System.Windows.Forms.MessageBox.Show(globalScobeTokens[i].id.ToString());
                    globalScobeTokens[i].setType("datatype");
                    globalScobeTokens[i].setLexeme(globalScobeTokens[i].getLexeme());
                }

            }
            foreach (int t in remove)
            {
                holeCodeTokens.Remove(holeCodeTokens.Find(n=>n.id == t));
                globalScobeTokens.Remove(globalScobeTokens.Find(n => n.id == t));
            }
        }
        public void findIdentifiers() // 1- recognize varibles and  change their tokens to variable and set data type if exists then if this variable repeated set the token object into same one that first time defined
        {
            //structAsDatatype(this.globalScobeTokens);
            for (int i = 0; i < globalScobeTokens.Count; i++)
            {
                bool pointer = upperLevelPointers.Concat(thisScopePointers).ToList().Select(a => a.getLexeme()).ToList().Contains(globalScobeTokens[i].getLexeme());
                bool array = upperLevelArray.Concat(thisScopeArray).ToList().Select(a => a.getLexeme()).ToList().Contains(globalScobeTokens[i].getLexeme());

                if ( ((globalScobeTokens[i].isPointer) || pointer))
                {
                    if ((i > 0) && globalScobeTokens[i].isPointer && ((globalScobeTokens[i - 1].isDatatype() || (globalScobeTokens[i - 1].getLexeme() == ","))))
                    {
                        pointer p = (pointer)globalScobeTokens[i];
                        if (globalScobeTokens[i - 1].isDatatype())
                        { p.dataType = globalScobeTokens[i - 1]; }
                        else if (globalScobeTokens[i - 1].getLexeme() == ",")
                        {
                            int k = i - 1;
                            while ((k > 0) && (!globalScobeTokens[k].isDatatype()))
                                k--;
                            if (k != 0)
                                p.dataType = globalScobeTokens[k];
                        }
                        thisScopePointers.Add(p);
                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == globalScobeTokens[i].id)
                                holeCodeTokens[q] = p;

                        globalScobeTokens[i] = p;
                    }
                    else if (thisScopePointers.Select(a => a.getLexeme()).ToList().Contains(globalScobeTokens[i].getLexeme()))
                    {
                        pointer id;
                        id = thisScopePointers.Find(a => a.getLexeme() == globalScobeTokens[i].getLexeme());

                        //for (int q = 0; q < thisCodeToken.Count; q++)
                        //    if (thisCodeToken[q].id == globalScobeTokens[i].id)
                        //        thisCodeToken[q] = id;
                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == globalScobeTokens[i].id)
                                holeCodeTokens[q] = id;
                        globalScobeTokens[i] = id;
                        
                    }
                    else if (upperLevelPointers.Select(a => a.getLexeme()).ToList().Contains(globalScobeTokens[i].getLexeme()))
                    {
                        pointer id;
                        id = upperLevelPointers.Find(a => a.getLexeme() == globalScobeTokens[i].getLexeme());
                        //for (int q = 0; q < thisCodeToken.Count; q++)
                        //    if (thisCodeToken[q].id == globalScobeTokens[i].id)
                        //        thisCodeToken[q] = id;
                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == globalScobeTokens[i].id)
                                holeCodeTokens[q] = id;
                        globalScobeTokens[i] = id;

                    }
                    continue;
                }
                if ((i > 0) && ((globalScobeTokens[i].isArray) || array))
                {
                    if ((i > 0) && globalScobeTokens[i].isArray && (globalScobeTokens[i - 1].isDatatype()))
                    {
                        array p = (array)globalScobeTokens[i];
                        if (globalScobeTokens[i - 1].isDatatype())
                        { p.dataType = globalScobeTokens[i - 1]; }
                        else if (globalScobeTokens[i - 1].getLexeme() == ",")
                        {
                            int k = i - 1;
                            while ((k > 0) && (!globalScobeTokens[k].isDatatype()))
                                k--;
                            if (k != 0)
                                p.dataType = globalScobeTokens[k];
                        }
                        thisScopeArray.Add(p);
                    }
                    else if (thisScopeArray.Select(a => a.getLexeme()).ToList().Contains(globalScobeTokens[i].getLexeme()))
                    {
                        array id;
                        id = thisScopeArray.Find(a => a.getLexeme() == globalScobeTokens[i].getLexeme());

                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == globalScobeTokens[i].id)
                                holeCodeTokens[q] = id;
                        globalScobeTokens[i] = id;
                    }
                    else if (upperLevelArray.Select(a => a.getLexeme()).ToList().Contains(globalScobeTokens[i].getLexeme()))
                    {
                        array id;
                        id = upperLevelArray.Find(a => a.getLexeme() == globalScobeTokens[i].getLexeme());

                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == globalScobeTokens[i].id)
                                holeCodeTokens[q] = id;
                        globalScobeTokens[i] = id;

                    }
                    continue;
                }
                if ((globalScobeTokens[i].isIdentifierTokenObject()) && !globalScobeTokens[i].isPointer && !globalScobeTokens[i].isArray && !array && !pointer)
                {
                    identifier id;

                    if ((i > 0) && globalScobeTokens[i].isIdentifierTokenObject() && ((globalScobeTokens[i - 1].isDatatype() || globalScobeTokens[i - 1].getLexeme() == ",")))
                    {
                        id = new identifier(globalScobeTokens[i]);
                        if ((i > 0) && (globalScobeTokens[i - 1].isDatatype()))
                        { id.dataType = globalScobeTokens[i - 1]; }
                        else if (globalScobeTokens[i - 1].getLexeme() == ",")
                        {
                            int k = i - 1;
                            while ((k > 0) && (!globalScobeTokens[k].isDatatype()))
                                k--;
                            if (k != 0)
                                id.dataType = globalScobeTokens[k];
                        }
                        thisScopeVars.Add(id);

                        //for (int q = 0; q < thisCodeToken.Count; q++)
                        //    if (thisCodeToken[q].id == globalScobeTokens[i].id)
                        //        thisCodeToken[q] = id;

                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == globalScobeTokens[i].id)
                                holeCodeTokens[q] = id;

                        globalScobeTokens[i] = id;
                    }
                    else if (thisScopeVars.Select(a => a.getLexeme()).ToList().Contains(globalScobeTokens[i].getLexeme()))
                    {
                        id = thisScopeVars.Find(a => a.getLexeme() == globalScobeTokens[i].getLexeme());

                        //for (int q = 0; q < thisCodeToken.Count; q++)
                        //    if (thisCodeToken[q].id == globalScobeTokens[i].id)
                        //        thisCodeToken[q] = id;
                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == globalScobeTokens[i].id)
                                holeCodeTokens[q] = id;
                        globalScobeTokens[i] = id;
                    }
                    else if (upperLevelVar.Select(a => a.getLexeme()).ToList().Contains(globalScobeTokens[i].getLexeme()))
                    {
                        id = upperLevelVar.Find(a => a.getLexeme() == globalScobeTokens[i].getLexeme());
                        //for (int q = 0; q < thisCodeToken.Count; q++)
                        //    if (thisCodeToken[q].id == globalScobeTokens[i].id)
                        //        thisCodeToken[q] = id;
                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
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
                        //for (int q = 0; q < thisCodeToken.Count; q++)
                        //    if (thisCodeToken[q].id == globalScobeTokens[i].id)
                        //        thisCodeToken[q] = id;
                        for (int q = 0; (q < holeCodeTokens.Count) && (holeCodeTokens != null); q++)
                            if (holeCodeTokens[q].id == globalScobeTokens[i].id)
                                holeCodeTokens[q] = id;
                        globalScobeTokens[i] = id;

                    }
                    continue;
                }

            }

            setDownLevel();
        }


        #region find functions , main , classes and structes functions
        public void findMain()
        {
            int parentheses = 0;
            string datatype = "";
            List<token> funcTokens = new List<token>();
            string funcBody = "";
            List<token> remove = new List<token>();
            for (int i = 0; i < globalScobeTokens.Count; i++)
            {
                if (globalScobeTokens[i].getLexeme() == "main")
                {

                    {
                        //globalScobeTokens.Remove(globalScobeTokens[i]);
                        remove.Add(globalScobeTokens[i]);
                        if (globalScobeTokens[i - 1].getType() == "datatype")
                        {
                            datatype = globalScobeTokens[i - 1].getLexeme();
                            //globalScobeTokens.Remove(globalScobeTokens[i - 1]);
                            remove.Add(globalScobeTokens[i - 1]);

                        }
                    }
                    i++;
                    //globalScobeTokens.Remove(globalScobeTokens[i]);
                    remove.Add(globalScobeTokens[i]);
                    while (globalScobeTokens[i].getLexeme() != "{")
                    {
                        remove.Add(globalScobeTokens[i]);
                        //globalScobeTokens.Remove(globalScobeTokens[i]);
                        i++;
                    }
                    remove.Add(globalScobeTokens[i]);
                    //globalScobeTokens.Remove(globalScobeTokens[i]);
                    i++;
                    while ((i < globalScobeTokens.Count) &&((globalScobeTokens[i].getLexeme() != "}") || (parentheses != 0)))
                    {
                        funcBody += globalScobeTokens[i].getLexeme() + " ";
                        funcTokens.Add(globalScobeTokens[i]);
                        //globalScobeTokens.Remove(globalScobeTokens[i]);
                        remove.Add(globalScobeTokens[i]);
                        if (globalScobeTokens[i].getLexeme() == "{")
                            parentheses++;
                        if (globalScobeTokens[i].getLexeme() == "}")
                            parentheses--;
                        i++;
                    }
                    if (!(i < globalScobeTokens.Count))
                    {
                        error = true;
                        errormsg += "paratheses Error  "+"\n";
                        break;
                    }
                    remove.Add(globalScobeTokens[i]);
                    main = new function(funcBody, this.ScopeId, ref funcTokens, new List<identifier>(), datatype, "main", cppfile);
                    main.funcDataType = datatype;
                    functions.Add(main);
                    foreach (token t in remove)
                        globalScobeTokens.Remove(t);
                    break;
                }
            }

        }


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
                    while ((i < globalScobeTokens.Count)&&((globalScobeTokens[i].getLexeme() != "}") || (parentheses != 0)))
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
                bool id2 = globalScobeTokens[i].isPointer || globalScobeTokens[i].isArray || globalScobeTokens[i].isIdentifierTokenObject() || globalScobeTokens[i].isIdentifierTokenObject();
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
                    remove.Add(globalScobeTokens[i + 1]);
                    if (func)
                    {
                        j = i + 3; ;
                        remove.Add(globalScobeTokens[i + 2]);

                    }
                    else
                        j = i + 2;
                    bool prototype = false;
                    int k = j;
                    while ((k < globalScobeTokens.Count) && (globalScobeTokens[k].getLexeme() != ")"))
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
                            remove.Add(globalScobeTokens[k + 1]);
                        }
                    //parameters are frome j to k
                    for (int s = j; (s < k) && ((k + 1 < globalScobeTokens.Count)); s++)
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
                        while ((j < globalScobeTokens.Count - 1) && (globalScobeTokens[j].getLexeme() != "{"))
                            j++;
                        remove.Add(globalScobeTokens[j]);
                        j++;
                        while ((((j < globalScobeTokens.Count)) && (globalScobeTokens[j].getLexeme() != "}") || (parentheses != 0)))
                        {

                            funcBody += globalScobeTokens[j].getLexeme() + " ";
                            funcTokens.Add(globalScobeTokens[j]);
                            remove.Add(globalScobeTokens[j]);
                            if (globalScobeTokens[j].getLexeme() == "{")
                                parentheses++;
                            if (globalScobeTokens[j].getLexeme() == "}")
                                parentheses--;
                            j++;
                            //if (!(j < globalScobeTokens.Count) && (globalScobeTokens[j].getLexeme() != "}"))
                            //{
                            //    error = true;
                            //    errormsg += "paratheses Error between " + i.ToString() + "  " + j.ToString() + "  lines\n";
                            //    break;
                            //}
                        }
                        //if (error)
                        //    continue;
                        if ((j >= globalScobeTokens.Count))
                        {
                            error = true;
                            errormsg += "paratheses Error between " + i.ToString() + "  " + j.ToString() + "  lines\n";
                            break;
                        }
                        remove.Add(globalScobeTokens[j]);
                        i = j - 1;
                        functions.Add(new function(funcBody, this.ScopeId, ref funcTokens, parameters, datatype, funcName, cppfile));

                    }
                    else
                    {
                        func_prototypes.Add(new function(parameters, this.ScopeId, datatype, funcName, true));
                    }
                }
            }
            foreach (token t in remove)
                globalScobeTokens.Remove(t);

        }

        public void findClassesAndStructs()
        {
            List<int> remove = new List<int>();
            for (int i = 0; i < globalScobeTokens.Count - 2; i++)
            {
                int parentheses = 0;
                List<token> parameters = new List<token>();
                bool id = globalScobeTokens[i + 1].isPointer || globalScobeTokens[i + 1].isArray || globalScobeTokens[i + 1].isIdentifierTokenObject() || globalScobeTokens[i + 1].isIdentifierTokenObject();
                bool Isclass = (globalScobeTokens[i].getLexeme() == "class") && (id) && (globalScobeTokens[i + 2].getLexeme() == "{");
                bool Isstruct = (globalScobeTokens[i].getLexeme() == "struct") && (id) && (globalScobeTokens[i + 2].getLexeme() == "{");

                bool typedefStruct = (globalScobeTokens[i].getLexeme() == "typedef") && (globalScobeTokens[i + 1].getLexeme() == "struct") && (globalScobeTokens[i + 2].getLexeme() == "{");
                bool typedefStructName = i > 0 ? (globalScobeTokens[i - 1].getLexeme() == "typedef") && (globalScobeTokens[i].getLexeme() == "struct") && (id) && (globalScobeTokens[i + 2].getLexeme() == "{") : false;
                if (typedefStructName)
                    remove.Add(globalScobeTokens[i - 1].id);
                if (Isclass || Isstruct || typedefStruct || typedefStructName)
                {
                    remove.Add(globalScobeTokens[i].id);
                    remove.Add(globalScobeTokens[i + 1].id);
                    remove.Add(globalScobeTokens[i + 2].id);
                    string Name = globalScobeTokens[i + 1].getLexeme();//it will change for typedefStruct down
                    i += 3;
                    string Body = "";
                    List<token> Tokens = new List<token>();
                    int kk = i;
                    while ((globalScobeTokens[i].getLexeme() != "}") || (parentheses != 0))
                    {
                        Body += globalScobeTokens[i].getLexeme() + " ";
                        Tokens.Add(globalScobeTokens[i]);
                        remove.Add(globalScobeTokens[i].id);
                        if (globalScobeTokens[i].getLexeme() == "{")
                            parentheses++;
                        if (globalScobeTokens[i].getLexeme() == "}")
                            parentheses--;
                        i++;
                    }
                    //now i reffer to }
                    remove.Add(globalScobeTokens[i].id);
                    if (Isclass)
                    {

                        classes.Add(new code(Body, Name, this.ScopeId, Tokens, cppfile, "class"));
                        AllstructesClassesNames.Add(Name);
                        remove.Add(globalScobeTokens[++i].id);
                    }
                    else if (typedefStruct)
                    {
                        if ((globalScobeTokens[++i].isIdentifierTokenObject()))
                        {
                            Name = globalScobeTokens[i].getLexeme();
                        }
                        remove.Add(globalScobeTokens[i].id);
                        remove.Add(globalScobeTokens[++i].id);
                        code temp = new code(Body, Name, this.ScopeId, Tokens, cppfile, "struct");
                        temp.typedefName = Name;
                        AllstructesClassesNames.Add(Name);
                        AlltypdefNames.Add(temp.typedefName);
                        structes.Add(temp);
                    }
                    else if (Isstruct)
                    {
                        code temp = new code(Body, Name, this.ScopeId, Tokens, cppfile, "struct");
                        AllstructesClassesNames.Add(Name);
                        structes.Add(temp);
                        if (!typedefStructName)
                            while (globalScobeTokens[++i].isIdentifierTokenObject())
                            {
                                structObjects.Add(new token(globalScobeTokens[i].getLexeme(), "struct object"));
                                remove.Add(globalScobeTokens[i].id);
                            }
                        else
                        {
                            if (globalScobeTokens[++i].isIdentifierTokenObject())
                            {
                                temp.typedefName = globalScobeTokens[i].getLexeme();
                                AlltypdefNames.Add(temp.typedefName);
                                remove.Add(globalScobeTokens[i++].id);//i++ not ++i they are different ! MUS
                            }
                        }
                        remove.Add(globalScobeTokens[i].id);
                    }

                }
            }
            structAsDatatype();//befor delete change datatypes
            pointersArraysAnalzer(holeCodeTokens);
            pointersArraysAnalzer(globalScobeTokens);
            foreach (int t in remove)
            {
                globalScobeTokens.Remove(globalScobeTokens.Find(a=>a.id ==t));
            }
        }

        #endregion



        public static void spitYourClassesLan(code something)
        {
            foreach (var dodo in something.structes)
            {
                spitYourClassesLan(dodo);
                code.lines[linecounter++] = dodo.name;
            }

        }
        public static void spitAllSnames(code something)
        {
            string s = something.filename + "\n ScopeId " + something.ScopeId.ToString() + "\n containingScopeId " + something.containingScopeId.ToString() + " type:" + something.type;
            System.Windows.Forms.MessageBox.Show(s);
            s = "";
            foreach (var dodo in something.functions)
            {
                s = dodo.funcname + "\n ScopeId " + dodo.ScopeId.ToString() + "\n containingScopeId " + dodo.containingScopeId.ToString() + " type: function";
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

        public void recognizeIdentifiers(code something)
        {
            something.findIdentifiers();
            foreach (function f in something.functions)
                f.findIdentifiers();
            foreach (var strct in something.Allstructs)
            {
                recognizeIdentifiers(strct);
            }
            foreach (var cls in something.classes)
            {

                recognizeIdentifiers(cls);
            }

        }
        public void countAllScopes(code something)
        {
            something.count();
            foreach (function f in something.functions)
                f.count();
            foreach (var strct in something.Allstructs)
            {
                countAllScopes(strct);
            }
            foreach (var cls in something.classes)
            {

                countAllScopes(cls);
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
                s += tk.getLexeme() + " Count:" + tk.getCount() + "\n";
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

        #region results recursive functions

        public List<scopeTokenCounter> keywordsLL1(code st)
        {
            scopeTokenCounterList1.Add(new scopeTokenCounter(st.ScopeId, st.containingScopeId, st.name, KeyWordsCounterGS, st.type));
            foreach (function f in st.Allfunctions)
                scopeTokenCounterList1.Add(new scopeTokenCounter(f.ScopeId, f.containingScopeId, f.name, f.KeyWordsCounter, "function"));
            foreach (code cd in st.structes)
            { scopeTokenCounterList1.Concat(cd.keywordsLL1(cd)).ToList(); }

            return scopeTokenCounterList1;
        }
        public List<scopeTokenCounter> operatorsLL1(code st)
        {
            scopeTokenCounterList2.Add(new scopeTokenCounter(st.ScopeId, st.containingScopeId, st.name, operationsCounterGS, st.type));
            foreach (function f in st.Allfunctions)
                scopeTokenCounterList2.Add(new scopeTokenCounter(f.ScopeId, f.containingScopeId, f.name, f.operationsCounter, "function"));
            foreach (code cd in st.structes)
            { scopeTokenCounterList2.Concat(cd.operatorsLL1(cd)).ToList(); }

            return scopeTokenCounterList2;
        }
        public List<scopeTokenCounter> datatypesLL1(code st)
        {

            scopeTokenCounterList3.Add(new scopeTokenCounter(st.ScopeId, st.containingScopeId, st.name, dataTypesCounterGS, st.type));
            foreach (function f in st.Allfunctions)
                scopeTokenCounterList3.Add(new scopeTokenCounter(f.ScopeId, f.containingScopeId, f.name, f.dataTypesCounter, "function"));
            foreach (code cd in st.structes)
            { scopeTokenCounterList3.Concat(cd.datatypesLL1(cd)).ToList(); }

            return scopeTokenCounterList3;
        }
        public List<scopeTokenCounter> ValuesLL1(code st)
        {

            scopeTokenCounterList4.Add(new scopeTokenCounter(st.ScopeId, st.containingScopeId, st.name, valuesCounterGS, st.type));
            foreach (function f in st.Allfunctions)
                scopeTokenCounterList4.Add(new scopeTokenCounter(f.ScopeId, f.containingScopeId, f.name, f.valuesCounter, "function"));
            foreach (code cd in st.structes)
            { scopeTokenCounterList4.Concat(cd.ValuesLL1(cd)).ToList(); }

            return scopeTokenCounterList4;
        }
        public List<scopeTokenCounter> specialCharList(code st)
        {
            scopeTokenCounterList8.Add(new scopeTokenCounter(st.ScopeId, st.containingScopeId, st.name, specialCharGS, st.type));
            foreach (function f in st.Allfunctions)
                scopeTokenCounterList8.Add(new scopeTokenCounter(f.ScopeId, f.containingScopeId, f.name, f.specialChar, "function"));
            foreach (code cd in st.structes)
            { scopeTokenCounterList8.Concat(cd.specialCharList(cd)).ToList(); }

            return scopeTokenCounterList8;
        }
        public List<scopeVarCounter> VarsLL1(code st)
        {

            scopeTokenCounterList5.Add(new scopeVarCounter(st.ScopeId, st.containingScopeId, st.name, variablesCounterGS, st.type));
            foreach (function f in st.Allfunctions)
                scopeTokenCounterList5.Add(new scopeVarCounter(f.ScopeId, f.containingScopeId, f.name, f.variablesCounter, "function"));
            foreach (code cd in st.structes)
            { scopeTokenCounterList5.Concat(cd.VarsLL1(cd)).ToList(); }

            return scopeTokenCounterList5;
        }
        public List<scopeArrayCounter> ArraysLL1(code st)
        {

            scopeTokenCounterList6.Add(new scopeArrayCounter(st.ScopeId, st.containingScopeId, st.name, arraysCounterGS, st.type));
            foreach (function f in st.Allfunctions)
                scopeTokenCounterList6.Add(new scopeArrayCounter(f.ScopeId, f.containingScopeId, f.name, f.arraysCounter, "function"));
            foreach (code cd in st.structes)
            { scopeTokenCounterList6.Concat(cd.ArraysLL1(cd)).ToList(); }

            return scopeTokenCounterList6;
        }
        public List<scopePointersCounter> pointersLL1(code st)
        {

            scopeTokenCounterList7.Add(new scopePointersCounter(st.ScopeId, st.containingScopeId, st.name, pointersCounterGS, st.type));
            foreach (function f in st.Allfunctions)
                scopeTokenCounterList7.Add(new scopePointersCounter(f.ScopeId, f.containingScopeId, f.name, f.pointersCounter, "function"));
            foreach (code cd in st.structes)
            { scopeTokenCounterList7.Concat(cd.pointersLL1(cd)).ToList(); }

            return scopeTokenCounterList7;
        }
        public List<scopefunctionCallCounter> codeFuncCalls(code st)
        {

            foreach (function f in st.Allfunctions)
                scopeTokenCounterList9.Add(new scopefunctionCallCounter(f.ScopeId, f.containingScopeId, f.name, f.codeFunctionCallsCounter, "function"));
            foreach (code cd in st.structes)
            { scopeTokenCounterList9.Concat(cd.codeFuncCalls(cd)).ToList(); }

            return scopeTokenCounterList9;
        }
        public List<scopefunctionCallCounter> freqUsedFuncCalls(code st)
        {

            foreach (function f in st.Allfunctions)
                scopeTokenCounterList11.Add(new scopefunctionCallCounter(f.ScopeId, f.containingScopeId, f.name, f.frequentlyUsedFunctionCallsCounter, "function"));
            foreach (code cd in st.structes)
            { scopeTokenCounterList11.Concat(cd.freqUsedFuncCalls(cd)).ToList(); }

            return scopeTokenCounterList11;
        }
        public List<scopeTokens> scopeTokens(code st)
        {
            scopeTokenCounterList10.Add(new scopeTokens(st.ScopeId, st.containingScopeId, st.name, st.globalScobeTokens, st.type));
            foreach (function f in st.Allfunctions)
                scopeTokenCounterList10.Add(new scopeTokens(f.ScopeId, f.containingScopeId, f.name, f.getTokens, "function"));
            foreach (code cd in st.structes)
            { scopeTokenCounterList10.Concat(cd.scopeTokens(cd)).ToList(); }

            return scopeTokenCounterList10;
        }
    }
    #endregion
}


