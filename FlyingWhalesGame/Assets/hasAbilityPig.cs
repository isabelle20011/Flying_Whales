using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class hasAbilityPig : MonoBehaviour
{
	[SerializeField] private GameObject[] exclamations;
	[SerializeField] private Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager_Master.Instance.hasAttack)
		{
			collider.enabled = false;
			foreach (GameObject exc in exclamations)
			{
				exc.gameObject.SetActive(false);
			}
		}
    }
}
