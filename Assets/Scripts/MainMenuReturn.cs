using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefence
{

    public class MainMenuReturn : MonoBehaviour
    {
        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

    }

}
