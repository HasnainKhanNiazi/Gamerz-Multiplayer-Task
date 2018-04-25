using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public string TeamTag;
	public GameObject SpawningPanel;
	public GameObject ChooseTeamPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TeamRed(){
		TeamTag = "Red";	
		ChooseTeamPanel.SetActive (false);
		SpawningPanel.SetActive (true);
	}

	public void TeamBlue(){
		TeamTag = "Blue";	
		ChooseTeamPanel.SetActive (false);
		SpawningPanel.SetActive (true);
	}
}