using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    
    public class BuyControl : MonoBehaviour
    {
        private RectTransform m_RectTransform;
        [SerializeField] private TowerBuyControl m_TowerBuyPrefab;
        [SerializeField] private UpgradeAsset mageTowerUpgrade;
        private List<TowerBuyControl> m_ActiveControl;
        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
            
        }

        private void MoveToBuildSite(BuildSite buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);
                m_RectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
                //Debug.Log("on build site");
                m_ActiveControl = new List<TowerBuyControl>();
                foreach (var asset in buildSite.buildableTowers) if(asset.IsAvaliable())
                {
                        //Debug.Log(asset.name + " upgrade level " + Upgrades.GetUpgradeLevel(asset.requiredUpgrade));
                        var newControl = Instantiate(m_TowerBuyPrefab, transform);
                        Debug.Log("instantiated " + asset.name);
                        m_ActiveControl.Add(newControl);
                        newControl.SetTowerAsset(asset);
                    
                }

                if (m_ActiveControl.Count > 0)
                {
                    var angle = 360 / m_ActiveControl.Count;
                    for (int i = 0; i < m_ActiveControl.Count; i++)
                    {
                        var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.left * 80);
                        m_ActiveControl[i].transform.position += offset;
                    }

                    foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                    {
                        tbc.SetBuildSite(buildSite.transform.root);
                    }

                }

                
            }
            else
            {
                if (m_ActiveControl != null)
                {
                    foreach (var control in m_ActiveControl) Destroy(control.gameObject);
                    m_ActiveControl.Clear();
                }
                    gameObject.SetActive(false);
                
                
            }

           
        }

        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }


    }
}
