using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    

    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        [Header("View")]
        public Sprite sprite;

        [Header("settings")]
        public int[] costByLevel = { 1 };

        public new string name;
    }
}