using GameManager;
using TMPro;
using UnityEngine;

public class allowAbility : MonoBehaviour
{
	public bool attack;
	public bool sprinting;
	public bool crouching;

	[SerializeField] private GameObject canvas;
	[SerializeField] private TextMeshProUGUI text;
	private bool changed = false;
	private PlayerMovement playerMovement;
	private string button;
	private string ability;

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
			playerMovement = other.GetComponent<PlayerMovement>();
			if (playerMovement != null)
			{
				if (sprinting)
				{
					playerMovement.allowSprinting = true;
					GameManager_Master.Instance.hasSprint = true;
					changed = true;
					button = "SHIFT";
					ability = "Sprint";
				}
				else if (attack)
				{
					playerMovement.allowAttack = true;
					GameManager_Master.Instance.hasAttack = true;
					changed = true;
					button = "CTRL or Left Click";
					ability = "Attack";
				}
				else if (crouching)
				{
					playerMovement.allowCrouch = true;
					GameManager_Master.Instance.hasCrouch = true;
					changed = true;
					button = "ALT or Right Click";
					ability = "Crouch";

				}
				if (changed)
				{
					canvas.SetActive(true);
					text.SetText("You can now press " + button + " to " + ability);
					playerMovement.enabled = false;
					Time.timeScale = 0;
				}
			}
			changed = false;
		}
	}
}
