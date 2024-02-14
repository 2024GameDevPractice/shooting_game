using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Boss1 : BossMonsterController
{
    private string bulletName2;
    private int x;
    protected override void Start()
    {
        base.Start();
        bulletName1 = "Projectileb";
        bulletName2 = "Projectilec";
        moveSpeed = 0.4f;
        attackSpeed = 1.55f;
        fielldtime = 0;
        hp = 255;
        damage = 15;
        increaseScore = 200 * (int)GameManager.Game.stage;
        isAttack = true;
        x = Random.Range(0,2);
        if(x == 0) { x = -1; }
        anime.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load($"Animation/Boss/Boss1/Boss1");
        transform.position = new Vector3(0 + x * 0.001f, 11, 0);
    }
    protected override void Update()
    {
        if (transform.position.y >= 0)
        {
            transform.position += new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
        }
        else
        {
            if(transform.position.x > 2)
            {
                x = -x;
            }
            else if(transform.position.x < -2)
            {
                x = -x;
            }
            transform.position += new Vector3(x * 1, 0, 0) * moveSpeed * Time.deltaTime;
        }
    }
    protected override IEnumerator setState()
    {
        int cooltime = 8;
        while(true)
        {
            if(hp < 125)
            {
                cooltime = 4;
            }
            yield return new WaitForSeconds(cooltime);
            int rand = Random.Range(0, 4);
            switch(rand)
            {
                case 0:
                    isAttack = true;
                    break;
                case 1:
                    isAttack = false;
                    break;
                case 2:
                    isAttack = false;
                    break;
            }
        }
    }
    protected override IEnumerator defaultAttack()
    {
        while(true)
        {
            if(GameManager.Game.player.hp <= 0)
            {
                yield return null;
            }
            else
            {
                if (!isAttack)
                {
                    yield return null;
                }
                else
                {
                    GameObject go = (GameObject)Instantiate(Resources.Load($"Prefab/{bulletName1}"), transform.position + new Vector3(-1.5f, -2.3f, 0), Quaternion.identity); ;
                    go.transform.localScale = new Vector3(2,2,1);
                    go.AddComponent<Projectile1>().setDamage(damage);
                    go = (GameObject)Instantiate(Resources.Load($"Prefab/{bulletName1}"), transform.position + new Vector3(1.5f, -2.3f, 0), Quaternion.identity); ;
                    go.transform.localScale = new Vector3(2,2,1);
                    go.AddComponent<Projectile1>().setDamage(damage);
                    go = (GameObject)Instantiate(Resources.Load($"Prefab/{bulletName2}"), transform.position + new Vector3(0, -2.45f, 0), Quaternion.identity);
                    go.transform.localScale = new Vector3(2,2,1);
                    go.AddComponent<Projectile2>().setDamage(damage);
                    yield return new WaitForSeconds(attackSpeed);
                }
            }
        }
    }
}
