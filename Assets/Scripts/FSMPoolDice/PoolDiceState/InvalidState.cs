using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gotohell.FSMPoolDice.PoolDiceState
{
    public class InvalidState : State
    {

        public InvalidState(DicePoolFSM fsm) : base(fsm)
        {
        }
        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }
        public override State Transition()
        {
            throw new System.NotImplementedException();
        }
    }
}
