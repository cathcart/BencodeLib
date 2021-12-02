using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using BencodeLibRedo.Models;

namespace BencodeLibRedo.Interfaces
{
    public abstract class BencodeItemBase<T>:IBencodeItem
    {
        protected Encoding defaultEncoding = Encoding.UTF8;

        //private string _value;
        public void Build(string value)
        {
            byte[] byteArray = defaultEncoding.GetBytes(value);
            RawBuild(byteArray);
        }

        private byte[] _rawValue;
        public void RawBuild(byte[] value)
        {
            _rawValue = value;
        }

        private T _compoundValue;
        public void BuildCompund(T value)
        {
            _compoundValue = value;
        }

        //private int _startPos;
        //private int _stopPos;
        public int StartPos { get; set; }
        public int StopPos { get; set; }

        public int Count()
        {
            return _rawValue.Length;
        }

        public dynamic Export()
        {

            if (typeof(T) == typeof(int))
            {
                //return BitConverter.ToInt32(_rawValue, 0);

                var num_str = defaultEncoding.GetString(_rawValue);

                return Int32.Parse(num_str);
                //return int.Parse(_value);
            }
            else if(typeof(T) == typeof(string))
            {
                return defaultEncoding.GetString(_rawValue);
            }
            else if (typeof(T) == typeof(List<IBencodeItem>))
            {
                return _compoundValue;
            }
            else if (typeof(T) == typeof(Dictionary<string, IBencodeItem>))
            {
                return _compoundValue;
            }
            else
            {
                throw new System.InvalidCastException("Issue exporting bencode item");
            }
        }

        public abstract void Parse(string input);
        public abstract void Parse(SafeStream stream);
    }
}
