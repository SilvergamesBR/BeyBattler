using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryDetect : MonoBehaviour
{
    public Text victoryText;
    private GameObject P2;
    public GameObject EndScrnBttns;
    // Start is called before the first frame update
    void Start()
    {
        P2 = GameObject.Find("Player2");
        EndScrnBttns.gameObject.SetActive(false);
        gameObject.GetComponent<MovementP1>().gameRestarting();
    }

    // Update is called once per frame
    void Update()
    {
        if(P2.GetComponent<MovementP2>().GetLoser() == true || gameObject.GetComponent<MovementP1>().GetLoser() == true)
        {
            gameObject.GetComponent<MovementP1>().gameFinished();
            EndScrnBttns.gameObject.SetActive(true);
            if (gameObject.GetComponent<MovementP1>().GetLoser() == true)
            {
                victoryText.text = "Player 2 Wins !";
            }
            else
            {
                victoryText.text = "Player 1 Wins !";
            }
        }
    }

    public void RestartGame()
    {
        gameObject.GetComponent<MovementP1>().gameRestarting();
        SceneManager.LoadScene("GameRm");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
