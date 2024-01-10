using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_SOUND_MODEL
{
    FIRST_ATTACK = 0,
    SECOND_ATTACK,
    THIRD_ATTACk,
    MOVE,
    BLOCK_IN,
    BLOCK,
    EVASION,
    PARRY,
    STIFF,
    DOWN,
    BOUNCE
}


public class PlayerSoundModel : MonoBehaviour
{
    public AudioClip[] soundArray;
}
