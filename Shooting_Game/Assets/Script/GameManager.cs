using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager game;
    public static GameManager Game { get { init(); return game; } }
    public static void init()
    {
        GameObject go = GameObject.Find("GameManager");
        if(go == null)
        {
            go = new GameObject { name = "GameManager" };
            go.AddComponent<GameManager>();
            game = go.GetComponent<GameManager>();
            DontDestroyOnLoad(go);
        }
    }
    public enum stages
    {
        Stage1, Stage2
    }
    public enum monsterTypes
    {
        a, b, c
    }
    public string f;
    public string s;
    public string t;
    public int fs = 0;
    public int ss = 0;
    public int ts = 0;
    public List<string> board = new List<string>();
    public Dictionary<string, int> scores = new Dictionary<string, int>();
    public int score;
    public int stageLevel;
    public int damageLevel;
    public int killCount;
    public int monsterCount;
    public bool invincibility;
    public bool spawnBoss;
    public bool stopSpawn = false;
    private float spawnDelay;
    public Player player;
    public Stopwatch stopWatch;
    public stages stage;
    public Result result;
    public ScoreBoard scoreboard;
    public void gameSet()
    {
        GameObject go = (GameObject)Instantiate(Resources.Load("Prefab/Player"), new Vector3(0, -3.5f, 0), new Quaternion(0,0,0,0));
        player = go.GetComponent<Player>();
        result = GameObject.FindObjectOfType<Result>();
        scoreboard = GameObject.FindObjectOfType<ScoreBoard>();
        score = 0;
        stageLevel = 1;
        damageLevel = 1;
        killCount = 0;
        spawnDelay = 2;
        monsterCount = 0;
        invincibility = false;
        stage = stages.Stage1;
        stopWatch = new();
        spawnBoss = false;
    }
    public void gameStart()
    {
        gameSet();
        stopWatch.Start();
        StartCoroutine(SpawnMonster());
    }
    public void gameEnd()
    {
        invincibility = true;
        stopWatch.Stop();
    }
    public void nextStage()
    {
        player.skillCount = 4;
    }
    public IEnumerator SpawnMonster()
    {
        int rand = 0;
        while(true)
        {
            if(!stopSpawn)
            {
                GameObject go;
                if (stopWatch.Elapsed.Minutes == 1 * ((int)stage + 1) && stopWatch.Elapsed.Seconds == 0 && !spawnBoss)
                {
                    spawnBoss = true;
                    string name = ((int)stage + 1).ToString();
                    stopWatch.Stop();
                    monsterCount++;
                    go = (GameObject)Instantiate(Resources.Load($"Prefab/Boss{name}"));
                    if (stage == stages.Stage1)
                    {
                        go.AddComponent<Boss1>();
                    }
                    else
                    {
                        go.AddComponent<Boss2>();
                    }
                    stopWatch.Stop();
                }
                if (monsterCount - killCount < 28 && !spawnBoss)
                {
                    rand = Random.Range(0, (int)stage + 3);
                    monsterCount++;
                    switch (rand)
                    {
                        case 0:
                        case 1:
                            go = (GameObject)Instantiate(Resources.Load($"Prefab/{(monsterTypes)0}"));
                            go.AddComponent<monster_a>();
                            break;
                        case 2:
                            go = (GameObject)Instantiate(Resources.Load($"Prefab/{(monsterTypes)1}"));
                            go.AddComponent<monster_b>();
                            break;
                        case 3:
                            go = (GameObject)Instantiate(Resources.Load($"Prefab/{(monsterTypes)2}"));
                            go.AddComponent<monster_c>();
                            break;
                    }
                }
                if (!spawnBoss)
                {
                    rand = Random.Range(0, 4);
                    if (rand == 0)
                    {
                        go = (GameObject)Instantiate(Resources.Load("Prefab/meteor"));
                        go.AddComponent<meteor>();
                    }
                }
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    public IEnumerator isInvincibility()
    {
        int sec = stopWatch.Elapsed.Seconds;
        while(invincibility)
        {
            if(sec + 5 == stopWatch.Elapsed.Seconds) { invincibility = false; }
            yield return null;
        }
    }
    public void Update()
    {
        if(Input.GetKey(KeyCode.F1))
        {
            Monster[] monsters = GameObject.FindObjectsOfType<Monster>();
            Projectile1[] projectiles1 = GameObject.FindObjectsOfType<Projectile1>();
            Projectile2[] projectiles2 = GameObject.FindObjectsOfType<Projectile2>();
            Projectile3[] projectiles3 = GameObject.FindObjectsOfType<Projectile3>();
            Projectile4[] projectiles4 = GameObject.FindObjectsOfType<Projectile4>();
            BossMonsterController boss = GameObject.FindObjectOfType<BossMonsterController>();
            foreach(Monster mon in monsters)
            {
                mon.attacked(99999);
            }
            foreach(Projectile1 pro in projectiles1)
            {
                Destroy(pro.gameObject);
            }
            foreach (Projectile2 pro in projectiles2)
            {
                Destroy(pro.gameObject);
            }
            foreach (Projectile3 pro in projectiles3)
            {
                Destroy(pro.gameObject);
            }
            foreach (Projectile4 pro in projectiles4)
            {
                Destroy(pro.gameObject);
            }
            if(boss != null)
            {
                boss.attacked(9999999);
            }
        }
        else if(Input.GetKey(KeyCode.F2))
        {
            for(int i = 0; i < 4 - damageLevel; i++)
            {
                player.damage += 3;
            }
        }
        else if(Input.GetKey(KeyCode.F3))
        {
            player.skillCount = 0;
            player.skill1.StopAllCoroutines();
            player.skill1.inCooltime = false;
            player.skill2.StopAllCoroutines();
            player.skill2.inCooltime = false;
        }
        else if(Input.GetKey(KeyCode.F4))
        {
            player.hp = 100;
        }
        else if(Input.GetKey(KeyCode.F5))
        {
            player.fuel = 100;
        }
        else if(Input.GetKeyDown(KeyCode.F6))
        {
            BossMonsterController boss = GameObject.FindObjectOfType<BossMonsterController>();
            if(boss!= null)
            {
                Destroy(boss.gameObject);
            }
            if(stage == stages.Stage2)
            {
                stopSpawn = true;
                result.active();
                gameEnd();
            }
            else { nextStage();stage = stages.Stage2; }
        }
    }
}
