using Gotohell.Dice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadsDiceLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject _deadsDicePrefab;
    [SerializeField]
    private Transform _diceSpawn;
    [SerializeField]
    private float _launchForce;

    private GameObject _actualDice;
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
        DiceManager.NewWave += LaunchDice;
        DiceManager.CleanWave += DestroyDice;
    }
    private void OnDisable()
    {
        DiceManager.NewWave -= LaunchDice;
        DiceManager.CleanWave += DestroyDice;
    }
    public void LaunchDice()
    {
        _actualDice = Instantiate(_deadsDicePrefab, _diceSpawn.position, Quaternion.identity);
        _actualDice.GetComponent<Rigidbody>().AddForce(_diceSpawn.forward * _launchForce, ForceMode.Impulse);
        _actualDice.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * 10);
    }
    public void DestroyDice()
    {
        Destroy(_actualDice);
    }
}
