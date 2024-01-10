using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public void NextLevel()
    {
        EnemySpawner.inctance.SpawnEnemy();
    }

    public void ChangePlayerPosition(Player player)
    {
        player.transform.position = GameManager.inctance.playerStartPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out var player))
        {
            Fade.inctance.FadeIn();
            //ChangePlayerPosition(player);
            StartCoroutine(ChangePlayerPositionCo(player));
            NextLevel();
        }
    }

    IEnumerator ChangePlayerPositionCo(Player player)
    {
        yield return new WaitForSeconds(1.0f);
        player.transform.position = GameManager.inctance.playerStartPos;
    }
}
