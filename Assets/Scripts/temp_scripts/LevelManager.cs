using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance = null;

	void Awake ()
	{
		if (instance != null) {
			Destroy (gameObject);
			Debug.Log (Time.time + ": Duplicated copy of Level Manager was destroyed");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	public void LoadLevel (string name)
	{
		Debug.Log (Time.time + ": Level load requested: " + name);
		SceneManager.LoadScene(name);
	}

	public void QuitRequest()
	{
		Debug.Log (Time.time + ": Quit action requested: ");
		Application.Quit();
	}

	public void LoadNextLevel ()
	{
		Debug.Log (Time.time + ": Loading next scene... ");
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	public void BrickDestroyed ()
	{
		if (Brick.bricksLeft <= 0) {
			Invoke ("LoadNextLevel", 2f);
			Ball[] balls = FindObjectsOfType<Ball> ();
			foreach (Ball ball in balls) {
				//Destroy (ball.gameObject);
				ball.DestroyBall();
			}
		}
	} 
}
