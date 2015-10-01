/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;

/// <summary>
/// Trigger die.
/// </summary>
public class TriggerDie : TriggerBase {

	/// <summary>
	/// Enters the action.
	/// </summary>
	public override void EnterAction (GameObject sender)
	{
		base.EnterAction (sender);

		SceneRoot.Instance.MainHero.Die();
	}

	/// <summary>
	/// Times the activated.
	/// </summary>
	protected override void TimeActivated ()
	{
		base.TimeActivated ();

		if ( SceneRoot.Instance.MainHero != null )
		{
			EnterAction( null );
		}
	}

}
