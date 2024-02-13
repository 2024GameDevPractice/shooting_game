using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item2 : Item
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player(Clone)") { return; }
        GameManager.Game.invincibility = true;
        GameManager.Game.StartCoroutine(GameManager.Game.isInvincibility());
        base.OnCollisionEnter2D(collision);
    }
}
