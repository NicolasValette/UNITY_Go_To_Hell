using Gotohell.FSMPoolDice;
using Gotohell.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
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

        private int _scoreToBeat;
        public int ScoreToBeat { get => _scoreToBeat; }
        private bool _isWin;
        public bool IsWin { get => _isWin; }
        private int _deathScore;
        public int DeathScore { get => _deathScore; }
        public Vector3 InitialPoolPosition { get; private set; }

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
            _isWin = false;
            _pool.transform.SetPositionAndRotation(InitialPoolPosition, Quaternion.identity);
        }
        private void InitDicePool()
        {
            for (int i = 0; i < _initialPoolSize; i++)
            {
                GameObject go = Instantiate(_dicePrefab, _diceSpawns[i].position, Quaternion.Euler(UnityEngine.Random.insideUnitSphere));
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

        public void ScoreToBeatThisRound(int score)
        {
            _scoreToBeat = score;
        }
        public void ReadDice(DiceFace face)
        {
            _deathScore += (int)face;
            if (_deathScore >= _scoreToBeat)
            {
                Debug.Log("Win");
                _isWin = true;
                RoundWin?.Invoke();
            }
        }
        public void RoundFinish()
        {
            _scoreToBeat = 0;
            _deathScore = 0;
            Debug.Log("RoundFinish");
            if (!_isWin)
            {
                RoundLoose?.Invoke();
            }
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