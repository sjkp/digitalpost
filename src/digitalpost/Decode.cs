using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace digitalpost
{
    internal class Decode
    {
        public static void DecodeFromFile(string inFileName, string outFileName)
        {
            using (FromBase64Transform myTransform = new FromBase64Transform(FromBase64TransformMode.IgnoreWhiteSpaces))
            {

                byte[] myOutputBytes = new byte[myTransform.OutputBlockSize];

                // Open the input and output files.
                using (FileStream myInputFile = new FileStream(inFileName, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream myOutputFile = new FileStream(outFileName, FileMode.Create, FileAccess.Write))
                    {

                        // Retrieve the file contents into a byte array. 
                        byte[] myInputBytes = new byte[myInputFile.Length];
                        myInputFile.Read(myInputBytes, 0, myInputBytes.Length);

                        // Transform the data in chunks the size of InputBlockSize. 
                        int i = 0;
                        while (myInputBytes.Length - i > 4/*myTransform.InputBlockSize*/)
                        {
                            int bytesWritten = myTransform.TransformBlock(myInputBytes, i, 4/*myTransform.InputBlockSize*/, myOutputBytes, 0);
                            i += 4/*myTransform.InputBlockSize*/;
                            myOutputFile.Write(myOutputBytes, 0, bytesWritten);
                        }

                        // Transform the final block of data.
                        myOutputBytes = myTransform.TransformFinalBlock(myInputBytes, i, myInputBytes.Length - i);
                        myOutputFile.Write(myOutputBytes, 0, myOutputBytes.Length);

                        // Free up any used resources.
                        myTransform.Clear();
                    }
                }
            }
        }
    }
}
