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
        moveSpeed = 2.45f;
        vec = (GameManager.Game.player.transform.position - transform.position).normalized;
    }
    private void Upate()
    {

    }
}
