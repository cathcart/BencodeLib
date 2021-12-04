using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BencodeLibRedo.Interfaces;
using BencodeLibRedo.Models;

public class BencodeEncoder
{
    public BencodeEncoder()
    {
    }

    public string Encode(dynamic input)
    {
        if(input is string)
        {
            int inputInt;
            var isInt = Int32.TryParse(input, out inputInt);

            if(isInt == true)
            {
                return Encode(inputInt);
            }
            else
            {
                var s = string.Format("{0}:{1}", ((string)input).Length, input);
                return s;
            }
       
        }
        else if(input is int)
        {
            var s = string.Format("i{0}e", (input));
            return s;
        }
        else if(input.GetType() == (new List<string>()).GetType())
        {
            var s = "l";
            foreach(var x in input)
            {
                s += Encode(x);
            }
            s += "e";
            return s;
        }
        else if (input.GetType() == (new Dictionary<string, string>()).GetType())
        {
            var inputDict = (Dictionary<string, string>)input;
            var list = inputDict.Keys.ToList<string>();
            list.Sort();

            var s = "d";
            foreach (var x in list)
            {
                s += Encode(x);
                s += Encode(inputDict[x]);
            }
            s += "e";
            return s;
        }
        else
        {
            throw new Exception(string.Format("Can't encode type {0}", input.type()));
        }
    }
}