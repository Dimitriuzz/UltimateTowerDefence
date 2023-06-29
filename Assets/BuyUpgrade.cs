using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private Image upgradeIcon;
        [SerializeField] private Text level, cost;
        [SerializeField] private Button buyButton;
        public Button BuyButton => buyButton;
        [SerializeField] private UpgradeAsset asset;
        private int costNumber = 0;

        public void Initialise()
        {
            
            upgradeIcon.sprite = asset.sprite;
            var savedlevel = Upgrades.GetUpgradeLevel(asset);
            Debug.Log("Upgrade initialisation "+asset.name+" level "+savedlevel);
            if (savedlevel >= asset.costByLevel.Length)
            {
                buyButton.interactable = false;
                buyButton.transform.Find("Star").gameObject.SetActive(false);
                buyButton.transform.Find("Text").gameObject.SetActive(false);
                cost.text = "X";
                level.text = $"Lvl: { savedlevel } (Max)";
                costNumber = int.MaxValue;

            }
            else
            { 
                level.text = $"Lvl: { savedlevel }";
                costNumber = asset.costByLevel[savedlevel];
                cost.text = costNumber.ToString();
               
            }
        }

        public void CheckCost(int money)
        {
            buyButton.interactable = money >= costNumber;

        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialise();
        }

    }
}
