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
            _fsm.PrepareToReroll();
        }

        public override void Execute()
        {
            _fsm.SelectingDiceToReroll();
        }

        public override void ExitState()
        {
            _fsm.DiceToReroll();
            _fsm.DiceSelected();
        }

        public override State Transition()
        {
            return (_fsm.CanReroll()) ? new WaitingState(_fsm) : null;
        }
    }
}
