using Multi_Server_Test.Server.Blocks.LetterBlock;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multi_Server_Test.Server.Models.LetterBlock
{
    public class LettersCollection
    {
        public static string Name = "Multi-list-letters";
        public List<Letter> Collection = new List<Letter>();
        public LettersCollection(List<Letter> collection)
        {
            if (collection != null)
            {
                Collection = collection;
            }
        }
    }
}
