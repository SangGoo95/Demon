using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Canvas playerUi;
    public Canvas enemyUi;
    public Slider enemyHpSlider;
}