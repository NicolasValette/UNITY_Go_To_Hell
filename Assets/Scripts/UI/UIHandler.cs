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
        [SerializeField]
        private Color _winColor;
        [SerializeField]
        private Color _looseColor;
        [SerializeField]
        private DiceManager _diceManager;

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
        }
        private void OnDisable()
        {
            DiceManager.NewWave -= InitUI;
            DicePoolFSM.UpdateDice -= AddValueToDisplay;
            DeadsManager.ScoreToBeat -= DisplayDeadsScore;
            DiceManager.RoundWin -= RoundWin;
        }
        // Update is called once per frame
        void Update()
        {
            string str = ("Dice Values : " + _diceManager.DeathScore + "\n" + _strbuilder.ToString());
            
            _valueText.text = str;
        }
        public void InitUI()
        {
            _strbuilder = new StringBuilder();
            _valueText.text = "Let's Roll Dices !";
            _strbuilder.AppendLine("Dice Values : ");
            _diceDisplayed = 0;
        }
        public void AddValueToDisplay(DiceFace face)
        {
            Debug.Log("Face : " + face.ToString());
            _diceDisplayed++;
            _strbuilder.AppendLine("Dice #" + _diceDisplayed + " => " + (int)face);
        }
        public void DisplayDeadsScore(int score)
        {
            _deadsToDisplay.text = $"Beat {score} to take their souls";
            _deadsToDisplay.color = _looseColor;
        }
        public void RoundWin()
        {
            _deadsToDisplay.color = _winColor;
        }

    }
}
