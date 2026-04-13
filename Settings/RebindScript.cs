using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindScript : MonoBehaviour
{
    [SerializeField] private InputActionReference action;
    [SerializeField] private TextMeshProUGUI bindingText;
    [SerializeField] private Button button;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    public void Start()
    {
        string newBinding = action.action.GetBindingDisplayString();
        bindingText.text = newBinding;
    }
    public void Rebind()
    {
        action.action.Disable();
        bindingText.text = "...";
        button.interactable = false;
        rebindingOperation = action.action.PerformInteractiveRebinding().OnComplete(operation => RebindCompleted());
        rebindingOperation.Start();
    }

    private void RebindCompleted()
    {
        rebindingOperation.Dispose();
        button.interactable = true;
        string newBinding = action.action.GetBindingDisplayString();
        bindingText.text = newBinding;
        action.action.Enable();
    }
}
