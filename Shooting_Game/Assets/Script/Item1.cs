using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item1 : Item
{
    private int increaseDamageAmount = 3;
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player(Clone)") { return; }
        if (GameManager.Game.damageLevel != 4)
        {
            GameManager.Game.damageLevel++;
            GameManager.Game.player.damage += increaseDamageAmount;
        }
        else if(GameManager.Game.damageLevel == 4)
        {
            GameManager.Game.score += 15;
        }
        base.OnCollisionEnter2D(collision);
    }
}
