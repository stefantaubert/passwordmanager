namespace PM.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class MarkdownExporter
    {
        public static void Export(Model obj, string fileName)
        {
            var result = string.Empty;

            foreach (var entry in obj.Entries.OrderBy(s => s.Created).Reverse())
            {
                result += string.Format("#{0}\n\n", entry.Content.Trim());
            }

            File.WriteAllText(fileName, result);
        }

    }
}
