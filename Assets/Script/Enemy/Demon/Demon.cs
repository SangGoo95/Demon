using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class Demon : Enemy
{
    public DemonSoundModel soundModel;
    public AudioSource audioSource;

    float maxHp;

    public override float Hp
    { 
        get => hp;
        set
        {
            hp = value;
            uiController.SetHpBar(Hp, maxHp);

            if(hp <= 0)
            {
                ChangeState(ENEMY_STATE.DIE);
            }
            if(hp >= maxHp)
            {
                hp = maxHp;
            }
        }
    }

    public Material demonMat;

    public DemonEffectModel effectModel;

    public int level;

    protected void Start()
    {
        animatorController = GetComponent<EnemyAnimatorController>();
        uiController = GetComponent<EnemyUIController>();
        rb = GetComponent<Rigidbody>();
        effectModel = GetComponent<DemonEffectModel>();
        soundModel = GetComponent<DemonSoundModel>();
        audioSource = GetComponent<AudioSource>(); 

        demonMat.color = Color.white;

        chaseRadius = 27.0f;
        maxHp = 100 * level;
        Hp = 90 * level;

        stateDic.Add(ENEMY_STATE.IDLE, new DemonIdle(this));
        stateDic.Add(ENEMY_STATE.ROAR, new DemonRoar(this));
        stateDic.Add(ENEMY_STATE.CHASE, new DemonChase(this));
        stateDic.Add(ENEMY_STATE.ATTACK, new DemonAttack(this));
        stateDic.Add(ENEMY_STATE.GROGGY, new DemonGroggy(this));
        stateDic.Add(ENEMY_STATE.DIE, new DemonDie(this));

        EnemyState = ENEMY_STATE.IDLE;
        GameManager.inctance.IsEnemyDie = false;
    }

    private void Update()
    {
        ChangeStateCheck();
        if (currentState != null)
        {
            currentState.UpdateAction();
        }
    }

    void ChangeStateCheck()
    {
        if(currentState.StateCheck())
        {
            targetCols = Physics.OverlapSphere(transform.position, chaseRadius, 1 << 7);
            if(targetCols.Length > 0)
            {
                ChangeState(ENEMY_STATE.ROAR);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out var player))
        {
            if (player.IsHitAble() && EnemyState == ENEMY_STATE.ATTACK)
            {
                player.PlayerState = (PLAYER_STATE)((DemonAttack)stateDic[ENEMY_STATE.ATTACK]).attackStrategy.AttackTpye;
                Vector3 playerPos = player.transform.position;
                Vector3 knockBackPos = (playerPos - transform.position).normalized;
    
                player.rb.AddForce(knockBackPos * 200);
                player.TakeDamage(level * 10);
            }
        }
    }

    public void PlaySound(DEMON_SOUND_MODEL soundNumber)
    {
        audioSource.clip = soundModel.soundArray[(int)soundNumber];
        audioSource.Play();
    }
}
