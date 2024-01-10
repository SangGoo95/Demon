using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : PlayerState
{
    public PlayerDie(Player player) : base(player)
    {

    }

    public override void ExitAction()
    {
        
    }

    public override void InputAction()
    {
        player.animatorController.SetTriggerAnimation("Die");
        GameManager.inctance.PlayerDeath();
    }

    public override void UpdateAction()
    {
        
    }

    public override bool StateCheck()
    {
        return false;
    }

}
