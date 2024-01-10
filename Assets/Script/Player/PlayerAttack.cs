using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ATTACK_ORDER
{
    FIRST = 0,
    SECOND,
    THIRD,
    MAX
}

public class FirstAttack : PlayerAttackStrategy
{
    public FirstAttack(Player player) : base(player)
    {
        damageMultiplier = 1.0f;
        actionDel += () => player.animatorController.SetAttackAnimation((int)ATTACK_ORDER.FIRST);
        actionDel += () => player.PlaySound(PLAYER_SOUND_MODEL.FIRST_ATTACK);
    }
}

public class SecondAttack : PlayerAttackStrategy
{
    public SecondAttack(Player player) : base(player)
    {
        damageMultiplier = 2.0f;
        actionDel += () => player.animatorController.SetAttackAnimation((int)ATTACK_ORDER.SECOND);
        actionDel += () => player.PlaySound(PLAYER_SOUND_MODEL.SECOND_ATTACK);
    }
}

public class ThirdAttack : PlayerAttackStrategy
{
    public ThirdAttack(Player player) : base(player)
    {
        damageMultiplier = 3.0f;
        actionDel += () => player.animatorController.SetAttackAnimation((int)ATTACK_ORDER.THIRD);
        actionDel += () => player.PlaySound(PLAYER_SOUND_MODEL.THIRD_ATTACk);
    }
}

public class PlayerAttack : PlayerState
{
    public PlayerAttack(Player player) : base(player)
    {
        status = player.status;
        rb = player.rb;
        attackStrategyDic.Add((int)ATTACK_ORDER.FIRST, new FirstAttack(player));
        attackStrategyDic.Add((int)ATTACK_ORDER.SECOND, new SecondAttack(player));
        attackStrategyDic.Add((int)ATTACK_ORDER.THIRD, new ThirdAttack(player));

        attackStrategy = attackStrategyDic[(int)ATTACK_ORDER.FIRST];
    }

    public PlayerAttackStrategy attackStrategy;
    
    private Status status;
    private Rigidbody rb;
    private Dictionary<int , PlayerAttackStrategy> attackStrategyDic = new Dictionary<int , PlayerAttackStrategy>();

    private int attackCount = 0;
    private float comboTimer = 0;

    private float comboAbleTime = 0.2f;
    private float comboStartTime = 0.65f;
    private float attackEndTime = 0.9f;

    public override void ExitAction()
    {
        attackCount = 0;
        comboTimer = 0.0f;
        attackStrategy = attackStrategyDic[(int)ATTACK_ORDER.FIRST];
        player.PlayerState = PLAYER_STATE.IDLE;
    }

    public override void InputAction()
    {
        attackStrategy.Action();
    }

    public override void UpdateAction()
    {
        comboTimer += Time.deltaTime;
        if (comboTimer <= attackEndTime)
        {
            if (comboTimer >= comboAbleTime && Input.GetMouseButtonDown(0))
            {
                attackStrategy.IsCombo = true;
            }

            if (comboTimer >= comboStartTime && attackStrategy.IsCombo)
            {
                SetStractegy();
                InputAction();
            }
        }
        else
        {
            ExitAction();
        }
    }

    public override bool StateCheck()
    {
        return false;
    }

    public void SetStractegy()
    {
        comboTimer = 0.0f;
        attackCount++;

        if(attackCount >= (int)ATTACK_ORDER.MAX)
        {
            attackCount = (int)ATTACK_ORDER.FIRST;
        }

        attackStrategy.IsCombo = false;
        attackStrategy = attackStrategyDic[attackCount];
    }
}

