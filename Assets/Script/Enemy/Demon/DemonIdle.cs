using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonIdle : DemonState
{
    public DemonIdle(Demon demon) : base(demon)
    {

    }

    public override void ExitAction()
    {
        GameManager.inctance.IsBattle = true;
    }

    public override void InputAction()
    {

    }

    public override bool StateCheck()
    {
        return true;
    }

    public override void UpdateAction()
    {

    }
}
