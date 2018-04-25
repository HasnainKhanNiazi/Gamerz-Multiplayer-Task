using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Health : MonoBehaviour {
	float health = 100f;

	//Wait For Death Animation To End
	float timer = 0.0f;
	float timerMax = 3f;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		SetAllPlayers ();
	}

	// Update is called once per frame
	void Update () {
		if (timer < timerMax) {
			if (health <= 0) {
				GetComponent<CapsuleCollider> ().enabled = false;
				GetComponent<AIManager> ().enabled = false;
				GetComponent<ThirdPersonCharacter> ().enabled = false;
				timer += Time.deltaTime;
				anim.SetBool ("Death",true);
			}
		}
		else if (timer > timerMax) {
			DestroyPlayer ();
		}
	}

	[PunRPC]
	public void DoDamage(float damage){
		health -= damage;
	}

	void DestroyPlayer(){
		if (GetComponent<PhotonView> ().instantiationId == 0) {
			Destroy (gameObject);
		}else{
			if (GetComponent<PhotonView>().isMine) {
					PhotonNetwork.Destroy (gameObject);
			}
		}
	}


	// TO reduce bandwidth consumption I am using a local method instead of RPC
	void SetAllPlayers(){
		GameObject[] GB = GameObject.FindGameObjectsWithTag (transform.tag);
		if (transform.tag == "Red") {
			RedTeamCounter RTC = GameObject.FindObjectOfType<RedTeamCounter> ();
			RTC.SetTotalPlayers (GB.Length);
		}
		else if (transform.tag == "Blue") {
			BlueTeamCounter BTC = GameObject.FindObjectOfType<BlueTeamCounter> ();
			BTC.SetTotalPlayers (GB.Length);
		}
	}
}