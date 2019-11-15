using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class allowAbility : MonoBehaviour
{
    public bool attack;
    public bool sprinting;
    public bool crouching;

	private void Start()
	{
		if (sprinting && !GameManager_Master.Instance.hasSprint)
		{
			this.enabled = false;
		}
		else if (attack && !GameManager_Master.Instance.hasAttack)
		{
			this.enabled = false;
		}
		else if (crouching && !GameManager_Master.Instance.hasCrouch)
		{
			this.enabled = false;
		}
	}

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
					playerMovement.allowSprinting = true;
					GameManager_Master.Instance.hasSprint = true;
				}
                else if (attack)
                {
					playerMovement.allowAttack = true;
					GameManager_Master.Instance.hasAttack = true;
				}
                else if (crouching)
                {
                    playerMovement.allowCrouch = true;
					GameManager_Master.Instance.hasCrouch = true;

                }

            }
        }
    }
}
