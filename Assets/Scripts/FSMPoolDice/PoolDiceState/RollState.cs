using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gotohell.FSMPoolDice.PoolDiceState
{
    public class RollState : State
    {
        public RollState(DicePoolFSM fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
           
        }

        public override void Execute()
        {
        }

        public override void ExitState()
        {
        }
        public override State Transition()
        {
            return new WaitingState(_fsm);
        }
    }
}
