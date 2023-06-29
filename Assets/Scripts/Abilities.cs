using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SpaceShooter;

namespace TowerDefence
{
    public class Abilities : MonoSingleton<Abilities>
    {
       
        [Serializable]

        public class FireAbiliy
        {
            [SerializeField] private int m_Damage = 2;
            [SerializeField] private int m_Cost = 2;
            public void Use() 
            {
                m_Damage *= Upgrades.GetUpgradeLevel(Abilities.Instance.m_FireUpgrade);
                if (TDPlayer.Instance.TryUseAbility(Abilities.Instance.m_FireUpgrade, m_Cost))
                {
                    ClickProtection.Instance.Activate((Vector2 v) =>
                    {
                        Vector3 position = v;
                        position.z = -Camera.main.transform.position.z;
                        position = Camera.main.ScreenToWorldPoint(position);
                        foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                        {
                            if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                            {
                                enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Fire);
                            }
                        }
                    });

                }
            
            
            }
        }
        [Serializable]
        public class TimeAbiliy
        {
            [SerializeField] private float m_Duration = 5;
            [SerializeField] private int m_Cost = 2;
            [SerializeField] private float m_Cooldown = 5f;
            public void Use() 
            { 
                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>(). HalfMaxLinearVelocity();

                }

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                        ship.RestoreMaxLinearVelocity();
                    EnemyWavesManager.OnEnemySpawn -= Slow;
                }

                foreach(var ship in FindObjectsOfType<SpaceShip>())
                    ship.HalfMaxLinearVelocity();
                
                EnemyWavesManager.OnEnemySpawn += Slow;
                Instance.StartCoroutine(Restore());

                IEnumerator TimeAbilityButton()
                {
                    Instance.m_TimeButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    Instance.m_TimeButton.interactable = true;
                }

                Instance.StartCoroutine(TimeAbilityButton());
            }
            
            
        }

        private void Start()
        {
            Debug.Log("Fire "+Upgrades.GetUpgradeLevel(m_FireUpgrade));
            Debug.Log("Time "+Upgrades.GetUpgradeLevel(m_TimeUpgrade));
            if (Upgrades.GetUpgradeLevel(m_FireUpgrade) > 0)
            {
                m_FireButton.gameObject.SetActive(true);
                
            }
            else m_FireButton.gameObject.SetActive(false);
            if (Upgrades.GetUpgradeLevel(m_TimeUpgrade) > 0) m_TimeButton.gameObject.SetActive(true);
            else m_TimeButton.gameObject.SetActive(false);
        }


        [SerializeField] private FireAbiliy m_FireAbiliy;
        public void UseFireAbility() => m_FireAbiliy.Use();

        [SerializeField] private TimeAbiliy m_TimeAbiliy;
        public void UseTimeAbility() => m_TimeAbiliy.Use();

        [SerializeField] public Button m_TimeButton;
        [SerializeField] public Button m_FireButton;
        [SerializeField] private Image m_TargetingCircle;
        [SerializeField] private UpgradeAsset m_FireUpgrade;
        [SerializeField] private UpgradeAsset m_TimeUpgrade;

        private void InitiateTargeting(Color color, Action<Vector2> mouseAction)
        {
            m_TargetingCircle.color = color;
           
        }
    }
}
