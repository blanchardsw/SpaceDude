using UnityEngine;
using System.Collections;

public class lightingScript : MonoBehaviour {

	public GameObject generator;
	private GeneratorScript lightpower;
	private Light lights; 
	// Use this for initialization
	void Start () {
		lightpower = generator.GetComponent<GeneratorScript>();
		lights = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if (lightpower.lightsOn) {
			lights.enabled = true;
		} else lights.enabled = false;
	}
}
