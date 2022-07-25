using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void ShareButton()
    {
        new NativeShare().SetTitle("Share AltTicTacToe").SetText("This is an awesome game, try it out!").SetUrl("https://altom.com/tools/altunity").SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
		.Share();
    }
}
