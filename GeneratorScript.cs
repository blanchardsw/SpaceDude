using UnityEngine;
using System.Collections;

public class GeneratorScript : MonoBehaviour {
	public GameObject player;
	public bool lightsOn = false;
	private PlayerHealth playerHealth;

	void Start ()
	{	
		playerHealth = player.GetComponent<PlayerHealth>();
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
						if (playerHealth.hasGeneratorKey) {
								lightsOn = true;
						}
				}
		else if (other.gameObject.tag == "Mutant") {
				lightsOn = false;
			}
		}
}
