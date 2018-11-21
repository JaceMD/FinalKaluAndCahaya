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
	public GameObject[] eyeObjs = new GameObject[9];
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
	private AudioSource source;
	public AudioClip alertGrowlSFX, spottedGrowlSFX;
	private bool canAlertGrowl = true;

	// Use this for initialization
	void Start () {
		source = this.gameObject.GetComponent<AudioSource> ();
		player = GameObject.Find ("Player");
		playerT = player.transform;
		playerSM = player.GetComponent<StealthManager> ();
		centrePRCTarget = GameObject.Find ("PlayerRayCastTarget9");
		currentAlertTime = alertTime;
		EnemyDetectionUI.SetActive (false);
		initialRotDirection = transform.rotation.eulerAngles;
		initialPos = this.transform.position;
		charAnim.SetInteger ("State", 0);
		for (int i = 0; i < 9;i++){
			eyeObjs [i].GetComponent<SkinnedMeshRenderer> ().material = alertMat;
		}
	}
	
	// Update is called once per frame
	void Update () {
		nightMode = player.gameObject.GetComponent<MainCharacterController>().ChecknightMode();

		if (gameOver == true && (Time.time >= startGameOverTime + 2f)) {
			canAlertGrowl = true;
			Debug.Log ("GameoVer");
			Vector3 checkpointPos = GameObject.Find ("KillCollider").GetComponent<RestartLevelController> ().CheckPointLocation;
			player.transform.position = checkpointPos;

			player.GetComponent<MainCharacterController> ().EnableControls ();
			if (nightMode == true) {
				GameObject.Find ("Support Character").transform.position = new Vector3 (checkpointPos.x, checkpointPos.y + 10f, checkpointPos.z);
				GameObject.Find ("Support Character").GetComponent<SuppCharController> ().EnableControls ();
				GameObject.Find ("Oversee").GetComponent<OverseeController> ().ResetOverseePos ();
			}
			gameOver = false;
			currentAlertTime = alertTime;
			this.transform.position = initialPos;
			for (int i = 0; i < 9;i++){
				eyeObjs [i].GetComponent<SkinnedMeshRenderer> ().material = alertMat;
			}
			charAnim.SetInteger ("State", 0);
		} else if(gameOver == true) {
			if(canMoveTowardPlayer == true){
				moveTowardPlayer();

			}
		}
		if (alerted == true && canAlertGrowl == true) {
			canAlertGrowl = false;
			source.PlayOneShot (alertGrowlSFX, 1f);
		}

		if (alerted == true && gameOver == false) {
			
			charAnim.SetInteger ("State", 1);
			currentAlertTime -= Time.deltaTime;
			EnemyDetectionUI.gameObject.SetActive (true);
			rotateTowardPlayer ();
			if (currentAlertTime <= 0f) {
				source.PlayOneShot (spottedGrowlSFX, 0.4f);
				gameOver = true;
				startGameOverTime = Time.time;
				EnemyDetectionUI.gameObject.GetComponent<MeshRenderer> ().material = spottedMat;
				for (int i = 0; i < 9;i++){
					eyeObjs [i].GetComponent<SkinnedMeshRenderer> ().material = spottedMat;
				}

				player.GetComponent<MainCharacterController> ().DisableControls ();
				if (nightMode == true) {
					GameObject.Find ("Support Character").GetComponent<SuppCharController> ().followMode = true;
					GameObject.Find ("Support Character").GetComponent<SuppCharController> ().DisableControls ();
				}

                ActivateFade.Activate = true;
			} else {
				EnemyDetectionUI.gameObject.GetComponent<MeshRenderer> ().material = alertMat;
				for (int i = 0; i < 9;i++){
					eyeObjs [i].GetComponent<SkinnedMeshRenderer> ().material = alertMat;
				}
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
		Debug.Log ("moving towards player");
		playerDistance = Vector3.Magnitude (playerT.position - transform.position);
		if (playerDistance >= 3f) {
			charAnim.SetInteger ("State", 2);
			transform.position = Vector3.MoveTowards (transform.position, playerT.position, moveTowardPlayerSpeed * Time.deltaTime);
		} else {
			charAnim.SetInteger ("State", 1);
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
					canAlertGrowl = true;
				}

			} else {
				alerted = false;
				canAlertGrowl = true;
			}

			//If Player Is In Light - Get Alerted - After alertTime seconds chase player and game over
			//else continue surveying area.
		} else {
			alerted = false;
			canAlertGrowl = true;
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
