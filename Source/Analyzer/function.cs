using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class function
    {
        #region counterLists
        public List<tokenCounter> specialChar = new List<tokenCounter>();
        public List<variableCounter> variablesCounter = new List<variableCounter>();
        public List<pointerCounter> pointersCounter = new List<pointerCounter>();
        public List<arrayCounter> arraysCounter = new List<arrayCounter>();
        public List<tokenCounter> KeyWordsCounter = new List<tokenCounter>();
        public List<tokenCounter> dataTypesCounter = new List<tokenCounter>();
        public List<tokenCounter> valuesCounter = new List<tokenCounter>();
        public List<tokenCounter> operationsCounter = new List<tokenCounter>();
        public List<functionCallCounter> functionCalls = new List<functionCallCounter>();

        #endregion

        public int ScopeId;
        public int containingScopeId;
        public string name;
        private string funcBody;
        List<token> tokens;
        List<token> holeCodeTokens;
        bool protoType = false;
        public bool recursive { get; set; }
        public string funcDataType { get; set; }
        
        public List<token> getTokens {get{return tokens;} }
        public List<identifier> thisScopeVars = new List<identifier>();
        public List<identifier> upperLevelVar = new List<identifier>();
        public List<pointer> upperLevelPointers = new List<pointer>();
        List<pointer> thisScopePointers = new List<pointer>();
        public List<array> upperLevelArray = new List<array>();
        List<array> thisScopeArray = new List<array>();
        
        public void setTokens(List<token> funcTokens) { tokens = funcTokens; }
        private List<identifier> parameters = new List<identifier>();
        public string funcAsStr { get { return funcBody; } set { funcBody = value; } }
        public List<identifier> funcParameters { get { return parameters; } }

        public function(string codee,int containingScopeId, ref List<token> tokens, List<identifier> parameters, string datatype, string funcName, cppFile cppfile)
        {
            this.ScopeId = code.idno++;
            this.containingScopeId = containingScopeId;
            this.holeCodeTokens = cppfile.holeCodeTokens;
            this.funcBody = codee;
            this.tokens = tokens;
            this.parameters = parameters;
            this.funcDataType = datatype;
            this.name = funcName;
            for (int j = 0; j < tokens.Count(); j++)
                if ((tokens[j].getLexeme() == "return") && (tokens[j + 1].getLexeme() == funcName))
                    this.recursive = true;
        }
        public function(List<identifier> parameters, int containingScopeId, string datatype, string funcName, bool protoType = true)
        {
           this.ScopeId = code.idno++;
            this.containingScopeId = containingScopeId;
            this.protoType = protoType;
            this.parameters = parameters;
            this.funcDataType = datatype;
            this.name = funcName;
        }
        public string funcname { get { return name; } set { name = value; } }
        public void count()
        {
            for (int ii = 0; ii < tokens.Count; ii++)
            {
                if (tokens[ii].getType() == "library")
                { }
                else if (tokens[ii].isOperation())
                    tokenCounter.AddOneByLexeme(tokens[ii], operationsCounter);
                else if (tokens[ii].isKeyword())
                { tokenCounter.AddOneByLexeme(tokens[ii], KeyWordsCounter); }
                else if (tokens[ii].isDatatype())
                    tokenCounter.AddOneByLexeme(tokens[ii], dataTypesCounter);
                else if (tokens[ii].isValue())
                    tokenCounter.AddOneValueByDataType(tokens[ii], valuesCounter);
                else if (tokens[ii].isIdentifierObject())
                {
                    variableCounter.AddOneByToken(tokens[ii], ((identifier)tokens[ii]).dataType, variablesCounter);
                    if ((ii > 0) && ((tokens[ii - 1].getLexeme() == ",") || (tokens[ii - 1].isDatatype())) && (!tokens[ii + 1].isOperation()))
                        variablesCounter[variablesCounter.Count - 1].decCount();// we want count to be zero at initilization unless there is assignment afetr it like int b = 5; or init a, b=10,c;  a and c count equal zero but b count is 1

                }
                else if (tokens[ii].isPointer)
                    pointerCounter.AddOneByToken((pointer)tokens[ii], pointersCounter);
                else if (tokens[ii].isArray)
                    arrayCounter.AddOneByToken((array)tokens[ii], arraysCounter);
                else
                    tokenCounter.AddOneByLexeme(tokens[ii], specialChar);
            }
        }

        public void findIdentifiers()
        {
          //  code.structAsDatatype(this.tokens);
            for (int i = 0; i < tokens.Count-1; i++)
            {
                if ((tokens[i].isIdentifierTokenObject()) && (tokens[i + 1].getLexeme() == "("))
                {

                    functionCallCounter fc = new functionCallCounter(tokens[i]);
                    tokens[i] = fc;
                    for (int q = 0; q < holeCodeTokens.Count; q++)
                        if (holeCodeTokens[q].id == tokens[i].id)
                            holeCodeTokens[q] = fc;
                }

            }

            for (int i = 0; i < tokens.Count; i++)
            {
                
                bool pointer = upperLevelPointers.Concat(thisScopePointers).ToList().Select(a => a.getLexeme()).ToList().Contains(tokens[i].getLexeme());
                bool array = upperLevelArray.Concat(thisScopeArray).ToList().Select(a => a.getLexeme()).ToList().Contains(tokens[i].getLexeme());
                if ((tokens[i].isIdentifierTokenObject()) && !tokens[i].isPointer && !tokens[i].isArray && !array && !pointer)
                {
                    identifier id;

                    if ((i > 0) && tokens[i].isIdentifierTokenObject() && ((tokens[i - 1].isDatatype() || tokens[i - 1].getLexeme() == ",")))
                    {
                        id = new identifier(tokens[i]);
                        if ((i > 0) && (tokens[i - 1].isDatatype()))
                        { id.dataType = tokens[i - 1]; }
                        else if (tokens[i - 1].getLexeme() == ",")
                        {
                            int k = i - 1;
                            while ((k > 0) && (!tokens[k].isDatatype()))
                                k--;
                            if (k == 0)
                                break;
                            id.dataType = tokens[k];
                        }
                        thisScopeVars.Add(id);

                        //for (int q = 0; q < thisCodeToken.Count; q++)
                        //    if (thisCodeToken[q].id == globalScobeTokens[i].id)
                        //        thisCodeToken[q] = id;

                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == tokens[i].id)
                                holeCodeTokens[q] = id;

                        tokens[i] = id;
                    }
                    else if (thisScopeVars.Select(a => a.getLexeme()).ToList().Contains(tokens[i].getLexeme()))
                    {
                        id = thisScopeVars.Find(a => a.getLexeme() == tokens[i].getLexeme());

                        //for (int q = 0; q < thisCodeToken.Count; q++)
                        //    if (thisCodeToken[q].id == globalScobeTokens[i].id)
                        //        thisCodeToken[q] = id;
                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == tokens[i].id)
                                holeCodeTokens[q] = id;
                        tokens[i] = id;
                    }
                    else if (upperLevelVar.Select(a => a.getLexeme()).ToList().Contains(tokens[i].getLexeme()))
                    {
                        id = upperLevelVar.Find(a => a.getLexeme() == tokens[i].getLexeme());
                        //for (int q = 0; q < thisCodeToken.Count; q++)
                        //    if (thisCodeToken[q].id == globalScobeTokens[i].id)
                        //        thisCodeToken[q] = id;
                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == tokens[i].id)
                                holeCodeTokens[q] = id;
                        tokens[i] = id;

                    }
                    else
                    {
                        id = new identifier(tokens[i]);
                        if ((i > 0) && (tokens[i - 1].isDatatype()))
                            id.dataType = tokens[i - 1];
                        thisScopeVars.Add(id);
                        //for (int q = 0; q < thisCodeToken.Count; q++)
                        //    if (thisCodeToken[q].id == globalScobeTokens[i].id)
                        //        thisCodeToken[q] = id;
                        for (int q = 0; (q < holeCodeTokens.Count) && (holeCodeTokens != null); q++)
                            if (holeCodeTokens[q].id == tokens[i].id)
                                holeCodeTokens[q] = id;
                        tokens[i] = id;

                    }
                    continue;
                }
                if ((i > 0) && ((tokens[i].isPointer) || pointer))
                {
                    if ((i > 0) && tokens[i].isPointer &&( (tokens[i - 1].isDatatype()||(tokens[i - 1].getLexeme()==","))))
                    {
                        pointer p = (pointer)tokens[i];

                        if (tokens[i - 1].isDatatype())
                        { p.dataType = tokens[i - 1]; }
                        else if (tokens[i - 1].getLexeme() == ",")
                        {
                            int k = i - 1;
                            while ((k > 0) && (!tokens[k].isDatatype()))
                                k--;
                            if (k == 0)
                                break;
                            p.dataType = tokens[k];
                        }
                        thisScopePointers.Add(p);
                    }
                    else if (thisScopePointers.Select(a => a.getLexeme()).ToList().Contains(tokens[i].getLexeme()))
                    {
                        pointer id;
                        id = thisScopePointers.Find(a => a.getLexeme() == tokens[i].getLexeme());

                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == tokens[i].id)
                                holeCodeTokens[q] = id;
                        tokens[i] = id;
                    }
                    else if (upperLevelPointers.Select(a => a.getLexeme()).ToList().Contains(tokens[i].getLexeme()))
                    {
                        pointer id;
                        id = upperLevelPointers.Find(a => a.getLexeme() == tokens[i].getLexeme());

                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == tokens[i].id)
                                holeCodeTokens[q] = id;
                        tokens[i] = id;

                    }
                    continue;
                }
                if ((i > 0) && ((tokens[i].isArray) || array))
                {
                    if ((i > 0) && tokens[i].isPointer && (tokens[i - 1].isDatatype()))
                    {
                        array p = (array)tokens[i];
                        if (tokens[i - 1].isDatatype())
                        { p.dataType = tokens[i - 1]; }
                        else if (tokens[i - 1].getLexeme() == ",")
                        {
                            int k = i - 1;
                            while ((k > 0) && (!tokens[k].isDatatype()))
                                k--;
                            if (k == 0)
                                break;
                            p.dataType = tokens[k];
                        }
                        thisScopeArray.Add(p);
                    }
                    else if (thisScopeArray.Select(a => a.getLexeme()).ToList().Contains(tokens[i].getLexeme()))
                    {
                        array id;
                        id = thisScopeArray.Find(a => a.getLexeme() == tokens[i].getLexeme());

                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == tokens[i].id)
                                holeCodeTokens[q] = id;
                        tokens[i] = id;
                    }
                    else if (upperLevelArray.Select(a => a.getLexeme()).ToList().Contains(tokens[i].getLexeme()))
                    {
                        array id;
                        id = upperLevelArray.Find(a => a.getLexeme() == tokens[i].getLexeme());

                        for (int q = 0; (holeCodeTokens != null) && (q < holeCodeTokens.Count); q++)
                            if (holeCodeTokens[q].id == tokens[i].id)
                                holeCodeTokens[q] = id;
                        tokens[i] = id;

                    }
                    continue;
                }
            }
            
        }
    }
}
