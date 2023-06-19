using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform targetTransform2;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer buildingMeshRenderer;
    [SerializeField] private List<Transform> foundGoals = new List<Transform>();

    [SerializeField] private float rayDistance = 10f;



    // Resets the state to continue training once goal is achieved or not
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(30f, -20f), -3.81f, Random.Range(-14f, +6f));
        targetTransform.localPosition = new Vector3(Random.Range(45f, -7f), 2.91f, Random.Range(-37f, -11f));
        targetTransform2.localPosition = new Vector3(Random.Range(45f, -7f), 2.91f, Random.Range(-37f, -11f));

        foundGoals.Clear();
    }

    // How an Agent observes its envrionment
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
        sensor.AddObservation(targetTransform2.localPosition);

        // Perform raycasts to detect the goal
        RaycastHit hit;
        Vector3 raycastDirection = targetTransform.position - transform.position;
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

        // Perform raycasts to detect the goal
        RaycastHit hit2;
        Vector3 raycastDirection2 = targetTransform2.position - transform.position;
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

        float moveSpeed = 6f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

    }

    // Modify actions that will be recieved in OnActionsRecieved function
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
            if (!foundGoals.Contains(goal.transform))
            {
                foundGoals.Add(goal.transform);
                SetReward(+20f); // Provide a reward for finding a goal
            }

            // Check if all goals have been found
            if (foundGoals.Count == 2)
            {
                SetReward(+100f); // Provide a higher reward for finding all goals
                buildingMeshRenderer.material = winMaterial;

                EndEpisode(); // End the episode only when all goals have been found
            }
        }

        // Check if the agent collided with a wall
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-10f);
            buildingMeshRenderer.material = loseMaterial;

            EndEpisode(); // End the episode when the agent hits a wall
        }
    }

}