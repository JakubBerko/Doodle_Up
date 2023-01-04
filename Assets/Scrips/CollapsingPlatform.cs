using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
		if (rb != null && collision.relativeVelocity.y <= 0f)
		{
			//Invoke ("DropPlatform", 0.5f); //pozn. Vložit animaci
			Destroy(gameObject);
		}
	}
}
