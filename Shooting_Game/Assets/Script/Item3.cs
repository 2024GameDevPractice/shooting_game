using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item3 : Item
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player(Clone)") { return; }
        if (GameManager.Game.player.hp != 100)
        {
            if(GameManager.Game.player.hp > 85)
            {
                GameManager.Game.player.hp = 100;
            }
            else
            {
                GameManager.Game.player.hp += 15;
            }
        }
        base.OnCollisionEnter2D(collision);
    }
}
