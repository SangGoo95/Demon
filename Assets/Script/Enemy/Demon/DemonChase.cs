using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonChase : DemonState
{
    public DemonChase(Demon demon) : base(demon)
    {

    }

    Collider[] targetCols;
    [SerializeField]
    float chaseRadius = 27.0f;
    float chaseSpeed = 400.0f;

    public override void ExitAction()
    {
        demon.animatorController.SetTriggerAnimation("ChaseEnd");
    }

    public override void InputAction()
    {
        targetCols = null;
        demon.PlaySound(DEMON_SOUND_MODEL.CHASE);
        demon.animatorController.SetTriggerAnimation("ChaseStart");
    }

    public override bool StateCheck()
    {
        return false;
    }

    public override void UpdateAction()
    {
        targetCols = Physics.OverlapSphere(demon.transform.position, chaseRadius, 1 << 7);
        if (targetCols.Length > 0)
        {
            Vector3 targetVec = targetCols[0].transform.position;
            float distance = Vector3.Distance(demon.transform.position, targetVec);

            targetVec.y = demon.transform.position.y;
            demon.transform.LookAt(targetVec);

            if (distance >= 3.0f)
            {
                Vector3 move = (targetVec - demon.transform.position).normalized;
                move.y = 0;
                demon.rb.velocity = move * chaseSpeed * Time.deltaTime;
            }
            else
            {
                demon.ChangeState(ENEMY_STATE.ATTACK);
            }
        }
    }
}
