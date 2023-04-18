using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem;
using Gotohell.FSMPoolDice;
using Gotohell.Dice;

public class UI_ChooseSprite : MonoBehaviour
{
    [SerializeField]
    private Texture2D _textureToSet;
    [SerializeField]
    private int _diceFace = 0;
    [SerializeField]
    private int DiceNumber;
    [SerializeField]
    private GameObject DiceObj;
    [SerializeField]
    private List<Sprite> _sprites;

    private Image _flipbookImage = null;
    private Sprite[] Sprites = null;
    [SerializeField]
    private List<GameObject> UI_Dice_Holder;
    private int _dice_Index;

    void Start()
    {
        _flipbookImage = GetComponent<Image>();
        LoadSpriteSheet();
        Debug.Log("je compte :" + UI_Dice_Holder.Count);
    }

    private void OnEnable()
    {
        DicePoolFSM.UpdateDice += Display_UI_Dice;
        DicePoolFSM.SelectedForReroll += Remove_UI_Dice;
        //DicePoolFSM.
    }

    private void OnDisable()
    {
        DicePoolFSM.UpdateDice -= Display_UI_Dice;
        DicePoolFSM.SelectedForReroll -= Remove_UI_Dice;
    }

    public void Display_UI_Dice(DiceFace face)
    {
        
        for (int i = 0; i < UI_Dice_Holder.Count; i++)
        {
            if (UI_Dice_Holder[i].GetComponent<Image>().sprite == null)
            { 
                UI_Dice_Holder[i].GetComponent<Image>().sprite = Sprites[(int)face-1];
                StartCoroutine("FadeIn", i);
                UI_Dice_Holder[i].GetComponentInChildren<Animator>().Play("Explode", -1, 0f);
                return;
            }
        }
    }

    public void Remove_UI_Dice(DiceFace face)
    {
        bool gotthisdice = false;
        for (int i = 0; i < UI_Dice_Holder.Count; i++)
        {
            if (UI_Dice_Holder[i].GetComponent<Image>().sprite == Sprites[(int)face-1] && !gotthisdice)
            {
                UI_Dice_Holder[i].GetComponent<Image>().sprite = null;
                UI_Dice_Holder[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                gotthisdice = true;
            }
        }
    }



        private void LoadSpriteSheet()
    {
        //string ImgPath = AssetDatabase.GetAssetPath(_textureToSet);
        //Sprites = AssetDatabase.LoadAllAssetsAtPath(ImgPath).OfType<Sprite>().ToArray();

        //string imgPath = _textureToSet.name;
        //Sprites = Resources.LoadAll<Sprite>(imgPath);
        Sprites = _sprites.ToArray();
    }



    //public void SetSprite(DiceFace face)
    //{
    //    StartCoroutine("FadeIn");
    //    _flipbookImage.sprite = ;
    //}


    IEnumerator FadeIn(int _place_Holder)
    {
            for (float i = 0; i <= 1; i += Time.deltaTime * 2)
            {
            // set color with i as alpha
                UI_Dice_Holder[_place_Holder].GetComponent<Image>().color = new Color(1, 1, 1, i);
                yield return null;
            }
    }
}
