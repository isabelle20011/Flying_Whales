using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollsion : MonoBehaviour
{
    PlayerMovement script;

    void OnTriggerEnter(Collider other)
    {
        print("yay");
        if (other.CompareTag("Player"))
        {
            script = other.GetComponent<PlayerMovement>();
            if (script != null && script.IsAttacking())
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        print("yay");
        if (other.CompareTag("Player"))
        {
            script = other.GetComponent<PlayerMovement>();
            if (script != null && script.IsAttacking())
            {
                Destroy(gameObject);
            }
        }
    }
}
