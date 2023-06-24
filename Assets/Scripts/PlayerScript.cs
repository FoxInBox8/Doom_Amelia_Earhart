using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed, jumpForce, maxFallSpeed, gravity, startHealth, cameraXSensitivity, cameraYSensitivity;

    [SerializeField]
    private Slider healthBar;

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

        currentHealth = healthBar.maxValue = startHealth;
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

        // If grounded, jump when button pressed, otherwise reset y velocity to prevent it from building up
        if (controller.isGrounded)
        {
            yVelocity = playerInput.Game.Jump.WasPerformedThisFrame() ? jumpForce : 0;
        }

        // We need to do this even when grounded to make sure isGrounded works correctly
        yVelocity -= gravity * Time.deltaTime;

        // Don't fall faster than max fall speed
        yVelocity = Mathf.Max(yVelocity, maxFallSpeed);

        controller.Move(new Vector3(0, yVelocity, 0) * Time.deltaTime);
    }

    public void dealDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

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

        healthBar.value = currentHealth;
    }
}