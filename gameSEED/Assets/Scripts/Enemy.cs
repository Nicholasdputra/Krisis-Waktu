using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySpawn enemySpawnScript;
    public Tree treeScript;
    public int lane;
    public int category;
    public float speed;
    public bool canMove;
    private int damageToTree;
    public int goldDrop;
    public string toType1;  // Word associated with this enemy
    public string toType2;
    public TextMeshProUGUI displayWord; //what will actually be displayed on the enemy
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        toType2 = null;
        treeScript = GameObject.FindWithTag("Tree").GetComponent<Tree>();
        displayWord = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if (displayWord == null)
        {
            Debug.LogError("TextMeshPro component not found.");
            return;
        }

        //Set the word to be displayed on the enemy
        displayWord.text = toType1;
        Canvas canvas = displayWord.GetComponentInParent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.sortingOrder = 10; // Set a higher sorting order to ensure it appears on top

        canMove = true;
        // Set enemy attributes based on category
        switch (category)
        {
            case 1:
                speed = 1.8f;
                damageToTree = 5;
                goldDrop = 1;

                break;

            case 2:
                speed = 1.3f;
                damageToTree = 15;
                goldDrop = 2;

                break;

            case 3:
                speed = 0.8f;
                damageToTree = 25;
                goldDrop = 3;

                break;

            case 4:
                speed = 0.6f;
                damageToTree = 50;
                goldDrop = 5;
                toType2 = enemySpawnScript.GetRandomWord();
                displayWord.text += " " + toType2;
                break;
            default:
                break;
        }

        // Optionally, you can set up other initial properties here
    }

    void Update()
    {
        // Move left if canMove is true
        if (canMove)
        {
            if (treeScript.enemiesAreStunned == false)
            {
                if(treeScript.enemiesAreSlowed){
                    Debug.Log("Slowed enemy by " + treeScript.slowAmount + ", speed is now " + (1 - treeScript.slowAmount) * speed + " from " + speed);
                    transform.position += new Vector3((1 - treeScript.slowAmount) * -speed * Time.deltaTime, 0, 0);
                } else{
                    transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tree")
        {
            canMove = false;
            animator.SetBool("attack", true);
            StartCoroutine(Attack());
            
            if (treeScript.isMitigatingDamage)
            {
                // Debug.Log("Mitigated damage");
                // Debug.Log($"Original damageToTree: {damageToTree}");
                // Debug.Log($"Mitigation amount: {treeScript.mitigationAmount}");
                damageToTree -= treeScript.mitigationAmount;
                // Debug.Log($"New damageToTree: {damageToTree}");
            }
            
            if(treeScript.shield != 0){
                if(treeScript.shield < damageToTree){
                    damageToTree -= treeScript.shield;
                    treeScript.shield = 0;
                } else{
                    treeScript.shield -= damageToTree;
                    damageToTree = 0;
                }
            }
            
            if(damageToTree < 0){
                damageToTree = 0;
            }
            // Debug.Log("Tree hit for " + damageToTree + " damage");
            enemySpawnScript.currentCount--; // Reduce the enemy count
            enemySpawnScript.spawnedEnemies.Remove(this); // Remove from active enemy list
            treeScript.health -= damageToTree; // Reduce tree health based on the enemy's damage
            treeScript.gold += goldDrop; // Add gold to the tree
        }
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);  // Destroy the enemy after hitting the tree
    }
}
