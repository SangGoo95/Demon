using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDown : PlayerState
{
    public PlayerDown(Player player) : base(player)
    {

    }

    float downDuration = 0.0f;

    public override void ExitAction()
    {
        downDuration = 0.0f;
        player.PlayerState = PLAYER_STATE.IDLE;
    }

    public override void InputAction()
    {
        player.PlaySound(PLAYER_SOUND_MODEL.DOWN);
        player.animatorController.SetTriggerAnimation("Hit");
    }

    public override void UpdateAction()
    {
        downDuration += Time.deltaTime;
        player.animatorController.SetDownAnimation(downDuration);

        if (downDuration >= 2.0f)
        {
            ExitAction();
        }
    }

    public override bool StateCheck()
    {
        return false;
    }
}
