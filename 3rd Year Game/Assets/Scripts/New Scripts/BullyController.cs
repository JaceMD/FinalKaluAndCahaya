using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyController : MonoBehaviour {

	public bool canMoveTowardPlayer = false;
	public float detectionRadius = 6f;
	public float minDetectionRadius = 2f;

	private float playerDistance;
	private GameObject player;
	private Transform playerT;

	private Ray visualDetectionRay;
	private GameObject centrePRCTarget;

	public float alertTime = 1.2f;
	private float currentAlertTime;

	public GameObject EnemyDetectionUI;
	public Material alertMat, spottedMat;
	private StealthManager playerSM; 

	private Vector3 initialRotDirection;
	public float rotationSpeed = 3f;
	public float moveTowardPlayerSpeed = 5f;

	[SerializeField]
	private bool alerted = false;
	[SerializeField]
	private bool gameOver = false;
	private float startGameOverTime;
    public FadeEffectController ActivateFade;

	private Vector3 initialPos;
	public Animator charAnim;
	private bool nightMode = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerT = player.transform;
		playerSM = player.GetComponent<StealthManager> ();
		centrePRCTarget = GameObject.Find ("PlayerRayCastTarget9");
		currentAlertTime = alertTime;
		EnemyDetectionUI.SetActive (false);
		initialRotDirection = transform.rotation.eulerAngles;
		initialPos = this.transform.position;
		charAnim.SetInteger ("State", 0);
	}
	
	// Update is called once per frame
	void Update () {
		nightMode = player.gameObject.GetComponent<MainCharacterController>().ChecknightMode();

		if (gameOver == true && (Time.time >= startGameOverTime + 2f)) {
			Debug.Log ("GameoVer");
			Vector3 checkpointPos = GameObject.Find ("KillCollider").GetComponent<RestartLevelController>().CheckPointLocation;
			player.transform.position = checkpointPos;

			player.GetComponent<MainCharacterController> ().EnableControls ();
			if (nightMode == true) {
				GameObject.Find ("Support Character").transform.position = new Vector3(checkpointPos.x, checkpointPos.y + 10f, checkpointPos.z);
				GameObject.Find ("Support Character").GetComponent<SuppCharController> ().EnableControls ();
				GameObject.Find ("Oversee").GetComponent<OverseeController> ().ResetOverseePos ();
			}
			gameOver = false;
			currentAlertTime = alertTime;
			this.transform.position = initialPos;
			charAnim.SetInteger ("State", 0);
		}
		if (alerted == true && gameOver == false) {
			charAnim.SetInteger ("State", 1);
			currentAlertTime -= Time.deltaTime;
			EnemyDetectionUI.gameObject.SetActive (true);
			rotateTowardPlayer ();
			if (currentAlertTime <= 0f) {
				gameOver = true;
				startGameOverTime = Time.time;
				EnemyDetectionUI.gameObject.GetComponent<MeshRenderer> ().material = spottedMat;

				if(canMoveTowardPlayer == true){
					moveTowardPlayer();
					charAnim.SetInteger ("State", 2);
				}
				player.GetComponent<MainCharacterController> ().DisableControls ();
				if (nightMode == true) {
					GameObject.Find ("Support Character").GetComponent<SuppCharController> ().followMode = true;
					GameObject.Find ("Support Character").GetComponent<SuppCharController> ().DisableControls ();
				}

                ActivateFade.Activate = true;
			} else {
				EnemyDetectionUI.gameObject.GetComponent<MeshRenderer> ().material = alertMat;
			}

		} 
		else if(alerted == false && gameOver == false){
			//continue surveying
			EnemyDetectionUI.SetActive (false);
			currentAlertTime = alertTime;
			charAnim.SetInteger ("State", 0);
			//rotateToNeutral ();
		}



	}

	void FixedUpdate(){
		checkPlayerDistance ();
	}

	void rotateTowardPlayer(){
		Vector3 playerDir = playerT.position - this.transform.position;
		playerDir.y = 0f;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (playerDir), Time.deltaTime * rotationSpeed);
	}
	void rotateToNeutral(){
		Vector3 dir = initialRotDirection - this.transform.position;
		dir.y = 0f;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), Time.deltaTime * rotationSpeed);
	}
	void moveTowardPlayer(){
		if (playerDistance >= 2f) {
			transform.position = Vector3.MoveTowards (transform.position, playerT.position, moveTowardPlayerSpeed * Time.deltaTime);
		}
	}

	void checkPlayerDistance(){
		playerDistance = Vector3.Magnitude (playerT.position - transform.position);
		//Debug.Log ("Player Distance: " + playerDistance);
		if (playerDistance <= minDetectionRadius) {
			alerted = true;
			currentAlertTime = -1f;
		}
		else if (playerDistance <= detectionRadius) {


			//CheckIfPlayerInLight
			RaycastHit hit;

			visualDetectionRay = new Ray (transform.position, centrePRCTarget.transform.position - transform.position);
			Debug.DrawLine (transform.position, centrePRCTarget.transform.position, Color.red);

			if (Physics.Raycast (visualDetectionRay, out hit, detectionRadius) && ((hit.transform.gameObject.tag == "PRCTarget") || hit.transform.gameObject.tag == "Player")) {

				bool playerInLight = player.gameObject.GetComponent<StealthManager> ().isPlayerInLight ();

				if (playerInLight == true || nightMode == false) {
					alerted = true;
					Debug.DrawLine (hit.point, hit.point + Vector3.up * 2f, Color.green);
				} else {
					alerted = false;
				}

			} else {
				alerted = false;
			}

			//If Player Is In Light - Get Alerted - After alertTime seconds chase player and game over
			//else continue surveying area.
		} else {
			alerted = false;
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
			alerted = true;
			currentAlertTime = -1f;
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, detectionRadius);
		Gizmos.DrawWireSphere (transform.position, minDetectionRadius);

	}
}
