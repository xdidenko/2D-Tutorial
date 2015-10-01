/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;

/// <summary>
/// Creature.
/// </summary>
public class CreatureBase : GameBehaviour, ICreature {

	#region variables
	public float health = 100f;
	protected float mSpeed;
	protected NavMeshAgent mNavMeshAgent;
	#endregion

	#region implementation
	// Use this for initialization
	protected override void Start () 
	{
	
		base.Start();

		navMeshAgent = GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;

		if ( navMeshAgent == null ) 
		{
			Debug.LogWarning( "Cant find NavMeshAgent at " + name );
		}

	}

	/// <summary>
	/// Dos the attack.
	/// </summary>
	protected virtual void DoAttack()
	{

	}
	
	/// <summary>
	/// Die this instance.
	/// </summary>
	public virtual void Die()
	{

	}

	/// <summary>
	/// Fear this instance.
	/// </summary>
	public virtual void Fear()
	{

	}

	/// <summary>
	/// Damage this instance.
	/// </summary>
	/// <param name="damageValue">Damage value.</param>
	public virtual void Damage( float damageValue = 100f )
	{
		health -= damageValue;

		if ( health <= 0 )
		{
			Die();
		}
	}

	#endregion

	#region properties
	/// <summary>
	/// Gets the nav mesh agent.
	/// </summary>
	/// <value>The nav mesh agent.</value>
	public NavMeshAgent navMeshAgent
	{
		get
		{
			return mNavMeshAgent;
		}
		set
		{
			mNavMeshAgent = value;
		}
	}
	#endregion

}
