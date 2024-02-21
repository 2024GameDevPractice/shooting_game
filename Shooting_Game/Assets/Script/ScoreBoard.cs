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
            GameManager.Game.board.Add(input.text);
            GameManager.Game.scores.Add(input.text, GameManager.Game.score);
            string fname = null;
            string sname = null;
            string tname = null;
            int fir = 0;
            int sec = 0;
            int thr = 0;
            foreach (string str in GameManager.Game.board)
            {
                if (GameManager.Game.fs <= GameManager.Game.scores[str])
                {
                    fname = str;
                    fir = GameManager.Game.scores[str];
                    break;
                }
                else if (GameManager.Game.ss <= GameManager.Game.scores[str])
                {
                    sname = str;
                    sec = GameManager.Game.scores[str];
                    break;
                }
                else if (GameManager.Game.ts <= GameManager.Game.scores[str])
                {
                    tname = str;
                    thr = GameManager.Game.scores[str];
                    break;
                }
            }
            foreach (TMP_Text text in texts)
            {
                if (fir != 0 || (fir == 0 && fname != null))
                {
                    text.gameObject.SetActive(true);
                    if (fname == "")
                    { fname = " "; }
                    GameManager.Game.f = fname;
                    GameManager.Game.fs = fir;
                    text.text = "name : " + GameManager.Game.f + " score : " + GameManager.Game.fs.ToString();
                }
                else if (sec != 0 || (sec == 0 && sname != null))
                {
                    text.gameObject.SetActive(true);
                    if(sname == "")
                    { sname = " "; }
                    GameManager.Game.s = sname;
                    GameManager.Game.ss = sec;
                    text.text = "name : " + GameManager.Game.s + " score : " + GameManager.Game.ss.ToString();
                }
                else if (thr != 0 || (thr == 0 && tname != null))
                {
                    text.gameObject.SetActive(true);
                    if (tname == "")
                    { tname = " "; }
                    GameManager.Game.t = tname;
                    GameManager.Game.ts = thr;
                    text.text = "name : " + GameManager.Game.t + " score : " + GameManager.Game.ts.ToString();
                }
            }
        }
    }
    public void main()
    {
        SceneManager.LoadScene(1);
    }
}
