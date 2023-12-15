using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This class controls the TextMeshPro text field.
/// </summary>
public class TMPController : MonoBehaviour
{
    [SerializeField] private TextMeshPro textElement;
    private GameObject mainBase;
    public MonsterSpawner monsterSpawner;
    [SerializeField]
    private Canvas canvas;
    

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        textElement.enabled = false;
        mainBase = GameObject.Find("castle-tower");
    }

    // Update is called once per frame
    void Update()
    {
        if (mainBase == null)
        {
            textElement.enabled = true;
            textElement.text = "Defeat";
            canvas.enabled = true;
        }

        // Check if the monster spawner exists and if it has finished all waves
        if (monsterSpawner.WavesEnded() == true)
        {
            textElement.enabled = true;
            textElement.text = "Victory";
            canvas.enabled = true;
        }
    }
}
