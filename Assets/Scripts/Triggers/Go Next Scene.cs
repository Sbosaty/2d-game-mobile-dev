using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoNextScene : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene < SceneManager.sceneCountInBuildSettings - 1)
        {
            GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(currentScene + 1));
        }
        else
        {
            GetComponent<Button>().enabled = false;

        }
    }

}
