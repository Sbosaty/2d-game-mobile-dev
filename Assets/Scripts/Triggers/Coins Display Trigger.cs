using TMPro;
using UnityEngine;

public class CoinsDisplayTrigger : MonoBehaviour
{
    private TMP_Text m_Text;

    private void Start()
    {
        m_Text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        UpdateCoins();
    }

    void UpdateCoins() 
    {
        m_Text.text = "Coins: " + GameManager.Instance.coins;
    }
}
