using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illumina.BaseSpace.SDK.MonoTouch.Tests.Helpers
{
    public class StringHelpers
    {
        private static readonly Random RandomSeed = new Random();
        private const string Alphanumericchars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// Generates a string containing random A-Za-z0-9 characters
        /// </summary>
        /// <param name="length">Length of string to generate</param>
        /// <returns></returns>
        public static string RandomAlphanumericString(int length)
        {
            var buffer = new char[length];
            for (var i = 0; i < length; i++)
            {
                buffer[i] = Alphanumericchars[RandomSeed.Next(Alphanumericchars.Length)];
            }
            return new string(buffer);
        }
    }
}
