using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public static class  usefulStuff
    {
        public static void exportAllFilesInDirectory(string [] paths,string newPath)
        {
            foreach (string path in paths)
            {
                string foldername = new DirectoryInfo(path).Name;
                if (File.Exists(path))
                {
                    // This path is a file
                    
                    ProcessFile(path,  newPath, foldername);
                }
                else if (Directory.Exists(path))
                {
                    // This path is a directory
                    ProcessDirectory(path,  newPath);
                }
            }
        }
        public static void ProcessDirectory(string targetDirectory, string newPath)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            string foldername = new DirectoryInfo(targetDirectory).Name;
            foreach (string fileName in fileEntries)
            {
                
                ProcessFile(fileName, newPath, foldername);
            }
            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory,  newPath);
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path, string newPath, string foldername)
        {
            string fileName = Path.GetFileName(path);
            File.Copy(path, Path.Combine(newPath, foldername+".cpp"), true);
        }
        public static List<functionCallCounter> frequentlyUsedFunctions()
        {
            List<string> fu = new List<string>() {
               "fopen","fgetc","fclose","malloc","feof","fscanf","scanf","ceil","printf","fprintf","free","strlen","fseek",
"fgets","puts"
            };
            List<functionCallCounter> fc = new List<functionCallCounter>();
            foreach (string s in fu)
                fc.Add(new functionCallCounter(new token(s, "functionCall"), 0));
            return fc;
        }
        public static List<tokenCounter> dataTypes()
        {
            var sDataType2 = new List<string>(){
                "bool", "char","short","float","int","FILE","int32","double","long","void"};
            List<tokenCounter> fc = new List<tokenCounter>();
            foreach (string s in sDataType2)
                fc.Add(new tokenCounter(new tokenCounter(new token(s, "datatype"),0)));
            return fc;
        }
    }
}
