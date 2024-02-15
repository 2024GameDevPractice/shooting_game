using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
public class Skill1 : Skill
{
    private int healAmount;
    protected override void init()
    {
        skillCooltime = 15;
        healAmount = 10;
        duration = 1;
        prefabName = "Skill1";
    }
    public override IEnumerator useSkill()
    {
        inCooltime = true;
        fielldTime = 0;
        GameObject go = (GameObject)Instantiate(Resources.Load($"Prefab/{prefabName}"), GameManager.Game.player.transform.position, Quaternion.identity);
        if(GameManager.Game.player.hp != 100)
        {
            if (GameManager.Game.player.hp > 100 - healAmount)
            {
                GameManager.Game.player.hp = 100;
            }
            else
            {
                GameManager.Game.player.hp += healAmount;
            }
        }
        while(fielldTime <= duration)
        {
            fielldTime += Time.deltaTime;
            go.transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
            yield return null;
        }
        Destroy(go);
        yield return StartCoroutine(coolTime());
    }
}
