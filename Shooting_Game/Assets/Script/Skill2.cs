using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
public class Skill2 : Skill
{
    private int damage;
    private Animator anime;
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
        float time = 0;
        GameObject go = (GameObject)Instantiate(Resources.Load($"Prefab/{prefabName}"), GameManager.Game.player.transform.position, Quaternion.Euler(0,0,90));
        while (time <= duration)
        {
            time += Time.deltaTime;
            if(time > 1.25f && anime == null)
            {
                anime = go.AddComponent<Animator>();
                anime.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animation/Skill");
            }
            go.transform.position += new Vector3(0, 2.25f, 0) * Time.deltaTime;
            yield return null;
        }
        anime = null;
        Destroy(go);
        Monster[] monsters = GameObject.FindObjectsOfType<Monster>();
        Projectile1[] projectiles1 = GameObject.FindObjectsOfType<Projectile1>();
        Projectile2[] projectiles2 = GameObject.FindObjectsOfType<Projectile2>();
        Projectile3[] projectiles3 = GameObject.FindObjectsOfType<Projectile3>();
        Projectile4[] projectiles4 = GameObject.FindObjectsOfType<Projectile4>();
        BossMonsterController boss = GameObject.FindObjectOfType<BossMonsterController>();
        if(boss != null)
        {
            boss.attacked(damage);
        }
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
        foreach(Projectile1 pro in projectiles1)
        {
            Destroy(pro.gameObject);
        }
        foreach (Projectile2 pro in projectiles2)
        {
            Destroy(pro.gameObject);
        }
        foreach (Projectile3 pro in projectiles3)
        {
            Destroy(pro.gameObject);
        }
        foreach (Projectile4 pro in projectiles4)
        {
            Destroy(pro.gameObject);
        }
        yield return StartCoroutine("coolTime");
    }
}
