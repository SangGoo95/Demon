using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    public Animator enemyAnimator;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    public void SetAnimatorState(int state)
    {
        enemyAnimator.SetInteger("State", state);
    }

    public void SetTriggerAnimation(string trigger)
    {
        enemyAnimator.SetTrigger(trigger);
    }
}
