using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : PlayerState
{
    public PlayerBlock(Player player) : base(player)
    {
        
    }

    float blockDuration;
    float parryAbleTime = 0.2f;
    float blockStart = 0.2f;
    float blockEnd = 1.5f;

    public bool IsBlockAble
    {
        get => isBlockAble;
    }
    private bool isBlockAble = false;

    public void TriggerAction()
    {
        player.PlaySound(PLAYER_SOUND_MODEL.BLOCK);
        player.animatorController.SetTriggerAnimation("BlockDamage");
        player.rb.AddForce(player.transform.forward * -200.0f);
    }

    public override void ExitAction()
    {
        blockDuration = 0.0f;
        player.PlayerState = PLAYER_STATE.IDLE;
        isBlockAble = false;
    }

    public override void InputAction()
    {
        player.PlaySound(PLAYER_SOUND_MODEL.BLOCK_IN);
        blockDuration = 0.0f;
        isBlockAble = false;
    }

    public override void UpdateAction()
    {
        blockDuration += Time.deltaTime;
        player.animatorController.SetBlockAnimation(blockDuration);
       
        isBlockAble = (blockDuration >= blockStart && blockDuration <= blockEnd);

        if(blockDuration >= parryAbleTime && Input.GetMouseButtonDown(0))
        {
            isBlockAble = false;
            player.PlayerState = PLAYER_STATE.PARRY;
        }

        if(blockDuration >= 2.0f)
        {
            ExitAction();
        }
    }

    public override bool StateCheck()
    {
        return false;
    }

}
