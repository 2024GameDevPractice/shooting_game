using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
public class Player : MonoBehaviour
{
    protected int skillCount;
    public int hp;
    private float moveSpeed;
    public int damage;
    private float attackSpeed;
    public float fuel;
    private Rigidbody2D rigid;
    private Skill1 skill1;
    private Skill2 skill2;
    private Animator anime;
    private float fielldtime;
    private bool beAttack;
    private void Start()
    {
        init();
        StartCoroutine(attack());
        StartCoroutine(useFuel());
        transform.position = new Vector3(0, -4);
    }
    private void init()
    {
        rigid = transform.gameObject.GetComponent<Rigidbody2D>();
        anime = transform.gameObject.AddComponent<Animator>();
        anime.runtimeAnimatorController = (AnimatorController)Resources.Load("Animation/Player/Player");
        skill1 = transform.gameObject.AddComponent<Skill1>();
        skill2 = transform.gameObject.AddComponent<Skill2>();
        rigid.gravityScale = 0;
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        hp = 100;
        fuel = 100;
        moveSpeed = 2.65f;
        damage = 3;
        attackSpeed = 0.325f;
        fielldtime = 0;
        skillCount = 4;
    }
    private void Update()
    {
        if (fuel <= 0) { return; }
        if (hp <= 0) { return; }
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0 || v != 0) { transform.position += new Vector3(h * moveSpeed, v * moveSpeed, 0) * Time.deltaTime; }
        if (Input.GetKey(KeyCode.Space))
        {
            beAttack = true;
        }
        else
        {
            beAttack = false;
        }
        if(skillCount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!skill1.inCooltime)
                {
                    skillCount--;
                    skill1.StartCoroutine(skill1.useSkill());
                }
                //else
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (!skill2.inCooltime)
                {
                    skillCount--;
                    skill2.StartCoroutine(skill2.useSkill());
                }
                //else
            }
        }
    }
    private IEnumerator attack()
    {
        while (true)
        {
            fielldtime += Time.deltaTime;
            if (fuel > 0 && hp > 0 && fielldtime > attackSpeed)
            {
                if (beAttack)
                {
                    fielldtime = 0;
                    GameObject go = (GameObject)Instantiate(Resources.Load("Prefab/PlayerBullet"));
                    go.AddComponent<Player_Projectile>();
                }
            }
            yield return null;
        }
    }
    private IEnumerator useFuel()
    {
        while(true)
        {
            fuel--;
            yield return new WaitForSeconds(0.8f);
        }
    }
    public void attacked(int damage)
    {
        if(!GameManager.Game.invincibility)
        {
            hp -= damage;
            if (hp <= 0)
            {
                anime.Play("death");
                Invoke("death", 0.75f);
            }
        }
    }
    private void death()
    {
        Destroy(transform.gameObject);
        GameManager.Game.gameEnd();
    }
}
