using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private bool turnFinshed;

    private IenemyAi enemyAi;

    private void Start()
    {
        enemyAi = GetComponent<IenemyAi>();
        enemyAi.TurnHasFinshed += () => turnFinshed = true;


    }

    public void TakeTurn()
    {
        enemyAi.StartingTurn();
    }

    public bool isFinished() => turnFinshed;
    
    public void Reset()
    {
        turnFinshed = false;
    }
}

public interface IenemyAi
{
    event Action TurnHasFinshed;
    void StartingTurn();

}