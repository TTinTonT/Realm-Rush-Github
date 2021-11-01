using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI UIText;

    int currentScene;

    public int CurrentBalance { get => currentBalance;}

    private void Start()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;        
    }

    private void Awake()
    {
        currentBalance = startingBalance;
        DisplayUIText();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        DisplayUIText();
    }

    public void WithDawn(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        DisplayUIText();
        if (currentBalance < 0)
        {
            SceneManager.LoadScene(currentScene);
        }

    }

    //Need the Win condition

    private void DisplayUIText()
    {
        if (UIText != null)
        {
            UIText.text = "Gold: " + currentBalance;
        }
    }
}
