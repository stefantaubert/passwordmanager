using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Gui
{
    internal static class CSVExporter
    {
        public static void ExportCSV(string path)
        {
            var str = new StringBuilder();
            foreach (var item in ModelLoader.CurrentModel.Entries)
            {
                var line = string.Format("{0};{1}", MakeValueCsvFriendly(item.Label), MakeValueCsvFriendly(item.Content));

                str.AppendLine(line);
            }

            File.WriteAllText(path, str.ToString());
        }

        //get the csv value for field.
        private static string MakeValueCsvFriendly(object value)
        {
            if (value == null) return "";
            if (value is Nullable && ((INullable)value).IsNull) return "";

            if (value is DateTime)
            {
                if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
                    return ((DateTime)value).ToString("yyyy-MM-dd");
                return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string output = value.ToString();

            if (output.Contains(",") || output.Contains("\""))
                output = '"' + output.Replace("\"", "\"\"") + '"';

            return output;

        }
    }
}
