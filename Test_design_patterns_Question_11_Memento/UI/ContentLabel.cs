using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_design_patterns_Question_11_Memento.UI
{
    public class ContentLabel
    {
        public char Character { get; private set; }
        public Color Color { get; private set; }

        public ContentLabel(char character, Color color)
        {
            Character = character;
            Color = color;
        }
    }
}
