using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MainMenu : MonoBehaviour
    {
        private const string GameSceneName = "Game";
        [SerializeField] private Button playBtn;

        private void Start()
        {
            playBtn.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(GameSceneName);
        }
    }
}