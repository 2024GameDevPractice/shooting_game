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
    protected int hp;
    protected int damage;
    protected int increaseScore;
    protected bool isAttack;
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
        GameManager.Game.killCount++;
        GameManager.Game.score += increaseScore;
        dropItem();
        Destroy(transform.gameObject);
    }
}
