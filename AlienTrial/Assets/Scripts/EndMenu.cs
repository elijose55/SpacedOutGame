using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public GameObject Score;
    public GameObject Wave;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Score.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.score).ToString();
        Wave.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.wave).ToString();

    }
    public void EndGame ()
    {
        SceneManager.LoadScene("Menu");
    }
}
