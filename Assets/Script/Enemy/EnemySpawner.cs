using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    private int stageCount;
    public List<Enemy> enemyList;

    private void Start()
    {
        stageCount = 0;

        foreach (Enemy enemy in enemyList)
        {
            enemy.gameObject.SetActive(false);
        }
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if(enemyList.Count <= stageCount)
        {
            GameManager.inctance.LoadScene("Clear");
            GameManager.inctance.IsClear = true;
            return;
        }
        enemyList[stageCount].gameObject.SetActive(true);
        GameManager.inctance.IsEnemyDie = false;
        stageCount++;
    }
}
