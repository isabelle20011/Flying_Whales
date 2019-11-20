using GameManager;
using UnityEngine;

public class hasAbility : MonoBehaviour
{
	private bool crouch;
	private bool sprint;
	private bool attack;
	private allowAbility ability;

	private void Start()
	{
		ability = GetComponentInChildren<allowAbility>();
		crouch = ability.crouching;
		sprint = ability.sprinting;
		attack = ability.attack;

		if (crouch && GameManager_Master.Instance.hasCrouch)
		{
			this.gameObject.SetActive(false);
		}
		else if (sprint && GameManager_Master.Instance.hasSprint)
		{
			this.gameObject.SetActive(false);
		}
		else if (attack && GameManager_Master.Instance.hasAttack)
		{
			this.gameObject.SetActive(false);
		}
	}
}
