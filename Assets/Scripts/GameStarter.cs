﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public static void StartGame()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
