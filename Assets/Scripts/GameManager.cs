using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private int playerHealth = 100;

    private float level;

    public void GetDamage (int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Debug.Log("Player died.");
            PlayerDied();
        }
    }

    public void PlayerDied ()
    {
        // Game over
    }
}
