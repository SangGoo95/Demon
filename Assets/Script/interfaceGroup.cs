using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IState
{
    void InputAction();
    void UpdateAction();
    void ExitAction();
    bool StateCheck();
}

public abstract class PlayerState : IState
{
    protected Player player;
    public PlayerState(Player player)
    {
        this.player = player;
    }

    public abstract void ExitAction();
    public abstract void InputAction();
    public abstract bool StateCheck();
    public abstract void UpdateAction();
}

public abstract class DemonState : IState
{
    protected Demon demon;
    public DemonState(Demon demon)
    {
        this.demon = demon;
    }

    public abstract void ExitAction();
    public abstract void InputAction();
    public abstract bool StateCheck();
    public abstract void UpdateAction();
}

public interface IAttackStrategy
{
    void Action();
}

public abstract class PlayerAttackStrategy : IAttackStrategy
{
    protected Player player;

    public PlayerAttackStrategy(Player player)
    {
        this.player = player;
        isCombo = false;
    }

    protected delegate void ActionDel();
    protected ActionDel actionDel;

    public float DamageMultiplier
    {
        get => damageMultiplier;
    }
    protected float damageMultiplier;

    public bool IsCombo
    {
        get => isCombo;
        set
        {
            isCombo = value;
        }
    }
    protected bool isCombo;

    public void Action()
    {
        actionDel();
    }
}

public abstract class DemonAttackStrategy : IAttackStrategy
{
    protected Demon demon;

    public DemonAttackStrategy(Demon demon)
    {
        this.demon = demon;
    }

    protected delegate void ActionDel();
    protected ActionDel actionDel;

    public ENEMY_ATTACKTPYE AttackTpye
    {
        get => attackTpye;
    }
    protected ENEMY_ATTACKTPYE attackTpye;

    public float Delay
    { 
        get => delay;
    }
    protected float delay;

    public float PostDelay
    { 
        get => postDelay;
    }
    protected float postDelay;

    public void Action()
    {
        actionDel();
    }
}

public class interfaceGroup : MonoBehaviour
{
    
}
