/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DronHomed : Dron {
	
	#region variables
	protected Vector3 mHomePosition;
	#endregion

	#region implementation

	/// <summary>
	/// Start this instance.
	/// </summary>
	protected override void Start ()
	{
		mHomePosition = transform.position;

		base.Start ();
	}

	/// <summary>
	/// Generates the new position.
	/// </summary>
	protected override void GenerateNewPosition ()
	{
		mNavMeshAgent.SetDestination( mHomePosition );
	}	

	/// <summary>
	/// Sweem this instance.
	/// </summary>
	protected override void Sweem ()
	{
		base.Sweem ();

		LookForKillSomeone();
	}

	#endregion

	#region properties

	#endregion

}
