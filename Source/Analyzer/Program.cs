using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Analyzer {
      public static class  Program {
        public static List<cppFile> cppFiles = new List<cppFile>();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
        public static void addCppFileToList(string code, string filename)
        {
            cppFiles.Add(new cppFile(code, filename));
        }

    }

}
