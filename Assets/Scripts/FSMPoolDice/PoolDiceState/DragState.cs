
using UnityEngine;

namespace Gotohell.FSMPoolDice.PoolDiceState
{
    public class DragState : State
    {
        public DragState(DicePoolFSM fsm) : base(fsm)
        {
        }
        public override void EnterState()
        {
            Cursor.visible = false;
            _fsm.DragPoolDice();
        }
        public override void Execute()
        {
            _fsm.MoveDice();
        }

        public override void ExitState()
        {
            _fsm.Roll();
            Cursor.visible = true;
        }
        public override State Transition()
        {
            return (_fsm.DiceIsRelease()) ? new RollState(_fsm) : null;
        }

    }
}
