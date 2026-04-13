using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private PlayerCharacter character;
    [SerializeField] private NavMeshAgent agent;

    private void Awake()
    {
        var moveClick = actions.FindAction("MoveClick");
        moveClick.performed += _ => OnMoveClick();

        var interaction = actions.FindAction("Interaction");
        interaction.performed += _ => OnInteraction();

        character = GetComponentInChildren<PlayerCharacter>();
    }

    public void OnMoveClick()
    {
        var mousePos = Mouse.current.position.ReadValue();
        Camera camera = Camera.main;
        var ray = camera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            character.MoveTo(hit.point);
        }
    }

    public void OnInteraction()
    {
        character.animator.SetTrigger("Axe");
    }
}
