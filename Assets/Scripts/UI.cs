using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Button resetButton;
    public TextMeshProUGUI winnerText;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        resetButton.onClick.AddListener(ResetGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowUI()
    {
        resetButton.gameObject.SetActive(true);
        winnerText.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void ShowWinner(string winner)
    {
        winnerText.text = winner;
    }
}
