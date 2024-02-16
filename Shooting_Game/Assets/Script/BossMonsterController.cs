using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossMonsterController : MonoBehaviour
{
    protected Animator anime;
    protected string bulletName1;
    protected float moveSpeed;
    protected float attackSpeed;
    protected float fielldtime;
    protected int damage;
    protected int increaseScore;
    protected bool isAttack;
    public int hp;
    public int maxhp;
    protected virtual void Start()
    {
        anime = transform.gameObject.AddComponent<Animator>();
        StartCoroutine(defaultAttack());
        StartCoroutine(setState());
    }
    protected virtual void Update() { }
    protected virtual IEnumerator defaultAttack() { yield return null; }
    protected virtual IEnumerator setState() { yield return null; }
    public void attacked(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            anime.Play("death");
            Invoke("death", 0.375f);
        }
    }
    protected void dropItem()
    {
        GameObject go;
        switch (Random.Range(0, 6))
        {
            case 0:
                go = (GameObject)Instantiate(Resources.Load("Prefab/Item1"), transform.position, new Quaternion(0, 0, 0, 0));
                go.AddComponent<Item1>();
                break;
            case 1:
                go = (GameObject)Instantiate(Resources.Load("Prefab/Item2"), transform.position, new Quaternion(0, 0, 0, 0));
                go.AddComponent<Item2>();
                break;
            case 2:
                go = (GameObject)Instantiate(Resources.Load("Prefab/Item3"), transform.position, new Quaternion(0, 0, 0, 0));
                go.AddComponent<Item3>();
                break;
            case 3:
            case 4:
            case 5:
                go = (GameObject)Instantiate(Resources.Load("Prefab/Item4"), transform.position, new Quaternion(0, 0, 0, 0));
                go.AddComponent<Item4>();
                break;
        }
    }
    protected virtual void death()
    {
        Monster[] monsters = GameObject.FindObjectsOfType<Monster>();
        Projectile1[] projectiles1 = GameObject.FindObjectsOfType<Projectile1>();
        Projectile2[] projectiles2 = GameObject.FindObjectsOfType<Projectile2>();
        Projectile3[] projectiles3 = GameObject.FindObjectsOfType<Projectile3>();
        Projectile4[] projectiles4 = GameObject.FindObjectsOfType<Projectile4>();
        BossMonsterController boss = GameObject.FindObjectOfType<BossMonsterController>();
        if (boss != null)
        {
            boss.attacked(damage);
        }
        foreach (Monster mon in monsters)
        {
            GameObject obj = mon.gameObject;
            string name = obj.name.Replace("(Clone)", "");
            switch (name)
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
        foreach (Projectile1 pro in projectiles1)
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
        GameManager.Game.stopSpawn = true;
        GameManager.Game.result.active();
        GameManager.Game.killCount++;
        GameManager.Game.score += increaseScore;
        dropItem();
        GameManager.Game.spawnBoss = false;
        if (GameManager.Game.player.hp != 100)
        {
            if (GameManager.Game.player.hp > 85)
            {
                GameManager.Game.player.hp = 100;
            }
            else
            {
                GameManager.Game.player.hp += 15;
            }
        }
        GameManager.Game.player.fuel += 30;
        Destroy(transform.gameObject);
    }
}
