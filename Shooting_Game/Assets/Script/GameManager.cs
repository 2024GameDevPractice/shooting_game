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
    public int score;
    public int stageLevel;
    public int damageLevel;
    public int killCount;
    public int monsterCount;
    public bool invincibility;
    public bool spawnBoss; 
    private float spawnDelay;
    public Player player;
    public Stopwatch stopWatch;
    public stages stage;
    public void gameSet()
    {
        GameObject go = (GameObject)Instantiate(Resources.Load("Prefab/Player"), new Vector3(0, -3.5f, 0), new Quaternion(0,0,0,0));
        player = go.GetComponent<Player>();
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
        stopWatch.Stop();
    }
    public void nextStage()
    {

    }
    public IEnumerator SpawnMonster()
    {
        int rand = 0;
        while(true)
        {
            GameObject go;
            if(stopWatch.Elapsed.Minutes == 1 && stopWatch.Elapsed.Seconds == 30 && !spawnBoss)
            {
                spawnBoss = true;
                go = (GameObject)Instantiate(Resources.Load($"Prefab/Boss{(int)stage + 1}"));
                go.AddComponent<Boss1>();
            }
            if (monsterCount - killCount < 15 && !spawnBoss)
            {
                rand = Random.Range(0, (int)stage + 4);
                monsterCount++;
                switch(rand)
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
            rand = Random.Range(0, 4);
            if (rand == 0)
            {
                go = (GameObject)Instantiate(Resources.Load("Prefab/meteor"));
                go.AddComponent<meteor>();
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

        }
        else if(Input.GetKey(KeyCode.F2))
        {

        }
        else if(Input.GetKey(KeyCode.F3))
        {

        }
        else if(Input.GetKey(KeyCode.F4))
        {

        }
        else if(Input.GetKey(KeyCode.F5))
        {

        }
        else if(Input.GetKey(KeyCode.F6))
        {

        }
    }
}
