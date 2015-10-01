/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Game behaviour.
/// </summary>
public class GameBehaviour : MonoBehaviour {
	
		
	/// <summary>
	/// Start this instance.
	/// </summary>
	protected virtual void Start()
	{

	}

	/// <summary>
	/// Finds the in tree.
	/// </summary>
	/// <returns>The in tree.</returns>
	/// <param name="name">Name.</param>
	protected virtual GameObject FindComponentInTree(string name )
	{
	
		Transform root = transform.root;

		Component[] allTransform = root.GetComponentsInChildren<Transform>(true);
		
		foreach( Transform t in allTransform )
		{
			if ( t.name == name )
			{
				return t.gameObject;
			}
		}
		
		Debug.LogWarning("Can't find " + name );
		
		return null;

	}

	/// <summary>
	/// Finds the in tree.
	/// </summary>
	/// <returns>The in tree.</returns>
	/// <param name="type">Type.</param>
	/// <param name="name">Name.</param>
	protected virtual Component FindComponentInTree(System.Type type, string name)
	{

		GameObject go = FindComponentInTree(name);

		Component result = null;
		if ( go != null )
		{
			result = go.GetComponent(type) as Component;
		}

		if ( result == null )
		{
			Debug.LogWarning("No attached component " + type );
		}

		return result;

	}


	/// <summary>
	/// Finds the in tree.
	/// </summary>
	/// <returns>The in tree.</returns>
	/// <param name="type">Type.</param>
	/// <param name="name">Name.</param>
	protected virtual Component[] FindComponentsInTree(System.Type type, string name)
	{
		
		GameObject go = FindComponentInTree(name);
		
		Component[] result = null;
		if ( go != null )
		{
			result = go.GetComponents(type) as Component[];
		}
		
		if ( result == null )
		{
			Debug.LogWarning("No attached component " + type );
		}
		
		return result;
		
	}

	/// <summary>
	/// Finds the components in parent.
	/// </summary>
	/// <returns>The components in parent.</returns>
	/// <param name="type">Type.</param>
	/// <param name="name">Name.</param>
	protected virtual Component[] FindComponentsInParent(System.Type type, string name)
	{
		
		GameObject go = transform.parent.gameObject;
		
		Component[] result = null;
		if ( go != null )
		{
			result = go.GetComponents(type) as Component[];
		}
		
		if ( result == null )
		{
			Debug.LogWarning("No attached component " + type );
		}
		
		return result;
		
	}

	/// <summary>
	/// Finds the in tree.
	/// </summary>
	/// <returns>The in tree.</returns>
	/// <param name="name">Name.</param>
	public static GameObject UtilFindInTree(GameObject go, string name )
	{
		Transform root = go.transform;
		if ( go.transform.root != null )
		{
			root = go.transform.root;
		}
		
		Component[] allTransform = root.GetComponentsInChildren<Transform>(true);
		
		foreach( Transform t in allTransform )
		{
			if ( t.name == name )
			{
				return t.gameObject;
			}
		}
		
		Debug.LogWarning("Can't find " + name );
		
		return null;
	}

	/// <summary>
	/// Finds the in tree.
	/// </summary>
	/// <returns>The in tree.</returns>
	/// <param name="type">Type.</param>
	/// <param name="name">Name.</param>
	public static Component UtilFindInTree( GameObject searchGo, System.Type type, string name )
	{
		GameObject go = UtilFindInTree( searchGo, name );
		
		Component result = null;
		if ( go != null )
		{
			result = go.GetComponent(type) as Component;
		}
		
		if ( result == null )
		{
			Debug.LogWarning("No attached component " + type );
		}
		
		return result;
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	private void Update()
	{
		if ( SceneRoot.Instance.SceneState.State != StateBase.ESubState.Pause )
		{
			GameUpdate();
		}

	}

	/// <summary>
	/// Fixeds the update.
	/// </summary>
	private void FixedUpdate()
	{
		if ( SceneRoot.Instance.SceneState.State != StateBase.ESubState.Pause )
		{
			GameFixedUpdate();
		}
	}

	/// <summary>
	/// Games the update.
	/// </summary>
	protected virtual void GameUpdate()
	{

	}
	
	/// <summary>
	/// Games the update.
	/// </summary>
	protected virtual void GameFixedUpdate()
	{
		
	}

	/// <summary>
	/// Draws the arror.
	/// </summary>
	/// <param name="">.</param>
	protected virtual void DrawPin( Vector3 start, Vector3 end )
	{
		Gizmos.DrawLine ( start, end );
		Gizmos.DrawWireSphere( end, 0.1f );

	}

	/// <summary>
	/// Draws the arrow.
	/// </summary>
	/// <param name="pos">Position.</param>
	/// <param name="direction">Direction.</param>
	/// <param name="arrowHeadLength">Arrow head length.</param>
	/// <param name="arrowHeadAngle">Arrow head angle.</param>
	protected virtual void DrawArrow(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
	{
		Gizmos.DrawRay(pos, direction);
		
		Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
		Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
		Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
		Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
	}

}
