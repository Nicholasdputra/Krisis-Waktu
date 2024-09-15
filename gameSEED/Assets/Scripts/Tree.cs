using TMPro;
using UnityEngine;
using UnityEngine.UI; // Needed for the Slider component
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
public class Tree : MonoBehaviour
{
    [Header("Tree Stats")]
    public int gold;
    public int health;
    public int maxHealth;

    [Header("Fate Sisters")]
    public GameObject Urd;
    public Animator urdAnimator;
    public GameObject Verdandi;
    public Animator verdandiAnimator;
    public GameObject Skuld;
    public Animator skuldAnimator;

    [Header("References to UI Elements")]
    public Slider treeHealthSlider; // Reference to the slider representing the tree's health
    public TextMeshProUGUI healthText; // Reference to the text displaying the tree's health
    public TextMeshProUGUI goldText; // Reference to the text displaying the tree's gold
    public EnemySpawn enemySpawnerScript; // Reference to the EnemySpawner script
    public ShopScript shopScript; // Reference to the ShopScript

    [Header("References to Other Game Objects")]
    public GameObject spawners; // Reference to the spawners object
    public GameObject shopPanel;

    [Header("Tree Attribute Upgrades")]
    public int lifeSteal = 0;
    public int shield = 0;
    public int addShield = 0;

    [Header("Stun")]
    public float stunDuration;
    public bool enemiesAreStunned = false;
    public int stunCooldown;
    public bool canStun = true;

    [Header("Slow")]
    public int slowDuration = 7;
    public bool enemiesAreSlowed = false;
    public float slowAmount = 0f;
    public int slowCooldown;
    public bool canSlow = true;

    [Header("Mitigation")]
    public int mitigationDuration = 5;
    public int mitigationAmount = 0;
    public bool isMitigatingDamage = false;
    public int mitigationCooldown;
    public bool canMitigate = true;

    [Header("Skill Buttons")]
    public GameObject slow;
    public GameObject stun;
    public GameObject mitigate;

    public TextMeshProUGUI slowCooldownText;
    public TextMeshProUGUI stunCooldownText;
    public TextMeshProUGUI mitigationCooldownText;

    public AudioSource audioSource;
    public AudioClip slowSound;
    public AudioClip mitigationSound;
    public AudioClip stunSound;


    // Start is called before the first frame update
    void Start()
    {
        stun.SetActive(false);
        slow.SetActive(false);
        mitigate.SetActive(false);

        urdAnimator = Urd.GetComponent<Animator>();
        urdAnimator.SetBool("canAttack", false);
        slowCooldown = 20;
        stunCooldown = 30;
        mitigationCooldown = 30;
        enemySpawnerScript = spawners.GetComponent<EnemySpawn>();
        health = 100;
        maxHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {

        if(health <= 0)
        {
            health = 0;
        }

        if(health == 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }

        if(health > maxHealth){
            health = maxHealth;
        }
        goldText.text = gold.ToString();
        treeHealthSlider.maxValue = maxHealth;
        treeHealthSlider.value = health;
        healthText.text = health.ToString() + " / " + maxHealth.ToString();

        if(enemySpawnerScript.waveValue == 0)
        {
            urdAnimator.SetBool("canAttack", false);
            shopPanel.SetActive(true); 
        }

        if (health <= 0)
        {
            // Game over
            // Debug.Log("Game Over");
            spawners.SetActive(false); // Disable the spawners
        }

        if(shopScript.stunUpgradedAmount > 0){
            stun.SetActive(true);
        }
        if(shopScript.slowUpgradedAmount > 0){
            slow.SetActive(true);
        }
        if(shopScript.mitigationUpgradedAmount > 0){
            mitigate.SetActive(true);
        }

        if(enemySpawnerScript.waveValue == 0){
            stun.SetActive(false);
            slow.SetActive(false);
            mitigate.SetActive(false);

            stunCooldown = 0;
            slowCooldown = 0;
            mitigationCooldown = 0;

            enemiesAreSlowed = false;
            enemiesAreStunned = false;
            isMitigatingDamage = false;

            shopPanel.SetActive(true);
        }
    }

    public void SlowSkill()
    {
        if(canSlow){
            Debug.Log("Slow Skill Activated");
            enemiesAreSlowed = true;
            audioSource.PlayOneShot(slowSound);
            StartCoroutine(Timer((float)slowDuration, 1));
            canSlow = false;
        }
    }
    public void MitigationSkill()
    {
        if(canMitigate){
            Debug.Log("Mitigation Skill Activated");
            isMitigatingDamage = true;
            audioSource.PlayOneShot(mitigationSound);
            StartCoroutine(Timer((float)mitigationDuration, 2));  
            canMitigate = false;   
        }
       
    }

    public void StunSkill()
    {
        if(canStun){
            Debug.Log("Stun Skill Activated");
            enemiesAreStunned = true;
            audioSource.PlayOneShot(stunSound);
            StartCoroutine(Timer(stunDuration, 3));
            canStun = false;
        }
    }

    public IEnumerator Cooldown(int purpose)
    {
        Debug.Log("Cooldown Started");
        switch(purpose)
        {
            case 1:
                for(int i = slowCooldown; i > 0; i--)
                {
                    slowCooldownText.text = "";
                    slowCooldownText.text = i.ToString() + "s";
                    yield return new WaitForSeconds(1);
                }
                canSlow = true;
                break;
            case 2:
                for(int i = mitigationCooldown; i > 0; i--)
                {
                    mitigationCooldownText.text = "";
                    mitigationCooldownText.text = i.ToString() + "s";
                    yield return new WaitForSeconds(1);
                }
                canMitigate = true;
                break;
            case 3:
                for(int i = stunCooldown; i > 0; i--)
                {
                    stunCooldownText.text = "";
                    stunCooldownText.text = i.ToString() + "s";
                    yield return new WaitForSeconds(1);
                }
                canStun = true;
                break;
        }
    }

    public IEnumerator Timer(float duration, int purpose)
    {
        yield return new WaitForSeconds(duration);

        if(purpose == 1)
        {
            Debug.Log("Slow Skill Deactivated");
            enemiesAreSlowed = false;
        }
        else if(purpose == 2)
        {
            Debug.Log("Mitigation Skill Deactivated");
            isMitigatingDamage = false;
        }
        else if(purpose == 3)
        {
            Debug.Log("Stun Skill Deactivated");
            enemiesAreStunned = false;
        } 
        StartCoroutine(Cooldown(purpose));
    }
}