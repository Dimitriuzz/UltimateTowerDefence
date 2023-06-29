using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SpaceShooter;

namespace TowerDefence
{

    public class TD_PatrolController : AIController
    {
        private Path m_Path;
        private int pathIndex;
        [SerializeField] private UnityEvent OnPathEnd;

        public void SetPath(Path newPath)
        {
            m_Path = newPath;
            pathIndex = 0;
            SetPatrolBehaviour(m_Path[pathIndex]);
        }

        protected override void GetNewPoint()
        {
            pathIndex++;
            if(m_Path.Lenght>pathIndex)
            {
                SetPatrolBehaviour(m_Path[pathIndex]);
            }
            else
            {
                OnPathEnd.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
