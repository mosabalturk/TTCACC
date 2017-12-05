using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analyzer
{
    public class result
    {
        public List<scopeTokenCounter> keyWord = new List<scopeTokenCounter>();
        public List<scopeTokenCounter> operations = new List<scopeTokenCounter>();
        public List<scopeTokenCounter> datatypes = new List<scopeTokenCounter>();
        public List<scopeTokenCounter> values = new List<scopeTokenCounter>();
        public List<scopeTokenCounter> specialChar = new List<scopeTokenCounter>();
        public List<scopeVarCounter> vars = new List<scopeVarCounter>();
        public List<scopePointersCounter> pointrs = new List<scopePointersCounter>();
        public List<scopeArrayCounter> arrays = new List<scopeArrayCounter>();
        public List<string> libraries = new List<string>();

    }

    public class scopeTokenCounter
    {
        public string scopeName;
        public int scopeId;
        public int containId;
        public List<tokenCounter> counter;
        public scopeTokenCounter(int scopeId, int containId, string scopeName, List<tokenCounter> counter)
        {
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
        public scopeVarCounter(int scopeId, int containId, string scopeName, List<variableCounter> vars)
        {
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
        public scopeArrayCounter(int scopeId, int containId, string scopeName, List<arrayCounter> arrayCounter)
        {
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
        public scopePointersCounter(int scopeId, int containId, string scopeName, List<pointerCounter> pointerCounter)
        {
            this.scopeId = scopeId;
            this.containId = containId;
            this.scopeName = scopeName;
            this.pointerCounter = pointerCounter;
        }

    }
}
