/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;

/// <summary>
/// Hero animation sync.
/// </summary>
public class HeroAnimationSync : GameBehaviour {
		
	/// <summary>
	/// Finishs the picking.
	/// </summary>
	public void FinishPicking()
	{
		SceneRoot.Instance.MainHero.State = HeroStates.Idle;
	}

	/// <summary>
	/// Takes the in hand.
	/// </summary>
	public void TakeInHand()
	{
		SceneRoot.Instance.MainHero.HeroControllerComponent.ProcessTakingObject();
	}

	/// <summary>
	/// Ends the turn.
	/// </summary>
	public void EndTurn()
	{
		if ( transform.localScale.x > 0 )
		{
			transform.localScale = new Vector3( -0.6f, 0.6f, 1f );
		}
		else if ( transform.localScale.x < 0 )
		{
			transform.localScale = new Vector3( 0.6f, 0.6f, 1f );
		}

		SceneRoot.Instance.MainHero.State = HeroStates.Idle;
	}
}
