using System;
using System.IO;
using BencodeLibRedo.Models;

namespace BencodeLibRedo.Interfaces
{
    public interface IBencodeItem
    {
        dynamic Export();

        void Parse(string input);
        void Parse(SafeStream stream);

        int StartPos { get; set; }
        int StopPos { get; set; }

    }
}
