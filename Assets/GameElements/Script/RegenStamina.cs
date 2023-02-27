using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenStamina : MonoBehaviour
{
    public int stamina;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        PlayerScript player = other.gameObject.GetComponent<PlayerScript>();

        if (player != null)
        {
            player.HealHunger(stamina);
            Debug.Log("Regenstam");
            Debug.Log(player.currentStamina);

        }
        
    }
}
