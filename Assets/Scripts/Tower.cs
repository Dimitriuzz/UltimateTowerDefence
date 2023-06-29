using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius = 5f;
        private float m_Lead = 0.3f;
        private Turret[] turrets;
        private Rigidbody2D target = null;

        private void Start()
        {

        }

        private void Update()
        {
            if (target)
            {
                //Vector2 targetVector = target.transform.position - transform.position;
                if (Vector3.Distance(target.transform.position, transform.position) <= m_Radius)
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = target.transform.position-
                            turret.transform.position+(Vector3)target.velocity*m_Lead;
                        turret.Fire();
                    }
                }
                else
                {
                    target = null;
                }

            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if (enter)
                {
                    
                   target = enter.transform.root.GetComponent<Rigidbody2D>();
                    
                }
            }
        }

        public void Use(TowerAsset asset)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = asset.sprite;

            turrets = GetComponentsInChildren<Turret>();
            foreach (var turret in turrets)
            {
                turret.AssignLoadout(asset.turretProperties);

            }
            GetComponentInChildren<BuildSite>().SetBuildablrTowers(asset.m_UpgradesTo);

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }

    }
}
