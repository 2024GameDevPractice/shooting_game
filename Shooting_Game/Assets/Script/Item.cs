using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Item : MonoBehaviour
{
    protected float speed;
    protected BoxCollider2D box;
    protected void Start()
    {
        box = transform.gameObject.AddComponent<BoxCollider2D>();
        speed = 4f;
    }
    protected void Update()
    {
        if (GameManager.Game.player.hp == 0 || GameManager.Game.player.fuel == 0) { return; }
        transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
        if(transform.position.y < -9) { Destroy(transform.gameObject); }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision) { Destroy(transform.gameObject); }
}
