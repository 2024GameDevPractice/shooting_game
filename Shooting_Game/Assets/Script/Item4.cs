using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item4 : Item
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name != "Player(Clone)") { return; }
        if(GameManager.Game.player.fuel != 0)
        {
            GameManager.Game.player.fuel += 30;
        }
        base.OnCollisionEnter2D(collision);
    }
}
