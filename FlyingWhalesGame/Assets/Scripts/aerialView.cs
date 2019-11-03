using UnityEngine;
using System.Collections;

public class aerialView : MonoBehaviour
{
	[SerializeField] private bool lookAt = true;
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offsetPosition = new Vector3();
	[SerializeField] private Space offsetPositionSpace = Space.Self;
	[SerializeField] private bool m_AutoTargetPlayer = true;  // Whether the rig should automatically target the player.

	protected virtual void Start()
	{
		// if auto targeting is used, find the object tagged "Player"
		// any class inheriting from this should call base.Start() to perform this action!
		if (m_AutoTargetPlayer)
		{
			FindAndTargetPlayer();
		}
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
			Debug.LogWarning("Missing target ref !", this);

			return;
		}

		//offsetPosition.x = targetTransform.position.x;
		offsetPosition.y = this.transform.position.y;
		offsetPosition.z = this.transform.position.z;
		this.transform.position = offsetPosition;
	}
}