using UnityEngine;

public class rotateWithPlayer : MonoBehaviour
{
	// Start is called before the first frame update
	private GameObject player;
	public bool isBig;
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		if (player)
		{
			Quaternion look = player.transform.rotation;

			if (isBig)
			{
				look = new Quaternion(0f, look.y, look.z, look.w) * Quaternion.Euler(0, 180, 0);
			}
			else
			{
				look = new Quaternion(0f, look.y, look.z, look.w) * Quaternion.Euler(90, 0, 0);
			}
			transform.localRotation = look;
		}
	}
}
