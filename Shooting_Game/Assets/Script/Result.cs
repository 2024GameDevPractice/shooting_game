using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
public class Result : MonoBehaviour
{
    private TMP_Text[] texts;
    private Image[] image;
    private Image img;
    private void Start()
    {
        texts = GameObject.FindObjectsOfType<TMP_Text>();
        image = GameObject.FindObjectsOfType<Image>();
        foreach(Image im in image)
        {
            if(im.gameObject.name == "Panel")
            {
                img = im;
            }
        }
        img.gameObject.SetActive(false);
    }
    public void active()
    {
        img.gameObject.SetActive(true);
        GameManager.Game.stopSpawn = true;
        foreach(TMP_Text text in texts)
        {
            if(text.gameObject.name == "score")
            {
                text.text = "Score : " + GameManager.Game.score;
            }
            else if(text.gameObject.name == "playtime")
            {
                text.text = "playtime " + GameManager.Game.stopWatch.Elapsed.Minutes + " : " + GameManager.Game.stopWatch.Elapsed.Seconds;
            }
        }
    }
    public void Press()
    {
        GameManager.Game.stopWatch.Start();
        GameManager.Game.stopSpawn = false;
        if(GameManager.Game.stage == GameManager.stages.Stage2 || GameManager.Game.player.hp <= 0)
        {
            GameManager.Game.gameEnd();
            GameManager.Game.scoreboard.active();
            img.gameObject.SetActive(false);
        }
        else
        {
            GameManager.Game.stopSpawn = false;
            GameManager.Game.stopWatch.Start();
            GameManager.Game.stage = stages.Stage2;
            GameManager.Game.nextStage();
            img.gameObject.SetActive(false);
        }
    }
}
