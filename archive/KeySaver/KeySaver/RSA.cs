using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.IO;

public class RSA
{
    private string _privKey;
    private string _publKey;

    private string _unverschlüsselt;
    private string _verschlüsselt;

    private string _verschlüsseltHex;
    private string _unverschlüsseltHex;

    public string PrivateKey { get { return _privKey; } }
    public string PublicKey { get { return _publKey; } }

    public RSA(bool create)
    {
        if (create)
            CreateKeys();
    }
    public RSA(string privateKey, string publicKey)
    {
        _privKey = privateKey;
        _publKey = publicKey;
    }

    private void CreateKeys()
    {
        var rsa = new RSACryptoServiceProvider();
        _privKey = rsa.ToXmlString(true);
        _publKey = rsa.ToXmlString(false);
    }
    private List<byte[]> Split(byte[] bytes, int arrayLenght)
    {
        int lenght = arrayLenght;
        List<byte[]> byts = new List<byte[]>();
        int durchläufe = bytes.Length / lenght;
        if (durchläufe == 0)
            byts.Add(bytes);
        else
        {
            int rest = (int)Math.IEEERemainder(bytes.Length, lenght);
            for (int i = 0; i < durchläufe; i++)
            {
                List<byte> byt = new List<byte>();
                for (int j = 0; j < lenght; j++)
                    byt.Add(bytes[i * lenght + j]);
                byts.Add(byt.ToArray());
            }
            List<byte> bytees = new List<byte>();
            for (int j = 0; j < rest; j++)
                bytees.Add(bytes[durchläufe * lenght + j]);
            if (bytees.Count != 0)
                byts.Add(bytees.ToArray());
        }
        return byts;
    }

    public void SetPublicKey(string key)
    {
        _publKey = key;
    }

    /// <summary>
    /// Gibt die verschlüsselte Hex wieder
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string Verschlüsseln(string text)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(_publKey); // Public

        byte[] bytes = Encoding.Unicode.GetBytes(text);
        List<byte[]> bates = Split(bytes, 86);
        List<byte[]> endbytes = new List<byte[]>();
        foreach (var item in bates)
            endbytes.Add(rsa.Encrypt(item, true));
        List<byte> ausg = new List<byte>();
        for (int i = 0; i < endbytes.Count; i++)
            foreach (var item in endbytes[i])
                ausg.Add(item);

        _verschlüsselt = ConvertToString(ausg.ToArray());
        _verschlüsseltHex = BytesToHex(ausg.ToArray());
        return _verschlüsseltHex;
    }
    public byte[] Verschlüsseln(byte[] text)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(_publKey); // Public
        byte[] bytes = text;
        List<byte[]> bates = Split(bytes, 86);
        List<byte[]> endbytes = new List<byte[]>();
        foreach (var item in bates)
            endbytes.Add(rsa.Encrypt(item, true));
        List<byte> ausg = new List<byte>();
        for (int i = 0; i < endbytes.Count; i++)
            foreach (var item in endbytes[i])
                ausg.Add(item);

        _verschlüsselt = ConvertToString(ausg.ToArray());
        _verschlüsseltHex = BytesToHex(ausg.ToArray());
        return ausg.ToArray();
    }
    /// <summary>
    /// Gibt den entschlüsselten Text wieder
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    public string Entschlüsseln(string hex)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(_privKey); // Private

        byte[] bytes = StringToByteArray(hex);
        List<byte[]> bates = Split(bytes, 128);
        List<byte[]> endbytes = new List<byte[]>();
        foreach (var item in bates)
            endbytes.Add(rsa.Decrypt(item, true));
        List<byte> ausg = new List<byte>();
        for (int i = 0; i < endbytes.Count; i++)
            foreach (var item in endbytes[i])
                ausg.Add(item);

        _unverschlüsselt = ConvertToString(ausg.ToArray());
        _unverschlüsseltHex = BytesToHex(ausg.ToArray());
        return _unverschlüsselt;
    }
    public byte[] Entschlüsseln(byte[] text)
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(_privKey); // Private

        byte[] bytes = text;
        List<byte[]> bates = Split(bytes, 128);
        List<byte[]> endbytes = new List<byte[]>();
        foreach (var item in bates)
            endbytes.Add(rsa.Decrypt(item, true));
        List<byte> ausg = new List<byte>();
        for (int i = 0; i < endbytes.Count; i++)
            foreach (var item in endbytes[i])
                ausg.Add(item);

        _unverschlüsselt = ConvertToString(ausg.ToArray());
        _unverschlüsseltHex = BytesToHex(ausg.ToArray());
        return ausg.ToArray();
    }

    public string Verschlüsseln(string text, string schlüssel)
    {
        string ende = String.Empty;
        RijndaelManaged manager = new RijndaelManaged();
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        Byte[] schluessel = provider.ComputeHash(Encoding.UTF8.GetBytes(schlüssel));
        provider.Clear();
        manager.Key = schluessel;
        manager.GenerateIV();
        Byte[] iv = manager.IV;
        MemoryStream stream = new MemoryStream();
        stream.Write(iv, 0, iv.Length);
        CryptoStream cs = new CryptoStream(stream, manager.CreateEncryptor(), CryptoStreamMode.Write);
        Byte[] data = Encoding.UTF8.GetBytes(text);
        cs.Write(data, 0, data.Length);
        cs.FlushFinalBlock();
        Byte[] encdata = stream.ToArray();
        ende = Convert.ToBase64String(encdata);
        cs.Close();
        manager.Clear();
        return ende;
    }
    public string Entschlüsseln(string text, string schlüssel)
    {
        string ende = "Falscher Schlüssel";
        try
        {
            RijndaelManaged manager = new RijndaelManaged();
            int Leange = 16;
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            Byte[] schluessel = provider.ComputeHash(Encoding.UTF8.GetBytes(schlüssel));
            provider.Clear();
            Byte[] encdata = Convert.FromBase64String(text);
            MemoryStream stream = new MemoryStream(encdata);
            Byte[] iv = new byte[16];
            stream.Read(iv, 0, Leange);
            manager.IV = iv;
            manager.Key = schluessel;
            CryptoStream cs = new CryptoStream(stream, manager.CreateDecryptor(), CryptoStreamMode.Read);
            byte[] data = new byte[stream.Length - Leange];
            int I = cs.Read(data, 0, data.Length);
            ende = Encoding.UTF8.GetString(data, 0, I);
            cs.Close();
            manager.Clear();
        }
        catch { }
        return ende;
    }

    private string ConvertToString(byte[] bytes)
    {
        string ed = String.Empty;
        foreach (var item in Encoding.Unicode.GetChars(bytes)) ed += item;
        return ed;
    }
    private static string BytesToHex(byte[] bytes)
    {
        var hexString = new StringBuilder(bytes.Length);
        for (int i = 0; i < bytes.Length; i++)
            hexString.Append(bytes[i].ToString("X2"));
        return hexString.ToString();
    }
    private static byte[] StringToByteArray(String hex)
    {
        int numberChars = hex.Length;
        var bytes = new byte[numberChars / 2];
        for (int i = 0; i < numberChars; i += 2)
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        return bytes;
    }

    private void CreateSumme()
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(_privKey); // Private

        var encryptedSymmetricKey = rsa.SignData(
            Encoding.Unicode.GetBytes(_unverschlüsselt),
            new SHA1CryptoServiceProvider());
        string a = String.Empty;
        foreach (var item in encryptedSymmetricKey) a += char.ConvertFromUtf32(item);
        _verschlüsselt = BytesToHex(encryptedSymmetricKey);

    }
    private void PrüfeSumme()
    {
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(_publKey);

        byte[] signText = StringToByteArray(_verschlüsselt);
        byte[] originalText = Encoding.Unicode.GetBytes(_unverschlüsselt);

        bool signOk = rsa.VerifyData(originalText, new SHA1CryptoServiceProvider(), signText);

        MessageBox.Show("Signature OK: " + signOk);
    }
}