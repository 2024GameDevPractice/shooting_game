using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Skill2 : Skill
{
    private int damage;
    protected override void init()
    {
        skillCooltime = 25f;
        duration = 1.5f;
        damage = 8;
        prefabName = "Skill2";
    }
    public override IEnumerator useSkill()
    {
        inCooltime = true;
        fielldTime = 0;
        GameObject go = (GameObject)Instantiate(Resources.Load($"Prefab/{prefabName}"), GameManager.Game.player.transform.position, Quaternion.Euler(0,0,90));
        Animator anime = go.AddComponent<Animator>();
        while (fielldTime <= duration)
        {
            fielldTime += Time.deltaTime;
            if(duration - fielldTime < 0.2f && anime.runtimeAnimatorController == null)
            {
                anime.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animation/Skill");
                anime.Play("boom");
            }
            go.transform.position += new Vector3(0, 2.25f, 0) * Time.deltaTime;
            yield return null;
        }
        Destroy(go);
        Monster[] monsters = GameObject.FindObjectsOfType<Monster>();
        Projectile[] projectiles = GameObject.FindObjectsOfType<Projectile>();
        yield return StartCoroutine("coolTime");
    }
}
