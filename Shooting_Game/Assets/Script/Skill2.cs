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
        Projectile1[] projectiles = GameObject.FindObjectsOfType<Projectile1>();
        foreach(Monster mon in monsters)
        {
            GameObject obj = mon.gameObject;
            string name = obj.name.Replace("(Clone)", "");
            switch(name)
            {
                case "a":
                    obj.GetComponent<monster_a>().attacked(damage);
                    break;
                case "b":
                    obj.GetComponent<monster_b>().attacked(damage);
                    break;
                case "c":
                    obj.GetComponent<monster_c>().attacked(damage);
                    break;
            }
        }
        foreach(Projectile1 pro in projectiles)
        {
            Destroy(pro.gameObject);
        }
        yield return StartCoroutine("coolTime");
    }
}
