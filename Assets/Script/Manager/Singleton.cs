using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T inctance;

    protected void Awake()
    {
        if(inctance == null)
        {
            inctance = (T)this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
