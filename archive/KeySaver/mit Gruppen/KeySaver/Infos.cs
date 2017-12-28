using System;
using System.Collections.Generic;
using System.Text;
using WindowsFormsApplication1;
using System.IO;

public class Infos
{
    List<string> _verschlüsselteKeys = new List<string>();
    List<string> _rubriken = new List<string>();
    List<string> _gruppen = new List<string>();
    private string _festesPasswort;//= "/aWWrBr8MU+m1IzQ6cgs2kxYUobtL1EVaYNen1wyhUo=";
    private string _startsWith;//= "Wo6";
    RSA _rsa = new RSA(false);
    string _key = String.Empty;
    bool _angemeldet = false;
    Searcher _such;

    public bool Ini(string pathKey)
    {
        if (!File.Exists(pathKey)) return false;
        string[] keys = ReadLines(pathKey);
        if (keys.Length < 2) return false;
        _festesPasswort = keys[0];
        _startsWith = keys[1];
        return true;
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

    public List<string> Keys { get { return _verschlüsselteKeys; } set { _verschlüsselteKeys = value; } }
    public List<string> Namen { get { return _rubriken; } set { _rubriken = value; } }
    public List<string> Gruppe { get { return _gruppen; } set { _gruppen = value; } }

    public void Add(string name, string key, string gruppe)
    {
        if (!_angemeldet) return;
        _verschlüsselteKeys.Add(_rsa.Verschlüsseln(key, _key));
        _rubriken.Add(name);
        _gruppen.Add(gruppe);
    }
    public void Remove(int index)
    {
        if (_angemeldet && index < _verschlüsselteKeys.Count)
        {
            _verschlüsselteKeys.RemoveAt(index);
            _rubriken.RemoveAt(index);
            _gruppen.RemoveAt(index);
        }
    }
    public void Change(int index, string neu)
    {
        if (_angemeldet && index < _verschlüsselteKeys.Count)
        {
            _verschlüsselteKeys.RemoveAt(index);
            _verschlüsselteKeys.Insert(index, _rsa.Verschlüsseln(neu, _key));
        }
    }
    public int[] Suche(string wort)
    {
        _such = new Searcher(_rubriken);
        return _such.Search(wort);
    }

    public bool Anmelden(string passwort)
    {
        try
        {
            if (!(_key = _rsa.Entschlüsseln(_festesPasswort, passwort)).StartsWith(_startsWith)) throw new Exception();
            _angemeldet = true;
            return true;
        }
        catch { _angemeldet = false; _key = String.Empty; return false; }
    }
    public string GetKey(int index)
    {
        if (_angemeldet && index < _verschlüsselteKeys.Count)
            try
            {
                return _rsa.Entschlüsseln(_verschlüsselteKeys[index], _key);
            }
            catch { return "Falscher Schlüssel"; }
        return String.Empty;
    }
}
