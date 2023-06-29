using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public enum Sound
    { 
        BGM=0,
        Arrow=1,
        ArrowHit=2,
        BGM1=3,
        Electro=4,
        Gun=5,
        Dying=6,
        Damage=7


    }

    public static class SoundExtension
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
    }
}
