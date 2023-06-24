using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrabType
{
	Crab,
	BigCrab,
	SmallCrab,
	ShooterCrab
}

/// <summary>
/// Should implement what every enemy needs to exist.
/// </summary>
public class Crab : MonoBehaviour, IEnemy
{
	[SerializeField]
	protected EnemyDataSO _statBlock;
	protected int _currentHealth;
	protected int _currentRow;

	// todo this shouldn't be public, it should get it from somewhere else (some global state, maybe it doesn't even need to exist here?)
	[SerializeField]
	protected Transform _playerPosition;
	private Vector3 _playerDirection;

	/// <summary>
	/// Normalized property of the direction to the player.
	/// </summary>
	protected Vector3 PlayerDirection
	{ 
		get
		{
			return _playerDirection.normalized;
		}

		set
		{
			_playerDirection = value;
		}
	}

	// Start is called before the first frame update
	protected virtual void Awake()
    {
		_currentHealth = _statBlock.MaxHealth;
	}

	// Update is called once per frame
	protected virtual void Update()
    {
        
    }

	// called on delta time I think? i don't remember
	protected virtual void FixedUpdate()
	{
		Move();
	}

	public virtual void Move()
	{
		PlayerDirection = _playerPosition.position - transform.position;

		// Don't need to use Time.DeltaTime since this is fixed update
		transform.Translate(PlayerDirection * _statBlock.Speed);
	}

	public virtual void TakeDamage(int damage)
	{
		_currentHealth -= damage;
		Debug.Log($"{gameObject.name} taking {damage} damage, health is {_currentHealth}");

		if (_currentHealth <= 0)
		{
			Die();
		}
	}

	public virtual void Die()
	{
		Destroy(gameObject);

		// set it as disabled to prevent weird psuedo-alive stuff from happening
		gameObject.SetActive(false);
	}
}
