using Gotohell.FSMPoolDice;
using Gotohell.Manager;
using Gotohell.Poker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private PokerHand _handToBeat;
        public PokerHand ScoreToBeat { get => _handToBeat; }
        private bool _isWin;
        public bool IsWin { get => _isWin; }
        private int _deathScore;
        public int DeathScore { get => _deathScore; }
        public Vector3 InitialPoolPosition { get; private set; }

        private PokerCombinaison _pokerComb;
        public PokerHand Hand { get => _pokerComb.ActualHand; }

        public static event Action NewWave;
        public static event Action RoundWin;
        public static event Action RoundLoose;
        public static event Action CleanWave;

        // Start is called before the first frame update
        private void Awake()
        {
            InitialPoolPosition = _pool.transform.position;
        }
        void Start()
        {
            _pokerComb = new PokerCombinaison();
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
            var key = Keyboard.current;

            if (key.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("new wave");
                InitDicePool();
                NewWave?.Invoke();
            }
        }
        public void StartRound()
        {
            _pokerComb.ChangeHand(PokerHand.None);
            _isWin = false;
            _pool.transform.SetPositionAndRotation(InitialPoolPosition, Quaternion.identity);
        }
        private void InitDicePool()
        {
            for (int i = 0; i < _initialPoolSize; i++)
            {
                GameObject go = Instantiate(_dicePrefab, _diceSpawns[i % _diceSpawns.Count].position, Quaternion.Euler(UnityEngine.Random.insideUnitSphere));
                go.transform.SetParent(_pool.transform);
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

    }
}