using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 direction; // Direcci�n de la flecha, asignada en el Inspector
    private bool isDirectionChanged = false; // Indica si la direcci�n ha sido cambiada
    private bool hasPassedEndArrow = false; // Indica si el jugador ha pasado por la �ltima flecha
    private Color originalColor; // Color original de la flecha
    private Renderer arrowRenderer;
    private Quaternion originalRotation; // Rotaci�n original de la flecha

    private void Start()
    {
        arrowRenderer = GetComponent<Renderer>();
        originalColor = arrowRenderer.material.color;
        originalRotation = transform.rotation; // Guardar la rotaci�n original
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LeftCalculator speedCalculator = other.GetComponent<LeftCalculator>();
            if (speedCalculator != null && IsMovingInCorrectDirection(other.transform))
            {
                speedCalculator.OnArrowPassed(this);
                ChangeColor(Color.cyan); // Cambia el color al ser pisada
            }
            RightCalculator rightPathSpeedCalculator = other.GetComponent<RightCalculator>();
            if (rightPathSpeedCalculator != null && IsMovingInCorrectDirection(other.transform))
            {
                rightPathSpeedCalculator.OnArrowPassed(this);
                ChangeColor(Color.cyan); // Cambia el color al ser pisada
            }

            // Aqu� es donde manejamos el cambio de direcci�n
            if (CompareTag("EndArrow") && other.CompareTag("RightPath"))
            {
                hasPassedEndArrow = true; // Marcar que se ha pasado la flecha final
            }

            if (CompareTag("MidArrow") && hasPassedEndArrow && other.CompareTag("RightPath"))
            {
                ChangeDirection(); // Cambia la direcci�n si ya ha pasado por la flecha final
            }
        }
    }

    private bool IsMovingInCorrectDirection(Transform playerTransform)
    {
        Vector3 playerDirection = playerTransform.GetComponent<CharacterController>().velocity.normalized;
        // Comprobar si el jugador se mueve en la direcci�n de la flecha
        return Vector3.Dot(direction.normalized, playerDirection) > 0; // Si el producto punto es positivo, va en la direcci�n correcta
    }

    public void ChangeColor(Color color)
    {
        arrowRenderer.material.color = color;
    }

    // M�todo para cambiar la direcci�n de la flecha
    public void ChangeDirection()
    {
        direction = -direction; // Cambiar la direcci�n a la opuesta
        isDirectionChanged = !isDirectionChanged; // Alternar el estado de direcci�n cambiada
        Debug.Log("La direcci�n de la flecha ha sido cambiada a: " + direction);
    }

    public void SetEndArrowPassed(bool value)
    {
        hasPassedEndArrow = value; // M�todo para establecer si se ha pasado la flecha final
    }

    public void RotateArrow()
    {
        // Rotar 180 grados
        transform.Rotate(0, 180, 0);
        // Actualizar la direcci�n
        direction = transform.forward;
        Debug.Log("La flecha ha sido rotada. Nueva direcci�n: " + direction);
    }

    // M�todo para restablecer el color original de la flecha
    public void ResetColor()
    {
        arrowRenderer.material.color = originalColor;
    }

    // M�todo para restablecer la direcci�n y la rotaci�n original de la flecha
    public void ResetArrow()
    {
        transform.rotation = originalRotation; // Restablecer la rotaci�n original
        direction = transform.forward; // Restablecer la direcci�n original
        isDirectionChanged = false; // Restablecer el estado de direcci�n cambiada
        ResetColor(); // Restablecer el color original
    }
}
