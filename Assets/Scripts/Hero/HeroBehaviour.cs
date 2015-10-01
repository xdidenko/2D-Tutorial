/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;
using System;
using System.Linq;

/// <summary>
/// Hero movoe states.
/// </summary>
public enum HeroStates
{
	Idle = 0, MoveForward = 1, Turn = 2, MoveDown = 3, MoveUp = 4, Dead = 6, StartPickingUp = 5, PushOffWall = 9, PushOffGround = 8
}

/// <summary>
/// Hero controller.
/// </summary>
public class HeroBehaviour : GameBehaviour {
			
	#region variables	
	protected GameObject mTakingObject;
	
	public GameObject sludgePrefab;
	private GameObject mBlood;
		
	//Interaction effects
	public GameObject[] effectDie;
	public GameObject[] effectPickUp;	
	public GameObject[] effectCollisionsStone;	

	private GameObject mMoveEffect;

	private Animator mAnimator;	
	#endregion

	#region implementation
	/// <summary>
	/// Start this instance.
	/// </summary>
	protected override void Start ()
	{
		base.Start();

		SceneRoot.Instance.OnIteract += HandleOnHumanIteract;

		mMoveEffect = FindComponentInTree( "MoveEffect" ) as GameObject;

		mAnimator = FindComponentInTree( typeof( Animator), "Graphics" ) as Animator;

		mBlood = FindComponentInTree( "Blood" ) as GameObject;

	}

	/// <summary>
	/// Handles the on human iteract.
	/// </summary>
	/// <param name="source">Source.</param>
	/// <param name="e">E.</param>
	void HandleOnHumanIteract (object source, IteractionArgs e)
	{
		if ( e.Iteraction == IteractionArgs.EIteractions.Sweem )
		{
			Vector3 dir = (Vector3) e.Data;

			if ( State != HeroStates.Dead && State != HeroStates.StartPickingUp )
			{					
				rigidbody.AddForce( dir * Constants.HeroPushForce * Time.deltaTime );
			}

			if ( (State == HeroStates.Idle || State == HeroStates.MoveForward || State == HeroStates.MoveUp || State == HeroStates.MoveDown ) )
			{														
				//Check turns first
				if ( dir.x < -0.2f && mAnimator.transform.localScale.x > 0f )		
				{
					State = HeroStates.Turn;
					return;
				}
				else if ( dir.x > 0.2f &&  mAnimator.transform.localScale.x < 0f )
				{
					State = HeroStates.Turn;
					return;
				}

				//Check moving
				else if ( (dir.x > 0 || dir.x < 0 ) && dir.z < 0.5f && dir.z > -0.5f && rigidbody.velocity.magnitude > 2f )
				{
					//Move forward
					State = HeroStates.MoveForward;
				}
				else if ( dir.z < -0.5f && rigidbody.velocity.magnitude > 2f )
				{
					//Move down
					State = HeroStates.MoveDown;
				}
				else if ( dir.z > 0.5f )
				{
					//Move up
					State = HeroStates.MoveUp;
				}						
			}
		}
	}
			
	/// <summary>
	/// Games the update.
	/// </summary>
	protected override void GameUpdate ()
	{
		base.GameUpdate ();

		ProcessMove();
	}
				
	/// <summary>
	/// Processes the move.
	/// </summary>
	private void ProcessMove()
	{
		if ( (State == HeroStates.MoveForward || State == HeroStates.MoveUp || State == HeroStates.MoveDown ) && rigidbody.velocity.magnitude < 2f )
		{
			State = HeroStates.Idle;
		}

		if ( (State == HeroStates.PushOffGround || State == HeroStates.PushOffWall ) && rigidbody.velocity.magnitude < 0.5f )
		{
			State = HeroStates.Idle;
		}

		mMoveEffect.audio.volume = rigidbody.velocity.magnitude * 0.3f;

	}
		
	/// <summary>
	/// Shows the blood.
	/// </summary>
	public void ShowBlood() 
	{
		mBlood.SetActive( true );
		mBlood.transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation( Vector3.down, transform.position ).eulerAngles.y, 0f );
	}
	
	/// <summary>
	/// Raises the collistion enter event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	void OnCollisionEnter( Collision collision )
	{
		if ( State == HeroStates.Turn )
		{
			return;
		}

		foreach (ContactPoint cp in collision.contacts) 
		{
			if ( cp.otherCollider.tag == "Thresure" )
			{
				mTakingObject = cp.otherCollider.gameObject;
				
				State = HeroStates.StartPickingUp;

				rigidbody.velocity = Vector3.zero;
			}
		}

		//
		ContactPoint firstContact = collision.contacts[0];
		if ( firstContact.otherCollider.transform.parent != null && collision.relativeVelocity.magnitude > Constants.HeroMinVelocityToReact )
		{
			if ( firstContact.otherCollider.transform.parent.name == "Stone" )
			{
				GameObject localSludge = Instantiate(sludgePrefab, firstContact.point, Quaternion.identity) as GameObject;
				localSludge.transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation( Vector3.down, firstContact.normal ).eulerAngles.y, 0f );

				int rndIndex = UnityEngine.Random.Range( 0, effectCollisionsStone.Length );
				GameObject.Instantiate( effectCollisionsStone[rndIndex], firstContact.point, Quaternion.identity );
			}
		}
	}

	/// <summary>
	/// Takes the object.
	/// </summary>
	/// <param name="go">Go.</param>
	public void ProcessTakingObject ()
	{
		if ( mTakingObject.tag == "Thresure"  )
		{
			SceneRoot.Instance.SceneThresures.Remove( mTakingObject );

			Destroy(mTakingObject);

			int rndIndex = UnityEngine.Random.Range( 0, effectPickUp.Length );
			GameObject.Instantiate( effectPickUp[rndIndex], transform.position, Quaternion.identity );

			if ( SceneRoot.Instance.SceneThresures.Count == 0 )
			{
				SceneRoot.Instance.LevelComplete();
			}
		}
	}
				
	/// <summary>
	/// Die this instance.
	/// </summary>
	protected virtual void Die ()
	{
		audio.Stop();
		audio.clip = null;

		rigidbody.useGravity = true;

		//Effects
		int rndIndex = UnityEngine.Random.Range( 0, effectDie.Length );
		GameObject.Instantiate( effectDie[rndIndex], transform.position, Quaternion.identity );

	}

	#endregion
	
	#region properties		
	/// <summary>
	/// Gets or sets the state of the move.
	/// </summary>
	/// <value>The state of the move.</value>
	public HeroStates State
	{
		get
		{
			return (HeroStates) mAnimator.GetInteger("State");
		}
		set
		{
			if  ( value != State ) 
			{
				if ( value == HeroStates.Dead )
				{	
					Die();
				}
												
				mAnimator.SetInteger("State", (int) value );
			}
		}
	}
	#endregion
}
