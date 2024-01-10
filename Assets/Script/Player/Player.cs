using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Status
{
    private float hp;
    private float maxHp;
    private int damage;

    private float atkDelay;
    private float moveSpeed;
    private float hitDelay;

    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
        }
    }

    public float MaxHp
    {
        get => maxHp;
    }

    public int Damage
    {
        get => damage;
    }
    public float AttackDelay
    {
        get => atkDelay;
    }
    public float MoveSpeed
    {
        get => moveSpeed;
    }
    public float HitDelay
    {
        get => hitDelay;
    }

    public Status(float maxHp, int damage, float atkDelay, float moveSpeed, float hitDelay)
    {
        this.maxHp = maxHp;
        this.hp = maxHp;
        this.damage = damage;
        this.atkDelay = atkDelay;
        this.moveSpeed = moveSpeed;
        this.hitDelay = hitDelay;
    }
}

public enum PLAYER_STATE
{
    IDLE = 0,
    MOVE,
    ATTACK,
    EVASION,
    BLOCK,
    PARRY,
    STIFF,
    DOWN,
    BOUNCE,
    DIE
}

public class Player : MonoBehaviour
{
    public Status status;
    public Rigidbody rb;
    public PlayerAnimatorController animatorController;
    public PlayerUiController uiController;
    public AudioSource audioSource;

    private PlayerEffectModel effectModel;
    private PlayerSoundModel soundModel;


    public bool IsLockOn
    {
        get => isLockOn;
        set
        {
            isLockOn = value;
            if(IsLockOn)
            {
                LockOn();
            }
            else
            {
                LockOff();
            }
        }
    }
    [SerializeField] private bool isLockOn = false;

    [SerializeField] private IState currentState;
    public Dictionary<PLAYER_STATE, IState> stateDic = new Dictionary<PLAYER_STATE, IState>();
    public PLAYER_STATE PlayerState
    {
        get => playerState;
        set
        {
            playerState = value;
            // currentState?.ExitAction();
            currentState = stateDic[playerState];
            currentState.InputAction();
            animatorController.SetAnimatorState((int)PlayerState);
        }
    }
    [SerializeField] PLAYER_STATE playerState;
    
    public Transform camTransform;

    public float Hp
    {
        get => status.Hp;
        set
        {
            status.Hp = value;
            uiController.SetHpBar(status.Hp, status.MaxHp);

            isHit = (PlayerState == PLAYER_STATE.STIFF) ||
                    (PlayerState == PLAYER_STATE.DOWN) ||
                    (PlayerState == PLAYER_STATE.BOUNCE);

            if(isHit)
            {
                uiController.PlayerCamHitEvent();
            }

            if(status.Hp <= 0)
            {
                StopCoroutine(hpRecoveryCo);
                status.Hp = 0;
                PlayerState = PLAYER_STATE.DIE;
            }
            if(status.Hp >= status.MaxHp)
            {
                status.Hp = status.MaxHp;
            }
        }
    }

    private Collider[] enemyCols = null;
    private Enemy lockOnTarget;

    private float lockOnDir = 20.0f;
    private float lockOnAngle = 37.0f;
    private float lookSpeed = 20.0f;

    private bool isHit;
    private float hpRecoveryValue = 0.5f;

    private IEnumerator hpRecoveryCo;

    private void Awake()
    {
        // 뒤에 상수 나중에 enum이나 const로 심볼릭화 할 것
        // maxHp, damage, atkDelay, moveSpeed, hitDelay
        status = new Status(100.0f, 20, 1.0f, 2000.0f, 0.5f);

        rb = GetComponent<Rigidbody>();
        animatorController = GetComponent<PlayerAnimatorController>();
        uiController = GetComponent<PlayerUiController>();
        effectModel = GetComponent<PlayerEffectModel>();
        soundModel = GetComponent<PlayerSoundModel>();
        audioSource = GetComponent<AudioSource>();

        stateDic.Add(PLAYER_STATE.IDLE, new PlayerIdle(this));
        stateDic.Add(PLAYER_STATE.MOVE, new PlayerMove(this));
        stateDic.Add(PLAYER_STATE.EVASION, new PlayerEvasion(this));
        stateDic.Add(PLAYER_STATE.ATTACK, new PlayerAttack(this));
        stateDic.Add(PLAYER_STATE.BLOCK, new PlayerBlock(this));
        stateDic.Add(PLAYER_STATE.PARRY, new PlayerParry(this));
        stateDic.Add(PLAYER_STATE.STIFF, new PlayerStiff(this));
        stateDic.Add(PLAYER_STATE.DOWN, new PlayerDown(this));
        stateDic.Add(PLAYER_STATE.BOUNCE, new PlayerBounce(this));
        stateDic.Add(PLAYER_STATE.DIE, new PlayerDie(this));

        PlayerState = PLAYER_STATE.IDLE;

        hpRecoveryCo = HpRecoveryCo();
        StartCoroutine(hpRecoveryCo);

        GameManager.inctance.player = this;
        GameManager.inctance.playerStartPos = transform.position;
        GameManager.inctance.IsBattle = false;
    }

    private void Update()
    {
        InputKeyBode();
        if (currentState != null)
        {
            currentState.UpdateAction();
        }
    }

    private void InputKeyBode()
    {
        if (currentState.StateCheck())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerState = PLAYER_STATE.EVASION;
                return;
            }

            if (InputMoveKey())
            {
                PlayerState = PLAYER_STATE.MOVE;
            }
            else
            {
                PlayerState = PLAYER_STATE.IDLE;
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlayerState = PLAYER_STATE.ATTACK;
                return;
            }

            if(Input.GetMouseButtonDown(1))
            {
                PlayerState = PLAYER_STATE.BLOCK;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            IsLockOn = !IsLockOn;
        }
    }

    private bool InputMoveKey()
    {
        return Input.GetKey(KeyCode.W) ||
               Input.GetKey(KeyCode.S) ||
               Input.GetKey(KeyCode.A) ||
               Input.GetKey(KeyCode.D);
    }

    private void LockOn()
    {
        enemyCols = Physics.OverlapSphere(transform.position, lockOnDir, 1<<6);
        if (enemyCols.Length > 0)
        {
            Vector3 targetVec = (enemyCols[0].transform.position - camTransform.position).normalized;
            Vector3 myVec = (transform.position - camTransform.position).normalized;
            lockOnTarget = enemyCols[0].GetComponent<Enemy>();

            targetVec.y = 0;
            myVec.y = 0;

            float dot = Vector3.Dot(targetVec, myVec);
            float theta = Mathf.Acos(dot);
            float angle = theta * Mathf.Rad2Deg;

            if(angle <= lockOnAngle)
                StartCoroutine(LookAtCo());
            else
                IsLockOn = false;
        }
        else
            IsLockOn = false;
    }

    private void LockOff()
    {
        StopCoroutine(LookAtCo());
    }

    private IEnumerator LookAtCo()
    {
        while (IsLockOn)
        {
            if (lockOnTarget.EnemyState == ENEMY_STATE.DIE)
            {
                IsLockOn = false;
                break;
            }

            Vector3 lookPos = new Vector3(enemyCols[0].transform.position.x,
                                          transform.position.y,
                                          enemyCols[0].transform.position.z);

            Vector3 pos = lookPos - transform.position;

            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(pos), Time.deltaTime * lookSpeed);

            yield return null;
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerState == PLAYER_STATE.ATTACK)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                if (enemy.IsHit && !(enemy.EnemyState == ENEMY_STATE.DIE))
                {
                    enemy.IsHit = false;
                    effectModel.attackEffect.SetActive(true);
                    int resultDamage = (int)(status.Damage * ((PlayerAttack)stateDic[PLAYER_STATE.ATTACK]).attackStrategy.DamageMultiplier);
                    enemy.TakeDamage(resultDamage);
                }
            }
        }

        if(PlayerState == PLAYER_STATE.PARRY)
        {
            if(other.TryGetComponent<Enemy>(out var enemy))
            {
                if(enemy.IsParryAble)
                {
                    enemy.ChangeState(ENEMY_STATE.GROGGY);
                }
            }
        }
    }
    
    private IEnumerator HpRecoveryCo()
    {
        while (true)
        {
            while (Hp < status.MaxHp)
            {
                Hp += Time.deltaTime * hpRecoveryValue;
                yield return null;
            }
            yield return null;
        }
    }

    public bool IsHitAble()
    {
        if(PlayerState == PLAYER_STATE.EVASION || PlayerState == PLAYER_STATE.DIE)
        {
            return false;
        }
        
        if(PlayerState == PLAYER_STATE.BLOCK)
        {
            if (((PlayerBlock)stateDic[PLAYER_STATE.BLOCK]).IsBlockAble)
            {
                effectModel.blockEffect.SetActive(true);
                ((PlayerBlock)stateDic[PLAYER_STATE.BLOCK]).TriggerAction();
            }
            return !((PlayerBlock)stateDic[PLAYER_STATE.BLOCK]).IsBlockAble;
        }

        return true;
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
    }

    public void StopSound()
    {
        audioSource.loop = false;
        audioSource.clip = null;
        audioSource.Stop();
    }

   // public void PlaySound(PLAYER_SOUND_MODEL soundNumber)
   // {
   //     audioSource.loop = false;
   //     audioSource.clip = soundModel.soundArray[(int)soundNumber];
   //     audioSource.Play();
   // }

    public void PlaySound(PLAYER_SOUND_MODEL soundNumber, bool isLoop = false)
    {
        audioSource.loop = isLoop;
        audioSource.clip = soundModel.soundArray[(int)soundNumber];
        audioSource.Play();
    }
}
