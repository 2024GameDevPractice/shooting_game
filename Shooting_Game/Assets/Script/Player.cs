using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public int hp;
    private float moveSpeed;
    private int damage;
    private float attackSpeed;
    public float fuel;
    private Rigidbody2D rigid;
    private void Start()
    {
        init();
        StartCoroutine(attack());
        transform.position = new Vector3(0, -4);
    }
    private void init()
    {
        rigid = transform.gameObject.GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        hp = 100;
        fuel = 100;
        moveSpeed = 2f;
        damage = 2; // gameManager.damageLevel
        attackSpeed = 0.75f;
    }
    private void Update()
    {
        if(fuel <= 0) { return; }
        if(hp <= 0) { return; }
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0 || v != 0) { transform.position += new Vector3(h * moveSpeed, v * moveSpeed, 0) * Time.deltaTime; }
    }
    private IEnumerator attack()
    {
        while(true)
        {
            //instantiate();
            yield return new WaitForSeconds(attackSpeed);
        }
    }
    public void attacked(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            death();
        }
    }
    private void death()
    {
        //UI, socre;
    }
}
