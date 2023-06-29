using System;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using TowerDefence;
using UnityEngine.SceneManagement;

public class TDPlayer : Player
{
    public new static TDPlayer Instance
    {
        get { return Player.Instance as TDPlayer; }
    }


    [SerializeField] private int m_Gold = 0;
    [SerializeField] private int m_Mana = 0;
    [SerializeField] private int m_LevelTime;
    public float m_CurrentTime;
    private float fireRate=1;
    private int bonusTime;

    //[SerializeField] GameObject m_BossCamp;
    //[SerializeField] private int m_Life = 10;

    private event Action<int> OnGoldUpdate;
    public void GoldUpdateSubscribe(Action<int> act)
    {
        OnGoldUpdate += act;
        act(Instance.m_Gold);
    }

    public void GoldUpdateUnSubscribe(Action<int> act)
    {
        OnGoldUpdate -= act;
        
    }


    private event Action<int> OnManaUpdate;
    public void ManaUpdateSubscribe(Action<int> act)
    {
        OnManaUpdate += act;
        act(Instance.m_Mana);
    }

    public void ManaUpdateUnSubscribe(Action<int> act)
    {
        OnManaUpdate -= act;

    }

    private event Action<int> OnTimeUpdate;
    public void TimeUpdateSubscribe(Action<int> act)
    {
        OnTimeUpdate += act;
        act(Instance.m_LevelTime);
    }

    public event Action<int> OnLifeUpdate;

   
    public void LifeUpdateSubscribe(Action<int> act)
    {
        OnLifeUpdate += act;
        act(Instance.NumLives);
    }

    
    public void ChangeGold(int change)
    {
        m_Gold += change;
        OnGoldUpdate(m_Gold);
    }

    public void ChangeMana(int change)
    {
        m_Mana += change;
        OnManaUpdate(m_Mana);
    }

    public void ChangeLife(int change)
    {
        TakeDamage(change);
        OnLifeUpdate(NumLives);
    }

    public void ChangeTime(int change)
    {
        
        OnTimeUpdate(NumLives);
    }

    private void Start()
    {
        m_LevelTime = TDLevelController.Instance.ReferenceTime+bonusTime;
        m_CurrentTime = m_LevelTime;
        var level = Upgrades.GetUpgradeLevel(healthUpgrade);
        TakeDamage(-level * 5);
        if (Upgrades.GetUpgradeLevel(fireRateUpgrade) > 0) fireRate = (1 - Upgrades.GetUpgradeLevel(fireRateUpgrade) * 0.1f);
       //Debug.Log("Fire rate " + fireRate.ToString());
        bonusTime = Upgrades.GetUpgradeLevel(timeUpgrade);
        //Debug.Log("bonus time" + bonusTime);
        bonusTime *= 5;
       // Debug.Log("bonus time1" + bonusTime);
        //m_BossCamp.SetActive(false);
    }

    private void Update()
    {
        m_CurrentTime -= Time.deltaTime;
        if(m_LevelTime-m_CurrentTime>0.99)
        {
            m_LevelTime = (int)m_CurrentTime;
            if (m_LevelTime <= 0) m_LevelTime = 0;
            //Debug.Log(m_LevelTime + " " + m_CurrentTime);
            OnTimeUpdate(m_LevelTime);
        }
       // if (m_LevelTime<0) SceneManager.LoadScene(1);
       // if (NumLives==0) SceneManager.LoadScene(0);
       // if (m_LevelTime <= 40) m_BossCamp.SetActive(true);
    }


    [SerializeField] private Tower m_TowerPrefab;
   public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(towerAsset.towerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);
            var turret = tower.GetComponentInChildren<Turret>();
            turret.m_TurretProperties.SetRateOfFire(fireRate);
            
            Destroy(buildSite.gameObject);
        
        }

    public bool TryUseAbility (UpgradeAsset upgradeAsset, int cost)
    {
        if (m_Mana >= cost)
        {
            ChangeMana(-cost);
            if (m_Mana < cost) Abilities.Instance.m_FireButton.interactable = false;
            return (true);
        }
        else return (false);
    }

    [SerializeField] private UpgradeAsset healthUpgrade;
    [SerializeField] private UpgradeAsset fireRateUpgrade;
    [SerializeField] private UpgradeAsset timeUpgrade;

  
}
