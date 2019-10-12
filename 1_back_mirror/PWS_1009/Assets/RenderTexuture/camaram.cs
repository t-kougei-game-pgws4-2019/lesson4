using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaram : MonoBehaviour
{
	private Transform verRot2;
	private Transform horRot2;

	// Use this for initialization
	void Start()
	{
		verRot2 = transform.parent;
		horRot2 = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		float X_Rotation = Input.GetAxis("Mouse X");
		float Y_Rotation = Input.GetAxis("Mouse Y");
		verRot2.transform.Rotate(0, -X_Rotation, 0);
		horRot2.transform.Rotate(-Y_Rotation, 0, 0);
	}
}