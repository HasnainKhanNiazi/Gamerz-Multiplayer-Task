using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueTeamCounter : MonoBehaviour {

	GameObject[] RedCount;
	GameObject[] BlueCount;
	public Text Players_Info_Text;
	private int TotalPlayers;
	public Text WinStateText;

	// Use this for initialization
	void Start () {
		RedCount = GameObject.FindGameObjectsWithTag ("Blue");
		Players_Info_Text.text = BlueCount.Length.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		BlueCount = GameObject.FindGameObjectsWithTag ("Blue");
		RedCount = GameObject.FindGameObjectsWithTag ("Red");
		Players_Info_Text.text = BlueCount.Length.ToString () + "/" + TotalPlayers;

		CheckWinState ();
	}

	public void SetTotalPlayers(int TP){
		TotalPlayers = TP;
	}

	void CheckWinState(){
		if (BlueCount.Length == 0 && RedCount.Length > 0 && TotalPlayers != 0) {
			WinStateText.gameObject.SetActive (true);
			WinStateText.color = Color.red;
			WinStateText.text = "Red Team Won";
			Time.timeScale = 0f;
		}
	}
}