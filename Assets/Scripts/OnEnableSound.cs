using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class OnEnableSound : MonoBehaviour
    {
        [SerializeField] Sound m_Sound;

        private void OnEnable()
        {
            m_Sound.Play();
        }

    }
}
