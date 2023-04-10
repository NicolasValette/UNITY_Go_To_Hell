using Gotohell.Dice;
using Gotohell.FSMPoolDice.PoolDiceState;
using System;
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
        [SerializeField]
        private DiceManager _diceManager;
        [SerializeField]
        private float _offset = 2;
        [SerializeField]
        private float _torqueForce = 25;
        [SerializeField]
        private float _dragSpeed = 5f;
        private State _currentState;
        private InputManager _inputManager;
        public Transform SelectedPool { get; private set; }

        public List<DiceFace> Values { get; private set; }
        public bool IsRollFinish { get; private set; }
        private List<GameObject> _listOfDice;
        private List<GameObject> _validDice;
        private List<GameObject> _diceToReroll;
        private Vector3 _initialPosition;

        public static event Action<DiceFace> UpdateDice;
        public static event Action OnRollFinished;
        public static event Action<DiceFace> SelectedForReroll;
        public static event Action StartEndSelectingDiceToReroll;
        public static event Action<List<DiceFace>> RefreshingFaceValues;
        public static event Action<List<GameObject>> OnPoolDrag;
        public static event Action OnPoolDrop;

        private Vector3 _velocity;
        private float _smoothTime = 0.2f;
        private int _valueAdded;
        // Start is called before the first frame update
        private void Awake()
        {
            _listOfDice = new List<GameObject>();
            _diceToReroll = new List<GameObject>();
            _validDice = new List<GameObject>();
        }
        void Start()
        {
            InitFSM();
            
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
                _listOfDice[i].GetComponent<DiceBehaviour>().OnDiceRollFinished -= ReadDiceValue;
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
        public void InitFSM()
        {
            _currentState = new WaitingState(this);
            _inputManager = GetComponent<InputManager>();
            Values = new List<DiceFace>();
            _valueAdded = 0;
        }
        private void ChangeState(State _nextState)
        {
            string str = "###Change state frome (" + _currentState.ToString() + ") to ";
            _currentState.ExitState();
            _currentState = _nextState;
            _currentState.EnterState();
            str += "(" + _currentState.ToString();
            Debug.Log(str);
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
                _listOfDice[i].GetComponentInChildren<Collider>().enabled= false;
                _listOfDice[i].GetComponent<Rigidbody>().isKinematic = true;
                _listOfDice[i].GetComponent<DiceBehaviour>().StartSpinnig();
            }
            OnPoolDrag?.Invoke(_listOfDice);
        }
        public void MoveDice()
        {
            //SelectedPool.position = Vector3.SmoothDamp(SelectedPool.position, GetHandPosition(), ref _velocity, _smoothTime);
            //SelectedPool.position = Vector3.MoveTowards(SelectedPool.position, GetHandPosition(), _dragSpeed * Time.deltaTime) ;
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
            Debug.Log("Roll " + _listOfDice.Count + " Dices !");
            for (int i = 0; i < _listOfDice.Count; i++)
            {
                _listOfDice[i].GetComponentInChildren<Collider>().enabled = true;
                _listOfDice[i].GetComponent<Rigidbody>().useGravity = true;
                _listOfDice[i].GetComponent<Rigidbody>().isKinematic = false;
                _listOfDice[i].GetComponent<Rigidbody>().AddForce(dir.normalized * _launchForce, ForceMode.Impulse);
                _listOfDice[i].GetComponent<Rigidbody>().AddTorque(UnityEngine.Random.insideUnitSphere * _torqueForce, ForceMode.Impulse);
                _listOfDice[i].GetComponent<DiceBehaviour>().Launch();
            }
            OnPoolDrop?.Invoke();
        }
        public void AddDice(GameObject dice)
        {
            _listOfDice.Add(dice);
            dice.GetComponent<DiceBehaviour>().OnDiceRollFinished += ReadDiceValue;
        }
        public void RemoveDice(GameObject dice)
        {
            dice.GetComponent<DiceBehaviour>().OnDiceRollFinished -= ReadDiceValue;
            _listOfDice.Remove(dice);
        }
        public void ClearPool()
        {
            for (int i=0;i< _listOfDice.Count;i++)
            {
                _listOfDice[i].GetComponent<DiceBehaviour>().OnDiceRollFinished -= ReadDiceValue;
            }
            _listOfDice.Clear();
            Values.Clear();
        }
        public void SelectPool(Transform pool)
        {
            SelectedPool= pool;
        }
        public void ReadDiceValue(DiceFace face)
        {
            Debug.Log("ReadDiceVaue : " + face.ToString());
            Values.Add(face);
            _valueAdded++;
            UpdateDice?.Invoke(face);
            if (_valueAdded >= _listOfDice.Count) 
            {
                Debug.Log("Finish");
                _listOfDice.AddRange(_validDice);
                IsRollFinish = true;
                //OnRollFinished?.Invoke();  //break game loop, to remove asap
            }
        }
        public void SelectingDiceToReroll()
        {
            GameObject dice = _inputManager.SelectingDiceToReroll();

            if (dice != null)
            {
                Debug.Log("Replace");
                DiceFace face = dice.GetComponent<DiceBehaviour>().Face;
                SelectedForReroll?.Invoke(face);
                RemoveDice(dice);
                Values.Remove(dice.GetComponent<DiceBehaviour>().Face);
                _diceToReroll.Add(dice);
                
                RefreshingFaceValues?.Invoke(Values);
                _diceManager.RerollDice(dice);
            } 
        }
        public void DiceToReroll()
        {
            StartEndSelectingDiceToReroll?.Invoke();
        }
        public void PrepareToRoll()
        {
            IsRollFinish = false;
        }
        public void PrepareToReroll()
        {
            _diceManager.ResetPoolPosition();
            _diceManager.WaitingForReroll();
        }
        public void DiceSelected()
        {
            for (int i = 0; i < _listOfDice.Count; i++)
            {
                _validDice.Add(_listOfDice[i]);
            }
            _listOfDice.Clear();
            for (int i = 0; i < _diceToReroll.Count; i++)
            {
               AddDice(_diceToReroll[i]);
            }
            _diceToReroll.Clear();
            _valueAdded = 0;
        }
        public bool CanReroll()
        {
            return _diceManager.IsReadyToReroll;
        }
    }
}