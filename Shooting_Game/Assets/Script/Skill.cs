using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Skill : MonoBehaviour
{
    public int skillCooltimeInt;
    public float skillCooltimeFloat;
    public int whenToUseInt;
    public float whenToUseFloat;
    public bool inCooltime;
    protected GameObject go;
    protected void Start()
    {
        inCooltime = false;
    }
    public virtual void useSkill()
    {
        if(inCooltime)
        {
            return;
        }
        inCooltime = true;
    }
}
