using UnityEngine;

public class ChestCollision : MonoBehaviour
{

	void OnTriggerEnter(Collider other)
	{
		print("yay");
		if (other.CompareTag("Player"))
		{
			ChestAnim.inside = true;

		}
	}

	void OnTriggerExit(Collider other)
	{
		print("yay");
		if (other.CompareTag("Player"))
		{
			ChestAnim.inside = false;

		}
	}
}
