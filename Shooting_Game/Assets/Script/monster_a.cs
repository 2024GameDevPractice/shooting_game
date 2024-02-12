using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_a : Monster
{
    protected override void Start()
    {
        attackSpeed = 2.5f;
        moveSpeed = 0.85f;
        damage = 5;
        rand = Random.Range(0, 1);
        rand = (rand == 0 ? 1 : -1);
        transform.position = new Vector3(5.25f * rand, 3.1f + Random.Range(0, 3), 0);
        base.Start();
    }
    protected override void Update()
    {
        transform.position += new Vector3(moveSpeed * (-rand), 0, 0) * Time.deltaTime;
        if(rand == -1)
        {
            if(transform.position.x >= 4)
            {
                rand = 1;
            }
        }
        else
        {
            if(transform.position.x <= -4)
            {
                rand = -1;
            }
        }
    }
}
