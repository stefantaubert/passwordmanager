using System;
using System.Collections.Generic;
using System.Text;
using WindowsFormsApplication1;

public class Infos
{
    List<string> _verschlüsselteKeys = new List<string>();
    List<string> _rubriken = new List<string>();
    List<DateTime> _erstelldatum = new List<DateTime>();
    public List<string> Keys { get { return _verschlüsselteKeys; } set { _verschlüsselteKeys = value; } }
    public List<string> Namen { get { return _rubriken; } set { _rubriken = value; } }
    private string _festesPasswort;//= "/aWWrBr8MU+m1IzQ6cgs2kxYUobtL1EVaYNen1wyhUo=";
    private string _startsWith;//= "Wo6";
    RSA _rsa = new RSA(false);
    string _key = String.Empty;
    bool _angemeldet = false;
    Searcher _such;

    public bool Ini(string pathKey)
    {
        if (!System.IO.File.Exists(pathKey)) return false;
        string[] keys = Do.ReadLines(pathKey);
        if (keys.Length < 2) return false;
        _festesPasswort = keys[0];
        _startsWith = keys[1];
        return true;
    }

    public void Add(string name, string key)
    {
        if (!_angemeldet) return;
        _verschlüsselteKeys.Insert(0, _rsa.Verschlüsseln(key, _key));
        //  _verschlüsselteKeys.Add(_rsa.Verschlüsseln(key, _key));
        _rubriken.Insert(0, name);
        //  _rubriken.Add(name);
        _erstelldatum.Add(DateTime.Now);
    }
    public void Remove(int index)
    {
        if (_angemeldet && index < _verschlüsselteKeys.Count)
        {
            _verschlüsselteKeys.RemoveAt(index);
            _rubriken.RemoveAt(index);
            if (_erstelldatum.Count > index) _erstelldatum.RemoveAt(index);
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
    public int[] Suche(string wort, bool pwSearchBool)
    {
        _such = new Searcher();
        _such.Insert(_rubriken, true);
        if (pwSearchBool)
            for (int i = 0; i < _verschlüsselteKeys.Count; i++)
                _such.Add(i, GetKey(i));
        return _such.Search(wort);
    }

    public bool Anmelden(string passwort)
    {
        try
        {
            _key = _rsa.Entschlüsseln(_festesPasswort, passwort);
            if (!(_key).StartsWith(_startsWith)) throw new Exception();
            _angemeldet = true;
            return true;
        }
        catch { _angemeldet = false; _key = String.Empty; return false; }
    }
    public string GetKey(int index)
    {
        if (_angemeldet && index < _verschlüsselteKeys.Count)
            return _rsa.Entschlüsseln(_verschlüsselteKeys[index], _key);
        return String.Empty;
    }
}