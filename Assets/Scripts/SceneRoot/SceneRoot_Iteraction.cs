/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Iteraction handler.
/// </summary>
public delegate void IteractionHandler(object source, IteractionArgs e);

/// <summary>
/// Iteraction arguments.
/// </summary>
public class IteractionArgs : EventArgs
{
	public enum EIteractions { Sweem, Fire2 };

	private EIteractions mIteraction;
	private object mData;

	/// <summary>
	/// Initializes a new instance of the <see cref="IteractionArgs"/> class.
	/// </summary>
	/// <param name="iteraction">Iteraction.</param>
	/// <param name="data">Data.</param>
	public IteractionArgs( EIteractions iteraction, object data )
	{
		mIteraction = iteraction;
		mData = data;
	}

	
	/// <summary>
	/// Gets or sets the data.
	/// </summary>
	/// <value>The data.</value>
	public object Data
	{
		get
		{
			return mData;
		}
		set
		{
			mData = value;
		}
	}

	/// <summary>
	/// Gets or sets the iteraction.
	/// </summary>
	/// <value>The iteraction.</value>
	public EIteractions Iteraction
	{
		get
		{
			return mIteraction;
		}
		set
		{
			mIteraction = value;
		}
	}
}


/// <summary>
/// Scene root.
/// </summary>
public partial class SceneRoot : MonoBehaviour {

	public event IteractionHandler OnIteract;
		
	/// <summary>
	/// Iteract the specified iteraction and data.
	/// </summary>
	/// <param name="iteraction">Iteraction.</param>
	/// <param name="data">Data.</param>
	public void Iteract(IteractionArgs.EIteractions iteraction, object data)
	{
		if ( OnIteract != null )
		{
			OnIteract( this, new IteractionArgs(iteraction, data ));
		}
	}					
}
