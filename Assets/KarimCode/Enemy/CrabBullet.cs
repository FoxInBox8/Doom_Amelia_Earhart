using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBullet : MonoBehaviour
{
	[SerializeField]
	public float Speed = 0;

	[SerializeField]
	float _lifetime = 5;
	float _timeElapsed = 0;

	Vector3 _direction;

    void FixedUpdate()
    {
		transform.Translate(_direction * Speed);

		_timeElapsed += Time.deltaTime;
		if (_timeElapsed >= _lifetime)
		{
			Destroy(gameObject);

			// set it as disabled to prevent weird psuedo-alive stuff from happening
			gameObject.SetActive(false);
		}
	}

	public void Initialize(Vector3 direction)
	{
		_direction = direction.normalized;
	}
}
