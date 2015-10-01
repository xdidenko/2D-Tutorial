/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;


/// <summary>
/// Multy trigger point.
/// </summary>
public class MultyTriggerPoint : GameBehaviour {

	Component[] mt;

	/// <summary>
	/// Start this instance.
	/// </summary>
	protected override void Start()
	{
		base.Start();

		mt = FindComponentsInParent(typeof(TriggerBase), transform.parent.name ) as Component[];
	}

	#region trigger
	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other) {

		foreach( TriggerBase tb in mt )
		{
			tb.TriggerPointEnter( gameObject, other );
		}

		//Debug.Log( "On trigger enter" );

	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerStay(Collider other) {
		
		foreach( TriggerBase tb in mt )
		{
			tb.TriggerPointStay( gameObject, other );
		}
		
		//Debug.Log( "On trigger enter" );
		
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerExit(Collider other) {
		
		foreach( TriggerBase tb in mt )
		{
			tb.TriggerPointExit( gameObject, other );
		}
		
		//Debug.Log( "On trigger enter" );
		
	}

	#endregion

	#region collision
	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter( Collision collision) 
	{
		foreach( TriggerBase tb in mt )
		{
			tb.TriggerPointOnCollisionEnter( gameObject, collision );
		}

		//Debug.Log( "On collision enter" );
	}


	/// <summary>
	/// Raises the collision stay event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionStay( Collision collision) 
	{
		foreach( TriggerBase tb in mt )
		{
			tb.TriggerPointOnCollisionStay( gameObject, collision );
		}
		
		//Debug.Log( "On collision enter" );
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="collision">Collision.</param>
	void OnCollisionExit( Collision collision) 
	{
		foreach( TriggerBase tb in mt )
		{
			tb.TriggerPointOnCollisionExit( gameObject, collision );
		}
		
		//Debug.Log( "On collision enter" );
	}
	#endregion

}
