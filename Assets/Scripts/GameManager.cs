using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;
    public Transform[] sectionStartPositions;  // Array de puntos de inicio por secci�n
    public int currentSection = 0;             // �ndice de la secci�n actual
    private bool isPlayerDead = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (player == null) player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) )
        {
            RestartSection();
        }
    }

    // Reiniciar la secci�n actual
    public void RestartSection()
    {
        if (player != null && sectionStartPositions != null && currentSection < sectionStartPositions.Length)
        {
            Debug.Log("Restarting section " + currentSection);

            // Obtener el CharacterController del jugador
            CharacterController charController = player.GetComponent<CharacterController>();
            if (charController != null)
            {
                charController.enabled = false;  // Desactivar temporalmente el CharacterController
            }

            // Mover al jugador a la posici�n de inicio de la secci�n actual
            player.transform.position = sectionStartPositions[currentSection].position;

            if (charController != null)
            {
                charController.enabled = true;  // Reactivar el CharacterController
            }

            ResetSection();
            isPlayerDead = false;
            Time.timeScale = 1f;  // Reanudar el juego
        }
        else
        {
            Debug.LogError("Player or section start position is not assigned, or current section index is out of range.");
        }
    }

    // Reiniciar la secci�n, reseteando obst�culos, enemigos, etc.
    public void ResetSection()
    {
        Debug.Log("Section reset. All progress lost.");
        // Aqu� puedes agregar el c�digo para resetear cualquier objeto o enemigo de la secci�n actual.
    }

    // Mostrar mensaje de derrota
    public void ShowDefeatMessage()
    {
        isPlayerDead = true;
        Time.timeScale = 0f;  // Detener el juego
        Debug.Log("You lost. Press 'R' to restart the section.");
    }

    // Avanzar a la siguiente secci�n solo cuando se completa la secci�n
    public void AdvanceToNextSection()
    {
        if (currentSection < sectionStartPositions.Length - 1)
        {
            currentSection++;  // Avanzar a la siguiente secci�n
            Debug.Log("Now in section " + currentSection);
        }
        else
        {
            Debug.Log("No more sections to advance.");
        }
    }

    // Funci�n que se llama cuando el jugador supera una secci�n (por ejemplo, alcanzando un objetivo o pasando por un trigger)
    public void CompleteSection()
    {
        AdvanceToNextSection();  // Avanzar a la siguiente secci�n
        Debug.Log("Section completed!");
    }
}
