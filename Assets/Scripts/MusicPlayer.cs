using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	// Use this for initialization
	void Awake ()
	{
		Debug.Log (Time.time + ": Awake()");
		if (instance != null) {
			Destroy (gameObject);
			Debug.Log (Time.time + ": Duplicated copy of MusicPlayer was destroyed");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}
	void Start () {
		Debug.Log (Time.time + ": Start()");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
