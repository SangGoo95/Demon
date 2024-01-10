using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY_ATTACKTPYE
{
    STIFF = PLAYER_STATE.STIFF,
    DOWN = PLAYER_STATE.DOWN,
    BOUNCE = PLAYER_STATE.BOUNCE,
    PARRYABLE = PLAYER_STATE.BOUNCE
}

public enum ENEMY_STATE
{
    IDLE = 0,
    ROAR,
    CHASE,
    ATTACK,
    GROGGY,
    DIE,
}

//public class StateMachine
//{
//    public State curState;

//    public Dictionary<string, State> stateDic;

//    public void AddState(State state)
//    {
//        stateDic.Add(state.name, state);
//    }
//}

public class Enemy : MonoBehaviour
{
    public Rigidbody rb;
    public EnemyAnimatorController animatorController;
    public EnemyUIController uiController;
  //  protected StateMachine sm;


    protected Collider[] targetCols;
    [SerializeField]
    protected float chaseRadius;
    protected float damage;

    protected float hitDelay = 0.4f;

    protected float hp;
    public virtual float Hp
    {
        get => hp;
        set
        {
            hp = value;
        }
    }

    protected bool isHit = true;
    public bool IsHit
    {
        get => isHit;
        set
        {
            isHit = value;
            if (!isHit)
            {
                StartCoroutine(HitDelayCo(hitDelay));
            }
        }
    }

    protected bool isParryAble = false;
    public bool IsParryAble
    {
        get => isParryAble;
        set
        {
            isParryAble = value;
        }
    }

    [SerializeField] protected ENEMY_STATE enemyState;
    public ENEMY_STATE EnemyState
    {
        get => enemyState;
        set
        {
            if(currentState != null)
                currentState.ExitAction();
            enemyState = value;

            currentState = stateDic[enemyState];
            currentState.InputAction();
            animatorController.SetAnimatorState((int)EnemyState);
        }
    }

    [SerializeField] protected IState currentState;

    public Dictionary<ENEMY_STATE, IState> stateDic = new Dictionary<ENEMY_STATE, IState>();

    public void ChangeState(ENEMY_STATE changeState)
    {
       
        EnemyState = changeState;
    }
    //

    public void TakeDamage(int damage)
    {
        Hp -= damage;
    }

    IEnumerator HitDelayCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        IsHit = true;
    }
}
