using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    public string LevelName;
    public void PlayLevel() {
        SceneManager.LoadScene(LevelName);
    }
}
