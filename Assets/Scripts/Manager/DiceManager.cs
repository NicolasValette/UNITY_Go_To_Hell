using Gotohell.FSMPoolDice;
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

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i<_initialPoolSize; i++)
            {
                GameObject go = Instantiate(_dicePrefab, _diceSpawns[i].position, Quaternion.Euler(Random.insideUnitSphere));
                go.transform.SetParent(_pool.transform);
                _dicePoolFSM.AddDice(go);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}