using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParry : PlayerState
{
    float parryDuration;

    public PlayerParry(Player player) : base(player)
    {

    }

    public override void ExitAction()
    {
        parryDuration = 0.0f;
        player.PlayerState = PLAYER_STATE.IDLE;
    }

    public override void InputAction()
    {
        player.PlaySound(PLAYER_SOUND_MODEL.PARRY);
    }

    public override void UpdateAction()
    {
        parryDuration += Time.deltaTime;
        if(parryDuration >= 1.0f)
        {
            ExitAction();
        }
    }

    public override bool StateCheck()
    {
        return false;
    }
}
