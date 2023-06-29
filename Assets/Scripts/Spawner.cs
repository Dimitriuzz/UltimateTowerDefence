using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{
    public abstract class Spawner : MonoBehaviour
    {
        // Start is called before the first frame update
        protected abstract GameObject GenerateSpawnedEntity();

        [SerializeField] private CircleArea m_Area;

        public enum SpawnMode
        {
            /// <summary>
            /// �� ������ Start()
            /// </summary>
            Start,

            /// <summary>
            /// ������������ ��������� ����� m_RespawnTime
            /// </summary>
            Loop
        }

        [SerializeField] private SpawnMode m_SpawnMode;

        /// <summary>
        /// ���-�� �������� ������� ����� ����� ����������.
        /// </summary>
        [SerializeField] private int m_NumSpawns;

        /// <summary>
        /// ����� ��������. ����� ����� �������� ��� ������� ������ ������ ���� ������ ������� ����� ��������.
        /// </summary>
        [SerializeField] private float m_RespawnTime;

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }
        }

        private void Update()
        {
            if (m_Timer > 0)
                m_Timer -= Time.deltaTime;

            if (m_SpawnMode == SpawnMode.Loop && m_Timer <= 0)
            {
                SpawnEntities();
                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                var e = GenerateSpawnedEntity();
                e.transform.position = m_Area.RandomInsideZone;
            }
        }
    }
}
