using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
	[SerializeField]
	private int speed = 1;
	
	private void Update()
	{
		Vector3 direction = new Vector3(0, 0, speed * Time.deltaTime);
		transform.Translate(direction);
	}
}
