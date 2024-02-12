using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_b : Monster
{
    protected override void Start()
    {
        base.Start();
        hp = 50;
        damage = 8;
        moveSpeed = 2;
        attackSpeed = 2.5f;
    }
}

