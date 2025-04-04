using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    [SerializeField]
    private List<Button> levelButtons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0;i < levelButtons.Count; i++)
        {
            if (GameManager.Instance.IsLevelUnlocked(i))
            {
                levelButtons[i].enabled = true;
            }
            else 
            {
                levelButtons[i].enabled = false;
            }
        }
    }
}
