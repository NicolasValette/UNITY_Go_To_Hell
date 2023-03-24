using Gotohell.Dice;
using Gotohell.Poker;
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
        private PokerCombinaison _pokerComb;

        public static event Action<PokerHand> ScoreToBeat;
        // Start is called before the first frame update
        void Start()
        {
            _pokerComb= new PokerCombinaison();
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
            List<DiceFace> df = new List<DiceFace> {
                (DiceFace)UnityEngine.Random.Range(1, 6),
                (DiceFace)UnityEngine.Random.Range(1, 6),
                (DiceFace)UnityEngine.Random.Range(1, 6),
                (DiceFace)UnityEngine.Random.Range(1, 6),
                (DiceFace)UnityEngine.Random.Range(1, 6)
            };
            _pokerComb.BuildHand(df);
            Debug.Log("Hand : " + _pokerComb.ToString());
            ScoreToBeat?.Invoke(_pokerComb.ActualHand);
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