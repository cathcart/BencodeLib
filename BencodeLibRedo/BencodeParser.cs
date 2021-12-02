using System;
using System.IO;
using System.Linq;
using System.Text;
using BencodeLibRedo.Interfaces;
using BencodeLibRedo.Models;

public class BencodeParser
{
    public BencodeParser()
    {
    }

    private IBencodeItem parser;

    private Encoding defaultEncoding = Encoding.UTF8;

    public IBencodeItem Parse(string input)
    {

        // convert string to stream
        byte[] byteArray = defaultEncoding.GetBytes(input);
        MemoryStream stream = new MemoryStream(byteArray);

        return Parse(stream);
    }

    public IBencodeItem Parse(Stream stream)
    {
        var bufStream = new SafeStream(stream);
        return Parse(bufStream);
    }

    public IBencodeItem Parse(SafeStream bufStream)
    {
        

        var c = ((char)bufStream.Peek());


        switch (c)
        {
            case '0':
                parser = new BencodeString();
                break;
            case '1':
                parser = new BencodeString();
                break;
            case '2':
                parser = new BencodeString();
                break;
            case '3':
                parser = new BencodeString();
                break;
            case '4':
                parser = new BencodeString();
                break;
            case '5':
                parser = new BencodeString();
                break;
            case '6':
                parser = new BencodeString();
                break;
            case '7':
                parser = new BencodeString();
                break;
            case '8':
                parser = new BencodeString();
                break;
            case '9':
                parser = new BencodeString();
                break;
            case 'i':
                parser = new BencodeInt();
                break;
            case 'l':
                parser = new BencodeList();
                break;
            case 'd':
                parser = new BencodeDict();
                break;
            default:
                throw new InvalidDataException(string.Format("Parser doesn't recognise input character {0}",(char)c));
        }

        //var input = (string)(reader.ReadToEnd());
        //parser.Parse(input);
        parser.Parse(bufStream);
        return parser;

        //var idx = input.IndexOf(':');

        //if (idx >= 0)
        //{
        //    var items = input.Split(':');

        //    var returnBencode = new BencodeString();
        //    returnBencode.Build(items.Last<string>());

        //    return returnBencode;
        //}
        //else if (input[0] == 'i' && input.Last() == 'e')
        //{
        //    var returnBencode = new BencodeInt();

        //    returnBencode.Build(input.Substring(1, input.Length-2));

        //    return returnBencode;
        //}
        //else if (input[0] == 'l' && input.Last() == 'e')
        //{
        //    var returnBencode = new BencodeList();

            

        //    returnBencode.Build(input.Substring(1, input.Length - 2));

        //    return returnBencode;
        //}
        //else
        //{
        //    throw new InvalidDataException();
        //}
    }
}