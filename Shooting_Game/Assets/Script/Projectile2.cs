using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile2 : MonoBehaviour
{
    public int damage;
    private float moveSpeed;
    private float size = 0.41f;
    private Vector3 vec;
    private void Start()
    {
        moveSpeed = 1.85f;
        vec = (GameManager.Game.player.transform.position - transform.position).normalized;
    }
    private void Update()
    {
        transform.position += vec * moveSpeed * Time.deltaTime;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(size, size), default);
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
    public void setDamage(int Damage)
    {
        damage = Damage;
    }
}
