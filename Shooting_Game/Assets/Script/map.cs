using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class map : MonoBehaviour
{
    private Renderer render;
    private float offset;
    private int rand;
    void Start()
    {
        render = GetComponent<Renderer>();
        rand = Random.Range(0, 1);
        if(rand == 0) { rand = -1; }
    }
    void Update()
    {
        offset += Time.deltaTime / 2;
        render.material.mainTextureOffset =  new Vector2(rand * offset / 6.5f,offset);
    }
}
