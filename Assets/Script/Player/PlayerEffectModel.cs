using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectModel : MonoBehaviour
{
    public GameObject attackEffect;
    public GameObject blockEffect;

    private void Start()
    {
        attackEffect = Instantiate(attackEffect, transform);
        blockEffect = Instantiate(blockEffect, transform);
    }
}
