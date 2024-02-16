using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class monster_c : Monster
{
    private int h;
    private int v;
    protected override void Start()
    {
        type = "c";
        increaseScore = 35;
        attackSpeed = 3.75f;
        moveSpeed = 0.6f;
        damage = 7;
        hp = 45;
        rand = Random.Range(0, 2);
        if (rand == 0) { rand = -1; }
        transform.position = new Vector3(rand * 4, 9, 0);
        h = 1;
        base.Start();
    }
    protected override void Update()
    {
        transform.position += new Vector3(0.5f * h, 1f * v, 0) * moveSpeed * Time.deltaTime;
        if (transform.position.y < -3)
        {
            v = 1;
        }
        else if(transform.position.y > 6.5f)
        {
            v = -1;
        }
        if (transform.position.x > 3.5f)
        {
            h = -1;
        }
        else if(transform.position.x < -3.5f)
        {
            h = 1;
        }
    }
}
