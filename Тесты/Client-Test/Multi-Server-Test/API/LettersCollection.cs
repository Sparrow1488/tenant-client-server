using System;
using System.Collections.Generic;
using System.Text;

namespace JumboServer.API
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
