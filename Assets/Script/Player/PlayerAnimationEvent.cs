using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{

    public Collider playerSwordCollider;
    public Collider playerShieldCollider;
    public void AttackEventStart()
    {
        playerSwordCollider.enabled = true;
    }

    public void AttackEventEnd()
    {
        playerSwordCollider.enabled = false;
    }

    public void BlockEventStart()
    {
        playerShieldCollider.enabled = true;
    }

    public void BlockEventEnd()
    {
        playerShieldCollider.enabled = false;
    }
}
