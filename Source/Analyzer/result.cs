using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class result
    {
        public bool ERROR = false;
        public string errormsg = "";
        public int commentLines, commentLetters;
        public List<scopeTokenCounter> keyWord = new List<scopeTokenCounter>();
        public List<scopeTokenCounter> operations = new List<scopeTokenCounter>();
        public List<scopeTokenCounter> datatypes = new List<scopeTokenCounter>();
        public List<scopeTokenCounter> values = new List<scopeTokenCounter>();
        public List<scopeTokenCounter> specialChar = new List<scopeTokenCounter>();
        public List<scopeVarCounter> vars = new List<scopeVarCounter>();
        public List<scopePointersCounter> pointrs = new List<scopePointersCounter>();
        public List<scopeArrayCounter> arrays = new List<scopeArrayCounter>();
        public List<scopefunctionCallCounter> functionCalls = new List<scopefunctionCallCounter>();
        public List<string> libraries = new List<string>();
        public List<tokenCounter> operationsAllFile = new List<tokenCounter>();
        public List<tokenCounter> keyWordsAllFile = new List<tokenCounter>();
        public List<tokenCounter> dataTypesAllFile = new List<tokenCounter>();
        public List<tokenCounter> valuesAllFile = new List<tokenCounter>();
        public void setValuesAll()
        {
            foreach (var item in values)
            {
                foreach (tokenCounter t in item.counter)
                {
                    if (valuesAllFile.Select(a => a.getType()).Contains(t.getType()))
                    {
                        tokenCounter t2 = valuesAllFile.Find(a => a.getType() == t.getType());
                        t2.setCount(t2.getCount() + t.getCount());
                    }
                    else
                    {
                        tokenCounter t5 = t.copy();
                        t5.setLexeme(t.getType());
                        valuesAllFile.Add(t5);
                    }
                }
            }
        }
        public void setOpAll()
        {
            foreach (var item in operations)
            {
                foreach (tokenCounter t in item.counter)
                {
                    if (operationsAllFile.Select(a => a.getLexeme()).Contains(t.getLexeme()))
                    {
                        tokenCounter t2 = operationsAllFile.Find(a => a.getLexeme() == t.getLexeme());
                        t2.setCount(t2.getCount() + t.getCount());
                    }
                    else
                        operationsAllFile.Add(t.copy());
                }
            }
        }
        public void setKWAll()
        {
            foreach (var item in keyWord)
            {
                foreach (tokenCounter t in item.counter)
                {
                    if (keyWordsAllFile.Select(a => a.getLexeme()).Contains(t.getLexeme()))
                    {
                        tokenCounter t2 = keyWordsAllFile.Find(a => a.getLexeme() == t.getLexeme());
                        t2.setCount(t2.getCount() + t.getCount());
                    }
                    else
                        keyWordsAllFile.Add(t.copy());
                }
            }
        }
        public void setDTAll()
        {
            foreach (var item in datatypes)
            {
                foreach (tokenCounter t in item.counter)
                {
                    if (dataTypesAllFile.Select(a => a.getLexeme()).Contains(t.getLexeme()))
                    {
                        tokenCounter t2 = dataTypesAllFile.Find(a => a.getLexeme() == t.getLexeme());
                        t2.setCount(t2.getCount() + t.getCount());
                    }
                    else
                        dataTypesAllFile.Add(t.copy());
                }
            }
        }
    }
    public class scopeTokenCounter
    {
        public string scopeName;
        public int scopeId;
        public int containId;
        public List<tokenCounter> counter;
        string scopeType;
        public string getScopeType() { return scopeType; }
        bool isClass { get { if (scopeType == "class") return true; else return false; } }
        bool isStruct { get { if (scopeType == "struct") return true; else return false; } }
        bool isGS { get { if (scopeType == "GS") return true; else return false; } }
        bool isFunction { get { if (scopeType == "function") return true; else return false; } }

        public scopeTokenCounter(int scopeId, int containId, string scopeName, List<tokenCounter> counter,string type)
        {
            this.scopeType = type;
            this.scopeName = scopeName;
            this.scopeId = scopeId;
            this.containId = containId;
            this.counter = counter;
        }

    }
    public class scopeVarCounter
    {
        public string scopeName;
        public int scopeId;
        public int containId;
        public List<variableCounter> vars;
        string scopeType;
        public string getScopeType() { return scopeType; }
        bool isClass { get { if (scopeType == "class") return true; else return false; } }
        bool isStruct { get { if (scopeType == "struct") return true; else return false; } }
        bool isGS { get { if (scopeType == "GS") return true; else return false; } }
        bool isFunction { get { if (scopeType == "function") return true; else return false; } }
        public scopeVarCounter(int scopeId, int containId, string scopeName, List<variableCounter> vars, string type)
        {
            this.scopeType = type;
            this.scopeName = scopeName;
            this.scopeId = scopeId;
            this.containId = containId;
            this.vars = vars;
        }

    }
    public class scopeArrayCounter
    {
        public string scopeName;
        public int scopeId;
        public int containId;
        public List<arrayCounter> arrayCounter;
        string scopeType;
        public string getScopeType() { return scopeType; }
        bool isClass { get { if (scopeType == "class") return true; else return false; } }
        bool isStruct { get { if (scopeType == "struct") return true; else return false; } }
        bool isGS { get { if (scopeType == "GS") return true; else return false; } }
        bool isFunction { get { if (scopeType == "function") return true; else return false; } }
        public scopeArrayCounter(int scopeId, int containId, string scopeName, List<arrayCounter> arrayCounter, string type)
        {
            this.scopeType = type;
            this.scopeName = scopeName;
            this.scopeId = scopeId;
            this.containId = containId;
            this.arrayCounter = arrayCounter;
        }

    }
    public class scopePointersCounter
    {
        public int scopeId;
        public int containId;
        public string scopeName;
        public List<pointerCounter> pointerCounter;
        string scopeType;
        public string getScopeType() { return scopeType; }
        bool isClass { get { if (scopeType == "class") return true; else return false; } }
        bool isStruct { get { if (scopeType == "struct") return true; else return false; } }
        bool isGS { get { if (scopeType == "GS") return true; else return false; } }
        bool isFunction { get { if (scopeType == "function") return true; else return false; } }
        public scopePointersCounter(int scopeId, int containId, string scopeName, List<pointerCounter> pointerCounter, string type)
        {
            this.scopeType = type;
            this.scopeId = scopeId;
            this.containId = containId;
            this.scopeName = scopeName;
            this.pointerCounter = pointerCounter;
        }

    }
    public class scopefunctionCallCounter
    {
        public string scopeName;
        public int scopeId;
        public int containId;
        public List<functionCallCounter> functionCalls;
        string scopeType;
        public string getScopeType() { return scopeType; }
        bool isClass { get { if (scopeType == "class") return true; else return false; } }
        bool isStruct { get { if (scopeType == "struct") return true; else return false; } }
        bool isGS { get { if (scopeType == "GS") return true; else return false; } }
        bool isFunction { get { if (scopeType == "function") return true; else return false; } }
        public scopefunctionCallCounter(int scopeId, int containId, string scopeName, List<functionCallCounter> functionCalls, string type)
        {
            this.scopeType = type;
            this.scopeName = scopeName;
            this.scopeId = scopeId;
            this.containId = containId;
            this.functionCalls = functionCalls;
        }

    }

}
