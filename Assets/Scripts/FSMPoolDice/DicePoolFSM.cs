using Gotohell.Dice;
using Gotohell.FSMPoolDice.PoolDiceState;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        [SerializeField]
        private DiceManager _diceManager;
        [SerializeField]
        private float _offset = 2;
        private State _currentState;
        private InputManager _inputManager;
        public Transform SelectedPool { get; private set; }

        public List<DiceFace> Values { get; private set; }
        private List<GameObject> _listOfDice;

        public static event Action<DiceFace> UpdateDisplay;
        // Start is called before the first frame update
        private void Awake()
        {
            _listOfDice = new List<GameObject>();
        }
        void Start()
        {
            _currentState = new WaitingState(this);
            _inputManager = GetComponent<InputManager>();
            Values= new List<DiceFace>();
            
        }
        private void OnEnable()
        {
            InputManager.OnDragDice += SelectPool;
        }
        private void OnDisable()
        {
            InputManager.OnDragDice -= SelectPool;
            for (int i=0; i< _listOfDice.Count; i++)
            {
                _listOfDice[i].GetComponent<DiceBehaviour>().OnRollFinished -= ReadDiceValue;
            }
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
            pos.y += _offset;
            return pos;
            
        }
        public void DragPoolDice()
        {
            for (int i = 0; i < _listOfDice.Count; i++)
            {
                _listOfDice[i].GetComponent<Rigidbody>().useGravity = false;
            }
        }
        public void MoveDice()
        {
            SelectedPool.position = GetHandPosition();
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
            for (int i = 0; i < _listOfDice.Count; i++)
            {
                _listOfDice[i].GetComponent<Rigidbody>().useGravity = true;
                _listOfDice[i].GetComponent<Rigidbody>().AddForce(dir.normalized * _launchForce, ForceMode.Impulse);
                _listOfDice[i].GetComponent<Rigidbody>().AddTorque(UnityEngine.Random.insideUnitSphere * 25);
                _listOfDice[i].GetComponent<DiceBehaviour>().Launch();
            }
            
        }
        public void AddDice(GameObject dice)
        {
            _listOfDice.Add(dice);
            dice.GetComponent<DiceBehaviour>().OnRollFinished += ReadDiceValue;
        }
        public void SelectPool(Transform pool)
        {
            SelectedPool= pool;
        }
        public void ReadDiceValue(DiceFace face)
        {
            Debug.Log("ReadDiceVaue : " + face.ToString());
            Values.Add(face);
            UpdateDisplay?.Invoke(face);
        }
    }
}