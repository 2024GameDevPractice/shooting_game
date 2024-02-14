using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;
public abstract class Monster : MonoBehaviour
{
    protected float rand;
    protected int position;
    public int hp;
    protected BoxCollider2D box;
    protected float moveSpeed;
    protected int damage;
    protected float attackSpeed;
    protected Animator anime;
    protected bool active;
    protected int increaseScore;
    protected string type;
    protected virtual void Start()
    {
        box = transform.gameObject.AddComponent<BoxCollider2D>();
        anime = transform.gameObject.AddComponent<Animator>();
        active = true;
        hp += (int)GameManager.Game.stage * 5;
        damage += (int)GameManager.Game.stage * 1;
        StartCoroutine(Attack());
    }
    protected virtual void Update() { }
    protected virtual IEnumerator Attack() 
    {
        while(active)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load($"Prefab/Projectile{type}"), transform.position, new Quaternion(0,0,0,0));
            Projectile1 projectiled = go.AddComponent<Projectile1>();
            projectiled.setDamage(damage);
            yield return new WaitForSeconds(attackSpeed);
        }
    }
    public void attacked(int attackdamage)
    {
        hp -= attackdamage;
        if(hp <= 0)
        {
            anime.runtimeAnimatorController = (AnimatorController)Resources.Load("Animation/Enemy/Enemy");
            active = false;
            Invoke("Destroy", 0.35f);
        }
    }
    private void Destroy()
    {
        GameManager.Game.killCount++;
        GameManager.Game.score += increaseScore;
        Destroy(transform.gameObject);
        rand = Random.Range(0, 10);
        if(rand < 6)
        {
            dropItem(rand);
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Game.player.attacked((int)(damage / 2));
    }
    protected void dropItem(float rand)
    {
        GameObject go;
        switch(rand)
        {
            case 0:
                go = (GameObject)Instantiate(Resources.Load("Prefab/Item1"), transform.position, new Quaternion(0,0,0,0));
                go.AddComponent<Item1>();
                break;
            case 1:
                go = (GameObject)Instantiate(Resources.Load("Prefab/Item2"), transform.position, new Quaternion(0,0,0,0));
                go.AddComponent<Item2>();
                break;
            case 2:
                go = (GameObject)Instantiate(Resources.Load("Prefab/Item3"), transform.position, new Quaternion(0,0,0,0));
                go.AddComponent<Item3>();
                break;
            case 3:
            case 4:
            case 5:
                go = (GameObject)Instantiate(Resources.Load("Prefab/Item4"), transform.position, new Quaternion(0,0,0,0));
                go.AddComponent<Item4>();
                break;
        }
    }
}
