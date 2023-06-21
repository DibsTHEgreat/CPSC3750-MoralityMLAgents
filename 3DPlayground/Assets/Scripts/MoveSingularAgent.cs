using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveSingularAgent : Agent
{
    [SerializeField] private Transform targetTransform2;
    [SerializeField] private Material winMaterial2;
    [SerializeField] private Material loseMaterial2;
    [SerializeField] private MeshRenderer buildingMeshRenderer2;

    [SerializeField] private float rayDistance = 10f;

    // Resets the state to continue training once goal is achieved or not
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(30f, -20f), -3.81f, Random.Range(-14f, +6f));
        targetTransform2.localPosition = new Vector3(Random.Range(45f, -7f), 2.91f, Random.Range(-37f, -11f));

    }

    // How an Agent observes its environment
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform2.localPosition);

        // Perform raycasts to detect the goals
        RaycastHit hit;
        Vector3 raycastDirection = targetTransform2.position - transform.position;
        if (Physics.Raycast(transform.position, raycastDirection, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Goal"))
            {
                sensor.AddObservation(hit.distance);
                sensor.AddObservation(hit.normal);
            }
        }
        else
        {
            sensor.AddObservation(rayDistance);
            sensor.AddObservation(Vector3.zero);
        }
 
    }

    // This function allows AI to take actions
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 8f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    // Modify actions that will be received in OnActionsReceived function
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    // Basic Reward System
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
                SetReward(+50f); // Provide a reward for finding a goal
                buildingMeshRenderer2.material = winMaterial2;
                EndEpisode(); // End the episode only when all goals have been found
        }

        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-10f);
            buildingMeshRenderer2.material = loseMaterial2;
            EndEpisode(); // End the episode when the agent hits a wall
        }
    }

}