#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class Punch : MonoBehaviour {
	public Texture2D punch1;
	public Texture2D punch2;
	public Texture2D nowPunch;
	public Texture2D punchSuccess;

	public float secondsPerFrame;
	public float frames;
	public float punchGForce;

	private ThalmicMyo myo;
	private Texture2D punch;
	private float startTime;
	private bool punched;
	
	void Start () {
		myo = GameObject.Find("Myo").GetComponent<ThalmicMyo> ();
		startTime = Time.time;
	}
	
	void OnGUI () {
		if (Time.time - startTime < frames * secondsPerFrame) {
			if (Mathf.RoundToInt ((Time.time - startTime) / secondsPerFrame) % 2 == 1) {
				punch = punch2;
			} else {
				punch = punch1;
			}
		} else {
			if (!punched) {
				punch = nowPunch;
			} else {
				punch = punchSuccess;
			}

			if (Mathf.Abs(myo.accelerometer.z) > punchGForce && !punched) {
				punched = true;
				//Debug.Log ("Calling GUI");
				//Debug.Log ("Success");
				StartCoroutine (delay ());
			}
		}
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), punch);
	}
	
	IEnumerator delay () {
		myo.Vibrate (Thalmic.Myo.VibrationType.Short);
		yield return new WaitForSeconds (1);
		Debug.Log ("Delayed 1 second");
		yield return null;
		Application.LoadLevel(4);
	}
	
	/*void FixedUpdate () {
		if (myo.pose == Thalmic.Myo.Pose.Fist) {
			Debug.Log ("GET READY TO RUMBLE");
		}
		if (myo.accelerometer.z > 0.5) {
			Debug.Log("YOU PUNCHED");
		}
	}*/
}
