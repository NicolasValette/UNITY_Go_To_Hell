using Gotohell.Dice;
using Gotohell.FSMPoolDice;
using Gotohell.Manager;
using Gotohell.Poker;
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
        [SerializeField]
        private Color _winColor;
        [SerializeField]
        private Color _looseColor;
        [SerializeField]
        private DiceManager _diceManager;
        [SerializeField]
        private GameObject _rerollButtons;

        private string _displayText;
        private StringBuilder _strbuilder;
        private int _diceDisplayed;
        private int _scoreDeath;

        // Start is called before the first frame update
        void Start()
        {
            InitUI();
        }
        private void OnEnable()
        {
            DiceManager.NewWave += InitUI;
            DicePoolFSM.UpdateDice += AddValueToDisplay;
            DeadsManager.ScoreToBeat += DisplayDeadsScore;
            DiceManager.RoundWin += RoundWin;
            DicePoolFSM.StartEndSelectingDiceToReroll += ToggleRerollButton;
            DicePoolFSM.RefreshingFaceValues += RefrechValueToDisplay;
        }
        private void OnDisable()
        {
            DiceManager.NewWave -= InitUI;
            DicePoolFSM.UpdateDice -= AddValueToDisplay;
            DeadsManager.ScoreToBeat -= DisplayDeadsScore;
            DiceManager.RoundWin -= RoundWin;
            DicePoolFSM.StartEndSelectingDiceToReroll += ToggleRerollButton;
            DicePoolFSM.RefreshingFaceValues -= RefrechValueToDisplay;
        }
        // Update is called once per frame
        void Update()
        {
            string str = ("Dice Values : " + _diceManager.Hand.ToString() + "\n" + _strbuilder.ToString());

            _valueText.text = str;
        }
        public void InitUI()
        {
            _strbuilder = new StringBuilder();
            _valueText.text = "Let's Roll Dices !";
            _strbuilder.AppendLine("Dice Values : ");
            _diceDisplayed = 0;
            _rerollButtons.SetActive(false);
        }
        public void AddValueToDisplay(DiceFace face)
        {
            Debug.Log("Face : " + face.ToString());
            _diceDisplayed++;
            _strbuilder.AppendLine("Dice #" + _diceDisplayed + " => " + (int)face);
        }
        public void RefrechValueToDisplay(List<DiceFace> faces)
        {
            _strbuilder = new StringBuilder();
            _strbuilder.AppendLine("Dice Values : ");
            _diceDisplayed = 0;
            for (int i = 0; i < faces.Count; i++)
            {
                _diceDisplayed++;
                _strbuilder.AppendLine("Dice #" + _diceDisplayed + " => " + (int)faces[i]);
            }
        }
        public void DisplayDeadsScore(PokerHand hand)
        {
            _deadsToDisplay.text = $"Beat {hand} to take their souls";
            _deadsToDisplay.color = _looseColor;
        }
        public void RoundWin()
        {
            _deadsToDisplay.color = _winColor;
        }
        public void ToggleRerollButton()
        {
            _rerollButtons.SetActive(!_rerollButtons.activeSelf);
        }
    }
}
