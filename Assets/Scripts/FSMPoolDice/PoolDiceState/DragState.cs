
namespace Gotohell.FSMPoolDice.PoolDiceState
{
    public class DragState : State
    {
        public DragState(DicePoolFSM fsm) : base(fsm)
        {
        }
        public override void EnterState()
        {
            
        }
        public override void Execute()
        {
            _fsm.MoveDice();
        }

        public override void ExitState()
        {
            _fsm.Roll();
        }
        public override State Transition()
        {
            return (_fsm.DiceIsRelease()) ? new RollState(_fsm) : null;
        }

    }
}
