using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class token
    {
        public int id { get; set; }
        public static int idCounter=0;
        public static void zeroIdCounter() { idCounter = 0; }
        protected string lexeme;
        private string type;
        private int count;
        public bool isPointer { get; set; }
        public int pointerLevel { get; set; }
        public bool isArray { get; set; }
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
        #region isBlabla bollean
        public bool isOperation() { if (getType() == "op") return true; else return false; }
        public bool isIdentifier() { if (getType() == "identifier") return true; else return false; }
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
        public token(string lexeme)
        {
            this.id = idCounter++;
            this.lexeme = lexeme;
            count = 1;
        }
        public token(string lexeme, string type)
        {
            this.id = idCounter++;
            this.lexeme = lexeme;
            this.type = type;
            this.count = 1;
        }
        public void setLexeme(string lexeme) { this.lexeme = lexeme; }
        public void setType(string type) { this.type = type; }
        public string getType() { return type; }
        public string getLexeme() { return lexeme; }
        public List<int> arrayIndexes = new List<int>();
        public static List<string> getLexemesFromList(List<token> tkns)
        {
            return tkns.Select(a => a.lexeme).ToList();
        }
        public int getCount() { return count; }
        public void incCount() { this.count++; }
        /// <summary>
        /// add sToken to the list or increment count if exist 
        /// </summary>
        /// <param name="sToken"></param>
        /// <param name="tkn"></param>
        public static void addOne(token TknObj, List<token> tkn, bool byLexeme)
        {
            if (byLexeme)
            {
                string sToken = TknObj.getLexeme();
                if (tkn.Exists(x => x.getLexeme() == sToken))
                {
                    tkn.Find(x => x.getLexeme() == sToken).incCount();
                }
                else
                {
                    TknObj.count = 1;
                    tkn.Add(TknObj);
                }
            }
            else
            {
                string dt = TknObj.getType();
                if (tkn.Exists(x => x.getType() == dt))
                {
                    tkn.Find(x => x.getType() == dt).incCount();
                }
                else
                {
                    token temp = new token("", TknObj.getType());
                    tkn.Add(temp);
                }

            }
        }
        public token Copy()
        {
            var result = new token(this.lexeme,type);
            result.id = this.id;            
            result.lexeme = this.lexeme;
            result.isArray = this.isArray;
            result.count = this.count;
            result.type = this.type;
            return result;
        }
    }

}
