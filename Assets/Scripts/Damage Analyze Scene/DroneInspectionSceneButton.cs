using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DroneInspectionSceneButton : MonoBehaviour
{
    public void GoToDroneInspectionScene()
    {
        SceneManager.LoadScene("Drone Inspection Scene", LoadSceneMode.Single);
    }
}