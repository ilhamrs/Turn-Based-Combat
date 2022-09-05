using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaUnit : MonoBehaviour
{
    public string charaName;
    public int charaLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
}
