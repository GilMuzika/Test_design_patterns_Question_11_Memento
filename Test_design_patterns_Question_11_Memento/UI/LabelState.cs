using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_design_patterns_Question_11_Memento.UI
{
    public class LabelState
    {
        public ContentLabel Content { get; private set; }
        public Point MatrixIndex { get; private set; }
        public Point Location { get; set; }

        public LabelState(ContentLabel content, Point matrixIndex, Point location)
        {
            Content = content;
            MatrixIndex = matrixIndex;
            Location = location;
        }
    }
}
