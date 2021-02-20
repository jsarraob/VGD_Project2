using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{

    [SerializeField]
    public GameObject goal;
    [SerializeField]
    public GameObject canvas;
    private Color goalColor;
    private List<GameObject> blocks;
    
    
    // Start is called before the first frame update
    void Start()
    {
        canvas.GetComponent<LevelUIController>().levelComplete = false;
        goalColor = goal.GetComponent<Renderer>().material.color;
        Object[] objects = GameObject.FindObjectsOfType(typeof (GameObject));
        blocks = new List<GameObject>();
        foreach (Object o in objects) {
            GameObject g = (GameObject)o;
            if (g.CompareTag("ColorBlock")) {
                blocks.Add(g);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject block in blocks) {
            Color c = block.GetComponent<Renderer>().material.color;
            if (eq(c.r, goalColor.r) && eq(c.g, goalColor.g) && eq(c.b, goalColor.b)) {
                LevelComplete();
                break;
            }
        }
    }
    void LevelComplete() {
        canvas.GetComponent<LevelUIController>().levelComplete = true;
    }
    private bool eq(float a, float b) {
        return a - b < 0.002 && a - b > -0.002;
    }
}
