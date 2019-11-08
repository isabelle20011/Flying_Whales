using UnityEngine;
using System.Collections;

public class TransformFollower : MonoBehaviour
{
    [SerializeField] private bool lookAt = true;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offsetPosition = new Vector3(0, 0, 0);
    [SerializeField] private Space offsetPositionSpace = Space.Self;
    [SerializeField] private bool m_AutoTargetPlayer = true;  // Whether the rig should automatically target the player.
	[SerializeField] private Vector3 offsetUp = new Vector3(0, 1, 0);

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
            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target.position + offsetUp);
        }
    }
}