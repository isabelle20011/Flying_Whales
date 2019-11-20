using System.Collections;
using UnityEngine;

public class TransformFollower : MonoBehaviour
{
	[SerializeField] private bool lookAt = true;
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offsetPosition = new Vector3(0, 0, 0);
	[SerializeField] private Vector3 offsetPositionDialog = new Vector3(3, 0, 0);
	[SerializeField] private Vector3 offsetUp = new Vector3(0, 1, 0);
	[SerializeField] private Space offsetPositionSpace = Space.Self;
	[SerializeField] public bool b_offsetPositionDialog = false;
	[SerializeField] private bool m_AutoTargetPlayer = true;  // Whether the rig should automatically target the player.
	[SerializeField] public float f_transitionTimeFinal = 10f;
	[HideInInspector] public float f_transitionTime = 3f;
	[HideInInspector] public bool stopFollowing = false;

	private Vector3 TargetPosition;
	private bool b_smooth;


	protected virtual void Start()
	{
		// if auto targeting is used, find the object tagged "Player"
		// any class inheriting from this should call base.Start() to perform this action!
		if (m_AutoTargetPlayer)
		{
			FindAndTargetPlayer();
		}
		StartCoroutine(ChangeSpeedCamera());
	}

	private void LateUpdate()
	{
		Refresh();
		if (m_AutoTargetPlayer && (target == null || !target.gameObject.activeSelf))
		{
			FindAndTargetPlayer();
		}
	}

	public void FindAndTargetPlayer()
	{
		// auto target an object tagged player, if no target has been assigned
		var targetObj = GameObject.FindGameObjectWithTag("Player");
		if (targetObj)
		{
			SetTarget(targetObj.transform);
		}
	}

	public virtual void SetTarget(Transform newTransform)
	{
		target = newTransform;
	}

	public Transform Target
	{
		get { return target; }
	}

	public void Refresh()
	{
		if (target == null)
		{
			return;
		}
		if (!stopFollowing)
		{
			// compute position
			if (offsetPositionSpace == Space.Self)
			{
				if (b_offsetPositionDialog)
				{
					b_smooth = true;
					TargetPosition = target.TransformPoint(offsetPosition + offsetPositionDialog);
				}
				else
				{
					StartCoroutine(SwitchBool());
					TargetPosition = target.TransformPoint(offsetPosition);
				}
			}
			else
			{
				TargetPosition = target.position + offsetPosition;
			}

			if (b_smooth)
			{

				transform.position = Vector3.Lerp(transform.position, TargetPosition, f_transitionTime * Time.fixedDeltaTime);

			}
			else
			{
				transform.position = TargetPosition;
			}
		}

		// compute rotation
		if (lookAt)
		{
			transform.LookAt(target.position + offsetUp);
		}
	}

	IEnumerator ChangeSpeedCamera()
	{
		yield return new WaitForSeconds(2);
		f_transitionTime = Mathf.Lerp(f_transitionTime, f_transitionTimeFinal, 0.5f);
	}
	IEnumerator SwitchBool()
	{
		yield return new WaitForSeconds(1);
		b_smooth = false;
	}

}