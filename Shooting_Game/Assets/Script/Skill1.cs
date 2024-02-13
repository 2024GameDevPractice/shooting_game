using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Skill1 : Skill
{
    private SpriteRenderer render;
    public override void useSkill()
    {
        base.useSkill();
        go = (GameObject)Instantiate(Resources.Load("Prefab/Skill1"), transform.position, new Quaternion(0, 0, 0, 0));
        render = go.GetComponent<SpriteRenderer>();
        render.flipX = true;
        StartCoroutine(invisible());
    }
    private IEnumerator invisible()
    {
        int i = 255;
        while(i != 0)
        {
            i -= 2;
            render.color = new Color(render.color.r, render.color.g, render.color.b, i);
            if(i == 1)
            {
                Destroy(go);
                go = null;
            }
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
}
