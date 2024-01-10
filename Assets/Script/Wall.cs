using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject[] destroyEffects;

    private void Start()
    {
        GameManager.inctance.wall = this;
    }

    public void DestroyWall(bool isEnemyDie)
    {
        if(isEnemyDie)
        {
            StartCoroutine(SetActiveFalseCo());
            return;
        }
        gameObject.SetActive(true);
    }

    IEnumerator SetActiveFalseCo()
    {
        foreach(GameObject effect in destroyEffects)
        {
            effect.SetActive(true);
        }
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}
