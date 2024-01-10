using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonRoar : DemonState
{
    public DemonRoar(Demon demon) : base(demon)
    {

    }

    float roarDuration = 0.0f;

    public override void ExitAction()
    {
        roarDuration = 0.0f;
    }

    public override void InputAction()
    {
        demon.uiController.EnemyUISetActive(true);
        demon.effectModel.roarEffect.SetActive(true);
    }

    public override bool StateCheck()
    {
        return false;
    }

    public override void UpdateAction()
    {
        roarDuration += Time.deltaTime;

        demon.Hp += Time.deltaTime * demon.level * 2.5f;

        if(roarDuration >= 3.0f)
        {
            demon.ChangeState(ENEMY_STATE.CHASE);
        }
    }
}
