using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Animations;
using UnityEngine;
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
        StartCoroutine(Attack());
    }
    protected virtual void Update() { }
    protected virtual IEnumerator Attack() 
    {
        while(active)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load($"Prefab/Projectile{type}"), transform.position, new Quaternion(0,0,0,0));
            Projectile projectiled = go.AddComponent<Projectile>();
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
    }
}
