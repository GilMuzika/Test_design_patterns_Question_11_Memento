using System;
using System.Collections.Generic;
using System.Text;
using Test_design_patterns_Question_11_Memento.UI;

namespace Test_design_patterns_Question_11_Memento.memento
{
    public class Memento
    {
        public LabelField State { get; private set; }

        public Memento(LabelField state)
        {
            State = state;
        }


    }
}
