using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class turnBasedSystem : MonoBehaviour
{
    Queue<Enemies> enemyQueue = new Queue<Enemies>();
    public GameObject endScreen;

    public UnityEvent OnBlockPlayerInput, OnUnblockPlayerInput;

    public void NextTurn()
    {
        Debug.Log("Waiting  ");
        OnBlockPlayerInput?.Invoke();
        EnemiesTurn();

    }

    private void EnemiesTurn()
    {
        enemyQueue 
            = new Queue<Enemies>(FindObjectsOfType<Enemies>());

        if (enemyQueue.Count == 0)
        {
            endScreen.SetActive(true);
            Debug.Log("all enemies dead");
        }
        StartCoroutine(EnemyTakeTurn(enemyQueue));
    }

    private IEnumerator EnemyTakeTurn(Queue<Enemies> enemyQueue)
    {
        while (enemyQueue.Count > 0)
        {

            Enemies turnTaker = enemyQueue.Dequeue();
            turnTaker.TakeTurn();
            yield return new WaitUntil(turnTaker.isFinished);
            turnTaker.Reset();
            
            
        }
        
        Debug.Log(enemyQueue.Count);
        Debug.Log("PLAYERS turn begin");
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        foreach (PlayerTurn turnTaker in FindObjectsOfType<PlayerTurn>())
        {
            turnTaker.WaitTurn();
            Debug.Log($"Unit {turnTaker.name} is waiting");
           
        }
        

        Debug.Log("New turn");
        OnUnblockPlayerInput?.Invoke();
    }
}

public interface ITurnDependant
{
    void WaitTurn();
}
