using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneLoader : MonoBehaviour
{
    public XRSocketInteractor socketInteractor;

    private bool objectIsInSocket = false;

    void Start()
    {
        socketInteractor.onSelectEntered.AddListener(OnObjectEnteredSocket);
        socketInteractor.onSelectExited.AddListener(OnObjectExitedSocket);
    }

    void OnObjectEnteredSocket(XRBaseInteractable interactable)
    {
        objectIsInSocket = true;
    }

    void OnObjectExitedSocket(XRBaseInteractable interactable)
    {
        objectIsInSocket = false;
    }

    void Update()
    {
        if (objectIsInSocket)
        {
            SceneManager.LoadScene("MainVRScene");
        }
    }
}
