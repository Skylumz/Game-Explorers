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
    }
}
