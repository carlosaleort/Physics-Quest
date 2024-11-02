using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Slider energySlider; // Slider que muestra la energ�a
    private int maxEnergyPerLevel; // M�ximo de energ�a que se puede recolectar
    private int currentKnowledgeCount; // Contador actual de conocimiento recolectado

    private void Start()
    {
        // Obt�n el m�ximo de energ�a desde KnowledgeManager
        maxEnergyPerLevel = KnowledgeManager.Instance.MaxEnergyPerLevel; // Aseg�rate de que este m�todo exista y sea accesible
        energySlider.maxValue = maxEnergyPerLevel; // Establece el m�ximo del slider
        currentKnowledgeCount = 0; // Inicializa el contador
        energySlider.value = currentKnowledgeCount; // Establece el valor inicial del slider
    }

    public void UpdateEnergyBar()
    {
        // Verifica que el conocimiento recolectado no supere el m�ximo permitido
        if (currentKnowledgeCount < maxEnergyPerLevel)
        {
            currentKnowledgeCount++; // Incrementa el contador
            energySlider.value = currentKnowledgeCount; // Actualiza el slider con el nuevo valor
            Debug.Log("Knowledge collected! Current energy: " + currentKnowledgeCount);
        }
        else
        {
            Debug.Log("M�ximo de energ�a alcanzado para este nivel.");
        }
    }

    // Este m�todo puede ser usado para resetear la barra al cambiar de nivel
    public void ResetEnergy()
    {
        currentKnowledgeCount = 0;
        energySlider.value = 0; // Resetea la barra visualmente
    }
}
