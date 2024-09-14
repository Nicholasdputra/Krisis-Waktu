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

    [Header("Buttons")]
    public GameObject buttonToUpgradeHP;

    public GameObject buttonToUpgradeShield;
    
    public GameObject buttonToUpgradeLifesteal;
    
    public GameObject buttonToUpgradeStun;
    
    public GameObject buttonToUpgradeMitigation;
    
    public GameObject buttonToUpgradeSlow;

    public GameObject Next;
    public GameObject prev;
    public GameObject nextWave;

    [Header("Gold Costs")]
    public int goldForHP;
    public int goldForShield;
    public int goldForLifesteal;
    public int goldForStun;
    public int goldForMitigation;
    public int goldForSlow;
    public int addShield;

    [Header("Upgrades")]
    public int hpUpgradedAmount;
    public int shieldUpgradedAmount;
    public int lifestealUpgradedAmount;
    public int stunUpgradedAmount;
    public int mitigationUpgradedAmount;
    public int slowUpgradedAmount;
    public int ShopPageIndex = 1;

    void Start()
    {
        Next.SetActive(false);
        prev.SetActive(false);
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
        // if(StatUpgrade.activeInHierarchy)
        // {
        //     Debug.Log("panel 1 on");
        // }

        if(enemySpawn.currentCount == 0 && enemySpawn.enemiesToSpawnLeft == 0)
        {
            Debug.Log("Shop Page Index = " + ShopPageIndex);
            if(ShopPageIndex == 1)
            {
                Debug.Log("panel 1 on");
                StatUpgrade.SetActive(true);
                MercenaryUpgrade.SetActive(false);
                SkillUpgrade.SetActive(false);
                nextWave.SetActive(false);
                Next.SetActive(true);

                if(treeScript.gold > goldForHP){
                    buttonToUpgradeHP.SetActive(true);
                } else{
                    buttonToUpgradeHP.SetActive(false);
                }

                if(treeScript.gold > goldForShield){
                    buttonToUpgradeShield.SetActive(true);
                } else{
                    buttonToUpgradeShield.SetActive(false);
                }   

                if(treeScript.gold > goldForLifesteal){
                    buttonToUpgradeLifesteal.SetActive(true);
                } else{
                    buttonToUpgradeLifesteal.SetActive(false);
                }

            }
            if(ShopPageIndex == 2)
            {
                Debug.Log("panel 2 on");
                StatUpgrade.SetActive(false);
                SkillUpgrade.SetActive(true);
                MercenaryUpgrade.SetActive(false);
                nextWave.SetActive(false);
                Next.SetActive(true);

                if(treeScript.gold > goldForStun){
                    buttonToUpgradeStun.SetActive(true);
                } else{
                buttonToUpgradeStun.SetActive(false);
                }
                if(treeScript.gold > goldForMitigation){
                buttonToUpgradeMitigation.SetActive(true);
                } else{
                buttonToUpgradeMitigation.SetActive(false);
                }

                if(treeScript.gold > goldForSlow){
                    buttonToUpgradeSlow.SetActive(true);
                } else{
                buttonToUpgradeSlow.SetActive(false);
                }
            }
            if(ShopPageIndex == 3)
            {
                Debug.Log("panel 3 on");
                StatUpgrade.SetActive(false);
                SkillUpgrade.SetActive(false);
                MercenaryUpgrade.SetActive(true);
                nextWave.SetActive(true);
                Next.SetActive(false);
            }
        }    
    }

    public void HPUpgrade(){
        int upgradeAmountInt = 0;
        switch (hpUpgradedAmount){
            case 0:
                upgradeAmountInt = 10;
                goldForHP = 10;
                break;
            case 1:
                upgradeAmountInt = 20;
                goldForHP = 35;
                break;
            case 2:
                upgradeAmountInt = 30;
                goldForHP = 50;
                break;
            case 3:
                upgradeAmountInt = 40;
                goldForHP = 70;
                break;
            case 4:
                upgradeAmountInt = 50;
                goldForHP = 100;
                break;
        }
        if(treeScript.gold > goldForHP){
            hpUpgradedAmount++;
            treeScript.gold -= goldForHP;
            treeScript.maxHealth += upgradeAmountInt;
            treeScript.health += upgradeAmountInt;
        }
    }



    public void UpgradeShield(){
        switch (shieldUpgradedAmount){
            case 0:
                addShield = 5;
                goldForShield = 5;
                break;
            case 1:
                addShield = 15;
                goldForShield = 25;
                break;
            case 2:
                addShield = 30;
                goldForShield = 40;
                break;
            case 3:
                addShield = 50;
                goldForShield = 60;
                break;
            case 4:
                addShield = 75;
                goldForShield = 80;
                break;
        }
        if(treeScript.gold > goldForShield){
            shieldUpgradedAmount++;
            treeScript.gold -= goldForShield;
            treeScript.addShield = addShield;
        }
    }

    public void UpgradeLifesteal(){
        int lifeSteal = 0;
        switch (lifestealUpgradedAmount){
            case 0:
                lifeSteal = 1;
                goldForLifesteal = 10;
                break;
            case 1:
                lifeSteal = 3;
                goldForLifesteal = 15;
                break;
            case 2:
                lifeSteal = 5;
                goldForLifesteal = 20;
                break;
        }
        if(treeScript.gold > goldForLifesteal){
            lifestealUpgradedAmount++;
            treeScript.gold -= goldForLifesteal;
            treeScript.lifeSteal = lifeSteal;
        }
    }

    public void UpgradeStun(){
        float stunDuration = 0;
        switch (stunUpgradedAmount){
            case 0:
                stunDuration = 1.5f;
                goldForStun = 15;
                break;
            case 1:
                stunDuration = 2.0f;
                goldForStun = 30;
                break;
            case 2:
                stunDuration = 2.5f;
                goldForStun = 50;
                break;
            case 3:
                stunDuration = 3.0f;
                goldForStun = 75;
                break;
            case 4:
                stunDuration = 4.0f;
                goldForStun = 100;
                break;
        }
        if(treeScript.gold > goldForStun){
            stunUpgradedAmount++;
            treeScript.gold -= goldForStun;
            treeScript.stunDuration = stunDuration;
        }
    }

    public void UpgradeMitigation(){
        int mitigation = 0;
        switch (mitigationUpgradedAmount){
            case 0:
                mitigation = 5;
                goldForMitigation = 15;
                break;
            case 1:
                mitigation = 10;
                goldForMitigation = 20;
                break;
            case 2:
                mitigation = 20;
                goldForMitigation = 35;
                break;
            case 3:
                mitigation = 25;
                goldForMitigation = 45;
                break;
            case 4:
                mitigation = 30;
                goldForMitigation = 60;
                break;
        }
        if(treeScript.gold > goldForMitigation){
            mitigationUpgradedAmount++;
            treeScript.gold -= goldForMitigation;
            treeScript.mitigationAmount = mitigation;
        }
    }

    public void UpgradeSlow(){
        float slowAmount = 0;
        switch (slowUpgradedAmount){
            case 0:
                slowAmount = 0.2f;
                goldForSlow = 15;
                break;
            case 1:
                slowAmount = 0.25f;
                goldForSlow = 30;
                break;
            case 2:
                slowAmount = 0.3f;
                goldForSlow = 45;
                break;
            case 3:
                slowAmount = 0.35f;
                goldForSlow = 55;
                break;
            case 4:
                slowAmount = 0.4f;
                goldForSlow = 80;
                break;
        }

        if(treeScript.gold > goldForSlow){
            slowUpgradedAmount++;
            treeScript.gold -= goldForSlow;
            treeScript.slowAmount = slowAmount;
        }
    }
}
