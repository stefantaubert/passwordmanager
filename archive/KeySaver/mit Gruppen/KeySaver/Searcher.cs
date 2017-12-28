using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApplication1
{
    class Searcher
    {
        List<string> _schlagwörter = new List<string>();
        List<int> _indexes = new List<int>();

        public Searcher(List<string> rubriken)
        {
            _schlagwörter.Clear();
            _indexes.Clear();
            for (int i = 0; i < rubriken.Count; i++)
            {
                foreach (var wort in rubriken[i].Split(' '))
                {
                    _schlagwörter.Add(wort.Trim().ToLower());
                    _indexes.Add(i);
                }
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
