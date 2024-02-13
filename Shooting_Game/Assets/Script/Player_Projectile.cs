using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
public class Player_Projectile : MonoBehaviour
{
    private float speed;
    private BoxCollider2D col;
    private Rigidbody2D rigid;
    private void Start()
    {
        transform.position = GameManager.Game.player.gameObject.transform.position + new Vector3(0,0.75f,0);
        col = transform.gameObject.AddComponent<BoxCollider2D>();
        col.size = new Vector2(0.3333f, 0.45f);
        rigid = transform.gameObject.AddComponent<Rigidbody2D>();
        rigid.gravityScale = 0f;
        speed = 5.75f;
    }
    private void Update()
    {
        transform.position += new Vector3(0,1,0) * speed * Time.deltaTime;
        if(transform.position.y > 9)
        {
            Destroy(transform.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "a(Clone)")
        {
            monster_a mon = collision.gameObject.GetComponent<monster_a>();
            mon.attacked(GameManager.Game.player.damage);
        }
        else if(collision.gameObject.name == "b(Clone)")
        {
            monster_b mon = collision.gameObject.GetComponent<monster_b>();
            mon.attacked(GameManager.Game.player.damage);
        }
        else if(collision.gameObject.name == "c(Clone)")
        {
            monster_c mon = collision.gameObject.GetComponent<monster_c>();
            mon.attacked(GameManager.Game.player.damage);
        }
        else if(collision.gameObject.name == "meteor(Clone)")
        {
            meteor met = collision.gameObject.GetComponent<meteor>();
            met.hitCount--;
        }
        Destroy(transform.gameObject);
    }
}
