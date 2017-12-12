using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    class ComparisonTable
    {
        private tokenCounter CongruentTokenCounter;
        private pointerCounter CongruentPointerCounter;
        private arrayCounter CongruentArrayCounter;
        private variableCounter CongruentVariableCounter;
        private functionCallCounter CongruentFunctionCallCounterCounter;
        private int FirstScopeId;
        private int SecondScopeId;
        private string FirstFileName;
        private string SecondFileName;

        public ComparisonTable(tokenCounter congruentTokenCounter, int firstScopeId, int secondScopeId)
        {
            CongruentTokenCounter = congruentTokenCounter;
            FirstScopeId = firstScopeId;
            SecondScopeId = secondScopeId;
        }

        public ComparisonTable(pointerCounter congruentPointerCounter, int firstScopeId, int secondScopeId)
        {
            CongruentPointerCounter = congruentPointerCounter;
            FirstScopeId = firstScopeId;
            SecondScopeId = secondScopeId;
        }

        public ComparisonTable(arrayCounter congruentArrayCounter, int firstScopeId, int secondScopeId)
        {
            CongruentArrayCounter = congruentArrayCounter;
            FirstScopeId = firstScopeId;
            SecondScopeId = secondScopeId;
        }

        public ComparisonTable(variableCounter congruentVariableCounter, int firstScopeId, int secondScopeId)
        {
            CongruentVariableCounter = congruentVariableCounter;
            FirstScopeId = firstScopeId;
            SecondScopeId = secondScopeId;
        }

        public ComparisonTable(functionCallCounter congruentFunctionCallCounterCounter, int firstScopeId, int secondScopeId)
        {
            CongruentFunctionCallCounterCounter = congruentFunctionCallCounterCounter;
            FirstScopeId = firstScopeId;
            SecondScopeId = secondScopeId;
        }

        public void SetFileNames(string firstFileName, string secondFileName)
        {
            FirstFileName = firstFileName;
            SecondFileName = secondFileName;
        }

        public tokenCounter GetCongruentTokenCounter()
        {
            return CongruentTokenCounter;
        }

        public pointerCounter GetCongruentPointerCounter()
        {
            return CongruentPointerCounter;
        }

        public arrayCounter GetCongruentArrayCounter()
        {
            return CongruentArrayCounter;
        }

        public variableCounter GetCongruentVariableCounter()
        {
            return CongruentVariableCounter;
        }

        public functionCallCounter GetCongruentFunctionCallCounterCounter()
        {
            return CongruentFunctionCallCounterCounter;
        }

        public int GetFirstScopeId()
        {
            return FirstScopeId;
        }

        public int GetSecondScopeId()
        {
            return SecondScopeId;
        }

        public string GetFirstFileName()
        {
            return FirstFileName;
        }

        public string GetSecondFileName()
        {
            return SecondFileName;
        }
    }
    //////////////////////////////////////////////////////////////////////////////
    class Temp
    {
        public string fn1;
        public string fn2;
        public int comparisonResult;
        public Temp(string fn1, string fn2, int comparisonResult)
        {
            this.fn1 = fn1;
            this.fn2 = fn2;
            this.comparisonResult = comparisonResult;
        }
    }
    //////////////////////////////////////////////////////////////////////////////
    class Comparison
    {
        public static List<ComparisonTable> StaticComparisonTable = new List<ComparisonTable>();
        //////////////////////////////////////////////////////////////////////////////
        public static List<Temp> StaticTemp = new List<Temp>();
        //////////////////////////////////////////////////////////////////////////////
        public static void CompareCppFiles(List<cppFile> allFilesList)
        {
            for (int i = 0; i < allFilesList.Count - 1; i++)
            {
                for (int j = i + 1; j < allFilesList.Count; j++)
                {
                    StaticComparisonTable = StaticComparisonTable.Concat(CompareCppFiles(allFilesList[i], allFilesList[j])).ToList();
                    //////////////////////////////////////////////////////////////////////////////
                    StaticTemp.Add(new Temp(allFilesList[i].name, allFilesList[j].name, StaticComparisonTable.Count));
                    StaticComparisonTable.Clear();
                    //////////////////////////////////////////////////////////////////////////////
                }
            }
        }

        public static List<ComparisonTable> CompareCppFiles(cppFile file1, cppFile file2)
        {
            List<ComparisonTable> CT = new List<ComparisonTable>();
            CT = CT.Concat(CompareScopesAddToComparisonTable(file1.result.keyWords, file2.result.keyWords)).ToList();
            CT = CT.Concat(CompareScopesAddToComparisonTable(file1.result.operations, file2.result.operations)).ToList();
            CT = CT.Concat(CompareScopesAddToComparisonTable(file1.result.values, file2.result.values)).ToList();
            CT = CT.Concat(CompareScopesAddToComparisonTable(file1.result.specialChars, file2.result.specialChars)).ToList();
            //CT = CT.Concat(CompareScopesAddToComparisonTable(file1.result.pointrs, file2.result.pointrs)).ToList();
            //CT = CT.Concat(CompareScopesAddToComparisonTable(file1.result.arrays, file2.result.arrays)).ToList();
            //CT = CT.Concat(CompareScopesAddToComparisonTable(file1.result.vars, file2.result.vars)).ToList();
            //CT = CT.Concat(CompareScopesAddToComparisonTable(file1.result.functionCalls, file2.result.functionCalls)).ToList();

            foreach (ComparisonTable CTRow in CT)
            {
                CTRow.SetFileNames(file1.name, file2.name);
            }
            return CT;
        }

        public static List<ComparisonTable> CompareScopesAddToComparisonTable(List<scopeTokenCounter> scopeList1, List<scopeTokenCounter> scopeList2)
        {
            List<ComparisonTable> CT = new List<ComparisonTable>();
            foreach (scopeTokenCounter stc1 in scopeList1)
            {
                foreach (scopeTokenCounter stc2 in scopeList2)
                {
                    if (stc1.getScopeType() == stc2.getScopeType())
                    {
                        CT = CT.Concat(CompareScopesContent(stc1, stc2)).ToList();
                    }
                }
            }
            return CT;
        }

        public static List<ComparisonTable> CompareScopesAddToComparisonTable(List<scopePointersCounter> scopeList1, List<scopePointersCounter> scopeList2)
        {
            List<ComparisonTable> CT = new List<ComparisonTable>();
            foreach (scopePointersCounter spc1 in scopeList1)
            {
                foreach (scopePointersCounter spc2 in scopeList2)
                {
                    if (spc1.getScopeType() == spc1.getScopeType())
                    {
                        CT = CT.Concat(CompareScopesContent(spc1, spc2)).ToList();
                    }
                }
            }
            return CT;
        }

        public static List<ComparisonTable> CompareScopesAddToComparisonTable(List<scopeArrayCounter> scopeList1, List<scopeArrayCounter> scopeList2)
        {
            List<ComparisonTable> CT = new List<ComparisonTable>();
            foreach (scopeArrayCounter sac1 in scopeList1)
            {
                foreach (scopeArrayCounter sac2 in scopeList2)
                {
                    if (sac1.getScopeType() == sac2.getScopeType())
                    {
                        CT = CT.Concat(CompareScopesContent(sac1, sac2)).ToList();
                    }
                }
            }
            return CT;
        }

        public static List<ComparisonTable> CompareScopesAddToComparisonTable(List<scopeVarCounter> scopeList1, List<scopeVarCounter> scopeList2)
        {
            List<ComparisonTable> CT = new List<ComparisonTable>();
            foreach (scopeVarCounter svc1 in scopeList1)
            {
                foreach (scopeVarCounter svc2 in scopeList2)
                {
                    if (svc1.getScopeType() == svc2.getScopeType())
                    {
                        CT = CT.Concat(CompareScopesContent(svc1, svc2)).ToList();
                    }
                }
            }
            return CT;
        }

        public static List<ComparisonTable> CompareScopesAddToComparisonTable(List<scopefunctionCallCounter> scopeList1, List<scopefunctionCallCounter> scopeList2)
        {
            List<ComparisonTable> CT = new List<ComparisonTable>();
            foreach (scopefunctionCallCounter sfcc1 in scopeList1)
            {
                foreach (scopefunctionCallCounter sfcc2 in scopeList2)
                {
                    if (sfcc1.getScopeType() == sfcc2.getScopeType())
                    {
                        CT = CT.Concat(CompareScopesContent(sfcc1, sfcc2)).ToList();
                    }
                }
            }
            return CT;
        }

        public static List<ComparisonTable> CompareScopesContent(scopeTokenCounter scope1, scopeTokenCounter scope2)
        {
            List<ComparisonTable> CT = new List<ComparisonTable>();
            foreach (tokenCounter tc1 in scope1.counter)
            {
                foreach (tokenCounter tc2 in scope2.counter)
                {
                    if (tc1.getCount() == tc2.getCount() && tc1.getLexeme() == tc2.getLexeme())
                    {
                        CT.Add(new ComparisonTable(tc1.copy(), scope1.scopeId, scope2.scopeId));
                    }
                }
            }
            return CT;
        }

        public static List<ComparisonTable> CompareScopesContent(scopePointersCounter scope1, scopePointersCounter scope2)
        {
            List<ComparisonTable> CT = new List<ComparisonTable>();
            foreach (pointerCounter pc1 in scope1.pointerCounter)
            {
                foreach (pointerCounter pc2 in scope2.pointerCounter)
                {
                    if (pc1.dataType != null && pc2.dataType != null)
                    {
                        if (pc1.getCount() == pc2.getCount() && pc1.dataType.getLexeme() == pc2.dataType.getLexeme() && pc1.pointerLevel == pc2.pointerLevel)
                        {
                            CT.Add(new ComparisonTable(pc1.copy(), scope1.scopeId, scope2.scopeId));
                        }
                    }
                    else if (pc1.getCount() == pc2.getCount() && pc1.pointerLevel == pc2.pointerLevel)
                    {
                        CT.Add(new ComparisonTable(pc1.copy(), scope1.scopeId, scope2.scopeId));
                    }
                }
            }
            return CT;
        }

        public static List<ComparisonTable> CompareScopesContent(scopeArrayCounter scope1, scopeArrayCounter scope2)
        {
            List<ComparisonTable> CT = new List<ComparisonTable>();
            foreach (arrayCounter ac1 in scope1.arrayCounter)
            {
                foreach (arrayCounter ac2 in scope2.arrayCounter)
                {
                    if (ac1.getCount() == ac2.getCount() /*&& ac1.dataType.getLexeme() == ac2.dataType.getLexeme()*/ && ac1.arrayIndices.Count == ac2.arrayIndices.Count) //The last condition can be substituted by (ac1.arrayIndices.SequenceEqual(ac2.arrayIndices)) for more accurate results, if it is certine that the indecies have values.
                    {
                        CT.Add(new ComparisonTable(ac1.copy(), scope1.scopeId, scope2.scopeId));
                    }
                }
            }
            return CT;
        }

        public static List<ComparisonTable> CompareScopesContent(scopeVarCounter scope1, scopeVarCounter scope2)
        {
            List<ComparisonTable> CT = new List<ComparisonTable>();
            foreach (variableCounter vc1 in scope1.vars)
            {
                foreach (variableCounter vc2 in scope2.vars)
                {
                    if (vc1.getCount() == vc2.getCount() && vc1.dataType.getLexeme() == vc2.dataType.getLexeme())
                    {
                        CT.Add(new ComparisonTable(vc1.copy(), scope1.scopeId, scope2.scopeId));
                    }
                }
            }
            return CT;
        }

        public static List<ComparisonTable> CompareScopesContent(scopefunctionCallCounter scope1, scopefunctionCallCounter scope2)
        {
            List<ComparisonTable> CT = new List<ComparisonTable>();
            foreach (functionCallCounter fcc1 in scope1.functionCalls)
            {
                foreach (functionCallCounter fcc2 in scope2.functionCalls)
                {
                    if (fcc1.getCount() == fcc2.getCount()/* && fcc1.dataType == fcc2.dataType*/)
                    {
                        CT.Add(new ComparisonTable(fcc1.copy(), scope1.scopeId, scope2.scopeId));
                    }
                }
            }
            return CT;
        }

    }
}
