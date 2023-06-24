using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Projectile crab (launches projectiles at player, unsure of real-life equivalent)
//		- Big crab w/ harpoon launcher on its bag manned by a smaller crab
public class ProjectileCrab : Crab
{
	[SerializeField]
	GameObject _projectile;

	[SerializeField]
	float _attackTimeInterval = 5;
	float _elapsedTime = 0;


	protected override void FixedUpdate()
	{
		_elapsedTime += Time.deltaTime;
		if (_elapsedTime > _attackTimeInterval)
		{
			_elapsedTime = 0;
			Attack();
		}

		base.FixedUpdate();
	}

	void Attack()
	{
		var projectile = Instantiate(_projectile, transform.position, transform.rotation);
		projectile.GetComponent<CrabBullet>().Initialize(PlayerDirection);
	}
}
