using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnlockLevels : MonoBehaviour
{
    [SerializeField]
    private string levelName;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { UnlockLevel(levelName); });
    }

    public void UnlockLevel(string levelName) 
    {
        GameManager.Instance.UnlockLevel(levelName);     
    }
}
