using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefence
{
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(TD_PatrolController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_Damage = 1;
        [SerializeField] private int m_Gold = 1;
        [SerializeField] private int m_Armor = 0;

        public enum ArmorType { Piercing=0, Fire=1, Electro=2 };

        private ArmorType m_ArmorType;

        private Destructible m_destructible;

        private int CalculateDamage(int power, int damagetype, int armortype, int armor)
            {

            if (damagetype == armortype) return Mathf.Max(power - armor, 1);
            else return power;

            }
            
        
        

        private void Awake()
        {
            m_destructible = GetComponent<Destructible>();
        }

        public int EnemyDamage => m_Damage;

        public event Action OnEnd;

        private void OnDestroy()
        {
            OnEnd?.Invoke();
        }

        public void Use(EnemyAsset asset)
        {
            var sr = transform.Find("VisualModel").GetComponent<SpriteRenderer>();
            sr.color = asset.color;
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);
            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animation;

            GetComponent<SpaceShip>().Use(asset);

            GetComponentInChildren<CircleCollider2D>().radius = asset.radius;

            m_Damage = asset.damage;
            m_Armor = asset.armor;
            m_Gold = asset.gold;
            m_ArmorType = asset.ArmorType;
            
        }

        public void TakeDamage(int damage, TDProjectile.DamageType damageType)
        {
            m_destructible.ApplyDamage(CalculateDamage(damage,(int)m_ArmorType,(int)damageType,m_Armor));
        }
        public void DamagePlayer()
        {
            TDPlayer.Instance.ChangeLife(m_Damage);
        }

        public void GivePlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_Gold);
        }
    }
}
