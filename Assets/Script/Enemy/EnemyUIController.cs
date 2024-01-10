using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    public Slider hpBar;

    void Start()
    {
        UIManager.inctance.enemyUi.gameObject.SetActive(false);
        hpBar = UIManager.inctance.enemyHpSlider;
    }

    public void EnemyUISetActive(bool isShow)
    {
        UIManager.inctance.enemyUi.gameObject.SetActive(isShow);
    }

    public void SetHpBar(float hp, float maxHp)
    {
        hpBar.value = (hp / maxHp);
    }
}
