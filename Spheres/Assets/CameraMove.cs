using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.W))
			transform.Translate(0.0f, 0.0f, 0.1f);

		if (Input.GetKey(KeyCode.S))
			transform.Translate(0.0f, 0.0f, -0.1f);

		if (Input.GetKey(KeyCode.A))
			transform.Translate(-0.1f, 0.0f, 0.1f);

		if (Input.GetKey(KeyCode.D))
			transform.Translate(0.1f, 0.0f, 0.1f);
	}
}
