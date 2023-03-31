using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
using Gotohell.FSMPoolDice;
using Gotohell.Dice;

public class UI_FlipBook : MonoBehaviour
{
    private AnimationCurve m_smoothCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });
    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    //public void Display_UI_Dice()
    //{
    //    anim.Play("Explode", -1, 0f);
    //}
}







//    private void LoadSpriteSheet() //je recupère les sprites dans la texture pour en faire un array
//    {
//        string ImgPath = AssetDatabase.GetAssetPath(_textureToSet);

//        //Sprites = AssetDatabase.LoadAllAssetsAtPath(ImgPath).OfType<Sprite>().ToArray();
//        Sprites = AssetDatabase.LoadAllAssetsAtPath(ImgPath).OfType<Sprite>();
//        //Debug.Log(Sprites.Length);

//        //if (Sprites != null && Sprites.Length > 0)
//        //{
//        //    _timePerFrame = 1f / _frameRate;
//        //}
//        //else
//        //Debug.Log("Impossible de loader les sprites !");
//    }
//    void Update()
//    {
//        //_elapseTime += Time.deltaTime * _speed; // je pense que c'est pas terrible ici

//        if (Input.GetKeyDown(KeyCode.DownArrow))
//        {
//            _flipbookImage.color = new Color(1, 1, 1, 1);
//            StartCoroutine(RunSprite());
//            //Debug.Log("arrow pressed");

//            //if (_elapseTime >= _timePerFrame)
//            //{
//            //++_currentFrame; //incrément pour les num du sprite
//            //SetSprite();

//            //if (_currentFrame >= Sprites.Length)
//            //{
//            //    _currentFrame = 0;
//            //StartCoroutine(FadeImage());
//            StartCoroutine(Fade(1, 0));
//            //}
//            //_currentFrame = 0;
//            //}
//        }
//    }
//    //private void SetSprite()
//    //{
//    //    //if (_currentFrame >= 0 && _currentFrame < Sprites.Length)
//    //    Debug.Log("sprite set to :" + _currentFrame);
//    //    _flipbookImage.sprite = Sprites[_currentFrame];
//    //}
//    IEnumerator RunSprite()
//    {
//        Debug.Log("i'm there");
//        while(_currentFrame < Sprites.Length)
//        {
//            yield return new WaitForSeconds(0.01f);
//            _flipbookImage.sprite = Sprites[_currentFrame];
//        _currentFrame++;
//        }
//        //yield return null;
//    }
//    IEnumerator FadeImage()
//    {
//        yield return new WaitForSeconds(0.5f);
//        _flipbookImage.color = Color.Lerp(new (1,1,1,1), new (1,1,1,0), t);
//        if ( t > 0.1f)
//        {
//            t += Time.deltaTime / _speed;
//        }

//        //Debug.Log("i'm there too");

//        //for (float i = 1; i >= 0; i -= 0.1f)
//        //{
//        //    Debug.Log("i'm there again");
//        //    // set color with i as alpha (0 is transparent 1 is full)
//        //    _flipbookImage.color = new Color(1, 1, 1, i);
//        //    yield return new WaitForSeconds(1);
//    }

//    private IEnumerator Fade(float start, float end)
//    {
//        yield return new WaitForSeconds(0.5f);
//        _CurrentTime = 0f;
//        while (_CurrentTime <= _fadeDuration)
//        {
//            _CurrentTime += Time.deltaTime;
//            Color c = _flipbookImage.color;
//            _flipbookImage.color = new Color(c.r, c.g, c.b, Mathf.Lerp(start, end, m_smoothCurve.Evaluate(_CurrentTime / _fadeDuration)));
//            _currentFrame = 0;
//        //yield return null;
//        }
//    }

//}
