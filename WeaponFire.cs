using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponFire : MonoBehaviour {

	public Rigidbody gravityBall;
	public Rigidbody lightBall;
	public Transform grappleBeam;
	public Rigidbody decoyBall;
	public Rigidbody grappleParticle;
	public GameObject weaponTypeIndicatorLight;
	//WeaponIndicatorLightChange weaponIndicatorLightScript;
	public float power = 5000f;
	public float moveSpeed = 500f;
	public Transform player;
	//GameObject gravityHole;
	//public Absorb absorbScript;
	public bool absorbing;
	public bool showBeam = false;
	float shotTimer;
	private int shotCounter;
	public float waitTime = 7.0f;
	public bool lightBallOn;
	public bool gravityBallOn;
	public bool netShotOn;
	public bool grappleBeamOn;
	Quaternion playerView;
	Animator anim;
	public Slider WeaponSlider;  //reference for slider


	// Use this for initialization
	void Start () {
	
	//	gravityHole = GameObject.Find("absorber");
//		absorbScript = gravityHole.GetComponent<Absorb>();

		shotCounter = 0;
		lightBallOn = false;
		gravityBallOn = false;
		netShotOn = false;
		grappleBeamOn = false;
		anim = GetComponent<Animator>();



	}




	// Update is called once per frame
	void Update () {

		//turn on gravity ball
		if(Input.GetKeyDown(KeyCode.Z)) {
			gravityBallOn=true;
			lightBallOn=false;
			netShotOn=false;
			grappleBeamOn=false;

			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().gravityGunOn = true;
			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().flareGunOn = false;
			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().decoyGunOn = false;
			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().grappleGunOn = false;

		}
		//turn on light ball
		if(Input.GetKeyDown(KeyCode.X)) {
			gravityBallOn=false;
			lightBallOn=true;
			netShotOn=false;
			grappleBeamOn=false;

			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().gravityGunOn = false;
			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().flareGunOn = true;
			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().decoyGunOn = false;
			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().grappleGunOn = false;

		}
		//turn on net shot
		if(Input.GetKeyDown(KeyCode.C)) {
			gravityBallOn=false;
			lightBallOn=false;
			netShotOn=true;
			grappleBeamOn=false;

			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().gravityGunOn = false;
			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().flareGunOn = false;
			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().decoyGunOn = true;
			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().grappleGunOn = false;
		}
		if(Input.GetKeyDown (KeyCode.V)) {
			gravityBallOn=false;
			lightBallOn=false;
			netShotOn=false;
			grappleBeamOn=true;

			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().gravityGunOn = false;
			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().flareGunOn = false;
			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().decoyGunOn = false;
			//weaponTypeIndicatorLight.GetComponent<WeaponIndicatorLightChange>().grappleGunOn = true;
		}


		float h = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
		float v = Input.GetAxis ("Vertical") * Time.deltaTime * moveSpeed;

		transform.Translate(h,v,0);

		if(Input.GetMouseButtonUp(0))
		{
			anim.SetBool("Fire",true);
			if(Time.time >= shotTimer + waitTime || shotCounter == 0) {
				if(gravityBallOn) {
					Rigidbody gravBallClone = Instantiate(gravityBall, transform.position + (transform.forward * 0.6f), transform.rotation) as Rigidbody;
					Vector3 forward = transform.TransformDirection(Vector3.forward);
					gravBallClone.AddForce(forward*1500);
					shotCounter = shotCounter + 1;
					//Destroy (gravBallClone.gameObject, 5.0f);
				}
				else if(lightBallOn) {
					Rigidbody lightBallClone = Instantiate(lightBall, transform.position + (transform.forward * 0.6f), transform.rotation) as Rigidbody;
					Vector3 forward = transform.TransformDirection(Vector3.forward);
					lightBallClone.AddForce(forward*1500);
					shotCounter = shotCounter + 1;
				}
				else if(netShotOn) {
					Rigidbody decoyBallClone = Instantiate(decoyBall, transform.position + (transform.forward * 1.0f) + (transform.up * 0.0f), transform.rotation) as Rigidbody;
					shotCounter = shotCounter + 1;
				}
				else if(grappleBeamOn) {
					//Instantiate(grappleBeam, transform.position + (transform.forward * 0.6f), transform.rotation);
					Rigidbody grappleShot = Instantiate(grappleParticle, transform.position + (transform.forward * 0.6f), transform.rotation) as Rigidbody;
					Vector3 forward = transform.TransformDirection(Vector3.forward);
					grappleShot.AddForce(forward*500);

					shotCounter = shotCounter+1;
				}
				shotTimer = Time.time;
			}
		}

	}
}
