using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BGM_LIST
{
    OPENIG = 0,
    NONBATTLE,
    BATTLE,
    DEATH,
    CLEAR
}

public class GameManager : Singleton<GameManager>
{
    public AudioClip[] bgms;
    private AudioSource audioSource;

    public Player player;
    public bool IsBattle
    {
        get => isBattle;
        set
        {
            isBattle = value;

            audioSource.clip = (bool)(isBattle) ? bgms[(int)BGM_LIST.BATTLE] :
                                                  bgms[(int)BGM_LIST.NONBATTLE];
            audioSource.Play();
        }
    }
    private bool isBattle;

    public bool IsClear
    {
        get => isClear;
        set
        {
            isClear = value;
            if(IsClear)
            {
                audioSource.clip = bgms[(int)BGM_LIST.CLEAR];
                audioSource.Play();
            }
        }
    }
    private bool isClear;

    public Vector3 playerStartPos;

    public Wall wall;
    public bool IsEnemyDie
    {
        get => isEnemyDie;
        set
        {
            isEnemyDie = value;
            wall.DestroyWall(IsEnemyDie);
        }
    }
    private bool isEnemyDie = false;
    private new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgms[(int)BGM_LIST.OPENIG];
        audioSource.Play();
    }

    public void LoadScene(string sceneName)
    {
        Fade.inctance.FadeIn(sceneName);
    }

    public void PlayerDeath()
    {
        LoadScene("Death");
        audioSource.clip = bgms[(int)BGM_LIST.DEATH];
        audioSource.Play();
    }

    public void GameExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        //UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
