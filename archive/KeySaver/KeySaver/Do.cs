using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public static class Do
    {
        private static string _ordnername = "appdata";
        public static string PathKey = Application.StartupPath + "\\" + _ordnername + "\\key.ksd";
        public static string PathXml = Application.StartupPath + "\\" + _ordnername + "\\saves.ksd";
        public static string PathEmails = Application.StartupPath + "\\" + _ordnername + "\\emails.ksd";

        public static void WriteFile(string pfad, string[] text)
        {
            StreamWriter sR = new StreamWriter(pfad);
            for (int i = 0; i < text.Length; i++)
            {
                string zeile = text[i].Trim(' ');
                if (zeile != String.Empty)
                    sR.WriteLine((text[i]).Trim());
            }
            sR.Dispose();
            sR.Close();
        }

        public static string[] ReadLines(string pfad)
        {
            StreamReader sR = new StreamReader(pfad);
            string text = sR.ReadToEnd();
            sR.Dispose();
            sR.Close();
            string[] lines = text.Split('\n');
            List<string> ausg = new List<string>();
            foreach (var item in lines)
                if (item.Trim() != String.Empty)
                    ausg.Add(item.Trim());
            return ausg.ToArray();
        }
        public static void FileReset(string file)
        {
            StreamWriter fs = new StreamWriter(file);
            fs.Write(String.Empty);
            fs.Flush();
            fs.Dispose();
            fs.Close();
        }
        public static string ConvertToText(string[] tex)
        {
            string ausg = String.Empty;
            for (int i = 0; i < tex.Length; i++)
                ausg += tex[i].Trim() + "\n";
            return ausg;
        }
        public static void WriteFile(string pfad, string text)
        {
            string[] tex = text.Split('\n');
            WriteFile(pfad, tex);
        }
        public static void CopyDirectory(string src, string dst)
        {
            String[] Files;
            if (dst[dst.Length - 1] != Path.DirectorySeparatorChar)
                dst += Path.DirectorySeparatorChar; if (!Directory.Exists(dst))
                Directory.CreateDirectory(dst); Files = Directory.GetFileSystemEntries(src);
            foreach (string Element in Files)
            {
                if (Directory.Exists(Element))
                    CopyDirectory(Element, dst + Path.GetFileName(Element));
                else File.Copy(Element, dst + Path.GetFileName(Element), true);
            }
        }
    }
}
