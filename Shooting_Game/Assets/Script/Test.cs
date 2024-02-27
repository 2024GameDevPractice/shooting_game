using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        GameManager.Game.gameStart();
    }
    bool a = true;
    void Update()
    {
        if(a)
        {
            if (GameManager.Game.stage == GameManager.stages.Stage2)
            {
                AudioSource audio = GetComponent<AudioSource>();
                audio.clip = Resources.Load<AudioClip>("Sound/BGM/Virus (Loopable)");
                audio.enabled = false;
                audio.enabled = true;
                a = !a;
            }
        }
        Debug.Log(GameManager.Game.score);
    }
}
