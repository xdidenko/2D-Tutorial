/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Upgrades.
/// </summary>
public class Thresure {
	
	#region vars
	private List<GameObject> mThresures;
	#endregion
	
	#region implementation
	/// <summary>
	/// Initializes a new instance of the <see cref="Anchors"/> class.
	/// </summary>
	public Thresure()
	{
		mThresures = new List<GameObject>();
	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	public void Awake()
	{
		mThresures = new List<GameObject>( GameObject.FindGameObjectsWithTag( "Thresure" ));
	}
	
	/// <summary>
	/// Remove the specified go.
	/// </summary>
	/// <param name="go">Go.</param>
	public void Remove( GameObject go )
	{
		mThresures.Remove( go );
		GameObject.Destroy( go );
	}

	#endregion
	
	#region properties
	public List<GameObject> ThresuresGos
	{
		get
		{
			return mThresures;
		}
		set
		{
			mThresures = value;
		}
	}
	
	public int Count
	{
		get
		{
			return mThresures.Count;
		}
	}
	#endregion
	
	
}
