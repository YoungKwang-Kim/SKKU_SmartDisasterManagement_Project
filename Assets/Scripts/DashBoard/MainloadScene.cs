using UnityEngine;
using UnityEngine.SceneManagement;

public class MainloadScene : MonoBehaviour
{
    public void GoToDroneInspectionScene()
    {
        SceneManager.LoadScene("DashBoard Scene", LoadSceneMode.Single);
    }
}