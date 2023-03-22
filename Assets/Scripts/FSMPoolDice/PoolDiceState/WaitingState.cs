using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gotohell.FSMPoolDice.PoolDiceState
{
    public class WaitingState : State
    {
        public WaitingState(DicePoolFSM fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
            //Do Nothing
        }

        public override void Execute()
        {
            //Do Nothing
        }

        public override void ExitState()
        {
            //Do Nothing
        }

        public override State Transition()
        {
            return (_fsm.IsDicePoolSelected())?new DragState(_fsm):null;
        }
    }
}