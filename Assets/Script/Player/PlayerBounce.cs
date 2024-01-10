using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : PlayerState
{
    public PlayerBounce(Player player) : base(player)
    {

    }

    float bounceDuration = 0.0f;

    public override void ExitAction()
    {
        bounceDuration = 0.0f;
        player.PlayerState = PLAYER_STATE.IDLE;
    }

    public override void InputAction()
    {
        player.PlaySound(PLAYER_SOUND_MODEL.BOUNCE);
        player.animatorController.SetTriggerAnimation("Hit");
        player.rb.AddForce(player.transform.up * 150);
    }

    public override void UpdateAction()
    {
        bounceDuration += Time.deltaTime;
        player.animatorController.SetBounceAnimation(bounceDuration);

        if (bounceDuration >= 2.5f)
        {
            ExitAction();
        }
    }

    public override bool StateCheck()
    {
        return false;
    }
}
