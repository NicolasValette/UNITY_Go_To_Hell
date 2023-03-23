using Gotohell.Dice;
using Gotohell.FSMPoolDice;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Gotohell.UI
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;
        private List<int> _valueToDisplay;

        private string _displayText;
        private StringBuilder _strbuilder;
        private int _diceDisplayed;

        // Start is called before the first frame update
        void Start()
        {
            _valueToDisplay = new List<int>();
            _strbuilder= new StringBuilder();
            _text.text = "Let's Roll Dices !";
            _strbuilder.AppendLine("Dice Values : ");
            _diceDisplayed = 0;
        }
        private void OnEnable()
        {
            DicePoolFSM.UpdateDisplay += AddValueToDisplay;
        }
        private void OnDisable()
        {
            DicePoolFSM.UpdateDisplay -= AddValueToDisplay;
        }
        // Update is called once per frame
        void Update()
        {
        }
        public void AddValueToDisplay(DiceFace face)
        {
            Debug.Log("Face : " + face.ToString());
            _diceDisplayed++;
            _strbuilder.AppendLine("Dice #" + _diceDisplayed + " => " + (int)face);
            //switch (face)
            //{
            //    case DiceFace.One:
            //        break;
            //    case DiceFace.Two:
            //        break;
            //    case DiceFace.Three:
            //        break;
            //    case DiceFace.Four:
            //        break;
            //    case DiceFace.Five:
            //        break;
            //    case DiceFace.Six:
            //        break;

            //}
            _text.text = _strbuilder.ToString();
        }
    }
}
