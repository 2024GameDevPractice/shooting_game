using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inCooltime : MonoBehaviour
{
    private Image image;
    [SerializeField]
    private int type;
    void Start()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        
        if(GameManager.Game.player.skillCount > 0)
        {
            if (type == 1)
            {
                if (!GameManager.Game.player.skill1.inCooltime)
                {
                    image.fillAmount = 1;
                }
                else
                {
                    image.fillAmount = GameManager.Game.player.skill1.fielldTime / GameManager.Game.player.skill1.skillCooltime;
                }
            }
            else if (type == 0)
            {
                if (!GameManager.Game.player.skill2.inCooltime)
                {
                    image.fillAmount = 1;
                }
                else
                {
                    image.fillAmount = GameManager.Game.player.skill2.fielldTime / GameManager.Game.player.skill2.skillCooltime;
                }
            }
        }
        else
        {
            image.fillAmount = 0;
        }
    }
}
