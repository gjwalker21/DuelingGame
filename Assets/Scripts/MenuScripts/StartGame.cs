using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button startButton;
    void Start()
    {
        startButton.onClick.AddListener(StartDuel);
    }

    public void StartDuel()
    {
        SceneManager.LoadScene("Dueling");
    }
}
