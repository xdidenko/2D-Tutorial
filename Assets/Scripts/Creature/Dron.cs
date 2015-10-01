/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// Creature.
/// </summary>
public class Dron : CreatureBase {
	
	#region variables
	public enum EState{ Sweem = 0, Hunt = 1 };
	
	protected Vector3 mTargetPosition;
	protected float mStartTargetDistance = float.MaxValue;

	public float floatDistance = 20f;		
	public float mHeroDetectDistance = 5f;
	//
	private GameObject mHuntGo;
	private Queue<GameObject> mUninterestingObjects;
	private float mForgetTime = 3f; //sec
	
	private Animator mAnimator;
	
	private float mStuckTimer = 0f;
	private float mHuntTimer = 0f; //sec

	private float mSweemSpeed;
	private float mSweemAcceleration;
	private float mSweemAngularSpeed;
	#endregion

	#region implementation
	// Use this for initialization
	protected override void Start () {

		base.Start();

		mSweemSpeed = mNavMeshAgent.speed;
		mSweemAcceleration = mNavMeshAgent.acceleration;
		mSweemAngularSpeed = mNavMeshAgent.angularSpeed;

		GenerateNewPosition();

		mUninterestingObjects = new Queue<GameObject>();

		InvokeRepeating( "ForgetObjects", 0f, mForgetTime );

		mAnimator = FindComponentInTree(typeof(Animator),"Graphics") as Animator;	
	}

	/// <summary>
	/// Forgets the objects.
	/// </summary>
	protected void ForgetObjects()
	{
		if ( mUninterestingObjects.Count > 0 )
		{
			mUninterestingObjects.Dequeue();
		}
	}

	/// <summary>
	/// Hunts the state.
	/// </summary>
	protected void Hunt()
	{
		if ( mHuntGo == null )
		{
			State = EState.Sweem;
			return;
		}
		
		mHuntTimer += Time.deltaTime;
		if ( mHuntTimer > Constants.DronMaxHuntTime )
		{
			mHuntTimer = 0f;
			mUninterestingObjects.Enqueue( SceneRoot.Instance.MainHero.gameObject );
			State = EState.Sweem;
			return;
		}
		
		navMeshAgent.SetDestination( mHuntGo.transform.position );
	}

	/// <summary>
	/// Sweem this instance.
	/// </summary>
	protected virtual void Sweem()
	{
		float currentDistance = Vector3.Distance( transform.position, mTargetPosition );
		
		if ( (navMeshAgent.desiredVelocity.magnitude < 1f && currentDistance != mStartTargetDistance) )
		{
			GenerateNewPosition();
		}
		
		LookForKillSomeone(); 
	}


	/// <summary>
	/// Stuck this instance.
	/// </summary>
	protected virtual void CheckStuck()
	{
		//Stuck check
		if ( mNavMeshAgent.velocity.magnitude < 1f )
		{
			mStuckTimer += Time.deltaTime;
			
			if ( mStuckTimer > Constants.StuckTimeThreshold )
			{
				GenerateNewPosition();
				mStuckTimer = 0f;
			}
		}
		else 
		{
			mStuckTimer = 0f;
		}
	}

	// Update is called once per frame
	protected override void GameUpdate () 
	{	
		base.GameUpdate();

		if ( State == EState.Sweem )
		{
			Sweem();
		}
		else if ( State == EState.Hunt )
		{
			Hunt();
		}

		CheckStuck();
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="collision">Collision.</param>
	public virtual void OnCollisionEnter(Collision collision)
	{
		if ( State == EState.Hunt )
		{
			if ( mHuntGo == null )
			{
				State = EState.Sweem;
				return;
			}
			
			navMeshAgent.SetDestination( mHuntGo.transform.position );

			if ( collision.contacts[0].otherCollider.gameObject == mHuntGo )
			{
				if ( collision.contacts[0].otherCollider.name == "Hero" )
				{
					SceneRoot.Instance.MainHero.Die();
				}

				mUninterestingObjects.Enqueue( mHuntGo );

				State = EState.Sweem;
				GenerateNewPosition();
			}
		}
	}
		
	/// <summary>
	/// Looks for kill someone.
	/// </summary>
	protected void LookForKillSomeone()
	{
		//Create each time for dynamically changed scene
		List<GameObject> huntGos = new List<GameObject>();

		if ( SceneRoot.Instance.MainHero.State != HeroStates.Dead )
		{
			huntGos.Add( SceneRoot.Instance.MainHero.gameObject );
		}

		foreach ( GameObject go in huntGos )
		{
			if ( mUninterestingObjects.Contains( go )) continue;

			if ( Vector3.Distance ( go.transform.position, transform.position) < mHeroDetectDistance )
			{
				int layerMask = 1<<LayerMask.NameToLayer("LevelCollider");
				if ( !Physics.Linecast( transform.position, go.transform.position, layerMask ))
				{
					State = EState.Hunt;
					mHuntGo = go;
				}
			}
		}
	}
	
	/// <summary>
	/// Generates the new position.
	/// </summary>
	protected virtual void GenerateNewPosition()
	{
		Vector3 rnd = transform.position + Random.insideUnitSphere * floatDistance;
		mTargetPosition = new Vector3( rnd.x, 0f, rnd.z );

		navMeshAgent.SetDestination( mTargetPosition );		
		mStartTargetDistance = Vector3.Distance( transform.position, mTargetPosition ); 

	}

	/// <summary>
	/// Raises the draw gizmos event.
	/// </summary>
	protected virtual void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere( transform.position, mHeroDetectDistance );
	}

	#endregion

	#region properties
	/// <summary>
	/// Gets or sets the state.
	/// </summary>
	/// <value>The state.</value>
	public EState State
	{
		get
		{
			return (EState) mAnimator.GetInteger("State");
		}
		set
		{
			if  ( value != State ) 
			{
				if ( value == EState.Sweem )
				{	
					mNavMeshAgent.speed = mSweemSpeed;
					mNavMeshAgent.acceleration = mSweemAcceleration;
					mNavMeshAgent.angularSpeed = mSweemAngularSpeed;
				}
				else if ( value == EState.Hunt )
				{
					mNavMeshAgent.speed = Constants.DronHuntSpeed;
					mNavMeshAgent.acceleration = Constants.DronHuntAcceleration;
					mNavMeshAgent.angularSpeed = Constants.DronHuntAngularSpeed;
				}
				
				mAnimator.SetInteger("State", (int) value );
			}
		}
	}
	#endregion

}
