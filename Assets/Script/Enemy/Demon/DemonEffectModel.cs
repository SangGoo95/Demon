using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEffectModel : MonoBehaviour
{
    public GameObject parryAbleEffect;
    public GameObject swipeAttack;
    public GameObject swipeUpAttack;
    public GameObject swipeComboAttack;
    public GameObject smashAttack;
    public GameObject parryEffect;
    public GameObject roarEffect;
    public GameObject jumpAttack;
    public GameObject dieEffect;

    private void Start()
    {
        parryAbleEffect = Instantiate(parryAbleEffect, this.transform);
        swipeAttack = Instantiate(swipeAttack, this.transform);
        swipeUpAttack = Instantiate(swipeUpAttack, this.transform);
        swipeComboAttack = Instantiate(swipeComboAttack, this.transform);
        smashAttack = Instantiate(smashAttack, this.transform);
        parryEffect = Instantiate(parryEffect, this.transform);
        roarEffect = Instantiate(roarEffect, this.transform);
        jumpAttack = Instantiate(jumpAttack, this.transform);
        dieEffect = Instantiate(dieEffect, this.transform);
    }
}
