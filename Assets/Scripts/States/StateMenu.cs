/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;

/// <summary>
/// State menu.
/// </summary>
public class StateMenu : StateBase {

	public override string Name { get{return "start";} }
	
	private SpriteRenderer mVolume;
	private SpriteRenderer mController;

	private Vector3 mHomePosition;
	private GameObject mTargetLevels;
	
	private GameObject mApplyClear;
	private GameObject mClear;
	private GameObject mLevels;

	private MenuDataContainer mDataContainer;

	private Vector3 mTargetCameraPos;

	/// <summary>
	/// Start this instance.
	/// </summary>
	public override void Start ()
	{
		base.Start ();

		mController = GameObject.Find("btn_Controller").GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

		mDataContainer = GameObject.Find("DataContainer").GetComponent(typeof(MenuDataContainer)) as MenuDataContainer;

		SetControllerSprite();

		SceneRoot.Instance.OnIteract += HandleOnHumanIteract;

		mHomePosition = Camera.main.transform.position;
		mTargetCameraPos = mHomePosition;

		PlayerPrefs.DeleteAll();

		SetCursor( true );

	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	public override void Update ()
	{
		base.Update ();

		Camera.main.transform.position = Vector3.Lerp( Camera.main.transform.position, mTargetCameraPos, Constants.MenuCameraFlySpeed * Time.deltaTime );

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
			Ray ray = Camera.main.ScreenPointToRay( (Vector2) e.Data );
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit )) 
			{
				if ( hit.collider.rigidbody != null )
				{
					hit.collider.rigidbody.AddForce( Constants.MenuPushRigidbodyForce * ( 0.5f - Random.value ), 0f , Constants.MenuPushRigidbodyForce * ( 0.5f - Random.value ));
				}

				SceneRoot.Instance.OnUIAction( hit.collider.gameObject, null );
			}
		}
	}

	/// <summary>
	/// Raises the user interface action event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="data">Data.</param>
	public override void OnUIAction (GameObject sender, Object data)
	{
		base.OnUIAction (sender, data);

		if ( State == ESubState.Exiting ) return;

		ProcessUI( sender, data );

	}

	/// <summary>
	/// Dos the input.
	/// </summary>
	protected override void DoInput ()
	{
		base.DoInput ();

		if ( Input.GetButtonDown("Pause"))
		{

			ChangeController( 1 );

			SceneRoot.Instance.LevelToLoad = SceneRoot.Instance.GetRandomLevelName ( PlayerPrefs.GetInt( Constants.LevelsPassed, 1 ));
			State = ESubState.Exiting;
		}
		else if ( Input.GetButtonDown("Exit"))
		{
			Application.Quit();
		}

		if ( Input.GetMouseButtonDown(0))			
		{						
			SceneRoot.Instance.Iteract(IteractionArgs.EIteractions.Sweem, new Vector2( Input.mousePosition.x, Input.mousePosition.y ));
		}	
	}

	/// <summary>
	/// Mains the menu user interface process.
	/// </summary>
	private void ProcessUI( GameObject sender, Object data )
	{
		if ( sender.name == "btn_Play" )
		{
			SceneRoot.Instance.LevelToLoad = SceneRoot.Instance.GetRandomLevelName ( PlayerPrefs.GetInt( Constants.LevelsPassed, 1 ));
			State = ESubState.Exiting;
		}
		else if ( sender.name == "btn_Controller" )
		{
			ChangeController();
		}
	}
	
	/// <summary>
	/// Changes the controller.
	/// </summary>
	private void ChangeController()
	{
		int controller = PlayerPrefs.GetInt( Constants.Controller, 0 );

		controller++;
		if ( controller > 1 )
		{
			controller = 0;
		}

		PlayerPrefs.SetInt( Constants.Controller, controller );

		SetControllerSprite();
	}

	/// <summary>
	/// Changes the controller. 0 - mouse, 1 - joystic
	/// </summary>
	private void ChangeController(int controllerId )
	{			
		PlayerPrefs.SetInt( Constants.Controller, controllerId );
		
		SetControllerSprite();
	}

	
	/// <summary>
	/// Sets the controller sprite.
	/// </summary>
	private void SetControllerSprite()
	{
		int controller = PlayerPrefs.GetInt( Constants.Controller, 0 );
		
		if ( controller == 0 )
		{
			mController.sprite = mDataContainer.mouse;
		}
		else if ( controller == 1 )
		{
			mController.sprite = mDataContainer.joystick;
		}
	}

}
