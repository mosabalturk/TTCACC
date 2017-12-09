using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class cppFile
    {
        public result result = new result();
        int commentLines=0, commentLetters=0;
        public code code { set; get; }
        public List<token> holeCodeTokens;//done
        private string cStr;//done
        public string name;//done
        public  List<token> defines = new List<token>();
        //# define varName value
        // define.type = value of define 
        // defin.lexeme = varName
        private List<string> libraries = new List<string>();//done
        public List<string> getLibraries { get { return libraries; } }
        public List<token> getDefines { get { return defines; } }//ساويه من نوع ايدنتيفير
        public string RemoveCommentsAndSpaces(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();
            value = deleteBetween(value, "//", "\n");
            string temp = value.Replace("  ", " ").Replace("} ;", "};").Replace("\r\n", " ").Replace("\n", " ").Replace("\t", " ").Replace("\r", " ").Replace(lineSeparator, " ").Replace(paragraphSeparator, " ");

            if (value == temp)
                return value.Replace("  ", " ").Replace("} ;", "};").Replace("\r\n", " ").Replace("\n", " ").Replace("\t", " ").Replace("\r", " ").Replace(lineSeparator, " ").Replace(paragraphSeparator, " ");
            else return RemoveCommentsAndSpaces(temp);
        }
        public string deleteBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            int strlength = strSource.Length;

            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0);
                End = strSource.IndexOf(strEnd, Start + strStart.Length) + strEnd.Length;
                if (End == 0)//I added this if statment to delete comment lin if it was the last line in the code
                    End = strlength;//you can delete this if  statement but it will make error if the fuction found start variable and didnt find the End
                string get = strSource.Substring(Start, End - Start);
                commentLetters += get.Length;
                string result = strSource.Replace(get, " ");
                if (strSource == result)
                    return result;
                else { this.commentLines++; return deleteBetween(result, strStart, strEnd); }
            }
            else
            {
                return strSource;
            }
        }
        public cppFile(string codestr, string filename)
        {
            codestr = RemoveCommentsAndSpaces(codestr);//delete comments
            this.cStr = codestr;//add hole code to cStr string
            this.name = filename;//filename or class or struct name
            Analyzer temp = new Analyzer(); // anlyzer class is the class that analyze code to tokens and lexemes
            holeCodeTokens = temp.Result2(codestr);//analyze code to tokens and lexemes to this list
            try
            {
                findLibrariesAndDefines();
                
                code = new code(codestr, filename, ++code.idno, holeCodeTokens,  this,"GS");
            }
            catch (Exception e)
            { result.ERROR = true; result.errormsg = e.ToString(); }
            try
            {
                code.recognizeIdentifiers(code);
            }
            catch (Exception e)
            { result.ERROR = true; result.errormsg = e.ToString(); System.Windows.Forms.MessageBox.Show(e.ToString()); }
            code.countAllScopes(code);
            setResults(code);
            result.filename = code.filename;
            token.zeroIdCounter();
        }        
        public void findLibrariesAndDefines()
        {
            if (holeCodeTokens != null)
                for (int i = 0; i < holeCodeTokens.Count; i++)
                {
                    if ((holeCodeTokens[i].getLexeme() == "#") && (holeCodeTokens[i + 1].getLexeme() == "include"))
                    {
                        string lib = "#include";
                        int cntr = 1;
                        while ((holeCodeTokens[i + cntr].getLexeme() != ">") && (holeCodeTokens[i + cntr].getType() != "vstring"))
                        { lib += holeCodeTokens[i + cntr + 1].getLexeme(); cntr++; }
                        cntr++;
                        for (int j = i; j < i + cntr - 1; j++)
                            holeCodeTokens.RemoveAt(i + 1);
                        holeCodeTokens[i].setLexeme(lib);
                        holeCodeTokens[i].setType("library");
                        libraries.Add(lib);
                    }
                    if ((holeCodeTokens[i].getLexeme() == "#") && (holeCodeTokens[i + 1].getLexeme() == "define"))
                    {
                        holeCodeTokens[i + 2].setType(holeCodeTokens[i + 3].getLexeme());
                        defines.Add(holeCodeTokens[i + 2]);
                        holeCodeTokens.RemoveAt(i);
                        holeCodeTokens.RemoveAt(i);
                        holeCodeTokens.RemoveAt(i);//error it could be operation like (2/2)
                        holeCodeTokens.RemoveAt(i);
                        i--;
                    }
                }
        }
        public void setResults( code t)
        {
            
            result.keyWord = t.keywordsLL1(t);
            result.operations = t.operatorsLL1(t);
            result.datatypes = t.datatypesLL1(t);
            result.values = t.ValuesLL1(t);
            result.vars = t.VarsLL1(t);
            result.pointrs = t.pointersLL1(t);
            result.arrays = t.ArraysLL1(t);
            result.libraries = libraries;
            result.specialChar = t.specialCharList(t);
            result.functionCalls = t.funcCalls(t);
            result.commentLetters = commentLetters;
            result.commentLines = commentLines;
            result.setOpAll();
            result.setKWAll();
            result.setDTAll();
            result.setValuesAll();
            result.tokens = t.scopeTokens(t);            
            code.zeroStatics();
        }
    }

}
