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
    }
    private void OnDisable()
    {
        DiceManager.NewWave -= LaunchDice;
    }
    public void LaunchDice()
    {
        GameObject go = Instantiate(_deadsDicePrefab, _diceSpawn.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(_diceSpawn.forward * _launchForce, ForceMode.Impulse);
        go.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * 10);
    }
}
