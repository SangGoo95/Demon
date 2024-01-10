using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(Player player) : base(player)
    {
        rb = player.rb;
    }

    private Rigidbody rb;
    float gravity = -3.0f;

    public override void ExitAction()
    {
        rb.velocity = Vector3.zero;
    }

    public override void InputAction()
    {
        player.StopSound();
        Vector3 vec = Vector3.zero;
        vec.y = gravity;
        rb.velocity = vec;
    }

    public override void UpdateAction()
    {
        player.stateDic[PLAYER_STATE.MOVE].UpdateAction();
    }

    public override bool StateCheck()
    {
        return true;
    }

}
