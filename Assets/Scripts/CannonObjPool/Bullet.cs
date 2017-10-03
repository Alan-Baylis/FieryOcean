using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public float accel;
	private float velocity;

	void OnEnable()
	{
		velocity = 0;
	}

	void Update()
	{
		//velocity += accel;

		//transform.Translate(0, velocity, 0);

		if(transform.position.y < 0)
		{
			Finish();
		}
	}

	void Finish()
	{
		PoolManager.ReleaseObject(this.gameObject);

		//Note: 
		// This takes the gameObject instance, and NOT the prefab instance.
		// Without this call the object will never be available for re-use!
		// gameObject.SetActive(false) is automatically called
	}
}
