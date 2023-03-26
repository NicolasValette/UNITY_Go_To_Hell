using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gotohell.FSMPoolDice.PoolDiceState
{
    public class WaitingSelectionState : State
    {
        public WaitingSelectionState(DicePoolFSM fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
            _fsm.DiceToReroll();
        }

        public override void Execute()
        {
        }

        public override void ExitState()
        {
            _fsm.DiceToReroll();
        }

        public override State Transition()
        {
            return null;
        }
    }
}
