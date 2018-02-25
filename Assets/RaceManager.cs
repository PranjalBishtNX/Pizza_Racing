using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour {
	public bool isRaceActive;



	[SerializeField]
	public float raceTime;

	[SerializeField]
	public float timerCountdown;

	[SerializeField]
	public Text raceCountDownTimer;

	[SerializeField]
	public GameObject raceUIPanel;

	[SerializeField]
	public Vehicle playerVehicle;

	[SerializeField]
	public GameObject playBtn;


	private float raceTimeStartTime;

	private float raceCountdownStartTime;
	private bool isRaceCountdownActive;
	// Use this for initialization
	void Start () {


		
	}
	public void startRace(){
	
		startCountdown ();
		raceUIPanel.SetActive (true);

	}
	public void startCountdown(){

		raceCountdownStartTime = Time.time;
		isRaceCountdownActive = true;
	}

	public void finishRace(){


	}
	public void stopRace(){

		//end the race
		//declare the winner
		//popup a ui
		//show stats

	}
	public void initiateRace(){
		isRaceActive = true;
		playerVehicle.enableCarPower ();
		playBtn.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
		if (isRaceCountdownActive && raceCountdownStartTime + 1 < Time.time) {
			if (timerCountdown == 0) {

				raceCountDownTimer.text = "GO!";


			} else if (timerCountdown > 0) {
				raceCountDownTimer.text = "" + timerCountdown;
				raceCountdownStartTime = Time.time;
			} else if (timerCountdown == -1) {
				raceCountDownTimer.gameObject.SetActive (false);
				isRaceCountdownActive = false;
				initiateRace ();
			}
			timerCountdown--;
		}

	}
}
