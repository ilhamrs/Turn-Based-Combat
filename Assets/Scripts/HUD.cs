using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;

    public void SetHUD(CharaUnit charaUnit)
    {
        nameText.text = charaUnit.charaName;
        levelText.text = "Lvl " + charaUnit.charaLevel;
        hpSlider.maxValue = charaUnit.maxHP;
        hpSlider.value = charaUnit.currentHP;

    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
