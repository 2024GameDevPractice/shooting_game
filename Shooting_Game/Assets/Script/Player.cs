using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int skillCount;
    public int hp;
    private float moveSpeed;
    public int damage;
    private float attackSpeed;
    public float fuel;
    private Rigidbody2D rigid;
    public Skill1 skill1;
    public Skill2 skill2;
    private Animator anime;
    private float fielldtime;
    private bool beAttack;
    private TMP_Text[] texts;
    private TMP_Text text1;
    private TMP_Text text2;
    private TMP_Text text3;
    private Slider[] sliders;
    private Slider slider1;
    private Slider slider2;
    private void Start()
    {
        init();
        StartCoroutine(attack());
        StartCoroutine(useFuel());
        transform.position = new Vector3(0, -4);
    }
    private void init()
    {
        texts = FindObjectsOfType<TMP_Text>();
        sliders = FindObjectsOfType<Slider>();
        foreach (TMP_Text text in texts)
        {
            switch(text.gameObject.name)
            {
                case "HP":
                    text2 = text;
                    break;
                case "LOG":
                    text1 = text;
                    text.text = "";
                    break;
                case "Score":
                    text3 = text;
                    break;
            }
        }
        foreach(Slider slider in sliders)
        {
            switch(slider.gameObject.name)
            {
                case "HP":
                    slider1 = slider;
                    break;
                case "Slider":
                    slider2 = slider;
                    break;
            }
        }
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
        attacked(0);
        text3.text = "Score : " + GameManager.Game.score;
        if (fuel <= 0) { return; }
        if (hp <= 0) { return; }
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h != 0)
        {
            if(h == 1 && !(transform.position.x > 8f))
            {
                transform.position += new Vector3(h * moveSpeed, 0, 0) * Time.deltaTime;
            }
            else if(h == -1 && !(transform.position.x < -8f))
            {
                transform.position += new Vector3(h * moveSpeed, 0, 0) * Time.deltaTime;
            }
        }
        if (v != 0)
        {
            if (v == 1 && !(transform.position.y > 7f))
            {
                transform.position += new Vector3(0, v * moveSpeed, 0) * Time.deltaTime;
            }
            else if (v == -1 && !(transform.position.y < -7f))
            {
                transform.position += new Vector3(0, v * moveSpeed, 0) * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            beAttack = true;
        }
        else
        {
            beAttack = false;
        }
        if(GameManager.Game.stopSpawn)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (skillCount > 0)
                {
                    if (!skill1.inCooltime)
                    {
                        skillCount--;
                        skill1.StartCoroutine(skill1.useSkill());
                    }
                    else
                    {
                        text1.text = "스킬 재사용 대기 중 입니다.";
                        text1.gameObject.SetActive(true);
                        Invoke("invisible", 1f);
                    }
                }
                else
                {
                    text1.text = "스킬 사용 가능 횟수를 다 사용했습니다.";
                    text1.gameObject.SetActive(true);
                    Invoke("invisible", 1f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (skillCount > 0)
                {
                    if (!skill2.inCooltime)
                    {
                        skillCount--;
                        skill2.StartCoroutine(skill2.useSkill());
                    }
                    else
                    {
                        text1.text = "스킬 재사용 대기 중 입니다.";
                        text1.gameObject.SetActive(true);
                        Invoke("invisible", 1f);
                    }
                }
                else
                {
                    text1.text = "스킬 사용 가능 횟수를 다 사용했습니다.";
                    text1.gameObject.SetActive(true);
                    Invoke("invisible", 1f);
                }
            }
        }
    }
    private void invisible()
    {
        text1.gameObject.SetActive(false);
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
            slider2.value = fuel / 100;
            yield return new WaitForSeconds(0.8f);
        }
    }
    public void attacked(int damage)
    {
        if(!GameManager.Game.invincibility)
        {
            hp -= damage;
            slider1.value = (float)hp / 100;
            if (hp <= 0)
            {
                anime.Play("death");
                Invoke("death", 0.75f);
            }
        }
    }
    private void death()
    {
        GameManager.Game.result.active();
        Destroy(transform.gameObject);
        GameManager.Game.gameEnd();
    }
}
