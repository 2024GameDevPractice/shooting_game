using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
public class Player_Projectile : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rigid;
    private void Start()
    {
        transform.position = GameManager.Game.player.gameObject.transform.position + new Vector3(0,0.75f,0);
        rigid = transform.gameObject.AddComponent<Rigidbody2D>();
        rigid.gravityScale = 0f;
        speed = 12f;
    }
    private void Update()
    {
        transform.position += new Vector3(0,1,0) * speed * Time.deltaTime; 
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.333f, 0.45f), default);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.name == "a(Clone)")
            {
                monster_a mon = col.gameObject.GetComponent<monster_a>();
                mon.attacked(GameManager.Game.player.damage);
                Destroy(transform.gameObject);
                break;
            }
            else if (col.gameObject.name == "b(Clone)")
            {
                monster_b mon = col.gameObject.GetComponent<monster_b>();
                mon.attacked(GameManager.Game.player.damage);
                Destroy(transform.gameObject);
                break;
            }
            else if (col.gameObject.name == "c(Clone)")
            {
                monster_c mon = col.gameObject.GetComponent<monster_c>();
                mon.attacked(GameManager.Game.player.damage);
                Destroy(transform.gameObject);
                break;
            }
            else if(col.gameObject.name == "Boss1(Clone)")
            {
                Boss1 boss = col.gameObject.GetComponent<Boss1>();
                boss.attacked(GameManager.Game.player.damage);
                Destroy(transform.gameObject);
                break;
            }
            else if(col.gameObject.name == "Boss2(Clone)")
            {
                Boss2 boss = col.gameObject.GetComponent<Boss2>();
                boss.attacked(GameManager.Game.player.damage);
                Destroy(transform.gameObject);
                break;
            }
            else if (col.gameObject.name == "meteor(Clone)")
            {
                meteor met = col.gameObject.GetComponent<meteor>();
                met.hitCount--;
                Destroy(transform.gameObject);
                break;
            }
        }
        if (transform.position.y > 9)
        {
            Destroy(transform.gameObject);
        }
    }
}
