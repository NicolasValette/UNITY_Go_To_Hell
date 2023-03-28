using Gotohell.Dice;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gotohell
{
    public class InputManager : MonoBehaviour
    {

        public static event Action<Transform> OnDragDice;
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
                Ray rayToMouse = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
                
                if (Physics.Raycast(rayToMouse, out RaycastHit hit))
                {
                    if (hit.transform.gameObject.CompareTag("ReadyToRoll"))
                    {
                        OnDragDice?.Invoke(hit.transform.gameObject.transform.parent);
                        return true;
                    }
                }
            }
            return false;
        }
        public GameObject SelectingDiceToReroll()
        {
            var mouse = Mouse.current;
            if (mouse.leftButton.wasPressedThisFrame)
            {
                Ray rayToMouse = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
                if (Physics.Raycast(rayToMouse, out RaycastHit hit))
                {
                    if (hit.transform.gameObject.GetComponent<DiceBehaviour>() != null)
                    {
                        return hit.transform.gameObject;
                    }
                }
            }
            return null;
        }

        public Vector3 GetPosition()
        {
            var mouse = Mouse.current;
            Ray rayToMouse = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(rayToMouse, out hit))
            {
                Vector3 vect = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                return vect;
            }
            return Vector3.zero;
        }
        public Vector3 GetPosition2()
        {
            var mouse = Mouse.current.position.ReadValue();
            Vector3 v = new Vector3(mouse.x, 0f, mouse.y);
            v = Camera.main.ScreenToWorldPoint(v);
            return v;
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
