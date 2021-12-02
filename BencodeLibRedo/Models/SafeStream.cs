using System;
using System.IO;
using System.Linq;

namespace BencodeLibRedo.Models
{
    public class SafeStream
    {
        private BufferedStream _stream;
        private int _currentPos;

        public SafeStream(Stream stream)
        {
            _stream = new BufferedStream(stream);
            _currentPos = 0;
        }

        public byte Peek(int offset=0)
        {
            return ReadOne(offset, false);
        }

        public byte ReadOne(int offset=0, bool advancePos=true)
        {
            return ReadMany(1, offset, advancePos).First();
        }



        public long Length { get { return _stream.Length; } }
        public int Position { get { return _currentPos; } }
        public long PositionActual { get { return _stream.Position; } }

        public byte[] ReadMany(int count, int offset=0, bool advancePos = true)
        {
            if (!_stream.CanRead)
            {
                throw new NotSupportedException("Stream can't be read.");
            }

            //Console.WriteLine("before {0}", _stream.Position);
            _stream.Position = _currentPos + offset;
            //Console.WriteLine("after {0}", _stream.Position);
            var buf = new byte[count];

            //Console.WriteLine("buffer length:{0} characters to read:{1}", buf.Length, count);
            var readCount = _stream.Read(buf, 0, count);

            if (readCount != count)
            {
                throw new InvalidOperationException("Read bytes does not match expected byte count.");
            }

            if (advancePos)
            {
                _currentPos += readCount;
            }

            return buf;
        }

    }
}
