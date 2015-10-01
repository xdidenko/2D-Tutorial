/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Hero.
/// </summary>
public class Hero {
	
	#region vars
	private static string mGameObjectName = "Hero";
	private GameObject mGameObject;
	#endregion
	
	#region implementation
	public Hero()
	{
	}
		
	/// <summary>
	/// Awake this instance.
	/// </summary>
	public void Awake()
	{			
		mGameObject = GameObject.Find( mGameObjectName );
					
		if ( mGameObject == null )
		{
			Debug.LogWarning("Cannot find object template " + mGameObjectName );
		}

	}

	/// <summary>
	/// Die this instance.
	/// </summary>
	public void Die()
	{
		if ( HeroControllerComponent.State != HeroStates.Dead )
		{
			HeroControllerComponent.ShowBlood();
			HeroControllerComponent.State = HeroStates.Dead;
			SceneRoot.Instance.GameOver();
		}
	}

	#endregion
	
	#region properties
	/// <summary>
	/// Games the object.
	/// </summary>
	/// <returns>The object.</returns>
	public GameObject gameObject
	{
		get
		{
			return mGameObject;
		}
	}
	
	/// <summary>
	/// Transform this instance.
	/// </summary>
	public Transform transform
	{
		get
		{
			return mGameObject.transform;
		}
	}	

	/// <summary>
	/// Sets the position.
	/// </summary>
	/// <value>The position.</value>
	public Vector3 position
	{

		get
		{
			return transform.position;
		}

	}
	
	/// <summary>
	/// Gets the rigidbody.
	/// </summary>
	/// <value>
	/// The rigidbody.
	/// </value>
	public Rigidbody rigidbody
	{
		get
		{
			return transform.rigidbody;
		}
	}

	/// <summary>
	/// Gets the hero controller component.
	/// </summary>
	/// <value>The hero controller component.</value>
	public HeroBehaviour HeroControllerComponent
	{
		get
		{
			return gameObject.GetComponent( typeof( HeroBehaviour )) as HeroBehaviour;
		}
	}

	
	/// <summary>
	/// Sets the state.
	/// </summary>
	/// <value>The state.</value>
	public HeroStates State
	{
		get
		{
			return HeroControllerComponent.State;
		}
		set
		{
			HeroControllerComponent.State = value;
		}
	}	
	#endregion
	
	
}
