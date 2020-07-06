using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Test_design_patterns_Question_11_Memento.UI;

namespace Test_design_patterns_Question_11_Memento.memento
{
    public class Originator
    {
        private LabelField _state;

        public void SetState(LabelField state)
        {
            this._state = state;
        }

        public LabelField State { get { return _state; } }

        public Memento SaveStateToMemento()
        {
            return new Memento(_state);
        }

        public void GetStateFromMemento(Memento memento)
        {
            if (memento != null) _state = memento.State;
            else _state = null;
            
        }
    }
}
