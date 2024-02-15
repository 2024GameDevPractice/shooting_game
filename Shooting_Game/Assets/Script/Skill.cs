using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Skill : MonoBehaviour
{
    public float skillCooltime;
    public float fielldTime;
    protected float duration;
    public bool inCooltime;
    protected string prefabName;
    protected GameObject go;
    protected void Start()
    {
        inCooltime = false;
        init();
    }
    protected virtual void init() { }
    public virtual IEnumerator useSkill() { yield return null; }
    protected IEnumerator coolTime()
    {
        fielldTime = 0;
        while(true)
        {
            if(fielldTime < skillCooltime)
            {
                fielldTime += Time.deltaTime;
                yield return null;
            }
            else
            {
                inCooltime = false;
                yield break;
            }
        }
    }
}
