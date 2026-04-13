using UnityEngine;
using UnityEngine.InputSystem;

public class EndGame : MonoBehaviour
{
    [SerializeField] private InputActionReference f11;
    [SerializeField] private GameObject GameOverScreen;

    private void Start()
    {
        f11.action.performed += ShowGameOverScreen;
    }
    private void ShowGameOverScreen(InputAction.CallbackContext ctx)
    {
        GameManager.instance.timeManager.StopTime();
        GameOverScreen.SetActive(true);
    }
}
