using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackupPower : MonoBehaviour {

	public bool isBackupOn = true;
	public GameObject flashlight;
	public GameObject generator;
	public float backUpPowerTime;
	public float flashLightTime;
	public float elapsedTime;
	private GeneratorScript power;
	private Light light;
	private bool timeUp = true;
	private bool lightDead = false;
	public Slider lightSlider;  //reference for slider
	private float lightVal = 0;
	// Use this for initialization
	void Start () {
		light = flashlight.GetComponent<Light> ();
		power = generator.GetComponent<GeneratorScript> ();
		lightSlider.maxValue = flashLightTime;
		lightVal = flashLightTime;
	}
	

	// Update is called once per frame
	void Update () {
		Flashlight ();
	}

	void Flashlight(){
		if (elapsedTime < 0)
			elapsedTime = 0;
		if (isBackupOn) {
			if (elapsedTime > flashLightTime) {
				isBackupOn = false;
				timeUp = false;
				light.intensity = 0;
				lightSlider.value = 0;
				lightDead = true;
			}
			if (Input.GetButtonDown("Flash")){
				light.intensity = 0;
				isBackupOn = false;
				timeUp = false;
				lightSlider.value = 0;
				
			}
			if (!power.lightsOn && timeUp){
				elapsedTime = elapsedTime + Time.deltaTime;
				lightVal = lightVal - Time.deltaTime;
				lightSlider.value = lightVal - Time.deltaTime;
			
			}
			else{
				elapsedTime = elapsedTime - Time.deltaTime;
				lightVal = lightVal + Time.deltaTime;
				lightSlider.value = lightVal + Time.deltaTime;

			}
		} else {
			if (Input.GetButtonDown("Flash") && !lightDead){
				light.intensity = 3.2f;
				isBackupOn = true;
				timeUp = true;
			}
			else if(Input.GetButtonDown("Flash") && lightDead){
				if(elapsedTime == 0){
					lightDead = false;
					light.intensity = 3.2f;
					isBackupOn = true;
					timeUp = true;
				}
			}
			if (!power.lightsOn && timeUp){
				elapsedTime = elapsedTime + Time.deltaTime;
				lightVal = lightVal - Time.deltaTime;
				lightSlider.value = lightVal - Time.deltaTime;
			}
			else{
				elapsedTime = elapsedTime - Time.deltaTime;
				lightVal = lightVal + Time.deltaTime;
				lightSlider.value = lightVal + Time.deltaTime;
			}
		}
	}
}
