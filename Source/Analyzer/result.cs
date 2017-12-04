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
    }

    public class scopeTokenCounter
    {
        public int scopeId;
        public int containId;
        public List<tokenCounter> counter = new List<tokenCounter>();
        public scopeTokenCounter(int scopeId, int containId)
        {
            this.scopeId = scopeId;
            this.containId = containId;
        }
        public scopeTokenCounter(int scopeId, int containId, List<tokenCounter> counter)
        {
            this.scopeId = scopeId;
            this.containId = containId;
            this.counter = counter;
        }

    }
}
