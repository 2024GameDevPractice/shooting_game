using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class monster_b : Monster
{
    protected override void Start()
    {
        type = "b";
        increaseScore = 20;
        attackSpeed = 4.5f;
        moveSpeed = 0.3f;
        damage = 12;
        hp = 25;
        rand = Random.Range(0, 2);
        if (rand == 0) { rand = -1; }
        transform.position = new Vector3(rand * 4, 9, 0);
        base.Start();
    }
    protected override void Update()
    {
        if(transform.position.y < 3 )
        {
            transform.position += new Vector3(moveSpeed * (-rand) * 2, moveSpeed * -2) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(moveSpeed * (-rand), moveSpeed * -2, 0) * Time.deltaTime;
        }
        if(Mathf.Abs(transform.position.x) > 5)
        {
            Destroy(transform.gameObject);
        }
    }
}

