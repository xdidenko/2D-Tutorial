/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Anchors.
/// </summary>
public class Actors : IEnumerable<GameObject> {
	
	#region vars
	private List<GameObject> mDrones;	
	private string mDronTagName = "Dron";
	#endregion
	
	#region implementation
	/// <summary>
	/// Initializes a new instance of the <see cref="Actors"/> class.
	/// </summary>
	public Actors()
	{
		mDrones = new List<GameObject>();
	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	public void Awake()
	{

		mDrones = new List<GameObject>( GameObject.FindGameObjectsWithTag( mDronTagName ));
		if ( mDrones.Count < 1 )
		{
			Debug.LogWarning("No " + mDronTagName + " in scene" );
		}
	}

	/// <summary>
	/// Gets the enumerator.
	/// </summary>
	/// <returns>The enumerator.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <summary>
	/// Sets the get enumerator.
	/// </summary>
	/// <value>The get enumerator.</value>
	public IEnumerator<GameObject> GetEnumerator()
	{
		foreach( GameObject go in mDrones )
		{
			yield return go;
		}
	}
	#endregion
	
	#region properties
		
	/// <summary>
	/// Gets or sets the neutral fishes.
	/// </summary>
	/// <value>The neutral fishes.</value>
	public List<GameObject> Drones
	{
		get
		{
			return mDrones;
		}
		set
		{
			mDrones = value;
		}
	}
	#endregion
	
	
}
