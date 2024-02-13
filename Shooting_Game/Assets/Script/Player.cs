using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
public class Player : MonoBehaviour
{
    public int hp;
    private float moveSpeed;
    public int damage;
    private float attackSpeed;
    public float fuel;
    private Rigidbody2D rigid;
    private Animator anime;
    public List<Skill> skills = new List<Skill>();
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
        skills.Add(transform.gameObject.AddComponent<Skill1>());
        skills.Add(transform.gameObject.AddComponent<Skill2>());
        rigid.gravityScale = 0;
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        hp = 100;
        fuel = 100;
        moveSpeed = 2f;
        damage = 5;
        attackSpeed = 0.55f;
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
        if(Input.GetKeyDown(KeyCode.Q))
        {
            skills[0].useSkill();
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            skills[1].useSkill();
        }
    }
    private IEnumerator attack()
    {
        while (true)
        {
            if (fuel > 0 && hp > 0)
            {
                if (beAttack)
                {
                    GameObject go = (GameObject)Instantiate(Resources.Load("Prefab/PlayerBullet"));
                    go.AddComponent<Player_Projectile>();
                }
            }
            yield return new WaitForSeconds(attackSpeed);
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
