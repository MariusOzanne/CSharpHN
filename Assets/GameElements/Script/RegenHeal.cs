using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenHeal : MonoBehaviour
{
    public int heal;
    public float rot;
    public float speed;

    //void Update()
    //{
    //    transform.Rotate(Vector3.right * Time.deltaTime * speed);

    //}
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        PlayerScript player = other.gameObject.GetComponent<PlayerScript>();

        if (player != null)
        {
            player.Heal(heal);
            Debug.Log("Regenheal");
            Debug.Log(player.currentHealth);

        }

    }
}
