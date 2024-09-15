using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [Header("Script References")]
    public EnemySpawn enemySpawn;
    public Tree treeScript;

    [Header("Object References")]
    public GameObject Shop;
    public TextMeshProUGUI Gold;
    public GameObject StatUpgrade;
    public GameObject SkillUpgrade;
    public GameObject MercenaryUpgrade;

    public GameObject SkuldPrefab;
    public bool boughtSkuld = false;
    public bool buyingSkuld = false;
    public GameObject VerdandiPrefab;
    public bool boughtVerdandi = false;
    public bool buyingVerdandi = false;

    [Header("Buttons")]
    public GameObject buttonToUpgradeHP;
    public GameObject buttonToUpgradeShield;
    public GameObject buttonToUpgradeLifesteal;
    public GameObject buttonToUpgradeStun;
    public GameObject buttonToUpgradeMitigation;
    public GameObject buttonToUpgradeSlow;
    public GameObject buttonToBuySkuld;
    public GameObject buttonToBuyVerdandi;

    public GameObject next;
    public GameObject prev;
    public GameObject buttonForNextWave;

    [Header("Gold Costs")]
    public int goldForHP;
    public int goldForShield;
    public int goldForLifesteal;
    public int goldForStun;
    public int goldForMitigation;
    public int goldForSlow;
    public int addShield;
    
    public int nextHPUpgradeCost = 10;
    public int nextShieldUpgradeCost = 5;
    public int nextLifestealUpgradeCost = 10;
    public int nextStunUpgradeCost = 15;
    public int nextMitigationUpgradeCost = 15;
    public int nextSlowUpgradeCost = 15;

    [Header("Upgrades")]
    public int hpUpgradedAmount;
    public int shieldUpgradedAmount;
    public int lifestealUpgradedAmount;
    public int stunUpgradedAmount;
    public int mitigationUpgradedAmount;
    public int slowUpgradedAmount;
    public int ShopPageIndex = 0;

    public bool hasBoughtSlow = false;
    public bool hasBoughtStun = false;
    public bool hasBoughtMitigation = false;

    [Header("Sliders")]
    public Slider hpSlider;
    public Slider shieldSlider;
    public Slider lifestealSlider;
    public Slider stunSlider;
    public Slider mitigationSlider;
    public Slider slowSlider;
   
     
    void OnEnable()
    {
        Debug.Log("Shop Enabled");
        ShopPageIndex = 0;
        next.SetActive(true);
        prev.SetActive(true);
        buttonForNextWave.SetActive(true);
    }

    public void nextPage()
    {
        ShopPageIndex += 1;
    }

    public void prevPage()
    {
        
        ShopPageIndex -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemySpawn.currentCount == 0 && enemySpawn.enemiesToSpawnLeft == 0)
        {
            if(ShopPageIndex < 0){
                ShopPageIndex = 2;
            }else if(ShopPageIndex > 2){
                ShopPageIndex = 0;
            }

            // Debug.Log("Shop Page Index = " + ShopPageIndex);
            if(ShopPageIndex == 0) //stat upgrade
            {
                StatUpgrade.SetActive(true);
                MercenaryUpgrade.SetActive(false);
                SkillUpgrade.SetActive(false);

                if(treeScript.gold >= nextHPUpgradeCost && hpUpgradedAmount < 5){
                    buttonToUpgradeHP.SetActive(true);
                } else{
                    buttonToUpgradeHP.SetActive(false);
                }

                if(treeScript.gold >= nextShieldUpgradeCost && shieldUpgradedAmount < 5){
                    buttonToUpgradeShield.SetActive(true);
                } else{
                    buttonToUpgradeShield.SetActive(false);
                }   

                if(treeScript.gold >= nextLifestealUpgradeCost && lifestealUpgradedAmount < 3){
                    buttonToUpgradeLifesteal.SetActive(true);
                } else{
                    buttonToUpgradeLifesteal.SetActive(false);
                }
            }
            else if(ShopPageIndex == 1) //mercenary upgrade
            {
                StatUpgrade.SetActive(false);
                SkillUpgrade.SetActive(true);
                MercenaryUpgrade.SetActive(false);
                
                if(treeScript.gold >= nextStunUpgradeCost && stunUpgradedAmount < 5){
                    buttonToUpgradeStun.SetActive(true);
                } else{
                    buttonToUpgradeStun.SetActive(false);
                }
                if(treeScript.gold >= nextMitigationUpgradeCost && mitigationUpgradedAmount < 5){
                    buttonToUpgradeMitigation.SetActive(true);
                } else{
                    buttonToUpgradeMitigation.SetActive(false);
                }

                if(treeScript.gold >= nextSlowUpgradeCost && slowUpgradedAmount < 5){
                    buttonToUpgradeSlow.SetActive(true);
                } else{
                    buttonToUpgradeSlow.SetActive(false);
                }
            } 
            else if(ShopPageIndex == 2)
            {
                StatUpgrade.SetActive(false);
                SkillUpgrade.SetActive(false);
                MercenaryUpgrade.SetActive(true);

                if(treeScript.gold >= 50 && !boughtVerdandi){
                    buttonToBuyVerdandi.SetActive(true);
                } else{
                    buttonToBuyVerdandi.SetActive(false);
                }

                if(treeScript.gold >= 100 && !boughtSkuld){
                    buttonToBuySkuld.SetActive(true);
                } else{
                    buttonToBuySkuld.SetActive(false);
                }
            }
        }    
    }

    public void HPUpgrade(){
        Debug.Log("Upgrading HP");
        int upgradeAmountInt = 0;
        switch (hpUpgradedAmount){
            case 0:
                upgradeAmountInt = 10;
                goldForHP = 10;
                nextHPUpgradeCost = 35;
                break;

            case 1:
                upgradeAmountInt = 20;
                goldForHP = 35;
                nextHPUpgradeCost = 50;
                break;
            
            case 2:
                upgradeAmountInt = 30;
                goldForHP = 50;
                nextHPUpgradeCost = 70;
                break;
            
            case 3:
                upgradeAmountInt = 40;
                goldForHP = 70;
                nextHPUpgradeCost = 100;
                break;
            
            case 4:
                upgradeAmountInt = 50;
                goldForHP = 100;
                nextHPUpgradeCost = 9999999;
                break;
        }
        if(treeScript.gold > goldForHP){
            hpUpgradedAmount++;
            treeScript.gold -= goldForHP;
            treeScript.maxHealth += upgradeAmountInt;
            treeScript.health += upgradeAmountInt;
            hpSlider.value += 10;
        }
    }

    public void UpgradeShield(){
        Debug.Log("Upgrading Shield");
        switch (shieldUpgradedAmount){
            case 0:
                addShield = 5;
                goldForShield = 5;
                nextShieldUpgradeCost = 25;
                break;
            case 1:
                addShield = 15;
                goldForShield = 25;
                nextShieldUpgradeCost = 40;
                break;
            case 2:
                addShield = 30;
                goldForShield = 40;
                nextShieldUpgradeCost = 60;
                break;
            case 3:
                addShield = 50;
                goldForShield = 60;
                nextShieldUpgradeCost = 80;
                break;
            case 4:
                addShield = 75;
                goldForShield = 80;
                nextShieldUpgradeCost = 9999999;
                break;
        }
        if(treeScript.gold > goldForShield){
            shieldUpgradedAmount++;
            treeScript.gold -= goldForShield;
            treeScript.addShield = addShield;
            shieldSlider.value += 10;
        }
    }

    public void UpgradeLifesteal(){
        Debug.Log("Upgrading Lifesteal");
        int lifeSteal = 0;
        switch (lifestealUpgradedAmount){
            case 0:
                lifeSteal = 1;
                goldForLifesteal = 10;
                nextLifestealUpgradeCost = 15;
                break;
            case 1:
                lifeSteal = 3;
                goldForLifesteal = 15;
                nextLifestealUpgradeCost = 20;
                break;
            case 2:
                lifeSteal = 5;
                goldForLifesteal = 20;
                nextLifestealUpgradeCost = 9999999;
                break;
        }
        if(treeScript.gold > goldForLifesteal){
            hasBoughtSlow = true;
            lifestealUpgradedAmount++;
            treeScript.gold -= goldForLifesteal;
            treeScript.lifeSteal = lifeSteal;
            lifestealSlider.value += 10;
        }
    }

    public void UpgradeStun(){
        Debug.Log("Upgrading Stun");
        float stunDuration = 0;
        switch (stunUpgradedAmount){
            case 0:
                stunDuration = 1.5f;
                goldForStun = 15;
                nextStunUpgradeCost = 30;
                break;
            case 1:
                stunDuration = 2.0f;
                goldForStun = 30;
                nextStunUpgradeCost = 50;
                break;
            case 2:
                stunDuration = 2.5f;
                goldForStun = 50;
                nextStunUpgradeCost = 75;
                break;
            case 3:
                stunDuration = 3.0f;
                goldForStun = 75;
                nextStunUpgradeCost = 100;
                break;
            case 4:
                stunDuration = 4.0f;
                goldForStun = 100;
                nextStunUpgradeCost = 9999999;
                break;
        }
        if(treeScript.gold > goldForStun){
            hasBoughtStun = true;
            stunUpgradedAmount++;
            treeScript.gold -= goldForStun;
            treeScript.stunDuration = stunDuration;
            stunSlider.value += 10;
        }
    }

    public void UpgradeMitigation(){
        Debug.Log("Upgrading Mitigation");
        int mitigation = 0;
        switch (mitigationUpgradedAmount){
            case 0:
                mitigation = 5;
                goldForMitigation = 15;
                nextMitigationUpgradeCost = 20;
                break;
            case 1:
                mitigation = 10;
                goldForMitigation = 20;
                nextMitigationUpgradeCost = 35;
                break;
            case 2:
                mitigation = 20;
                goldForMitigation = 35;
                nextMitigationUpgradeCost = 45;
                break;
            case 3:
                mitigation = 25;
                goldForMitigation = 45;
                nextMitigationUpgradeCost = 60;
                break;
            case 4:
                mitigation = 30;
                goldForMitigation = 60;
                nextMitigationUpgradeCost = 9999999;
                break;
        }
        if(treeScript.gold > goldForMitigation){
            hasBoughtMitigation = true;
            mitigationUpgradedAmount++;
            treeScript.gold -= goldForMitigation;
            treeScript.mitigationAmount = mitigation;
            mitigationSlider.value += 10;
        }
    }

    public void UpgradeSlow(){
        Debug.Log("Upgrading Slow");
        float slowAmount = 0;
        switch (slowUpgradedAmount){
            case 0:
                slowAmount = 0.2f;
                goldForSlow = 15;
                nextSlowUpgradeCost = 30;
                break;
            case 1:
                slowAmount = 0.25f;
                goldForSlow = 30;
                nextSlowUpgradeCost = 45;
                break;
            case 2:
                slowAmount = 0.3f;
                goldForSlow = 45;
                nextSlowUpgradeCost = 55;
                break;
            case 3:
                slowAmount = 0.35f;
                goldForSlow = 55;
                nextSlowUpgradeCost = 80;
                break;
            case 4:
                slowAmount = 0.4f;
                goldForSlow = 80;
                nextSlowUpgradeCost = 9999999;
                break;
        }

        if(treeScript.gold > goldForSlow){
            slowUpgradedAmount++;
            treeScript.gold -= goldForSlow;
            treeScript.slowAmount = slowAmount;
            slowSlider.value += 10;
        }
    }


    public void SpawnVerdandi(){
        if(treeScript.gold > 50 && !boughtVerdandi){
            treeScript.gold -= 50;
            boughtVerdandi = true;
            buyingVerdandi = true;
        }
    }

    public void SpawnSkuld(){
        if(treeScript.gold > 10 && !boughtSkuld){
            treeScript.gold -= 100;
            boughtSkuld = true;
            buyingSkuld = true;;
        }
    }
    public void StartRound(){
        if(buyingSkuld){
            GameObject Skuld = Instantiate(SkuldPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            treeScript.Skuld = Skuld;
            treeScript.skuldAnimator = Skuld.GetComponent<Animator>();
            Skuld.transform.position = new Vector3(0, 0, 0);
            Mercenary mercenaryScript = Skuld.GetComponent<Mercenary>();
            mercenaryScript.spawnerScript = enemySpawn;
        }
        if(buyingVerdandi){
            GameObject Verdandi = Instantiate(VerdandiPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            treeScript.Verdandi = Verdandi;
            treeScript.verdandiAnimator = Verdandi.GetComponent<Animator>();
            Verdandi.transform.position = new Vector3(0, 0, 0);
            Mercenary mercenaryScript = Verdandi.GetComponent<Mercenary>();
            mercenaryScript.spawnerScript = enemySpawn;
        }
        
        if(hasBoughtSlow){
            treeScript.slow.SetActive(true);
        } else{
            treeScript.slow.SetActive(false);
        }

        if(hasBoughtStun){
            treeScript.stun.SetActive(true);
        } else{
            treeScript.stun.SetActive(false);
        }

        if(hasBoughtMitigation){
            treeScript.mitigate.SetActive(true);
        } else{
            treeScript.mitigate.SetActive(false);
        }

        buyingSkuld = false;
        buyingVerdandi = false;

        enemySpawn.currentRound++;
        enemySpawn.Initialize();
        this.gameObject.SetActive(false);
    }
}
