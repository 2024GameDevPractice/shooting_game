using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
public abstract class Monster : MonoBehaviour
{
    protected float rand;
    protected int hp;
    protected float moveSpeed;
    protected int damage;
    protected float attackSpeed;
    protected virtual void Start()
    {
        StartCoroutine(Attack());
    }
    protected virtual void Update()
    {

    }
    protected virtual IEnumerator Attack() 
    {
        while(true)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load($"Prefab/Projectile"), transform.position, new Quaternion(0,0,0,0));
            Projectile projectiled = go.AddComponent<Projectile>();
            projectiled.setDamage(damage);
            yield return new WaitForSeconds(attackSpeed);
        }
    }
    protected void attacked(int attackdamage)
    {
        hp -= attackdamage;
        if(hp <= 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
