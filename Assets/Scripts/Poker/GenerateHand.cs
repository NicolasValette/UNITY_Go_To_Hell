using Gotohell.Dice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gotohell.Poker
{
    public class GenerateHand : MonoBehaviour
    {
        PokerCombinaison p;
        [SerializeField]
        private List<DiceFace> _df;
        // Start is called before the first frame update
        void Start()
        {
            p = new PokerCombinaison();
        }

        // Update is called once per frame
        void Update()
        {
            var key = Keyboard.current;
            if (key.rKey.wasPressedThisFrame)
            {
                //List<DiceFace> df = new List<DiceFace> { (DiceFace)2, (DiceFace)2, (DiceFace)2, (DiceFace)2, (DiceFace)2 };
                p.BuildHand(_df);
                Debug.Log("Hand : " + p.ToString());
            }
        }
    }
}