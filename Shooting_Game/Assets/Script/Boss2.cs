using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss2 : BossMonsterController
{
    private string bulletName2;
    private float x;
    private int y;
    protected override void Start()
    {
        bulletName1 = "Projectileb";
        bulletName2 = "Projectilea";
        moveSpeed = 1.25f;
        attackSpeed = 0.8f;
        fielldtime = 0;
        y = 1;
        hp = 215;
        damage = 14;
        increaseScore = 215 * (int)GameManager.Game.stage;
        isAttack = true;
        x = Random.Range(0, 2);
        if (x == 0) { x = -1; }
        transform.position = new Vector3(0 + x * 0.001f, 11, 0);
        base.Start();
        anime.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animation/Boss/Boss2/Boss2");
        StartCoroutine(attack());
    }
    protected override IEnumerator setState()
    {
        int cooltime = 6; while (true)
        {        
            if (hp < 125)
            {
                cooltime = 3;
            }
            yield return new WaitForSeconds(cooltime);
            int rand = Random.Range(0, 2);
            switch (rand)
            {
                case 0:
                    attackSpeed = 0.8f;
                    isAttack = true;
                    break;
                case 1:
                    attackSpeed = 0.4f;
                    isAttack = false;
                    break;
            }
        }
    }
    protected IEnumerator attack()
    {
        while(true)
        {
            fielldtime += Time.deltaTime;
            if (fielldtime >= 2.05f)
            {
                x = Random.Range(0, 2);
                y = Random.Range(0, 2);
                fielldtime = 0;
                if (x == 0) { x = -1; }
                if (y == 0) { y = -1; }
                GameObject go;
                go = (GameObject)Instantiate(Resources.Load($"Prefab/{bulletName2}"), transform.position - new Vector3(0.25f, 0, 0), Quaternion.identity);
                go.AddComponent<Projectile4>().setDamage(damage);
                go = (GameObject)Instantiate(Resources.Load($"Prefab/{bulletName2}"), transform.position - new Vector3(-0.25f, 0, 0), Quaternion.identity);
                go.AddComponent<Projectile4>().setDamage(damage);
            }
            yield return null;
        }
    }
    protected override void Update()
    {
        if(transform.position.x < -4)
        {
            x = -(x + Random.Range(0.1f, 0.3f));
            fielldtime = 0;
        }
        else if(transform.position.x > 4)
        {
            x = -(x + Random.Range(0.1f, 0.3f));
            fielldtime = 0;
        }
        transform.position += Vector3.right * x * moveSpeed * Time.deltaTime;
        if(transform.position.y < -1)
        {
            y = -1;
            fielldtime = 0;
        }
        else if (transform.position.y > 7)
        {
            y = 1;
            fielldtime = 0;
        }
        transform.position += Vector3.down * y * moveSpeed * Time.deltaTime;
    }
    protected override IEnumerator defaultAttack()
    {
        while (true)
        {
            if (GameManager.Game.player.hp <= 0)
            {
                yield return null;
            }
            else
            {
                if (isAttack)
                {
                    GameObject go = (GameObject)Instantiate(Resources.Load($"Prefab/{bulletName1}"), transform.position, Quaternion.identity);
                    go.AddComponent<Projectile3>().setDamage(damage);
                    yield return new WaitForSeconds(attackSpeed);
                }
                else
                {
                    GameObject go = (GameObject)Instantiate(Resources.Load($"Prefab/{bulletName1}"), transform.position, Quaternion.identity);
                    go.AddComponent<Projectile1>().setDamage(damage);
                    yield return new WaitForSeconds(attackSpeed);
                }
            }
        }
    }
}
