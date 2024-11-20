using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController playerController;
    [SerializeField] private InputAction moveAction;
    [SerializeField] private GameObject playerMesh;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    void Start()
    {
        moveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputDir = moveAction.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(inputDir.x, 0,inputDir.y);
        playerController.Move(moveDir.normalized * Time.deltaTime * moveSpeed);
        if (moveDir != Vector3.zero)
        {
            playerMesh.transform.rotation = Quaternion.Lerp(
                                                            playerMesh.transform.rotation,
                                                            Quaternion.LookRotation(moveDir, Vector3.up),
                                                            turnSpeed);
        }
    }
}
