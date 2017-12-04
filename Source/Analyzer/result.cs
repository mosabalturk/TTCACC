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
        public List<identifier> vars = new List<identifier>();
        public List<pointer> pointrs = new List<pointer>();
        public List<array> arrays = new List<array>();
    }

    public class scopeTokenCounter
    {
        public int scopeId;
        public int containId;
        public List<tokenCounter> counter;
        public List<identifier> vars;
        public List<pointer> pointrs;
        public List<array> arrays;
        public scopeTokenCounter(int scopeId, int containId, List<tokenCounter> counter)
        {
            this.scopeId = scopeId;
            this.containId = containId;
            this.counter = counter;
        }

    }
}
