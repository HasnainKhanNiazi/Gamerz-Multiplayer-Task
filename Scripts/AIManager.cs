using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson{
	
	public class AIManager : MonoBehaviour {
		
		private NavMeshAgent AI_Agent;		
		private ThirdPersonCharacter Character;
		public ParticleSystem MuzzleFlash;

		// Variables for firerate
		float fireRate = 0.5f;
		float nextFire;

		//Variables For Overlapsphere
		float radius = 5f;
		Collider[] Enemies;

		public enum States {
			PATROL,
			SHOOT
		}

		public States state;
		private bool alive;

		// Variable for Patrolling
		[Space(10)]
		[Header("Variables for Patrlling")]
		private WayPoint[] waypoints;
		private int waypointIndex = 0;
		public float patrolSpeed = 0.5f;


		// Variables for Shooting
		[Space(10)]
		[Header("Variables for Shooting")]
		public bool DoFire = false;


		// Use this for initialization
		void Start () {
			//Get all Waypoints here
			waypoints = GameObject.FindObjectsOfType<WayPoint>();
			waypointIndex = Random.Range (0, waypoints.Length);

			AI_Agent = GetComponent<NavMeshAgent> ();
			Character = GetComponent<ThirdPersonCharacter> ();

			AI_Agent.updatePosition = true;
			state = AIManager.States.PATROL;
			alive = true;
			patrolSpeed = 0.5f;
			//Here I need to start Finite State Machine

			StartCoroutine ("FiniteStateMachine");
		}

		IEnumerator FiniteStateMachine(){
			while (alive) {
				switch (state) {
				case States.PATROL:
					Patrol ();
					break;
				case States.SHOOT:
					Shoot ();
					break;
				}
				yield return null;
			}
		}

		void Patrol(){
			AI_Agent.speed = patrolSpeed;
			if (Vector3.Distance (transform.position, waypoints [waypointIndex].transform.position) >= 1) {
				AI_Agent.SetDestination (waypoints[waypointIndex].transform.position);
				Character.Move (AI_Agent.desiredVelocity, false, false);
			}
			if (Vector3.Distance (transform.position, waypoints [waypointIndex].transform.position) <= 1) {
				waypointIndex = Random.Range (0, waypoints.Length);
			}
			else {
				Character.Move (Vector3.zero,false,false);
			}
		}

		void Shoot(){
			AI_Agent.speed = 0;
			AI_Agent.SetDestination (Vector3.zero);
			Character.Move (Vector3.zero, false, false);

			GetComponent<PhotonView> ().RPC ("ActiveMuzzleFlashNetwork", PhotonTargets.All);
			foreach (Collider col in Enemies) {
				if (transform.tag != col.tag && col.tag != "Floor") {
					if (Time.time > nextFire) {
						int rand = Random.Range (0, 2);
						//using a  random number for miss rate
						if (rand == 1) {
							transform.LookAt (col.transform);
							Camera.main.GetComponent<AudioSource> ().PlayOneShot (Resources.Load ("ak47") as AudioClip);
							col.GetComponent<PhotonView> ().RPC ("DoDamage", PhotonTargets.AllBuffered, 30f);
							nextFire = Time.time + fireRate;
						}
					}
				}
			}
		}

		// Update is called once per frame
		void Update () {
			Enemies = Physics.OverlapSphere (transform.position,radius);
			foreach (Collider col in Enemies) {
				if (transform.tag != col.tag && col.tag != "Floor") {
					state = AIManager.States.SHOOT;
					return;
				}
			}
			state = AIManager.States.PATROL;
		}

		[PunRPC]
		void ActiveMuzzleFlashNetwork(){
			MuzzleFlash.Play ();
		}

	
	}		
}