using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class token
    {
        public int id { get; set; }
        protected string lexeme;
        private string type;
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
        public bool isIdentifier() {  if (getType() == "identifier") return true; else return false;   }
        public bool isDatatype() { if (getType() == "datatype") return true; else return false; }
        public bool isKeyword() { if (getType() == "keyword") return true; else return false; }
        public bool isValue() { if ((getType() == "vint") || (getType() == "vchar") || (getType() == "vstring") || (getType() == "vfloat")) return true; else return false; }
        public bool isVfloat() { if (getType() == "vfloat") return true; else return false; }
        public bool isVint() { if (getType() == "vint") return true; else return false; }
        public bool isVchar() { if (getType() == "vchar") return true; else return false; }
        public bool isVstring() { if (getType() == "vstring") return true; else return false; }
        public bool isLibrary() { if (getType() == "library") return true; else return false; }

        public token(int id,string lexeme)
        {
            this.id = id;
            this.lexeme = lexeme;
            count = 0;
        }
        public token(int id, string lexeme,int count)
        {
            this.id = id;
            this.lexeme = lexeme;
            this.count = count;
        }
        public token(int id, string lexeme, string type)
        {
            this.id = id;
            this.lexeme = lexeme;
            this.type = type;
            this.count = 0;
        }
        public token(int id, string lexeme, string type, int count)
        {
            this.id = id;
            this.lexeme = lexeme;
            this.type = type;
            this.count = count;
        }
        public void setLexeme(string lexeme) { this.lexeme = lexeme; }
        public void setType(string type) { this.type = type; }
        public string getType() { return type; }
        public string getLexeme() { return lexeme; }





        private int count;
        #region identifiers region
        public bool Pointer = false;
        public bool array = false;
        public List<int> arrayBoundariy;
        public bool isPointer { get { return Pointer; } set { Pointer = value; } }
        public int pointerLevel { get; set; }
        public bool isArray { get { return array; } set { array = value; } }
        public int arrayDimensions { get; set; }
        public List<int> arrayBoundaries
        {
            get { return arrayBoundariy; }
            set
            {

                if (value.Count != arrayDimensions)
                {
                    throw new System.ArgumentException("arrayDimension must be equal to arrayBoundaries list elements count (EXCEPTION 1) ", "arrayBoundaries");
                }
                else
                    value = arrayBoundariy;
            }
        }

        #endregion
        public void SetCount(int count) { this.count = count; }

        public int GetCount() { return count; }
        public void incCount() { this.count++; }
        /// <summary>
        /// add sToken to the list or increment count if exist 
        /// </summary>
        /// <param name="sToken"></param>
        /// <param name="tkn"></param>
        public static void addOne(string sToken, List<token> tkn)
        {
            if (tkn.Exists(x => x.getLexeme() == sToken))
            {
                tkn.Find(x => x.getLexeme() == sToken).incCount();
            }
            else
            {
                int id = tkn.Count + 1;
                token temp = new token(id,sToken, 1);
                tkn.Add(temp);
            }
        }
        public token Copy()
        {
            var result = new token(this.id,this.lexeme);
            result.id = this.id;
            result.lexeme = this.lexeme;
            result.Pointer = this.Pointer;
            result.array = this.array;
            result.arrayBoundariy = this.arrayBoundariy;
            result.count = this.count;
            result.type = this.type;
            return result;
        }
    }

}
