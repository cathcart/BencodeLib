using System;
using System.Collections.Generic;
using System.IO;
using BencodeLibRedo.Interfaces;

namespace BencodeLibRedo.Models
{
    public class BencodeList : BencodeItemBase<List<IBencodeItem>>
    {
        public override void Parse(string input)
        {
            throw new NotImplementedException();
        }

        public override void Parse(SafeStream stream)
        {
            this.StartPos = stream.Position;
            var i = (char)stream.ReadOne();

            if (i != 'l')
            {
                throw new InvalidDataException(String.Format("Expected bencoded list\nExpected l character instead got {0}", i));
            }

            var returnList = new List<IBencodeItem>();
            var itemParser = new BencodeParser();

            var e = (char)stream.Peek();
            while (e != 'e')
            {
                var item = itemParser.Parse(stream);
                returnList.Add(item);
                e = (char)stream.Peek();
            }

            BuildCompund(returnList);
            stream.ReadOne();
            this.StopPos = stream.Position;
        }
    }
}
