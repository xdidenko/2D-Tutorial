/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;

/// <summary>
/// Echolot.
/// </summary>
public class InteractionEffect : GameBehaviour {

	/// <summary>
	/// Games the update.
	/// </summary>
	protected override void GameUpdate ()
	{
		base.GameUpdate ();

		if ( !audio.isPlaying )
		{
			Destroy( gameObject );
		}
	}

}
