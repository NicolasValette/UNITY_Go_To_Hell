using Gotohell.FSMPoolDice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gotohell.Dice
{
    public class DiceHand : MonoBehaviour
    {

        bool isPoolSelected = false;

        private void OnEnable()
        {
            DicePoolFSM.OnPoolDrag += SelectPool;
            DicePoolFSM.OnPoolDrop += DropPool;
        }
        private void OnDisable()
        {
            DicePoolFSM.OnPoolDrag -= SelectPool;
            DicePoolFSM.OnPoolDrop -= DropPool;
        }

        private void Update()
        {
            if (isPoolSelected)
            {
                transform.Rotate(0, 0.5f, 0);
            }
        }
        public void SelectPool(List<GameObject> dices)
        {
            Debug.Log("SelectPool");
            Transform poolTransform = dices[0].transform.parent;
            for (int i = 0; i < dices.Count; i++)
            {
                dices[i].transform.position = poolTransform.position;
                dices[i].transform.rotation = Quaternion.Euler(0f, (360f / dices.Count) * i, 0f);
                dices[i].transform.Translate(dices[i].transform.right);
            }
            isPoolSelected = true;
        }
        public void DropPool()
        {
            isPoolSelected = false;
        }
    }
}