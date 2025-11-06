using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public Transform target; // Drag your target GameObject here in the Inspector
    public float rotationSpeed = 5f; // Adjust rotation speed as needed

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Ensure updateRotation is false so we can control it manually
        if (agent != null)
        {
            agent.updateRotation = false;
        }
    }

    void Update()
    {
        if (agent != null && target != null)
        {
            // 1. Set the destination for the NavMeshAgent to handle pathfinding
            agent.SetDestination(target.position);

            // 2. Handle the agent's rotation manually
            FaceTarget();
        }
    }

    private void FaceTarget()
    {
        // Get the next point on the path that the agent is steering towards
        Vector3 steeringTarget = agent.steeringTarget;

        // Calculate the direction to the steering target
        Vector3 direction = (steeringTarget - transform.position).normalized;

        // Only rotate if there's a valid direction and we are far enough from the target
        if (direction != Vector3.zero && agent.remainingDistance > agent.stoppingDistance)
        {
            // Create a rotation Quaternion looking in that direction (ignoring y-axis for 2D/top-down)
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // Smoothly rotate the agent towards the new rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
