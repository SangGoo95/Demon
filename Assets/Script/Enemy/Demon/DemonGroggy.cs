using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonGroggy : DemonState
{
    public DemonGroggy(Demon demon) : base(demon)
    {
    }

    float groggyDuration = 0.0f;

    public override void ExitAction()
    {
        groggyDuration = 0.0f;
    }

    public override void InputAction()
    {
        demon.animatorController.SetTriggerAnimation("Groggy");
        demon.effectModel.parryEffect.SetActive(true);
    }

    public override void UpdateAction()
    {
        groggyDuration += Time.deltaTime;
        if(groggyDuration >= 2.0f)
        {
            demon.ChangeState(ENEMY_STATE.ROAR);
        }
    }

    public override bool StateCheck()
    {
        return false;
    }

}
