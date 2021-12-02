using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BencodeLibRedo.Interfaces;
using BencodeLibRedo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class DecodingTests
    {
        [TestMethod]
        public void TestStringParse()
        {
            var parser = new BencodeParser();

            var input = "4:spam";

            var output = parser.Parse(input).Export();

            Assert.AreEqual<string>("spam", output);

        }

        [TestMethod]
        public void TestLongStringParse()
        {
            var parser = new BencodeParser();

            var input = "12:alphanumeric";

            var output = parser.Parse(input).Export();

            Assert.AreEqual<string>("alphanumeric", output);

        }

        [TestMethod]
        public void TestInvalidStringParse()
        {
            var parser = new BencodeParser();

            var input = "spam";
            try
            {
                var output = parser.Parse(input).Export();
            }
            catch(System.IO.InvalidDataException)
            {
                return;
            }

            Assert.Fail();

        }

        [TestMethod]
        public void TestIntParse()
        {
            var parser = new BencodeParser();

            var input = "i42e";

            var item = parser.Parse(input);
            var output = item.Export();
            //var output = 32;

            Assert.AreEqual<int>(42, output);
        }

        [TestMethod]
        public void TestListParse()
        {
            List<IBencodeItem> expected = new List<IBencodeItem>();
            var s = new BencodeString();
            s.Build("spam");
            expected.Add(s);

            var i = new BencodeInt();
            i.Build("42");
            expected.Add(i);

            var parser = new BencodeParser();

            var input = "l4:spami42ee";

            var output = parser.Parse(input).Export();


            Assert.AreEqual(expected.Count, output.Count);
            for (int index = 0; index <= expected.Count - 1; index++)
            {
                var e = expected[index];
                var o = output[index];
                Assert.IsTrue(BencodeItemsMatch(e, o));
            }
        }

        private static bool BencodeItemsMatch(IBencodeItem a, IBencodeItem b)
        {
            var aValue = a.Export();
            var bValue = b.Export();
            return aValue == bValue;
        }

        [TestMethod]
        public void TestDictionaryParse()
        {
            Dictionary<string, IBencodeItem> expected = new Dictionary<string, IBencodeItem>();
            var s = new BencodeString();
            s.Build("spam");
            expected["bar"] = s;
            //expected.Add(s);

            var i = new BencodeInt();
            i.Build("42");
            expected["foo"] = i;
            //expected.Add(i);


            var parser = new BencodeParser();

            var input = "d3:bar4:spam3:fooi42ee";

            var output = parser.Parse(input).Export();


            Assert.AreEqual(expected.Count, output.Count);
            foreach (var key in expected.Keys)
            {
                Assert.IsTrue(output.ContainsKey(key));
                var e = expected[key];
                var o = output[key];
                Assert.IsTrue(BencodeItemsMatch(e, o));
            }
        }

    }
}
