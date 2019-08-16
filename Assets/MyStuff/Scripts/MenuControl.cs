using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OutButton()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=5116279090368000910");
    }
}
