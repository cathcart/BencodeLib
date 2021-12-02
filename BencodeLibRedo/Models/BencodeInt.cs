using System;
using System.Collections.Generic;
using System.IO;
using BencodeLibRedo.Interfaces;

namespace BencodeLibRedo.Models
{
    public class BencodeInt : BencodeItemBase<int>
    {
        public override void Parse(string input)
        {
            int i = 0;
            while(input[i] != 'e')
            {
                i++;
            }
            this.Build(input.Substring(1, i-1));
        }

        public override void Parse(SafeStream stream)
        {
            this.StartPos = stream.Position;
            var i = (char)stream.ReadOne();

            if (i != 'i')
            {
                throw new InvalidDataException(String.Format("Expected bencoded int\nExpected i character instead got {0}", i));
            }


            var d = 0;
            var e = (char)stream.ReadOne(d, false);
            while (e != 'e')
            {
                d++;
                e = (char)stream.ReadOne(d, false);
            }

            var buf = stream.ReadMany(d);

            RawBuild(buf);

            //Console.WriteLine("{0} {1}",stream.Length,stream.PositionActual);

            stream.ReadOne();
            this.StopPos = stream.Position;

        }
    }
}
