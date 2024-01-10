using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAnimationEvent : MonoBehaviour
{
    // 0 = swipe
    // 1 = swipe
    // 2 = smash
    // 3 = Jump

    // swipe ÄÞº¸´Â °¢°¢ 0 , 1 µû·Îµû·Î ²°´Ù Ä×´Ù ÇßÀ½
    public Collider[] attackCols = new Collider[4];

    public void AttackStartEvent(int attackOder)
    {
        attackCols[attackOder].enabled = true;
    }

    public void AttackEndEvent(int attackOder)
    {
        attackCols[attackOder].enabled = false;
    }
}
