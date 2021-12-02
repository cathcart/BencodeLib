using System;
using System.IO;
using BencodeLibRedo.Interfaces;
using BencodeLibRedo.Models;

public class BencodeEncoder
{
    public BencodeEncoder()
    {
    }

    public string Encode(IBencodeItem input)
    {

        Console.WriteLine(input.GetType());
        //switch (input.GetType())
        //{
        //    case BencodeInt:
        //        break;
        //    case BencodeString:
        //        break;
        //    case BencodeDict:
        //        break;
        //    case BencodeList:
        //        break;
        //    default:
        //        throw new InvalidDataException(string.Format("encoder doesn't recognise input type {0}", output));
        //}
        return "";
    }
}