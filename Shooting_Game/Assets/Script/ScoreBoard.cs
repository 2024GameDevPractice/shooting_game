using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System;
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
                if (fir < GameManager.Game.scores[str])
                {
                    fname = str;
                    fir = GameManager.Game.scores[str];
                }
                else if (sec < GameManager.Game.scores[str])
                {
                    sname = str;
                    sec = GameManager.Game.scores[str];
                }
                else if (thr < GameManager.Game.scores[str])
                {
                    tname = str;
                    thr = GameManager.Game.scores[str];
                }
            }
            foreach (TMP_Text text in texts)
            {
                if (text.gameObject.name == "1")
                {
                    text.gameObject.SetActive(true);
                    text.text = "name : " + fname + " score : " + fir.ToString();
                }
                else if (text.gameObject.name == "2")
                {
                    text.gameObject.SetActive(true);
                    text.text = "name : " + sname + " score : " + sec.ToString();
                }
                else if (text.gameObject.name == "3")
                {
                    text.gameObject.SetActive(true);
                    text.text = "name : " + tname + " score : " + thr.ToString();
                }
            }
        }
    }
    public void main()
    {
        SceneManager.LoadScene(1);
    }
}
