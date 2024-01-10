using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerState
{
    public PlayerMove(Player player) : base(player)
    {
        status = player.status;
        rb = player.rb;
    }
    private Status status;
    private Rigidbody rb;

    float maxSpeed = 5.0f;

    public override void UpdateAction()
    {
        PositionMove();
        if(!player.IsLockOn)
        {
           RotateMove();
        }
    }

    public void PositionMove()
    {
        Vector3 curVec = Vector3.zero;
        float velY = rb.velocity.y;
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            curVec += Input.GetAxis("Vertical") * player.transform.forward;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            curVec += Input.GetAxis("Horizontal") * player.transform.right;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = Vector3.zero;
        }
        
        curVec = curVec.normalized * status.MoveSpeed * Time.deltaTime;
        curVec.y = velY;
        rb.velocity = curVec;

        if (rb.velocity.magnitude >= maxSpeed)
        {
            curVec = curVec.normalized * maxSpeed;
            curVec.y = velY;
            rb.velocity = curVec;
        }

        player.animatorController.SetMoveAnimation();
    }

    public void RotateMove()
    {
        float rotX = Input.GetAxis("Mouse X");
        player.transform.Rotate(rotX * Vector3.up * 500 * Time.deltaTime);
    }

    public override void InputAction()
    {
        if(player.audioSource.clip == null)
        {
            player.PlaySound(PLAYER_SOUND_MODEL.MOVE, true);
        }
    }

    public override void ExitAction()
    {
        return;
    }

    public override bool StateCheck()
    {
        return player.PlayerState == PLAYER_STATE.MOVE;
    }
}
