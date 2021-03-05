using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public TMP_Text XorO;
    public Image backgroundImage;
    // Start is called before the first frame update

    public string Player()
    {
        return XorO.text;
    }

    public void OnClick()
    {
        GameController gameController = FindObjectOfType<GameController>();
        backgroundImage.gameObject.SetActive(true);
        XorO.text = gameController.currentPlayer;
        gameController.NextRound();
    }
}
