using Gotohell.FSMPoolDice.PoolDiceState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gotohell.FSMPoolDice
{

    [RequireComponent(typeof(InputManager))]
    public class DicePoolFSM : MonoBehaviour
    {
        [SerializeField]
        private GameObject _dice;
        [SerializeField]
        private float _launchForce = 5f;
        private State _currentState;
        private InputManager _inputManager;
        // Start is called before the first frame update
        void Start()
        {
            _currentState = new WaitingState(this);
            _inputManager = GetComponent<InputManager>();
        }

        // Update is called once per frame
        void Update()
        {
            _currentState.Execute();
            State _nextState = _currentState.Transition();
            if (_nextState != null)
            {
                ChangeState(_nextState);
            }
        }

        private void ChangeState(State _nextState)
        {
            _currentState.ExitState();
            _currentState = _nextState;
            _currentState.EnterState();
        }

        public bool IsDicePoolSelected()
        {
           return _inputManager.IsDiceSelected();
        }
        public Vector3 GetHandPosition ()
        {
            Vector3 pos = _inputManager.GetPosition();
            pos.y = 5f;
            return pos;
            
        }

        public void MoveDice()
        {
            transform.position = GetHandPosition();
        }

        public bool DiceIsRelease()
        {
            return _inputManager.IsDrop();
        }
        public void Roll()
        {
            Vector2 vect = _inputManager.GetCursorDeltaPos();
            Vector3 dir = new Vector3(vect.x, 0f, vect.y);
            Debug.Log("Roll");
            _dice.GetComponent<Rigidbody>().AddForce(dir.normalized * _launchForce, ForceMode.Impulse);
            _dice.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * 25);
        }
    }
}