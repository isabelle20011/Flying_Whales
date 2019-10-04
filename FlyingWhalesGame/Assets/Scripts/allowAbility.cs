using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allowAbility : MonoBehaviour
{
    public bool doubleJump;
    public bool sprinting;
    public bool crouching;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                Debug.Log("Got here");
                if (sprinting)
                {

                }
                else if (doubleJump)
                {

                }
                else if (crouching)
                {
                    playerMovement.allowCrouch = true;
                }

            }
        }
    }
}
