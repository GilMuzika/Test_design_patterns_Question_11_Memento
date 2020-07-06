using System;
using System.Collections.Generic;
using System.Text;

namespace Test_design_patterns_Question_11_Memento.memento
{
    public class CareTaker
    {
        public int TakingCount { get; private set; }
        public int HowMuchMementos { get { return _mementoList.Count; } }

        private List<Memento> _mementoList = new List<Memento>();

        public void Add(Memento state)
        {
            _mementoList.Add(state);
            TakingCount = _mementoList.Count - 1;
        }

        public Memento Get(int index)
        {
            return _mementoList[index];
        }

        public Memento GetLast()
        {
            if(TakingCount > 0) TakingCount--;
            if (_mementoList.Count > 0) return _mementoList[TakingCount];
            else return null;
        }
    }
}
