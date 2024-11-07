using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;
    public Transform[] sectionStartPositions;  
    public int currentSection = 0;             
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

            CharacterController charController = player.GetComponent<CharacterController>();
            if (charController != null)
            {
                charController.enabled = false;  
            }

            player.transform.position = sectionStartPositions[currentSection].position;

            if (charController != null)
            {
                charController.enabled = true;  
            }

            ResetSection();
            isPlayerDead = false;
            Time.timeScale = 1f;  
        }
        else
        {
            Debug.LogError("Player or section start position is not assigned, or current section index is out of range.");
        }
    }


    public void ResetSection()
    {

    }

    // Mostrar mensaje de derrota
    public void ShowDefeatMessage()
    {
        isPlayerDead = true;
        Time.timeScale = 0f; 

    }

    public void AdvanceToNextSection()
    {
        if (currentSection < sectionStartPositions.Length - 1)
        {
            currentSection++;  
            Debug.Log("Now in section " + currentSection);
        }
        else
        {
            Debug.Log("No more sections to advance.");
        }
    }


    public void CompleteSection()
    {
        AdvanceToNextSection();  
    }
}
