using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerBendController : MonoBehaviour
{
    private CharacterControllerDriver characterControllerDriver;
    [SerializeField]
    private string BendEnablerTag = "BendEnabler";
    private void Awake()
    {
        characterControllerDriver = GetComponent<CharacterControllerDriver>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(BendEnablerTag))
        {
            characterControllerDriver.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(BendEnablerTag))
        {
            characterControllerDriver.enabled = true;
        }
    }
}
