using Gotohell.FSMPoolDice;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gotohell
{
    public class InputManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }

        public bool IsDiceSelected()
        {
            var mouse = Mouse.current;
            if (mouse.leftButton.wasPressedThisFrame)
            {
                Debug.Log("click");
                Ray rayToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(rayToMouse, out hit))
                {
                    if (hit.transform.gameObject.GetComponent<DicePoolFSM>() != null)
                    {
                        return true ;
                    }
                }
            }
            return false;
        }

        public Vector3 GetPosition()
        {
            var mouse = Mouse.current;
            Ray rayToMouse = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(rayToMouse, out hit))
            {
                Vector3 vect = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                Vector3 PawnToCamVect = vect - Camera.main.transform.position;

                return vect;
            }
            return Vector3.zero;
        }
        public bool IsDrop()
        {
            var mouse = Mouse.current;
            mouse.delta.ReadValue();
            return mouse.leftButton.wasReleasedThisFrame;
        }
        public Vector2 GetCursorDeltaPos()
        {
            var mouse = Mouse.current;
            return mouse.delta.ReadValue();
        }
    }
}
