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
        private GameObject _actualDeads;
        public static event Action<int> ScoreToBeat;
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
            DiceManager.RoundWin += SendToHell;
            DiceManager.RoundLoose += SendToHeaven;
            DiceManager.CleanWave += DestroyDeads;
        }

        private void OnDisable()
        {
            DiceManager.NewWave -= SpawnDeads;
            DiceManager.RoundWin -= SendToHell;
            DiceManager.RoundLoose -= SendToHeaven;
            DiceManager.CleanWave -= DestroyDeads;
        }
        public void SpawnDeads()
        {
            _actualDeads = Instantiate(_deadsPrefab, _deadsSpawn.position, Quaternion.identity);
            _scoreToBeat = UnityEngine.Random.Range(1, 6);
            Debug.Log("scor a battre : " + _scoreToBeat);
            ScoreToBeat?.Invoke(_scoreToBeat);
        }
        public void DestroyDeads()
        {
            Destroy(_actualDeads);
            _scoreToBeat = 0;
        }

        public void SendToHell()
        {
            _actualDeads.GetComponent<Animator>().SetTrigger("Hell");
        }
        public void SendToHeaven()
        {
            _actualDeads.GetComponent<Animator>().SetTrigger("Heaven");
        }
    }
}