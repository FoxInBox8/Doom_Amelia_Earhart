using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed, jumpForce, gravity, startHealth, cameraXSensitivity, cameraYSensitivity;

    private float cameraYaw = 0, cameraPitch = 0, yVelocity, currentHealth;

    private Transform mainCamera;
    private CharacterController controller;
    private PlayerControls playerInput;

    private void Start()
    {
        mainCamera = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        playerInput = new PlayerControls();

        playerInput.Game.Enable();

        currentHealth = startHealth;
    }

    private void Update()
    {
        updateCamera();
        updateMovement();
    }

    private void updateCamera()
    {
        Vector2 cameraInput = playerInput.Game.Camera.ReadValue<Vector2>();

        cameraYaw += cameraXSensitivity * cameraInput.x;
        cameraPitch -= cameraYSensitivity * cameraInput.y;

        // Clamp y axis to prevent camera from going too far
        cameraPitch = Mathf.Clamp(cameraPitch, -90, 90);

        // Player only rotates on x axis to prevent collision oddities
        transform.eulerAngles = new Vector3(0, cameraYaw, 0);
        mainCamera.eulerAngles = new(cameraPitch, cameraYaw, 0);
    }

    private void updateMovement()
    {
        Vector2 inputVector = playerInput.Game.Movement.ReadValue<Vector2>();

        if (inputVector != Vector2.zero)
        {
            // Redirect movement vector to be in direction of transform
            Vector3 movementVector = transform.right * inputVector.x + transform.forward * inputVector.y;

            controller.Move(moveSpeed * Time.deltaTime * movementVector.normalized);
        }

        // Can only jump when grounded
        if(!controller.isGrounded && playerInput.Game.Jump.WasPerformedThisFrame())
        {
            yVelocity = jumpForce;
        }

        yVelocity -= gravity * Time.deltaTime;
        controller.Move(new Vector3(0, yVelocity, 0) * Time.deltaTime);
    }

    public void dealDamage(float damage)
    {
        currentHealth -= damage;

        // Incomplete for now
        if(currentHealth <= 0)
        {
            throw new System.NotImplementedException();
        }
    }

    public void heal(float health)
    {
        currentHealth += health;

        // Never heal above starting health
        currentHealth = Mathf.Min(currentHealth, startHealth);
    }
}