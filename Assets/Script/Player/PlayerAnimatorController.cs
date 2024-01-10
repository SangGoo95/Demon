using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public Animator playerAnimator;

    public void SetAnimatorState(int state)
    {
        playerAnimator.SetInteger("State", state);
    }

    public void SetMoveAnimation()
    {
        playerAnimator.SetFloat("MoveX", Input.GetAxis("Horizontal"));
        playerAnimator.SetFloat("MoveY", Input.GetAxis("Vertical"));
    }

    public void SetEvationAnimation(float evaX, float evaY)
    {
        evaX = evaX == 0 ? 0 : evaX > 0 ? 1 : -1;
        evaY = evaY == 0 ? 0 : evaY > 0 ? 1 : -1;

        playerAnimator.SetFloat("EvationX", evaX);
        playerAnimator.SetFloat("EvationY", evaY);
    }

    public void SetAttackAnimation(int count)
    {
        playerAnimator.SetInteger("AttackCount", count);
    }

    public void SetBlockAnimation(float time)
    {
        playerAnimator.SetFloat("BlockDuration", time);
    }

    public void SetTriggerAnimation(string trigger)
    {
        playerAnimator.SetTrigger(trigger);
    }


    public void SetDownAnimation(float time)
    {
        playerAnimator.SetFloat("DownDuration", time);
    }

    public void SetBounceAnimation(float time)
    {
        playerAnimator.SetFloat("BounceDuration", time);
    }
}
