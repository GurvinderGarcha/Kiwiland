using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kiwiland.Core;

namespace Kiwiland.Controllers
{
    public class Methods
    {
        private IInputData _inputData;
        public Methods(IInputData data)
        {
            _inputData = data;
        }

        public List<Edge> Connections(char start)
        {
            return (from e in _inputData.NList
                    where e.Start == start
                    select e).ToList<Edge>();
        }

        public List<char> CheckInput(string s)
        {
            string input = s.ToUpper();                 // convert input to upperCase
            List<char> inputChar = new List<char>();

            foreach (char text in input)                // get rid of spaces and digits;
                if (char.IsLetter(text))
                    inputChar.Add(text);

            return inputChar;
        }
        
    }
}