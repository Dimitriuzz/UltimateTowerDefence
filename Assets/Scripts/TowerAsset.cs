using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{
    
    [CreateAssetMenu]
    public class TowerAsset: ScriptableObject
        {
            public int goldCost = 15;
            public Sprite GUISprite;
            public Sprite sprite;
            public Tower towerPrefab;
        public TurretProperties turretProperties;
        [SerializeField] public UpgradeAsset requiredUpgrade;
        [SerializeField] private int requiredUpgradeLevel;
        public TowerAsset[] m_UpgradesTo;

        public bool IsAvaliable() => !requiredUpgrade || 
            requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);
        
       

        }

    
}
