using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.SocialPlatforms.Impl;
public class ScoreBoard : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] texts;
    [SerializeField]
    private TMP_Text input;
    private bool pre;
    private void Start()
    {
        pre = true;
        gameObject.SetActive(false);
        foreach (TMP_Text text in texts)
        {
            text.gameObject.SetActive(false);
        }
    }
    public void active()
    {
        gameObject.SetActive(true);
    }
    public void press()
    {
        if(pre)
        {
            pre = false;
            string name = input.text;

            if(name == null)
            {
                name = "(None)";
            }
            int score = 0;
            if(GameManager.Game.fs<= GameManager.Game.score)
            {
                if(GameManager.Game.fs == GameManager.Game.score && GameManager.Game.fs != 0)
                {
                    GameManager.Game.s = name;
                    GameManager.Game.ss = GameManager.Game.score;
                }
                else if(GameManager.Game.fs < GameManager.Game.score)
                {
                    GameManager.Game.s = GameManager.Game.f;
                    GameManager.Game.ss = GameManager.Game.fs;
                    GameManager.Game.f = name;
                    GameManager.Game.fs = GameManager.Game.score;
                }
                else
                {
                    Debug.Log(GameManager.Game.score);
                    GameManager.Game.f = name;
                    GameManager.Game.fs = GameManager.Game.score;
                    Debug.Log(GameManager.Game.score);
                }
            }
            else if(GameManager.Game.ss <= GameManager.Game.score)
            {
                if (GameManager.Game.ss == GameManager.Game.score)
                {
                    GameManager.Game.t = name;
                    GameManager.Game.ts = GameManager.Game.score;
                }
                else if (GameManager.Game.ss < GameManager.Game.score)
                {
                    GameManager.Game.t = GameManager.Game.s;
                    GameManager.Game.ts = GameManager.Game.ss;
                    GameManager.Game.s = name;
                    GameManager.Game.ss = GameManager.Game.score;
                }
                else
                {
                    GameManager.Game.s = name;
                    GameManager.Game.ss = GameManager.Game.score;
                }
            }
            else if(GameManager.Game.ts <= GameManager.Game.score)
            {
                GameManager.Game.t = name;
                GameManager.Game.ts = GameManager.Game.score;
            }
            foreach(TMP_Text text in texts)
            {
                text.gameObject.SetActive(true);
                if(text.gameObject.name == "1")
                {
                    Debug.Log(GameManager.Game.fs);
                    text.text = "name : " + GameManager.Game.f + " score : " + GameManager.Game.fs.ToString();
                    Debug.Log(GameManager.Game.fs);
                }
                else if(text.gameObject.name == "2")
                {
                    text.text = "name : " + GameManager.Game.s + " score : " + GameManager.Game.ss.ToString();
                }
                else if(text.gameObject.name == "3")
                {
                    text.text = "name : " + GameManager.Game.t + " score : " + GameManager.Game.ts.ToString();
                }
                text.gameObject.SetActive(true);
            }
        }
    }
    public void main()
    {
        SceneManager.LoadScene(1);
    }
}
