using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile3 : MonoBehaviour
{
    public int damage;
    private float moveSpeed;
    private float size;
    private Vector3 vec;
    private void Start()
    {
        moveSpeed = 2.95f;
        size = 0.2f;
        vec = (GameManager.Game.player.transform.position - transform.position).normalized;
    }
    public void setDamage(int Damage)
    {
        damage = Damage;
    }
    private void Update()
    {
        transform.position += vec * moveSpeed * Time.deltaTime;
        size += Time.deltaTime / 2; 
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(size * 0.24f, size * 0.24f), default);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.name == "Player(Clone)")
            {
                GameManager.Game.player.attacked(damage);
                Destroy(transform.gameObject);
            }
        }
        transform.localScale = new Vector3(size, size);
        if(transform.position.y < -9)
        {
            Destroy(transform.gameObject);
        }
    }
}
