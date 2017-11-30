using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Analyzer {
      public static class  Program {
        public static List<code> codes = new List<code>();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
        public static void addCodeToList(string code, string filename)
        {
            codes.Add(new code(code, filename));
        }
        public static string RemoveCommentsAndSpaces(this string value)
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
        public static string deleteBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            int strlength = strSource.Length;

            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) ;
                End = strSource.IndexOf(strEnd, Start + strStart.Length) + strEnd.Length;
                if (End == 0)//I added this if statment to delete comment lin if it was the last line in the code
                    End = strlength;//you can delete this if  statement but it will make error if the fuction found start variable and didnt find the End
                string get= strSource.Substring(Start, End- Start);
                string result = strSource.Replace(get, " ");
                if (strSource == result)
                    return result;
                else return deleteBetween(result, strStart, strEnd);
            }
            else
            {
                return strSource;
            }
        }

    }

}
