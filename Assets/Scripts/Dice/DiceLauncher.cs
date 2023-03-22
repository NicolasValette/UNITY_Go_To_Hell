using Gotohell.FSMPoolDice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gotohell.Dice
{
    public class DiceLauncher : MonoBehaviour
    {
        [SerializeField]
        private GameObject _dicePrefab;
        [SerializeField]
        private Transform _diceLauncherPosition;
        [SerializeField]
        private float _launchForce;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var keyboard = Keyboard.current;
            var mouse = Mouse.current;

            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("Lauch");
                GameObject go = Instantiate(_dicePrefab, _diceLauncherPosition.position, Quaternion.identity);
                go.GetComponent<Rigidbody>().AddForce(go.transform.forward * _launchForce);
                go.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * 10);
            }

            if (mouse.leftButton.wasPressedThisFrame)
            {

                Debug.Log("click");
                Ray rayToMouse = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(rayToMouse, out hit))
                {
                    if (hit.transform.gameObject.GetComponent<DicePoolFSM>() != null)
                    {
                        Debug.Log("Drag");
                    }
                }

              
            }
        }
    }
}
