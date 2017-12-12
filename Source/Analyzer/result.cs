using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class result
    {
        private string _filename;//done
        public string filename { get { return _filename; } set { _filename = value; } }//done
        public bool ERROR = false;//done
        public string errormsg = "";//done
        public int commentLines, commentLetters;//done
        public List<token> allTokens = new List<token>();//done
        public List<scopeTokens> tokens = new List<scopeTokens>();//done

        public List<scopeTokenCounter> keyWords = new List<scopeTokenCounter>();//1
        public List<scopeTokenCounter> operations = new List<scopeTokenCounter>();//2
        public List<scopeTokenCounter> datatypes = new List<scopeTokenCounter>();//3
        public List<scopeTokenCounter> values = new List<scopeTokenCounter>();//4
        public List<scopeTokenCounter> specialChars = new List<scopeTokenCounter>();//5
        public List<scopeVarCounter> vars = new List<scopeVarCounter>();//6
        public List<scopePointersCounter> pointrs = new List<scopePointersCounter>();//7
        public List<scopeArrayCounter> arrays = new List<scopeArrayCounter>();//8
        public List<scopefunctionCallCounter> functionCalls = new List<scopefunctionCallCounter>();//9
        public List<tokenCounter> libraries = new List<tokenCounter>();//10
        public List<tokenCounter> operationsAllFile = new List<tokenCounter>();//11
        public List<tokenCounter> keyWordsAllFile = new List<tokenCounter>();//12
        public List<tokenCounter> dataTypesAllFile = new List<tokenCounter>();//13
        public List<tokenCounter> valuesAllFile = new List<tokenCounter>();//14
        public List<tokenCounter> specialCharAllFile = new List<tokenCounter>();//14
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
        public void setSpecialCahrAll()
        {
            foreach (var item in specialChars)
            {
                foreach (tokenCounter t in item.counter)
                {
                    if (specialCharAllFile.Select(a => a.getLexeme()).Contains(t.getLexeme()))
                    {
                        tokenCounter t2 = specialCharAllFile.Find(a => a.getLexeme() == t.getLexeme());
                        t2.setCount(t2.getCount() + t.getCount());
                    }
                    else
                        specialCharAllFile.Add(t.copy());
                    
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
            foreach (var item in keyWords)
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
                   if( !(new List<string>(){
                "bool", "char","short","float","int","FILE","int32","double","long","void"}.Exists(element => t.getLexeme() == element)))
                    {
                        if (dataTypesAllFile.Select(a => a.getLexeme()).Contains(""))
                        {
                            tokenCounter t2 = dataTypesAllFile.Find(a => a.getLexeme() == "");
                            t2.setCount(t2.getCount() + t.getCount());
                        }
                        else
                        {
                            tokenCounter t2 = new tokenCounter(new token("", ""));
                            t2.setCount(t.getCount());
                            dataTypesAllFile.Add(t2.copy());
                        }
                    }
                    else if (dataTypesAllFile.Select(a => a.getLexeme()).Contains(t.getLexeme()))
                    {
                        tokenCounter t2 = dataTypesAllFile.Find(a => a.getLexeme() == t.getLexeme());
                        t2.setCount(t2.getCount() + t.getCount());
                    }
                    else
                        dataTypesAllFile.Add(t.copy());
                }
            }
        }
        public static void adjustKeywords(List<scopeTokenCounter> stc)
        {

        }
        public static functionCallCounter[] getList()
        {
            return new functionCallCounter [] {new functionCallCounter(new token("",""))};
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
    public class scopeTokens
    {
        public string scopeName { get; set; }
        public int scopeId;
        public int containId;
        public List<token> tokens;
        string scopeType;
        public string getScopeType() { return scopeType; }
        bool isClass { get { if (scopeType == "class") return true; else return false; } }
        bool isStruct { get { if (scopeType == "struct") return true; else return false; } }
        bool isGS { get { if (scopeType == "GS") return true; else return false; } }
        bool isFunction { get { if (scopeType == "function") return true; else return false; } }
        public scopeTokens(int scopeId, int containId, string scopeName, List<token> tokens, string type)
        {
            this.scopeType = type;
            this.scopeName = scopeName;
            this.scopeId = scopeId;
            this.containId = containId;
            this.tokens = tokens;
        }

    }
}
