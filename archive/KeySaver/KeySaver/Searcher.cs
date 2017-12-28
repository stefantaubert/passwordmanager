using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApplication1
{
    class Searcher
    {
        List<string> _schlagwörter = new List<string>();
        List<int> _indexes = new List<int>();

        public void Insert(List<string> rubriken, bool clear)
        {
            if (clear) { _schlagwörter.Clear(); _indexes.Clear(); }
            for (int i = 0; i < rubriken.Count; i++)
                Add(i, rubriken[i]);
        }
        public void Add(int index, string schlagwort)
        {
            foreach (var wort in schlagwort.Split(' '))
            {
                _schlagwörter.Add(wort.Trim().ToLower());
                _indexes.Add(index);
            }
        }

        public int[] Search(string such)
        {
            List<int> indexe = new List<int>();
            for (int i = 0; i < _schlagwörter.Count; i++)
                if (_schlagwörter[i].Contains(such.Trim().ToLower()))
                    if (indexe.Count == 0 || !indexe.Contains(_indexes[i]))
                        indexe.Add(_indexes[i]);
            return indexe.ToArray();
        }
    }
}
