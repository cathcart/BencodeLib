using System;
using System.Collections.Generic;
using System.IO;
using BencodeLibRedo.Interfaces;

namespace BencodeLibRedo.Models
{
    public class BencodeDict: BencodeItemBase<Dictionary<string, IBencodeItem>>
    {
        public BencodeDict()
        {
        }

        public override void Parse(string input)
        {
            throw new NotImplementedException();
        }

        public override void Parse(SafeStream stream)
        {
            this.StartPos = stream.Position;
            var i = (char)stream.ReadOne();

            if (i != 'd')
            {
                throw new InvalidDataException(String.Format("Expected bencoded dictionary\nExpected d character instead got {0}", i));
            }

            var returnDict = new Dictionary<string, IBencodeItem>();
            var itemParser = new BencodeParser();

            var e = (char)stream.Peek();
            while (e != 'e')
            {

                var key = itemParser.Parse(stream);
                var value = itemParser.Parse(stream);


                returnDict[key.Export()] = value;


                e = (char)stream.Peek();
            }

            BuildCompund(returnDict);
            stream.ReadOne();
            this.StopPos = stream.Position;
        }
    }
}
