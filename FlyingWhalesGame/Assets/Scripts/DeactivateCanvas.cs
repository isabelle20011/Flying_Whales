using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	// Update is called once per frame

	private void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			Time.timeScale = 1;
			this.gameObject.SetActive(false);
			PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
			if (playerMovement)
			{
				playerMovement.enabled = true;
			}
		}
	}
}
