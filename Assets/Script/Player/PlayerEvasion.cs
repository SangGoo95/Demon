using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerEvasion : PlayerState
{
    public PlayerEvasion(Player player) : base(player)
    {
        status = player.status;
        rb = player.rb;
    }
    private Status status;
    private Rigidbody rb;

    private float evasionTimer;
    private float evasionStayTime = 0.8f;

    public override void InputAction()
    {
        Vector3 evaVec = rb.velocity.normalized;
        evaVec.y = 0;

        player.PlaySound(PLAYER_SOUND_MODEL.EVASION);

        // 300 회피거리, 나중에 심볼화
        if (evaVec == Vector3.zero)
        {
            rb.AddForce((player.transform.forward * -600));// + (player.transform.up * 350));
            player.animatorController.SetEvationAnimation(0, 0);
        }
        else
        {
            rb.AddForce(evaVec * 300);
            player.animatorController.SetEvationAnimation(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

    }

    public override void ExitAction()
    {
        evasionTimer = 0.0f;
        player.PlayerState = PLAYER_STATE.IDLE;
    }

    public override void UpdateAction()
    {
        evasionTimer += Time.deltaTime;
        if(evasionTimer >= evasionStayTime)
        {
            ExitAction();
        }
    }

    public override bool StateCheck()
    {
        return false;
    }
}
