using UnityEngine;

public class Billboard : MonoBehaviour
{
	void Update()
	{
		this.transform.LookAt(Camera.main.transform.position);
	}
}
