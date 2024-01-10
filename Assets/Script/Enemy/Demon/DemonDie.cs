using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonDie : DemonState
{
    public DemonDie(Demon demon) : base(demon)
    {

    }

    float dieDuration = 0.0f;

    public override void ExitAction()
    {
        demon.gameObject.SetActive(false);
        demon.uiController.EnemyUISetActive(false);
        GameManager.inctance.IsBattle = false;
    }

    public override void InputAction()
    {
        GameManager.inctance.IsEnemyDie = true;
        dieDuration = 0.0f;
        demon.PlaySound(DEMON_SOUND_MODEL.DIE);
        demon.animatorController.SetTriggerAnimation("Die");
    }

    public override bool StateCheck()
    {
        return false;
    }

    public override void UpdateAction()
    {
        dieDuration += Time.deltaTime;
        if (dieDuration > 2.0f)
        {
            demon.effectModel.dieEffect.SetActive(true);
        }

        if (dieDuration >= 3.5f)
        {
            ExitAction();
        }
    }
}
