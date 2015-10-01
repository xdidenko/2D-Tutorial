/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;

/// <summary>
/// Base interaction generator.
/// </summary>
public class BaseInteractionGenerator : GameBehaviour {

	public enum EMode { ManyTime, Once, ImpulseDepend };

	public GameObject interactionEffect;
	public EMode mode = EMode.ManyTime;
	public float interactionVelocityMagnitude = 5f; //

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	protected void OnCollisionEnter(Collision collision)
	{
		if ( mode == EMode.ManyTime )
		{
			GameObject go = Instantiate( interactionEffect, collision.contacts[0].point, Quaternion.identity ) as GameObject;
			go.name = interactionEffect.name;
		}
		else if ( mode == EMode.Once )
		{
			if ( FindComponentInTree( interactionEffect.name ) == null )
			{
				GameObject go = Instantiate( interactionEffect, collision.contacts[0].point, Quaternion.identity ) as GameObject;
				go.transform.parent = transform;
				go.name = interactionEffect.name;
			}
		}
		else if ( mode == EMode.ImpulseDepend )
		{
			if ( collision.relativeVelocity.magnitude > interactionVelocityMagnitude )
			{
				GameObject go = Instantiate( interactionEffect, collision.contacts[0].point, Quaternion.identity ) as GameObject;
				go.name = interactionEffect.name;
			}
		}
	}

}
