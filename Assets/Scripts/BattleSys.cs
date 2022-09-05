using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleStates { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSys : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    CharaUnit playerUnit;
    CharaUnit enemyUnit;

    public Text dialogueText;

    public HUD playerHUD;
    public HUD enemyHUD;

    public BattleStates state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleStates.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<CharaUnit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<CharaUnit>();

        dialogueText.text = "A wild " + enemyUnit.charaName + " approches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleStates.PLAYERTURN;
        PlayerTurn();

    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    public void OnAttackButton()
    {
        if(state != BattleStates.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack is Successful!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleStates.WON;
            EndBattle();
        }
        else
        {
            state = BattleStates.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.charaName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleStates.LOST;
            EndBattle();
        }
        else
        {
            state = BattleStates.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleStates.WON)
        {
            dialogueText.text = "You won the battle!";
        }else if(state == BattleStates.LOST)
        {
            dialogueText.text = "You were defeated!";
        }
    }

    public void OnHealButton()
    {
        if (state != BattleStates.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleStates.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
}
