/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;

/// <summary>
/// I creature.
/// </summary>
public interface  ICreature  {

	/// <summary>
	/// Die this instance.
	/// </summary>
	void Die();

	/// <summary>
	/// Damage this instance.
	/// </summary>
	void Damage( float damageValue = 100f );

	/// <summary>
	/// Fear this instance.
	/// </summary>
	void Fear();

}
