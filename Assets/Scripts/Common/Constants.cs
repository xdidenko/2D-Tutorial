/// <summary>
/// Games (C) 2013-2014
/// www.games.com
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Constants.
/// </summary>
public static class Constants {

	//Levels and sublevels 
	public static readonly Dictionary<int, string[]> Levels = new Dictionary<int, string[]>
	{
		{ 0, new string[]{"start"} },
		{ 1, new string[]{"1"}},
		{ 2, new string[]{"2"}},
		{ 3, new string[]{"end"} },
	};

	//Levels and music
	public static readonly Dictionary<int, int> LevelToMusic = new Dictionary<int, int>
	{
		{ 0, 0 },
		{ 1, 0 },
		{ 2, 0 },
		{ 3, 0 },
	};
	
	public const string MenuLevelName = "start";

	//Menu
	public const float MenuPushRigidbodyForce = 1000f; //Newtons
	public const float MenuCameraFlySpeed = 2f; //Deltatime

	//Hero
	public static float HeroMinVelocityToReact = 2f; // m/sec	
	public static float HeroPushForce = 250000; //Newtons

	public static float StuckTimeThreshold = 1.5f; //sec

	//Dron
	public static float DronHuntSpeed = 4f; // m/s
	public static float DronHuntAcceleration = 20f; // m/s^2
	public static float DronHuntAngularSpeed = 200f; // deltaangle/sec
	public static float DronMaxHuntTime = 4f; // hunt time
	public static float DronfloatDistance = 20f; //meters	

	//App
	public const string LevelsPassed = "LevelsPassed";
	public const string Controller = "Controller";
	public const float CameraFoollowHeroSpeed = 2.8f; //deltatime


}
