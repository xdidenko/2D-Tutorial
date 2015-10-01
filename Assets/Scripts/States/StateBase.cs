/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;

/// <summary>
/// State base.
/// </summary>
public class StateBase  {

	public enum ESubState { Entering, Pause, Default, Exiting }; 

	public virtual string Name { get{return "Default"; } }

	protected ESubState mCurrentState;

	protected SpriteRenderer mFadeInOutSprite;

	private float mAudioFadeoutSpeed; //calculated dynamically
		
	/// <summary>
	/// Pause this instance.
	/// </summary>
	private void Pause()
	{
		Time.timeScale = 0.0f;
		mFadeInOutSprite.gameObject.SetActive(true);
		mFadeInOutSprite.color = new Color( 1f, 1f, 1f, 1f );
	}

	/// <summary>
	/// Uns the pause.
	/// </summary>
	private void UnPause()
	{
		Time.timeScale = 1.0f;
		mFadeInOutSprite.gameObject.SetActive(false);
		mFadeInOutSprite.color = new Color( 1f, 1f, 1f, 0f );
	}

	/// <summary>
	/// Raises the user interface action event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="data">Data.</param>
	public virtual void OnUIAction( GameObject sender, Object data )
	{

	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	public virtual void Awake()
	{
		SetAudioListenerVolume();
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	public virtual void Start()
	{

		mFadeInOutSprite = GameObject.Find("FadeInOutPlane").GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		mFadeInOutSprite.enabled = true;
		Debug.Log( "Start " + this );
	}
		
	/// <summary>
	/// Update this instance.
	/// </summary>
	public virtual void Update()
	{
		//
		if ( State == ESubState.Entering )
		{

			float alpha = Mathf.MoveTowards( mFadeInOutSprite.color.a, 0f, 0.15f * Time.deltaTime );
			mFadeInOutSprite.color = new Color( 1f, 1f, 1f, alpha );
			
			if ( mFadeInOutSprite.color.a == 0 )
			{
				mFadeInOutSprite.gameObject.SetActive(false);



				State = ESubState.Default;
			}
		}
		//
		if ( State == ESubState.Exiting )
		{
			float alpha = Mathf.MoveTowards( mFadeInOutSprite.color.a, 1f, 0.3f * Time.deltaTime );

			AudioListener.volume = Mathf.MoveTowards( AudioListener.volume, 0f, mAudioFadeoutSpeed * Time.deltaTime );

			mFadeInOutSprite.color = new Color( 1f, 1f, 1f, alpha );
			
			if ( mFadeInOutSprite.color.a == 1f )
			{
				SceneRoot.Instance.LoadNextLevel();
			}
		}

		DoInput();

	}

	/// <summary>
	/// Dos the input.
	/// </summary>
	protected virtual void DoInput()
	{

	}

	/// <summary>
	/// Toggles the cursor.
	/// </summary>
	protected virtual void SetCursor()
	{
		if ( SceneRoot.Instance.ControllerType == EControllerType.Joystic )
		{
			Screen.showCursor = false;
		}
		else if ( SceneRoot.Instance.ControllerType == EControllerType.MouseTouch )
		{
			Screen.showCursor = true;
		}
	}

	/// <summary>
	/// Toggles the cursor.
	/// </summary>
	protected virtual void SetCursor(bool cursor)
	{
		Screen.showCursor = cursor;
	}


	/// <summary>
	/// Returns a <see cref="System.String"/> that represents the current <see cref="StateBase"/>.
	/// </summary>
	/// <returns>A <see cref="System.String"/> that represents the current <see cref="StateBase"/>.</returns>
	public override string ToString ()
	{
		return string.Format ("[StateBase: Name={0}, State={1}]", Name, State);
	}	
	/// <summary>
	/// Sets the audio listener volume.
	/// </summary>
	protected void SetAudioListenerVolume()
	{
		AudioListener.volume = 1f;
	}
	#region properties
	/// <summary>
	/// Sets the state.
	/// </summary>
	/// <value>The state.</value>
	public ESubState State
	{
		get
		{
			return mCurrentState;
		}
		set
		{
			if ( value == ESubState.Pause )
			{
				Pause();
			}
			else if ( mCurrentState == ESubState.Pause && value != ESubState.Pause )
			{
				UnPause();
			}
			else if ( value == ESubState.Exiting )
			{
				mFadeInOutSprite.gameObject.SetActive(true);

				SetCursor( false );

				mAudioFadeoutSpeed = 0.35f / ( 1f - mFadeInOutSprite.color.a ) / AudioListener.volume;
			}

			mCurrentState = value;

			Debug.Log( this );
		}
	}

	#endregion

}
