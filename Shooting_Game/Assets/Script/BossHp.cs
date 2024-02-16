using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHp : MonoBehaviour
{
    private Slider slider;
    private BossMonsterController boss;
    private void Start()
    {
        slider = GetComponent<Slider>();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }
    private void Update()
    {
        if(boss != null)
        {
            slider.value = (float)boss.hp / boss.maxhp;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            boss = GameObject.FindObjectOfType<BossMonsterController>();
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
