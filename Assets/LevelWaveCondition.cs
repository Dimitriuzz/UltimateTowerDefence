using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{

    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        private bool isCompleted;

        private void Start()
        {
            FindObjectOfType<EnemyWavesManager>().OnAllWavesDead += () =>
              {
                  isCompleted = true;
                  LevelController.Instance.LevelCompleted();
                  Debug.Log("is completed");
              };
        }
        public bool IsCompleted { get { return isCompleted; } }
    }
}
