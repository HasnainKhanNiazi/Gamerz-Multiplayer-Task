using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedTeamCounter : MonoBehaviour {
	
	GameObject[] RedCount;
	GameObject[] BlueCount;
	public Text Players_Info_Text;
	private int TotalPlayers = 0;
	public Text WinStateText;

	// Use this for initialization
	void Start () {
		RedCount = GameObject.FindGameObjectsWithTag ("Red");
		Players_Info_Text.text = RedCount.Length.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		RedCount = GameObject.FindGameObjectsWithTag ("Red");
		BlueCount = GameObject.FindGameObjectsWithTag ("Blue");
		Players_Info_Text.text = RedCount.Length.ToString () + "/" + TotalPlayers;

		CheckWinState ();
	}

	public void SetTotalPlayers(int TP){
		TotalPlayers = TP;
	}

	void CheckWinState(){
		if (RedCount.Length == 0 && BlueCount.Length > 0 && TotalPlayers != 0) {
			WinStateText.gameObject.SetActive (true);
			WinStateText.color = Color.blue;
			WinStateText.text = "Blue Team Won";
			Time.timeScale = 0f;
		}

	}
}