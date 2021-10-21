using UnityEngine;
using System.Collections;

public class HealthNBoosterManager : MonoBehaviour 
{
	private static HealthNBoosterManager instance = null;
	public static HealthNBoosterManager Instance
		// Use this for initialization
	
	{
		get {return instance;}
	}
	void Awake()
	{
		if (instance != null && instance != this)
		{		
			Destroy (this.gameObject);
			return;
			
		} 
		else 
		{
			instance = this;
		}
		
	}
	public string healthDisplayTag = "Health";
	public string staminaDisplayTag = "Stamina|Mana";
	public float maxHealth = 1;
	public float currentHealth;
	public float maxStamina = 10.0f;
	public float StaminaRegen = 10.0f;
	private float currentStamina;
	public float maxBomb = 3.0f;
	public float currentBomb = 1.0f;
	public Transform healthDisplay;
	public Renderer healthDisplayRenderer;
	public GameObject Reticle;
	public Transform staminaDisplay;
	public GameObject [] BombDisplay;
	private float healthOriginalYscale;
	private float staminaOriginalYscale;
	private GameObject player;
	private GameObject HeadsUp;
	public GameObject gib;
	public GameObject Smoke;
	public GameObject playerDamageSound;
	public GameObject playerDeathSound;
	public float damageCooldown = 1.5f;
	private float damageCooldownTimer = 0.0f;
	public Vector3 axis = Vector3.up;
	public Color maxHealthColor = Color.green;
	public Color minHealthColor = Color.red;
	public bool isTired = false;
	public bool isCutscene = false;
	private Rigidbody ship;

	void Start ()
	{
		for (int i = 0; i< currentBomb;i++)
		{
			BombDisplay[i].SetActive (true);
		}
		HeadsUp = GameObject.FindGameObjectWithTag ("HUD");
		player = GameObject.FindGameObjectWithTag("Player");
		ship = player.GetComponent<Rigidbody> ();
		currentHealth = maxHealth;
		currentStamina = maxStamina;
		healthDisplay = GameObject.FindGameObjectWithTag(healthDisplayTag).transform;
		staminaDisplay = GameObject.FindGameObjectWithTag(staminaDisplayTag).transform;
		healthOriginalYscale = healthDisplay.localScale.y; 
		staminaOriginalYscale = staminaDisplay.localScale.y;
		Debug.Log (currentHealth);
	}

	public void DamagedPlayer(float damageAmount)
	{
		if (damageCooldownTimer <= 0 && isCutscene == false) 
		{
			currentHealth -= damageAmount;
			Debug.Log (damageAmount + " ship got hit. " +currentHealth + "HP");
			{
				if (damageAmount <= 0.0f)
				{
					Debug.LogError ("You cannot pass a negative value to DamagePlayer!  Please use RestoreHealth instead!");
					return;
				}
				if (currentHealth <= 0.0f)
				{
					//Death
					Debug.Log ("Dead");
					currentHealth = 0;
					HeadsUp.SetActive (false);
					Reticle.SetActive (false);
					isTired = true;
					ship.isKinematic = false;
					//pushdown
					ship.AddForce (player.transform.up * -500, ForceMode.VelocityChange);
					//pushback
					ship.AddForce (player.transform.forward * -200, ForceMode.VelocityChange);
					//try to fucking rotate
					ship.AddTorque (player.transform.right * 300, ForceMode.VelocityChange);
					Invoke ("Explode", 1.0f);
				}


			}
			//Instantiate(playerDamageSound,player.transform.position,player.transform.rotation);
			healthDisplay.localScale = new Vector3 (healthDisplay.localScale.x, healthOriginalYscale * (currentHealth / maxHealth), healthDisplay.localScale.x);
			healthDisplayRenderer.material.color = Color.Lerp (minHealthColor, maxHealthColor, currentHealth / maxHealth);
			damageCooldownTimer = damageCooldown;
			}
		}

	void Explode()
	{
		player.SetActive(false);
		Instantiate(gib,player.transform.position,Random.rotation);
		Instantiate(Smoke,player.transform.position,Smoke.transform.rotation);
		//Instantiate(playerDeathSound,player.transform.position,player.transform.rotation);
		Invoke("Reload",1.5f);
		}

	void Reload ()
	{

		Application.LoadLevel(Application.loadedLevel);
	
	}

	public void Stamina(float staminaAmount)
	{

		if (Input.GetButton ("Jump") && isTired == false)
		{
			currentStamina -= 1*Time.deltaTime;
				}
		staminaDisplay = GameObject.FindGameObjectWithTag(staminaDisplayTag).transform;
		staminaDisplay.localScale = new Vector3(staminaDisplay.localScale.x, staminaOriginalYscale*(currentStamina/maxStamina),staminaDisplay.localScale.x);


	}
	public void Bombs(float bombAmount)
	{
		currentBomb += bombAmount;
		if (currentBomb >maxBomb)
		{
			currentBomb = maxBomb;
		}

		for(int i = 0; i< BombDisplay.Length; i++)
		{
			BombDisplay[i].SetActive (false);
		}
		for (int i = 0; i< currentBomb;i++)
		{
			BombDisplay[i].SetActive (true);
		}
	}

	public void RestoreHealth(float healthAmount)
	{
		if(healthAmount < 0)
		{
			Debug.LogError("You cannot pass a negative value to RestoreHealth!  Please use DamagePlayer instead!");
			return;
		}
		currentHealth += healthAmount;
		if(currentHealth >= maxHealth)
		{
			currentHealth = maxHealth;
		}
		healthDisplay = GameObject.FindGameObjectWithTag(healthDisplayTag).transform;
		healthDisplay.localScale = new Vector3(healthDisplay.localScale.x, healthOriginalYscale*(currentHealth/maxHealth),healthDisplay.localScale.z);
	}


	void Update ()
	{
	currentStamina += StaminaRegen *Time.deltaTime;
			
		if (currentStamina > maxStamina)
			currentStamina = maxStamina;

		if (currentStamina < 0.05)
		{
			isTired = true;
		} 
		else
		{
			isTired = false;
		}

		if (Input.GetButton ("Jump") && isTired == false)
		{
			currentStamina -= 5f *Time.deltaTime;
		}



		damageCooldownTimer -= Time.deltaTime;
		staminaDisplay = GameObject.FindGameObjectWithTag(staminaDisplayTag).transform;
		staminaDisplay.localScale = new Vector3(staminaDisplay.localScale.x, staminaOriginalYscale*(currentStamina/maxStamina),staminaDisplay.localScale.x);

		healthDisplay = GameObject.FindGameObjectWithTag(healthDisplayTag).transform;
		healthDisplay.localScale = new Vector3(healthDisplay.localScale.x, healthOriginalYscale*(currentHealth/maxHealth),healthDisplay.localScale.z);
		healthDisplay.GetComponentInChildren<Renderer>().material.color = Color.Lerp(minHealthColor,maxHealthColor,currentHealth/maxHealth);


	}

}
