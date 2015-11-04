using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public float health = 100f;                         // How much health the player has left.
	public float resetAfterDeathTime = 5f;      // How much time from the player dying to the level resetting.
	public int keysAquired = 0;
	public int flairsAquired = 0;
	public bool hasGeneratorKey = false;
	private float timer;                            // A timer for counting to the reset of the level once the player is dead.
	private bool playerDead;						// A bool to show if the player is dead or not.
	public bool canUseGraple = false;
	public bool canUseFlairs = false;  
	public bool canUseblackhole = false; 
	public bool canUseDecoy = false; 

	void Start(){
		//PlayerPrefs.DeleteAll ();
		  keysAquired = PlayerPrefs.GetInt ("KeysAquired");
		  flairsAquired = PlayerPrefs.GetInt ("FlairsAquired");
		if (PlayerPrefs.GetInt ("canUseGraple") == 1)
			canUseGraple = true;
		else
			canUseGraple = false;
		if (PlayerPrefs.GetInt ("canUseFlairs") == 1)
			canUseFlairs = true;
		else
			canUseFlairs = false;
		if (PlayerPrefs.GetInt ("canUseblackhole") == 1)
			canUseblackhole = true;
		else
			canUseblackhole = false;
		if (PlayerPrefs.GetInt ("canUseDecoy") == 1)
			canUseDecoy = true;
		else
			canUseDecoy = false;
		}
	void Update ()
	{
		// If health is less than or equal to 0...
		if(health <= 0f)
		{
			// ... and if the player is not yet dead...
			if(!playerDead)
				// ... call the PlayerDying function.
				PlayerDying();
			else
			{
				// Otherwise, if the player is dead, call the PlayerDead and LevelReset functions.
				PlayerDead();
				LevelReset();
			}
		}
	}
	
	void PlayerDying ()
	{
		// The player is now dead.
		playerDead = true;
	}
	
	void PlayerDead ()
	{
		// disable player movement
	}

	void LevelReset ()
	{
		// Increment the timer.
		timer += Time.deltaTime;
		//If the timer is greater than or equal to the time before the level resets...
		//if(timer >= resetAfterDeathTime)
			// ... reset the level.
	}
	
	
	public void TakeDamage (float amount)
	{
		// Decrement the player's health by amount.
		health -= amount;
	}
}
