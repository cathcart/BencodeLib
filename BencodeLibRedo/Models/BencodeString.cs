using System;
using System.IO;
using System.Linq;
using BencodeLibRedo.Interfaces;

namespace BencodeLibRedo.Models
{
    public class BencodeString : BencodeItemBase<string>
    {
        public override void Parse(string input)
        {
            var c = input.First();

            int d = int.Parse(c.ToString());

            this.Build(input.Substring(2, d));
        }

        public override void Parse(SafeStream stream)
        {
            this.StartPos = stream.Position;
            //quick check if it is bencode string-ish -> starts with num
            var c = (char)stream.Peek();

            var d = int.Parse(c.ToString());

            //int d;
            var result = int.TryParse(c.ToString(), out d);

            if (!result)
            {
                throw new InvalidDataException(String.Format("Expected bencoded string\nExpected numerical character instead got {0}\n", d));
            }

            //find out how many characters are needed for the leading integer
            var sep = (char)stream.Peek();
            int count = 0;
            while (sep != ':')
            {
                count++;
                sep = (char)stream.Peek(count);
            }

            var d_bytes = stream.ReadMany(count);

            var num_str = base.defaultEncoding.GetString(d_bytes);

            d = Int32.Parse(num_str);

            sep = ((char)stream.ReadOne());

            if (sep != ':')
            {
                throw new InvalidDataException(String.Format("Expected bencoded string\nExpected : character instead got {0}\n", sep));
            }

            var buf = stream.ReadMany(d);
            RawBuild(buf);
            this.StopPos = stream.Position;
        }
    }
}
