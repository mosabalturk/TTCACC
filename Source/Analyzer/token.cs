﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class token
    {

        public int id { get; set; }
        public static int idCounter = 0;
        public static void zeroIdCounter() { idCounter = 0; }
        protected string lexeme;
        protected string type;
        // type is equla to
        // identifier 
        // op
        // datatype 
        // keyword
        // vfloat (float as value)
        // vint (int as value)
        // vchar (char as value)
        // vstring (string as value)
        // library

        public token(string lexeme)
        {
            this.id = idCounter++;
            this.lexeme = lexeme;
        }
        public token(string lexeme, string type)
        {
            this.id = idCounter++;
            this.lexeme = lexeme;
            this.type = type;
        }
        public token(int id ,string lexeme, string type)
        {
            this.id = id;
            this.lexeme = lexeme;
            this.type = type;
        }
        //public token(token t)
        //{
        //    this.id = t.id;
        //    this.lexeme = t.getLexeme();
        //    //no set to type because this costructer will be called when declaring new variable or pointer or array so the type will be set there 
        //}
        public void setLexeme(string lexeme) { this.lexeme = lexeme; }
        public void setType(string type) { this.type = type; }
        public string getType() { return type; }
        public string getLexeme() { return lexeme; }
        public static List<string> getLexemesFromList(List<token> tkns)
        {
            return tkns.Select(a => a.lexeme).ToList();
        }

        /// <summary>
        /// add sToken to the list or increment count if exist 
        /// </summary>
        /// <param name="sToken"></param>
        /// <param name="tkn"></param>

        #region virtual region
        public virtual bool isPointer { get { return false; } }
        public virtual int pointerLevel { get { return 0; } set { value = 0; } }
        public virtual bool isArray { get { return false; } }
        public virtual List<token> arrayIndices { get { return null; } set { } }
        public virtual bool isVariable { get { return false; } }
        public virtual bool isFunctionCall { get { return false; } }

        #endregion
        #region isBlabla bollean
        public bool isOperation() { if (getType() == "op") return true; else return false; }
        public virtual bool isIdentifierTokenObject() { if (getType() == "identifier") return true; else return false; }
        public virtual bool isIdentifierObject() { return false; }
        public bool isDatatype() { if (getType() == "datatype") return true; else return false; }
        public bool isKeyword() { if (getType() == "keyword") return true; else return false; }
        public bool isValue() { if ((getType() == "vint") || (getType() == "vchar") || (getType() == "vstring") || (getType() == "vfloat")) return true; else return false; }
        public bool isInt() { if (getType() == "vint") return true; else return false; }
        public bool isFloat() { if (getType() == "vfloat") return true; else return false; }
        public bool isChar() { if (getType() == "vchar") return true; else return false; }
        public bool isString() { if (getType() == "vstring") return true; else return false; }
        public bool isVfloat() { if (getType() == "vfloat") return true; else return false; }
        public bool isVint() { if (getType() == "vint") return true; else return false; }
        public bool isVchar() { if (getType() == "vchar") return true; else return false; }
        public bool isVstring() { if (getType() == "vstring") return true; else return false; }
        public bool isLibrary() { if (getType() == "library") return true; else return false; }
        #endregion
    }

    public class identifier : token
    {
        public override bool isPointer { get { return false; } }
        public override int pointerLevel { get { return 0; } set { value = 0; } }
        public override bool isArray { get { return false; } }
        public override bool isIdentifierTokenObject() { return false; }
        public override bool isIdentifierObject() { return true; }
        public token dataType;
        public override bool isVariable { get { return true; } }
        public identifier(token t, token dataType,string type = "variable") : base(t.id,t.getLexeme(), type)
        {
            this.dataType = dataType;
        }
        public identifier(token t, string type = "variable") : base(t.id, t.getLexeme(), type)
        {
            this.dataType = new token("", "");
        }
    }
    public class array : identifier
    {
        public override bool isIdentifierObject() { return false; }
        List<token> _arrayIndices = new List<token>();
        public override bool isArray { get { return true; } }
        public override bool isVariable { get { return false; } }
        public override bool isPointer { get { return false; } }
        public override List<token> arrayIndices { get { return _arrayIndices; } set { _arrayIndices = new List<token>(value); } }
        public array(token t, token dataType) : base(t, dataType)
        {
            this.type = "array";
        }
        public array(token t) : base(t, "array")
        {
            this.dataType = new token("", "");

        }
        public array(array a) : base(a, a.dataType, "array")
        {


        }
    }
    public class pointer : identifier
    {
        public override bool isIdentifierObject() { return false; }
        public override bool isVariable { get { return false; } }
        public override bool isArray { get { return false; } }
        public override bool isPointer { get { return true; } }
        public override int pointerLevel { get; set; }
        public pointer(token t, token dataType, int pointerLevel) : base(t, dataType)
        {
            this.pointerLevel = pointerLevel;
            this.type = "pointer";

        }
        public pointer(token t, int pointerLevel) : base(t)
        {
            this.pointerLevel = pointerLevel;
            this.type = "pointer";

        }
        public pointer(pointer p) : base(p, p.dataType)
        {
            this.pointerLevel = p.pointerLevel;
            this.type = "pointer";

        }
    }
    public class variableCounter : identifier
    {
        int count;
        public void setCount(int value) { count = value; }
        public variableCounter copy()
        {
            variableCounter vc = new variableCounter(this);
            vc.count = this.count;
            return vc;
        }
        public variableCounter(token t, token dataType) : base(t, dataType)
        {
            this.type = "variable";
            count = 1;
        }
        public variableCounter(token t) : base(t)
        {
            this.type = "variable";
            count = 1;
        }
        public int getCount() { return count; }
        public void incCount() { this.count++; }
        public void decCount() { this.count--; }
        public static void AddOneByToken(token TknObj,token dt, List<variableCounter> tkn)
        {
            var t = tkn.FirstOrDefault(x => x.id == TknObj.id);
            if (t != null)
            {
                t.incCount();
            }
            else
            {
                tkn.Add(new variableCounter(TknObj,dt));
            }
        }
    }
    public class arrayCounter : array
    {

        public arrayCounter copy()
        {
            arrayCounter ac = new arrayCounter(this);
            ac.count = this.count;
            return ac;
        }
        int count; public void incCount() { this.count++; }
        public int getCount() { return count; }
        public static void AddOneByToken(array TknObj, List<arrayCounter> tkn)
        {
            var t = tkn.FirstOrDefault(x => x.id == TknObj.id);
            if (t != null)
            {
                t.incCount();
            }
            else
            {
                tkn.Add(new arrayCounter(TknObj));
            }
        }
        public arrayCounter(array t) : base(t)
        {
            count = 1;
        }
    }
    public class pointerCounter : pointer
    {
        public pointerCounter copy()
        {
            pointerCounter pt = new pointerCounter(this);
            pt.count = this.count;
            return pt;
        }

        int count; public void incCount() { this.count++; }
        public int getCount() { return count; }
        public pointerCounter(pointer t) : base(t)
        {
            count = 1;
        }
        public static void AddOneByToken(pointer TknObj, List<pointerCounter> tkn)
        {
            var t = tkn.FirstOrDefault(x => x.id == TknObj.id);
            if (t != null)
            {
                t.incCount();
            }
            else
            {
                tkn.Add(new pointerCounter(TknObj));
            }
        }
    }
    public class tokenCounter : token
    {
        int count;
        public tokenCounter(token t) : base(t.id,t.getLexeme(),t.getType())
        {
            this.count = 1;
            this.type = t.getType();
        }
        public tokenCounter(token t,int cout) : base(t.id, t.getLexeme(), t.getType())
        {
            this.count = cout;
            this.type = t.getType();
        }
        public int getCount() { return count; }
        public void setCount(int value) { count =value; }

        public void incCount() { this.count++; }
        public void decCount() { this.count--; }
        public static void AddOneByLexeme(token TknObj, List<tokenCounter> tkn)
        {
            string sToken = TknObj.getLexeme();
            if (tkn.Exists(x => x.getLexeme() == sToken))
            {
                tkn.Find(x => x.getLexeme() == sToken).incCount();
            }
            else
            {
                tkn.Add(new tokenCounter(TknObj));
            }
        }

        public static void AddOneValueByDataType(token TknObj, List<tokenCounter> tkn)
        {

            string dt = TknObj.getType();
            if (tkn.Exists(x => x.getType() == dt))
            {
                tkn.Find(x => x.getType() == dt).incCount();
            }
            else
            {
                tkn.Add(new tokenCounter(TknObj));
            }
        }


        public tokenCounter copy()
        {
            tokenCounter t = new tokenCounter(this);
            t.count = this.count;
            return t;
        }

    }
    public class functionCallCounter : identifier
    {
        int count;
        public void setCount(int value) { count = value; }
        public override bool isVariable { get { return false; } }
        public override bool isArray { get { return false; } }
        public override bool isPointer { get { return false; } }
        public override bool isIdentifierObject() { return false; }
        public functionCallCounter copy()
        {
            functionCallCounter fcc = new functionCallCounter(this);
            fcc.count = this.count;
            return fcc;
        }
        public override bool isFunctionCall { get { return true; } }

        public functionCallCounter(token t) : base(t)
        {
            this.count = 1;
            type = "functionCall";
        }
        public functionCallCounter(token t,int count) : base(t)
        {
            this.count = count;
            type = "functionCall";
        }
        public int getCount() { return count; }
        public void incCount() { this.count++; }
        public static void AddOneByLexeme(token TknObj, List<functionCallCounter> tkn)
        {
            string sToken = TknObj.getLexeme();
            if (tkn.Exists(x => x.getLexeme() == sToken))
            {
                tkn.Find(x => x.getLexeme() == sToken).incCount();
            }
            else
            {
                tkn.Add(new functionCallCounter(TknObj));
            }
        }
        public static void AddOneValueByDataType(token TknObj, List<tokenCounter> tkn)
        {

            string dt = TknObj.getType();
            if (tkn.Exists(x => x.getType() == dt))
            {
                tkn.Find(x => x.getType() == dt).incCount();
            }
            else
            {
                tkn.Add(new tokenCounter(TknObj));
            }
        }

    }

}

