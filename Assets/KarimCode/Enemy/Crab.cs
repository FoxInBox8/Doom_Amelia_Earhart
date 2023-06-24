using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

	protected Transform player;
	protected NavMeshAgent agent;

	protected virtual void Start()
    {
		_currentHealth = _statBlock.MaxHealth;

		player = FindAnyObjectByType<PlayerScript>().transform;
		agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	protected virtual void Update()
    {
		Move();
    }

	public virtual void Move()
	{
		agent.destination = player.position;
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

		// Set it as disabled to prevent weird psuedo-alive stuff from happening
		gameObject.SetActive(false);
	}
}
