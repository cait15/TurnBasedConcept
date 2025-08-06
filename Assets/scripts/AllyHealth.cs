using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class AllyHealth : MonoBehaviour
{
 
  public UnityEvent Death;

    [SerializeField]
    private int health;

    public void Damage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " health is " + health);
        if (health <= 0)
        {
            Death?.Invoke();
            Debug.Log(gameObject.name + " died!");
        }

    }
}
