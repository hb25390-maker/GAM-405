using UnityEngine;
using Unity.Cinemachine;

public class CameraZone : MonoBehaviour
{
    public CinemachineVirtualCamera zoneCam;
    public int activePriority = 20;
    public int inactivepriority = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            zoneCam.Priority = activePriority;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            zoneCam.Priority = inactivepriority;
    }

}
