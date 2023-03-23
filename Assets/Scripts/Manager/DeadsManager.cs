using Gotohell.Dice;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gotohell.Manager
{
    public class DeadsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _deadsPrefab;
        [SerializeField]
        private Transform _deadsSpawn;

        private int _scoreToBeat;

        public static event Action<int> DisplayScoreToBeat;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            DiceManager.NewWave += SpawnDeads;
        }

        private void OnDisable()
        {
            DiceManager.NewWave -= SpawnDeads;
        }
        public void SpawnDeads()
        {
            Instantiate(_deadsPrefab, _deadsSpawn.position, Quaternion.identity);
            _scoreToBeat = UnityEngine.Random.Range(1, 6);
            Debug.Log("scor a battre : " + _scoreToBeat);
            DisplayScoreToBeat?.Invoke(_scoreToBeat);
        }

        public void SendToHell()
        {

        }
        public void SendToHeaver()
        {

        }
    }
}