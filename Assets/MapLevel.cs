using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;
using System;

namespace TowerDefence
{

    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_episode;
        [SerializeField] private RectTransform resultPanel;
        [SerializeField] private Image[] resultImage;

        public bool IsComplete { get { return gameObject.activeSelf && resultPanel.gameObject.activeSelf; } }

        public void LoadLevel()
        {
           
            LevelSequenceController.Instance.StartEpisode(m_episode);


        }

        public int Initialaise()
        {
            var score = MapCompletion.Instance.GetEpisodeScore(m_episode);
                

            resultPanel.gameObject.SetActive(score > 0);
            for (int i = 0; i < score; i++)
            {
                resultImage[i].color = Color.white;
            }
            return score;
        }

        public void SetLevelData(Episode episode, int score)
        {
           

        }
    }
}
