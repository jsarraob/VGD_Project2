using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    [SerializeField]
    public GameObject nextLevelButton;
    [SerializeField]
    public GameObject levelCompleteText;
    [SerializeField]
    public string levelName;
    [SerializeField]
    public string nextLevelName;
    public bool levelComplete;
    public void RestartLevel() {
        levelComplete = false;
        SceneManager.LoadScene(levelName);
    }
    public void PlayNextLevel() {
        SceneManager.LoadScene(nextLevelName);
    }

    void Start(){
        levelComplete = false;
    }
    void Update() {
        if (levelComplete) {
            levelCompleteText.SetActive(true);
            if (nextLevelName != "None")
            nextLevelButton.SetActive(true);
        } else {
            levelCompleteText.SetActive(false);
            nextLevelButton.SetActive(false);
        }
    }
}
