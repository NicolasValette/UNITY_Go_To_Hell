using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiceDebug : MonoBehaviour
{
    [SerializeField]
    private float _launchForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;

        if (keyboard.aKey.wasPressedThisFrame)
        {
            Debug.Log("force");
            GetComponent<Rigidbody>().AddForce(transform.forward * _launchForce, ForceMode.Impulse);
        }
    }
}
