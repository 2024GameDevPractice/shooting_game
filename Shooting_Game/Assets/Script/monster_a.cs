using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class monster_a : Monster
{
    protected override void Start()
    {
        type = "a";
        increaseScore = 5;
        attackSpeed = 6.5f;
        moveSpeed = 0.85f;
        damage = 5;
        rand = Random.Range(0, 1);
        rand = (rand == 0 ? 1.5f : -1.5f);
        position = Random.Range(0,2);
        if(position == 0)
        {
            position = -1;
        }
        transform.position = new Vector3(position * 5.25f * rand, 3.1f + Random.Range(-2, 3), 0);
        hp = 15;
        base.Start();
    }
    protected override void Update()
    {
        transform.position += new Vector3(moveSpeed * (-rand), 0, 0) * Time.deltaTime;

        if(transform.position.x >= 4)
        {
            rand = 1;
        }
        else if (transform.position.x <= -4)
        {
            rand = -1;
        }
    }
}
