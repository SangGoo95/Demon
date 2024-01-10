using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonComponent : MonoBehaviour
{
    public void OnClick(string str)
    {
        GameManager.inctance.LoadScene(str);
    }

    public void GameExit()
    {
        GameManager.inctance.GameExit();
    }
}
