using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss2 : BossMonsterController
{
    private string bulletName2;
    private int x;
    protected override void Start()
    {
        base.Start();
        bulletName1 = "Projectileb";
        bulletName2 = "Projectilea";
        moveSpeed = 0.675f;
        attackSpeed = 1.375f;
        fielldtime = 0;
        hp = 215;
        damage = 18;
        increaseScore = 215 * (int)GameManager.Game.stage;
        isAttack = true;
        x = Random.Range(0, 2);
        if (x == 0) { x = -1; }
        anime.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load($"Animation/Boss/Boss1/Boss1");
        transform.position = new Vector3(0 + x * 0.001f, 11, 0);
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
            int rand = Random.Range(0, 4);
            switch (rand)
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
        while (true)
        {
            if(GameManager.Game.player.hp <= 0)
            {
                yield return null;
            }
            else
            {
                GameObject go = (GameObject)Instantiate(Resources.Load($"Prefab/{bulletName1}"), transform.position, Quaternion.identity);
                //go.
            }
        }
    }
}
