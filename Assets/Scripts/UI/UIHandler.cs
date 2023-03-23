using Gotohell.Dice;
using Gotohell.FSMPoolDice;
using Gotohell.Manager;
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
        private TMP_Text _valueText;
        [SerializeField]
        private TMP_Text _deadsToDisplay;

        private string _displayText;
        private StringBuilder _strbuilder;
        private int _diceDisplayed;

        // Start is called before the first frame update
        void Start()
        {
            _strbuilder= new StringBuilder();
            _valueText.text = "Let's Roll Dices !";
            _strbuilder.AppendLine("Dice Values : ");
            _diceDisplayed = 0;
        }
        private void OnEnable()
        {
            DicePoolFSM.UpdateDisplay += AddValueToDisplay;
            DeadsManager.DisplayScoreToBeat += DisplayDeadsScore;
        }
        private void OnDisable()
        {
            DicePoolFSM.UpdateDisplay -= AddValueToDisplay;
            DeadsManager.DisplayScoreToBeat -= DisplayDeadsScore;
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
            _valueText.text = _strbuilder.ToString();
        }
        public void DisplayDeadsScore(int score)
        {
            _deadsToDisplay.text = $"Beat {score} to take their souls";
        }

    }
}
