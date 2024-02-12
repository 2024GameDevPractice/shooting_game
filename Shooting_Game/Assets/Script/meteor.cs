using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class meteor : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private int damage;
    private int hitCount;
    private float moveSpeed;
    void Start()
    {
        float rand = Random.Range(-3.5f, 3.5f);
        transform.position = new Vector3(rand, 9, 0);
        damage = 10;
        moveSpeed = 2.5f;
        hitCount = 3;
        boxCollider = transform.gameObject.AddComponent<BoxCollider2D>();
        rand = Random.Range(1, 3);
        transform.localScale = new Vector3(rand, rand);
    }
    void Update()
    {
        if(hitCount == 0)
        {
            Destroy(transform.gameObject);
        }
        transform.position += new Vector3(0, -moveSpeed, 0) * Time.deltaTime;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(boxCollider.size.x, boxCollider.size.y), default);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.name == "Player(Clone)")
            {
                GameManager.Game.player.attacked(damage);
                Destroy(transform.gameObject);
            }
        }
        if (transform.position.y <= -9)
        {
            Destroy(transform.gameObject);
        }
    }
}
