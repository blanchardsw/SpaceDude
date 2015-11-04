using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public float elapsedTime;
	public bool takingDamage;
	public float regenTime;
	public float health;
	public float regenRate;
	public Slider healthBarSlider;  //reference for slider
	public float damageRate;


	// Use this for initialization
	void Start () {
		damageRate = .001f;
		health = 1f;
		healthBarSlider.value = health;
		regenRate = .0001f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!takingDamage && elapsedTime >= regenTime && health < 1) {
			health = health + regenRate;
			healthBarSlider.value = health + regenRate;
		}
		if (!takingDamage && health < 1) {
			elapsedTime = elapsedTime + Time.deltaTime; 
		}
		if (takingDamage && healthBarSlider.value > 0) {
			elapsedTime = 0;
			healthBarSlider.value = health - damageRate;
			health = health - damageRate;
		}
		if (health > 1)
			health = 1;
	}
}
