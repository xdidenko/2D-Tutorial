/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EControllerType{ MouseTouch, Joystic };

/// <summary>
/// Scene root.
/// </summary>
public partial class SceneRoot : MonoBehaviour {

	private static SceneRoot instance;
		
	private Hero mHero;
	private Actors mActors;	
	private Thresure mThresures; 

	//States
	private List<StateBase> mStates;
	private StateBase mCurrentState;
	private float mStartExitingTime;
	private string mLoadingLevel;
		
	//
	public static SceneRoot Instance
	{
		get { return instance; }
	}
	
	/// <summary>
	/// Raises the level complete event.
	/// </summary>
	public void LevelComplete()
	{
		LevelToLoad = GetNextLevelName();
		
		int progress = PlayerPrefs.GetInt( Constants.LevelsPassed, 0 );

		int nextLevel = GetLevel() + 1;

		if( nextLevel > progress )
		{
			PlayerPrefs.SetInt( Constants.LevelsPassed, nextLevel );		
		}

		mCurrentState.State = StateBase.ESubState.Exiting;
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
		mCurrentState.Start();
	}
	
	/// <summary>
	/// Raises the user interface action event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="data">Data.</param>
	public void OnUIAction( GameObject sender, Object data )
	{
		mCurrentState.OnUIAction( sender, data );
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {

		DoInput();

		mCurrentState.Update();
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	private void DoInput()
	{

	}
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		//
		instance = this;

		//
		mStates = new List<StateBase>();
		mStates.Add( new StateMenu() );
		mStates.Add( new StateEnd() );

		foreach ( StateBase sb in mStates )
		{
			if ( sb.Name == Application.loadedLevelName )
			{
				mCurrentState = sb;
			}
		}

		if ( mCurrentState == null )
		{
			mCurrentState = new StateLevel();
		}

		mCurrentState.Awake();
	}

	/// <summary>
	/// Gets the level.
	/// </summary>
	/// <returns>The level.</returns>
	public static int GetLevel()
	{
		foreach( KeyValuePair<int, string[]> kvp in Constants.Levels )
		{
			foreach ( string s in kvp.Value )
			{
				if ( Application.loadedLevelName == s )
				{
					return kvp.Key;
				}
			}		
		}

		return -1;
	}

	/// <summary>
	/// Gets the random name of the level.
	/// </summary>
	/// <returns>The random level name.</returns>
	/// <param name="level">Level.</param>
	public string GetRandomLevelName(int level)
	{
		string[] levels = Constants.Levels[ level ];

		int rnd = UnityEngine.Random.Range( 0, levels.Length );

		return levels[rnd];
	}

	/// <summary>
	/// Gets the name of the next level.
	/// </summary>
	/// <returns>The next level name.</returns>
	public string GetNextLevelName()
	{
		int currentLevel = GetLevel();
		int nextLevel = currentLevel + 1;

		return GetRandomLevelName( nextLevel );
	}

	/// <summary>
	/// Games the over.
	/// </summary>
	public void GameOver()
	{
		LevelToLoad = GetRandomLevelName( GetLevel() );
		mCurrentState.State = StateBase.ESubState.Exiting;
	}

	/// <summary>
	/// Loads the level.
	/// </summary>
	/// <param name="level">Level.</param>
	public void LoadLevel( int level )
	{
		Application.LoadLevel( level );
	}

	/// <summary>
	/// Loads the level.
	/// </summary>
	/// <param name="level">Level.</param>
	public void LoadLevel( string levelName )
	{
		LevelToLoad = levelName;
		Application.LoadLevel(levelName);
	}
	
	/// <summary>
	/// Loads the next level.
	/// </summary>
	public void LoadNextLevel()
	{
		LoadLevel( LevelToLoad );
	}
			
	#region properties
	/// <summary>
	/// Gets the type of the controller.
	/// </summary>
	/// <value>The type of the controller.</value>
	public EControllerType ControllerType
	{
		get
		{
			return (EControllerType) PlayerPrefs.GetInt( Constants.Controller, 0 );
		}
	}

	/// <summary>
	/// Gets or sets the level to load.
	/// </summary>
	/// <value>The level to load.</value>
	public string LevelToLoad 
	{
		get
		{
			return mLoadingLevel;
		}
		set 
		{
			mLoadingLevel = value;
		}
	}
			
	/// <summary>
	/// Gets or sets the main hero.
	/// </summary>
	/// <value>The main hero.</value>
	public Hero MainHero
	{
		get
		{
			return mHero;
		}
		set
		{
			mHero = value;
		}
	}
	
	/// <summary>
	/// Gets or sets the main hero.
	/// </summary>
	/// <value>The main hero.</value>
	public Actors SceneActors
	{
		get
		{
			return mActors;
		}
		set
		{
			mActors = value;
		}
	}
			
	/// <summary>
	/// Gets or sets the scene robot interests.
	/// </summary>
	/// <value>The scene robot interests.</value>
	public Thresure SceneThresures
	{
		get
		{
			return mThresures;
		}
		set
		{
			mThresures = value;
		}
	}
	
	/// <summary>
	/// Sets the state of the scene.
	/// </summary>
	/// <value>The state of the scene.</value>
	public StateBase SceneState
	{
		get
		{
			return mCurrentState;
		}
	}
	#endregion

}
