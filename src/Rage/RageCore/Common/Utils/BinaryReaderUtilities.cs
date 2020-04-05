using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RageCore.Common.Utils
{
    public static class BinaryReaderUtilities
    {
        public static string ReadNullTerminatedString(BinaryReader br, int stringLength)
        {
            string result = "";
            var sBytes = br.ReadBytes(stringLength);
            foreach (var sb in sBytes)
            {
                if (sb != 0)
                {
                    result += Convert.ToChar(sb);
                }
            }
            return result;
        }

        public static string ReadString(BinaryReader br, int stringLength)
        {
            string result = "";
            for (int i = 0; i < stringLength; i++)
            {
                result += br.ReadChar().ToString();
            }
            return result;
        }

        public static string ReadNullStartingString(BinaryReader br, int stringLength)
        {
            string result = "";
            for (int i = 0; i < stringLength; i++)
            {
                var b = br.ReadByte();
                if(b != 0) { result += Convert.ToChar(b); }
            }
            return result;
        }
    }
}
