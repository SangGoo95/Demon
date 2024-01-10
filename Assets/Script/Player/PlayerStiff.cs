using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStiff : PlayerState
{
    public PlayerStiff(Player player) : base(player)
    {

    }

    float hitDuration = 0.0f;

    public override void ExitAction()
    {
        hitDuration = 0.0f;
        player.PlayerState = PLAYER_STATE.IDLE;
    }

    public override void InputAction()
    {
        player.PlaySound(PLAYER_SOUND_MODEL.STIFF);
        player.animatorController.SetTriggerAnimation("Hit");
    }

    public override void UpdateAction()
    {
        hitDuration += Time.deltaTime;
        if(hitDuration >= 1.0f)
        {
            ExitAction();
        }
    }

    public override bool StateCheck()
    {
        return false;
    }

}
