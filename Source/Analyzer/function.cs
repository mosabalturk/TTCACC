using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class function
    {
        public List<tokenCounter> specialChar = new List<tokenCounter>();
        public List<variableCounter> variablesCounter = new List<variableCounter>();
        public List<pointerCounter> pointersCounter = new List<pointerCounter>();
        public List<arrayCounter> arraysCounter = new List<arrayCounter>();
        public List<tokenCounter> KeyWordsCounter = new List<tokenCounter>();
        public List<tokenCounter> dataTypesCounter = new List<tokenCounter>();
        public List<tokenCounter> valuesCounter = new List<tokenCounter>();
        public List<tokenCounter> operationsCounter = new List<tokenCounter>();

        bool protoType = false;
        public bool recursive { get; set; }
        public string fname;
        private string funcBody;
        public string funcDataType { get; set; }
        List<token> tokens;
        List<identifier> thisScopeVars = new List<identifier>();
        List<identifier> upperLevelVar = new List<identifier>();
        List<pointer> upperLevelPointers = new List<pointer>();
        List<pointer> thisScopePointers = new List<pointer>();
        List<array> upperLevelArray = new List<array>();
        List<array> thisScopeArray = new List<array>();
        List<token> holeCodeTokens;
        public void setTokens(List<token> funcTokens) { tokens = funcTokens; }
        private List<identifier> parameters = new List<identifier>();
        public string funcAsStr { get { return funcBody; } set { funcBody = value; } }
        public List<identifier> funcParameters { get { return parameters; } }

        public int ScopeId;
        public int containingScopeId;
        public function(string codee,int containingScopeId, ref List<token> tokens, List<identifier> parameters, string datatype, string funcName, ref List<token> holeCodeTokens, List<identifier> upperLevelVar, List<pointer> upperLevelPointers, List<array> upperLevelArray)
        {
            this.ScopeId = code.idno++;
            this.containingScopeId = containingScopeId;
            this.holeCodeTokens = holeCodeTokens;
            this.upperLevelVar = upperLevelVar;
            this.upperLevelPointers = upperLevelPointers;
            this.upperLevelArray = upperLevelArray;
            this.funcBody = codee;
            this.tokens = tokens;
            this.parameters = parameters;
            this.funcDataType = datatype;
            this.fname = funcName;
            for (int j = 0; j < tokens.Count(); j++)
                if ((tokens[j].getLexeme() == "return") && (tokens[j + 1].getLexeme() == funcName))
                    this.recursive = true;
            //findVar();
            count();
        }
        public function(List<identifier> parameters, int containingScopeId, string datatype, string funcName, bool protoType = true)
        {
           this.ScopeId = code.idno++;
            this.containingScopeId = containingScopeId;
            this.protoType = protoType;
            this.parameters = parameters;
            this.funcDataType = datatype;
            this.fname = funcName;
        }
        public string funcname { get { return fname; } set { fname = value; } }
        public void count()
        {
            foreach (token t in tokens)
            {
                if (t.isOperation())
                    tokenCounter.AddOneByLexeme(t, operationsCounter);
                else if (t.isKeyword())
                { tokenCounter.AddOneByLexeme(t, KeyWordsCounter); }
                else if (t.isDatatype())
                    tokenCounter.AddOneByLexeme(t, dataTypesCounter);
                else if (t.isValue())
                    tokenCounter.AddOneValueByDataType(t, valuesCounter);
                else if (t.isIdentifierObject())
                    variableCounter.AddOneByToken((identifier)t, variablesCounter);
                else if (t.isPointer)
                    pointerCounter.AddOneByToken((pointer)t, pointersCounter);
                else if (t.isArray)
                    arrayCounter.AddOneByToken((array)t, arraysCounter);
                else
                    tokenCounter.AddOneByLexeme(t, specialChar);
            }
        }

        public void findVar()
        {
            code.structAsDatatype(this.tokens);
            for (int i = 0; i < tokens.Count; i++)
            {
                identifier id;
                if ((tokens[i].getType() == "identifier") && !tokens[i].isPointer && !tokens[i].isArray)
                {
                    if ((tokens[i].getType() == "identifier") && (tokens[i + 1].getLexeme() == "("))
                        continue;
                        if ((i > 0) && tokens[i].isIdentifierTokenObject() && (tokens[i - 1].isDatatype()))
                    {
                        id = new identifier(tokens[i]);
                        if ((i > 0) && (tokens[i - 1].isDatatype()))
                            id.dataType = tokens[i - 1];
                        thisScopeVars.Add(id);
                        for (int q = 0; q < holeCodeTokens.Count; q++)
                            if (holeCodeTokens[q].id == tokens[i].id)
                                holeCodeTokens[q] = id;
                        tokens[i] = id;
                    }
                    else if (thisScopeVars.Select(a => a.getLexeme()).ToList().Contains(tokens[i].getLexeme()))
                    {
                        id = thisScopeVars.Find(a => a.getLexeme() == tokens[i].getLexeme());

                        for (int q = 0; q < holeCodeTokens.Count; q++)
                            if (holeCodeTokens[q].id == tokens[i].id)
                                holeCodeTokens[q] = id;

                        tokens[i] = id;
                    }
                    else if (upperLevelVar.Select(a => a.getLexeme()).ToList().Contains(tokens[i].getLexeme()))
                    {
                        id = upperLevelVar.Find(a => a.getLexeme() == tokens[i].getLexeme());
                        for (int q = 0; q < holeCodeTokens.Count; q++)
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
                        for (int q = 0; q < holeCodeTokens.Count; q++)
                            if (holeCodeTokens[q].id == tokens[i].id)
                                holeCodeTokens[q] = id;
                        tokens[i] = id;

                    }
                }

            }
        }
    }
}
