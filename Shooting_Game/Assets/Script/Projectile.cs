using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    public int damage;
    private float moveSpeed;
    private BoxCollider2D boxCollider;
    private void Start()
    {
        boxCollider = transform.gameObject.GetComponent<BoxCollider2D>();
        moveSpeed = 1.5f;
    }
    private void Update()
    {
        transform.position += new Vector3(0, -moveSpeed, 0) * Time.deltaTime;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(boxCollider.size.x, boxCollider.size.y), default);
        foreach(Collider2D col in colliders)
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
    public void setDamage(int Damage)
    {
        damage = Damage;
    }
}
