using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StatePattern;

public class MRUUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI formulaText; // Referencia al componente de texto para la f�rmula
    [SerializeField] private TextMeshProUGUI posFinalText; // Referencia al texto de la posici�n final
    [SerializeField] private float speedMRU = 5f; // Velocidad constante
    [SerializeField] private float posFinal = 100f; // Posici�n final que debe alcanzar

    private float tiempoInicio;
    private float posicionInicial;
    private PlayerController playerController;

    private bool calculating = false;

    private void Start()
    {
        // Inicializa las variables de inicio
        playerController = GetComponent<PlayerController>();
        UpdateFormulaText();
    }

    private void Update()
    {
        CalculatePosition();
    }

    private void CalculatePosition()
    {
        if (calculating)
        {

            float tiempoTranscurrido = Time.time - tiempoInicio;
            float posicionCalculada = posicionInicial + speedMRU * tiempoTranscurrido;

            formulaText.text = $"X = {posicionInicial:F2} + {speedMRU:F2} * {tiempoTranscurrido:F2}\n" +
                               $"Posici�n Actual: {posicionCalculada:F2}";

            posFinalText.text = $"Posici�n Final Objetivo: {posFinal:F2}";


            if (posicionCalculada > posFinal)
            {
                Debug.Log("Has superado la posici�n final. Has perdido.");
                Reset();
                GameManager.Instance.RestartSection();
             //   UpdateFormulaText();
                calculating = false;
            }
        }
        else
        {
            UpdateFormulaText();
        }
    }

    private void UpdateFormulaText()
    {
        formulaText.text = $"X = {posicionInicial:F2} + {speedMRU:F2} * t";
        posFinalText.text = $"Posici�n Final: {posFinal:F2}";
    }

    private void Reset()
    {
        tiempoInicio = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            calculating = true;
            Reset();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            calculating = false;
        }
    }
}
