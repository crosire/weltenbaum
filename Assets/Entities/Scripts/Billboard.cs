using UnityEngine;

public class Billboard : MonoBehaviour
{
	void Update()
	{
		var cameraTransform = Camera.main.transform;

		this.transform.LookAt(cameraTransform.position, cameraTransform.up);
	}
}
