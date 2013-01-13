using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

namespace CSL
{
    static class BEncoding
    {
        #region Decode
        private static int decode_int(byte[] x, ref int f)
        {
            string s = "";
            while (x[f] != 'e')
            {
                s += (char)x[f];
                f++;
            }
            f++;

            Regex r = new Regex("^(0|-?[1-9][0-9]*)$", RegexOptions.Compiled);

            //if (!r.IsMatch(s))
                //throw new BitTorrentException("Integer value is invalid");

            return int.Parse(s);
        }

        // Hack to cater for sha hashes
        private static byte[] decode_bytes(byte[] x, ref int f)
        {
            string s = "";
            while (x[f] != ':')
            {
                s += (char)x[f];
                f++;
            }
            f++;

            Regex r = new Regex("^(0|[1-9][0-9]*)$", RegexOptions.Compiled);

            //if (!r.IsMatch(s))
                //throw new BitTorrentException("Integer value is invalid");

            int l = int.Parse(s);
            if (l > 0)
            {
                byte[] rv = new byte[l];
                Buffer.BlockCopy(x, f, rv, 0, l);
                f += l;
                return rv;
            }
            return null;
        }

        public static string decode_string(byte[] x, ref int f)
        {
            string s = "";
            while (x[f] != ':')
            {
                s += (char)x[f];
                f++;
            }
            f++;

            Regex r = new Regex("^(0|[1-9][0-9]*)$", RegexOptions.Compiled);

            //if (!r.IsMatch(s))
                //throw new BitTorrentException("Integer value is invalid");

            int l = int.Parse(s);
            string temp = "";
            if (l > 0)
            {
                StringBuilder sb = new StringBuilder(l);
                int i = f;
                f += l;
                while (i < f)
                    sb.Append((char)x[i++]);
                return sb.ToString();
            }
            return temp;
        }

        public static object decode_list(byte[] x, ref int f)
        {
            ArrayList r = new ArrayList();
            while (x[f] != 'e')
            {
                object v = bdecode_rec(x, ref f);
                r.Add(v);
            }
            f += 1;
            return r;
        }

        public static ListDictionary decode_dict(byte[] x, ref int f)
        {
            ListDictionary r = new ListDictionary();
            string lastkey = null;

            while (x[f] != 'e')
            {
                string k = decode_string(x, ref f);
                if (lastkey != null && lastkey.CompareTo(k) >= 0)
                    //throw new BitTorrentException("Dictionary contains duplicate key or is incorrectly sorted");
                lastkey = k;

                object v;
                // Hack - certain keys are read as byte[]
                if ((k != "pieces") && (k != "peer id"))
                {
                    v = bdecode_rec(x, ref f);
                }
                else
                {
                    v = decode_bytes(x, ref f);
                }
                r.Add(k, v);
            }
            f += 1;
            return r;
        }

        public static object bdecode_rec(byte[] x, ref int f)
        {
            byte t = x[f];

            if (t == 'i')
            {
                f += 1;
                return decode_int(x, ref f);
            }
            else if (t == 'l')
            {
                f += 1;
                return decode_list(x, ref f);
            }
            else if (t == 'd')
            {
                f += 1;
                return decode_dict(x, ref f);
            }
            else
                return decode_string(x, ref f);
        }

        public static object Decode(string message)
        {
            return Decode(System.Text.Encoding.ASCII.GetBytes(message));
        }

        public static object Decode(FileInfo fi)
        {
            byte[] barray = File.ReadAllBytes(fi.FullName);
            return Decode(barray);
        }

        public static object Decode(byte[] message)
        {
            object r = null;
            int l = 0;
            try
            {
                r = bdecode_rec(message, ref l);
            }
            //TODO: Work out what type of expection is thrown when the bencoded file is too short
            catch // IndexError
            {
                throw new Exception("ValueError");
            }
            if (l != message.Length)
            {
                //throw new BitTorrentException("BEncoded message is too long");
            }
            return r;
        }

        #endregion
        #region Encode
        public static void bencode_rec(object x, MemoryStream sw)
        {
            if (x is int || x is long || x is ulong || x is uint)
            {
                byte[] op = Encoding.UTF8.GetBytes(string.Format("i{0:d}e", x));
                sw.Write(op, 0, op.Length);
            }
            else if (x is string)
            {
                byte[] op = Encoding.UTF8.GetBytes(string.Format("{0:d}:{1}", ((string)x).Length, x));
                sw.Write(op, 0, op.Length);
            }
            else if (x is byte[])
            {
                byte[] op = Encoding.UTF8.GetBytes(string.Format("{0:d}:", ((byte[])x).Length));
                sw.Write(op, 0, op.Length);
                op = (byte[])x;
                sw.Write(op, 0, op.Length);
            }
            else if (x is ArrayList)
            {
                sw.WriteByte((byte)'l');
                ArrayList a = (ArrayList)x;
                for (int i = 0; i < a.Count; i++)
                {
                    bencode_rec(a[i], sw);
                }
                sw.WriteByte((byte)'e');
            }
            else if (x is ListDictionary)
            {
                sw.WriteByte((byte)'d');
                ListDictionary a = (ListDictionary)x;
                ArrayList b = new ArrayList(a.Keys);
                b.Sort();
                foreach (string k in b)
                {
                    bencode_rec(k, sw);
                    bencode_rec(a[k], sw);
                }
                sw.WriteByte((byte)'e');
            }
            else
                Debug.Fail("Unknown Type");
        }

        //TODO: Should encode as string
        public static byte[] Encode(object x)
        {
            MemoryStream ms = new MemoryStream();
            bencode_rec(x, ms);
            return ms.ToArray();
        }
        #endregion
    }
}

