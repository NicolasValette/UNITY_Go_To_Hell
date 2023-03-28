using Gotohell.FSMPoolDice;
using Gotohell.Manager;
using Gotohell.Poker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gotohell.Dice
{
    public class DiceManager : MonoBehaviour
    {
        [Header("Pool settings")]
        [SerializeField]
        private int _initialPoolSize;
        private int _actualPoolSize;
        [SerializeField]
        private List<Transform> _diceSpawns;
        [SerializeField]
        private DicePoolFSM _dicePoolFSM;
        [SerializeField]
        private GameObject _dicePrefab;
        [SerializeField]
        private GameObject _pool;
        [SerializeField]
        private float _timeBetweenRounds = 3f;
        [SerializeField]
        private int _numberOfRerolls = 2;

        private PokerHand _handToBeat;
        public PokerHand ScoreToBeat { get => _handToBeat; }
        private bool _isWin;
        public bool IsWin { get => _isWin; }
        private int _deathScore;
        public int DeathScore { get => _deathScore; }
        public Vector3 InitialPoolPosition { get; private set; }

        private PokerCombinaison _pokerComb;
        public PokerHand Hand { get => _pokerComb.ActualHand; }
        public bool IsReadyToReroll { get; private set; }

        public static event Action NewWave;
        public static event Action RoundWin;
        public static event Action RoundLoose;
        public static event Action CleanWave;

        private int _positionIndice;

        // Start is called before the first frame update
        private void Awake()
        {
            InitialPoolPosition = _pool.transform.position;
        }
        void Start()
        {
            _pokerComb = new PokerCombinaison();
            InitDicePool();
            NewWave?.Invoke();
        }
        private void OnEnable()
        {
            DeadsManager.ScoreToBeat += ScoreToBeatThisRound;
            DicePoolFSM.UpdateDice += ReadDice;
            DicePoolFSM.OnRollFinished += RoundFinish;
        }
        private void OnDisable()
        {
            DeadsManager.ScoreToBeat -= ScoreToBeatThisRound;
            DicePoolFSM.UpdateDice -= ReadDice;
            DicePoolFSM.OnRollFinished -= RoundFinish;
        }
        // Update is called once per frame
        void Update()
        {
            //var key = Keyboard.current;

            //if (key.spaceKey.wasPressedThisFrame)
            //{
            //    Debug.Log("new wave");
            //    InitDicePool();
            //    NewWave?.Invoke();
            //}
        }
        public void StartRound()
        {
            _pokerComb.ChangeHand(PokerHand.None);
            _isWin = false;
            ResetPoolPosition();
        }
        private void InitDicePool()
        {
            for (int i = 0; i < _initialPoolSize; i++)
            {
                GameObject go = Instantiate(_dicePrefab, _diceSpawns[i % _diceSpawns.Count].position, Quaternion.Euler(UnityEngine.Random.insideUnitSphere));
                go.transform.SetParent(_pool.transform);
                go.tag = "ReadyToRoll";
                _dicePoolFSM.AddDice(go);
            }
        }
        private void CleanDicePool()
        {
            Debug.Log("Clean");
            _dicePoolFSM.ClearPool();
            for (int i = 0; i < _initialPoolSize; i++)
            {
                Destroy(_pool.transform.GetChild(i).gameObject);
            }
        }
        public void RerollDice(GameObject dice)
        {
            dice.transform.position = _diceSpawns[_positionIndice % _diceSpawns.Count].position;
            dice.transform.rotation = UnityEngine.Random.rotation;
            dice.transform.SetParent(_pool.transform);
            dice.tag = "ReadyToRoll";
        }
        public void ScoreToBeatThisRound(PokerHand hand)
        {
            _handToBeat = hand;
        }
        public void ReadDice(DiceFace face)
        {
            //_deathScore += (int)face;
            //if (_deathScore >= _scoreToBeat)
            //{
            //    Debug.Log("Win");
            //    _isWin = true;
            //    RoundWin?.Invoke();
            //}
        }
        public void RoundFinish()
        {
            _pokerComb.BuildHand(_dicePoolFSM.Values);
            int compare = _pokerComb.CompareHand(_handToBeat);
            if (compare >= 0)
            {
                Debug.Log("Win");
                _isWin = true;
                RoundWin?.Invoke();
            }
            else
            {
                RoundLoose?.Invoke();
            }
            _handToBeat = PokerHand.None;
            _deathScore = 0;
            Debug.Log("RoundFinish");

            StartCoroutine(WaitBetweenRound());

        }
        private IEnumerator WaitBetweenRound()
        {
            yield return new WaitForSeconds(_timeBetweenRounds);
            CleanWave?.Invoke();
            CleanDicePool();
            NewWave?.Invoke();
            StartRound();
            InitDicePool();
        }
        public void ResetPoolPosition()
        {
            _pool.transform.SetPositionAndRotation(InitialPoolPosition, Quaternion.identity);
        }
        //Method call by UI button
        public void ReadyToReroll()
        {
            IsReadyToReroll = true;
        }
        public void WaitingForReroll()
        {
            IsReadyToReroll = false;
        }
    }
}