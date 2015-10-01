/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// Trigger base.
/// </summary>
public class TriggerBase : GameBehaviour {

	public string gameObjectName;
	public bool isOnceFire = false;
	public bool isTimed = false;
	public float activateTime = 30f; //sec

	public bool isLooped = false;
	public float loopPeriod = 5f; //sec
	public float loopTimes = Mathf.Infinity; // 0 - infinity, >0 times

	public string[] activatorNames = {"Hero"};
	
	private float mTimer = 0f; // sec

	private float mLoopTimer = 0f; //sec
	private int mLoopFired = 0;

	protected bool mWasFired = false;
	protected bool mTimerFired = false;

	/// <summary>
	/// Start this instance.
	/// </summary>
	protected override void Start()
	{
		base.Start();

		mTimer = 0f;
		mLoopTimer = 0f;
	}

	/// <summary>
	/// Games the update.
	/// </summary>
	protected override void GameUpdate ()
	{
		base.GameUpdate ();

		if ( isTimed )
		{
			mTimer += Time.deltaTime;

			if ( mTimer > activateTime )
			{
				TimeActivated();
			}
		}

		if ( isLooped && (mLoopFired < loopTimes) )
		{
			mLoopTimer += Time.deltaTime;
			
			if ( mLoopTimer > loopPeriod )
			{
				LoopProccesor();

				mLoopFired++;
				mLoopTimer = 0f;
			}
		}
	}

	/// <summary>
	/// Loops the proccesor.
	/// </summary>
	protected virtual void LoopProccesor()
	{
		EnterAction( null );
		StayAction( null );
		ExitAction( null );
	}

	/// <summary>
	/// Times the activated.
	/// </summary>
	protected virtual void TimeActivated()
	{

	}

	#region trigger
	/// <summary>
	/// Triggers the point enter.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="other">Other.</param>
	public virtual void TriggerPointEnter( GameObject sender, Collider other )
	{
		if ( isOnceFire && mWasFired ) return;

		if ( ( !activatorNames.Contains( other.name )) || ( SceneRoot.Instance.MainHero == null ) || ( sender.name != gameObjectName )) return;

		EnterAction(sender);

		mWasFired = true;
	}

	/// <summary>
	/// Triggers the point stay.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="other">Other.</param>
	public virtual void TriggerPointStay( GameObject sender, Collider other )
	{
		if ( ( !activatorNames.Contains( other.name )) || ( SceneRoot.Instance.MainHero == null ) || ( sender.name != gameObjectName )) return;

		StayAction(sender);
	}

	/// <summary>
	/// Triggers the point enter.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="other">Other.</param>
	public virtual void TriggerPointExit( GameObject sender, Collider other )
	{
		if ( ( !activatorNames.Contains( other.name )) || ( SceneRoot.Instance.MainHero == null ) || ( sender.name != gameObjectName )) return;

		ExitAction(sender);
	}
	#endregion

	#region collision

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	public virtual void TriggerPointOnCollisionEnter( GameObject sender, Collision collision) 
	{
		if ( isOnceFire && mWasFired ) return;
		
		if (  !activatorNames.Contains( collision.contacts[0].otherCollider.name ) || SceneRoot.Instance.MainHero == null || sender.name != gameObjectName ) return;

		EnterAction( sender );

		mWasFired = true;
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	public virtual void TriggerPointOnCollisionStay( GameObject sender, Collision collision) 
	{
		if (  !activatorNames.Contains( collision.contacts[0].otherCollider.name ) || SceneRoot.Instance.MainHero == null || sender.name != gameObjectName ) return;

		StayAction( sender );
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	public virtual void TriggerPointOnCollisionExit( GameObject sender, Collision collision) 
	{
		if (  !activatorNames.Contains( collision.contacts[0].otherCollider.name ) || SceneRoot.Instance.MainHero == null || sender.name != gameObjectName ) return;

		ExitAction(sender);
	}

	#endregion

	#region actions
	/// <summary>
	/// Enters the action.
	/// </summary>
	public virtual void EnterAction(GameObject sender)
	{
		
	}
	
	/// <summary>
	/// Enters the action.
	/// </summary>
	public virtual void StayAction(GameObject sender)
	{
		
	}
	
	/// <summary>
	/// Staies the action.
	/// </summary>
	public virtual void ExitAction(GameObject sender)
	{
		
	}
	#endregion

}
