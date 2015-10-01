using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	
	private static MusicManager instance = null;

	public AudioClip[] mAudioClips;


	public static MusicManager Instance {
		get { return instance; }
	}
	
	void Awake() 
	{
		//Change music accroding to level
		if ( audio.clip != mAudioClips[ Constants.LevelToMusic[SceneRoot.GetLevel()]] )
		{
			audio.clip = mAudioClips[ Constants.LevelToMusic[SceneRoot.GetLevel()]];
		}

		if (instance != null && instance != this) 
		{
			audio.Stop();
			if(instance.audio.clip != audio.clip)
			{
				instance.audio.clip = audio.clip;
				instance.audio.volume = audio.volume;
				instance.audio.Play();
			}
			
			Destroy(this.gameObject);
			return;
		} 

		instance = this;
		audio.Play ();
		DontDestroyOnLoad(this.gameObject);
	}
}