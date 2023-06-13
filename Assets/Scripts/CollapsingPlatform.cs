using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision) //pokud dojde ke kolizi a doodler se nepohybuje nahoru, tak platforma si vezme RB od Doodlera (podle toho pozn� �e byla kolize) a zni�� se
	{
		Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
		if (rb != null && collision.relativeVelocity.y <= 0f)
		{
			Destroy(gameObject);
		}
	}
}
