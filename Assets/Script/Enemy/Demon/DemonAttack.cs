using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class SwipeAttack : DemonAttackStrategy
{
    public SwipeAttack(Demon demon) : base (demon)
    {
        delay = 0.6f;
        postDelay = 2.5f;
        attackTpye = ENEMY_ATTACKTPYE.STIFF;
        actionDel += () => demon.animatorController.SetTriggerAnimation("Swipe");
        actionDel += () => demon.effectModel.swipeAttack.SetActive(true);
    }
}

public class SwipeUpAttack : DemonAttackStrategy
{
    public SwipeUpAttack(Demon demon) : base(demon)
    {
        delay = 0.8f;
        postDelay = 2.8f;
        attackTpye = ENEMY_ATTACKTPYE.DOWN;
        actionDel += () => demon.animatorController.SetTriggerAnimation("SwipeUp");
        actionDel += () => demon.effectModel.swipeUpAttack.SetActive(true);
    }
}

public class SwipeCombo : DemonAttackStrategy
{
    public SwipeCombo(Demon onwer) : base(onwer)
    {
        delay = 1.0f;
        postDelay = 3.5f;
        attackTpye = ENEMY_ATTACKTPYE.BOUNCE;
        actionDel += () => demon.animatorController.SetTriggerAnimation("SwipeCombo");
        actionDel += () => demon.effectModel.swipeComboAttack.SetActive(true);
    }
}

public class Smash : DemonAttackStrategy
{
    public Smash(Demon onwer) : base(onwer)
    {
        delay = 2.0f;
        postDelay = 4.5f;
        attackTpye = ENEMY_ATTACKTPYE.PARRYABLE;
        actionDel += () => demon.animatorController.SetTriggerAnimation("Smash");
        actionDel += () => demon.effectModel.smashAttack.SetActive(true);
    }
}

public class JumpAttack : DemonAttackStrategy
{
    public JumpAttack(Demon onwer) : base(onwer)
    {
        delay = 1.5f;
        postDelay = 3.5f;
        attackTpye = ENEMY_ATTACKTPYE.BOUNCE;
        actionDel += () => demon.animatorController.SetTriggerAnimation("JumpAttack");
        actionDel += () => demon.effectModel.jumpAttack.SetActive(true);
    }
}

public enum DEMON_ATTACK_ORDER
{
    SWIPE = 0,
    SWIPEUP,
    SWIPECOMBO,
    SMASH,
    JUMPATTACK
}

public class DemonAttack : DemonState
{
    public DemonAttack(Demon demon) : base(demon)
    {
        attackStartegyDic.Add(DEMON_ATTACK_ORDER.SWIPE, new SwipeAttack(demon));
        attackStartegyDic.Add(DEMON_ATTACK_ORDER.SWIPEUP, new SwipeUpAttack(demon));
        attackStartegyDic.Add(DEMON_ATTACK_ORDER.SWIPECOMBO, new SwipeCombo(demon));
        attackStartegyDic.Add(DEMON_ATTACK_ORDER.SMASH, new Smash(demon));
        attackStartegyDic.Add(DEMON_ATTACK_ORDER.JUMPATTACK, new JumpAttack(demon));
    }

    public DemonAttackStrategy attackStrategy;
    public Dictionary<DEMON_ATTACK_ORDER, DemonAttackStrategy> attackStartegyDic = new Dictionary<DEMON_ATTACK_ORDER, DemonAttackStrategy>();
    private DEMON_ATTACK_ORDER attackOder;

    private float delay = 0.0f;
    private bool isAction = false;

    public override void ExitAction()
    {
        delay = 0.0f;
        isAction = false;
        demon.IsParryAble = false;
        demon.demonMat.color = Color.white;
    }

    public override void InputAction()
    {
        attackOder = (DEMON_ATTACK_ORDER)Random.Range(0, demon.level);
        attackStrategy = attackStartegyDic[attackOder];

        if(attackOder == DEMON_ATTACK_ORDER.SMASH)
        {
            demon.demonMat.color = Color.blue;
            demon.effectModel.parryAbleEffect.SetActive(true);
            demon.IsParryAble = true;
        }
    }

    public override bool StateCheck()
    {
        return false;
    }

    public override void UpdateAction()
    {
        delay += Time.deltaTime;
        if(delay >= attackStrategy.Delay && !isAction)
        {
            demon.demonMat.color = Color.white;
            attackStrategy.Action();
            isAction = true;
            demon.IsParryAble = false;
        }
        
        if(delay >= attackStrategy.PostDelay)
        {
            demon.ChangeState(ENEMY_STATE.CHASE);
        }
    }
}
