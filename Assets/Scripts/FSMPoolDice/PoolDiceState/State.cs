using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gotohell.FSMPoolDice.PoolDiceState
{
    public abstract class State
    {
        protected DicePoolFSM _fsm;

        public State (DicePoolFSM fsm)
        {
            _fsm = fsm;
        }
        public abstract void EnterState();
        public abstract void Execute();
        public abstract void ExitState();
        public abstract State Transition();
    }
}
