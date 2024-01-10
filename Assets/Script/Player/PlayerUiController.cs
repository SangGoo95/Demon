using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUiController : MonoBehaviour
{
    public Slider hpBar;
    public Image faceCamGlow;


    public void PlayerCamHitEvent()
    {
        faceCamGlow.color = Color.red;
        StartCoroutine(HitEventCo());
    }

    public void SetHpBar(float hp, float maxHp)
    {
        hpBar.value = (hp / maxHp);
    }

    IEnumerator HitEventCo()
    {
        yield return new WaitForSeconds(1.0f);
        faceCamGlow.color = Color.white;
    }
}
