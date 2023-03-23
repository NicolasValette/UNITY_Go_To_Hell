using Gotohell.FSMPoolDice;
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

        public static event Action NewWave;

        // Start is called before the first frame update
        void Start()
        {
            InitDicePool();
        }

        // Update is called once per frame
        void Update()
        {
            var key = Keyboard.current;

            if (key.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("new wave");
                NewWave?.Invoke();
            }
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
    }
}