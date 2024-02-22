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
            GameManager.Game.scores.Add(input.text, GameManager.Game.score);
            string name = input.text;
            if(name == null)
            {
                name = "(None)";
            }
            int score = 0;
            if(GameManager.Game.fs<= GameManager.Game.score)
            {
                if(GameManager.Game.fs == GameManager.Game.score && GameManager.Game.f != null)
                {
                    GameManager.Game.s = name;
                    GameManager.Game.ss = GameManager.Game.score;
                }
                else
                {
                    GameManager.Game.f = name;
                    GameManager.Game.fs = score;
                }
            }
            else if(GameManager.Game.ss <= GameManager.Game.score)
            {
                if (GameManager.Game.ss == GameManager.Game.score && GameManager.Game.t != null)
                {
                    GameManager.Game.t = name;
                    GameManager.Game.ts = GameManager.Game.score;
                }
                else
                {
                    GameManager.Game.s = name;
                    GameManager.Game.ss = score;
                }
            }
            else if(GameManager.Game.ts <= GameManager.Game.score)
            {
                GameManager.Game.t = name;
                GameManager.Game.ts = score;
            }
            foreach(TMP_Text text in texts)
            {
                if(text.gameObject.name == "1")
                {
                    text.text = "name : " + GameManager.Game.f + " score : " + GameManager.Game.fs.ToString();
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
