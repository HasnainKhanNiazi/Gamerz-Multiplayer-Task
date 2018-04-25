 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {

	[Space(10)]
	[Header("Set Offline Mode for testing purposes")]
	[Space]
	public bool offlinemode = false;

	bool connected = false;

	public GameObject SpawningPanel;
	public GameObject TeamPanel;
	public InputField Number;
	UIManager UI_Manager;

	public GameObject RedTeamSpawnPoint;
	public GameObject BlueTeamSpawnPoint;

	void Start () {
		PhotonNetwork.ConnectUsingSettings ("G1");
		UI_Manager = GetComponent<UIManager> ();
	}

	void Update(){
		
	}

	void OnJoinedLobby(){
		Debug.Log ("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log ("OnPhotonRandomJoinFailed");
		CreateRoom ();
	}

	void OnGUI(){
		if (connected == true) {
			
		}
	}

	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");

		TeamPanel.SetActive (true);
	}

	void CreateRoom(){
		PhotonNetwork.CreateRoom("Default");
	}

	void SpawnAgents(int number){
		string Tag = UI_Manager.TeamTag;
		Vector3 SpawnPoint;
		if (Tag == "Red") {
			SpawnPoint = RedTeamSpawnPoint.transform.position;
		}
		else {
			SpawnPoint = BlueTeamSpawnPoint.transform.position;
		}
		for (int i = 0; i < number; i++) {
			GameObject GB = PhotonNetwork.Instantiate (Tag, SpawnPoint, Quaternion.identity, 0);
		}
	}

	public void SpawnButton(){
		int number = int.Parse (Number.text);
		SpawnAgents (number);
		SpawningPanel.SetActive (false);
	}
}