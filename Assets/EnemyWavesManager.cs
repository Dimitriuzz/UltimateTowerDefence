using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class EnemyWavesManager : MonoBehaviour
    {
        [SerializeField] private Path[] paths;
        [SerializeField] private EnemyWave currentWave;
        [SerializeField] Enemy m_EnemyPrefab;
        [SerializeField] private int activeEnemyCount = 0;

        public static event Action<Enemy> OnEnemySpawn;
       
        public event Action OnAllWavesDead;

        private void RecordEnemyDead() 
        {
            if (--activeEnemyCount == 0)
            {
                if (currentWave)
                {
                    ForceNextWave();
                }
                else
                {
                    OnAllWavesDead?.Invoke();
                }
            }
                
        }

        private void Start()
        {
            currentWave.Prepare(SpawnEnemies);
        }

        private void SpawnEnemies()
        {
            foreach ((EnemyAsset asset, int count, int pathIndex) in currentWave.EnumerateSquads())
            {
                if (pathIndex < paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var e = Instantiate(m_EnemyPrefab,paths[pathIndex].StartArea.RandomInsideZone,Quaternion.identity);
                        e.OnEnd += RecordEnemyDead;
                        e.Use(asset);
                        e.GetComponent<TD_PatrolController>().SetPath(paths[pathIndex]);
                        activeEnemyCount += 1;
                        OnEnemySpawn?.Invoke(e);
                        
                    }


                }
            }

            currentWave = currentWave.PrepareNext(SpawnEnemies);
        }

        public void ForceNextWave()
        {
            if (currentWave)
            {
                TDPlayer.Instance.ChangeGold((int)currentWave.GetRemainingTime());
                SpawnEnemies();
            }
                else
                {
                Debug.Log("no wave");
                if(activeEnemyCount==0)
                    OnAllWavesDead?.Invoke();
                Debug.Log("no wave invoked");
            }
            }
    }
}
