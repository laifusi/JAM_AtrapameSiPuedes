using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FSMController : MonoBehaviour
{
    private IState currentState; // IState for the current state
    public FSMIdleState IdleState; // Idle state
    public FSMFollowState FollowState; // Follow state

    [SerializeField] private Text stateText; // Text to show what state we are in

    private NavMeshAgent navMeshAgent; // NavMeshAgent component

    [Header("Perception")]
    [SerializeField] private GameObject agentSeenIndicator; // indicator for perception

    [SerializeField] private float perceptionRadius = 5; // radius of perception
    [Range(0, 360)] [SerializeField] private float perceptionAngle = 30; // angle of perception
    [SerializeField] private LayerMask playerMask; // layer mask for the agent
    [SerializeField] private LayerMask obstacleMask; // layer mask for the obstacles

    private Vector3 lastKnownPosition = new Vector3(); // last position known of the other agent
    private Vector3 lastKnownDirection = new Vector3(); // last direction known of the other agent
    private float lastKnownSpeed; // last speed known of the other agent
    private Transform player; // Transform of the other agent

    [Header("Follow")]
    [SerializeField] private float minFollowDistance = 2; // minimum distance for the follow state
    [SerializeField] private float reachedDestinationDistanceThreshold = 0f; // threshold for the reached last known destination method

    [SerializeField] private Transform initialTransform;


    /// <summary>
    /// We get the NavMeshAgent component, initialize the states, initialize a random rotation and initialize the perception cone mesh
    /// </summary>
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        IdleState = new FSMIdleState();
        FollowState = new FSMFollowState();

        ChangeToState(IdleState);
    }
    
    /// <summary>
    /// Method to change from one state to another
    /// </summary>
    /// <param name="state">IState we have to switch to</param>
    public void ChangeToState(IState state)
    {
        currentState = state;
    }

    /// <summary>
    /// Update to call the currentState's UpdateState
    /// </summary>
    private void Update()
    {
        currentState.UpdateState(this);
    }

    /// <summary>
    /// OnTriggerEnter to call the currentState's OnTrigger
    /// </summary>
    /// <param name="other">Collider we triggered</param>
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTrigger(this, other);
    }

    /// <summary>
    /// Method where we check whether we are seeing an agent or not
    /// We find the agents in the radius
    /// We check if they are in the perception angle
    /// We check if there's any obstacle in between
    /// If we see an agent, we save its location, direction, speed and Transform
    /// </summary>
    /// <returns>true if we see an agent, false if we don't</returns>
    public bool Perceive()
    {
        //ActivateAgentSeenIndicator(false);

        Collider[] playersInSight = Physics.OverlapSphere(transform.position, perceptionRadius, playerMask);
        for (int i = 0; i < playersInSight.Length; i++)
        {
            Transform playerSeen = playersInSight[i].transform;
            Vector3 direction = (playerSeen.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, direction) <= perceptionAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, playerSeen.position);
                if (!Physics.Raycast(transform.position, direction, distance, obstacleMask))
                {
                    lastKnownPosition = playerSeen.position;
                    lastKnownDirection = playerSeen.forward;
                    lastKnownSpeed = playerSeen.GetComponent<NavMeshAgent>().speed;
                    player = playerSeen;
                    //ActivateAgentSeenIndicator(true);
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Method to activate or deactivate the perception indicator
    /// </summary>
    /// <param name="activate">bool that defines if we activate or deactivate</param>
    /*public void ActivateAgentSeenIndicator(bool activate)
    {
        agentSeenIndicator.SetActive(activate);
    }*/

    /// <summary>
    /// Method to follow an agent we see
    /// We calculate the direction and the future position to set our destination
    /// </summary>
    public void FollowAgent()
    {
        Vector3 direction = player.position - transform.position;
        float lookAhead = direction.magnitude / player.GetComponent<NavMeshAgent>().speed;
        Vector3 futurePosition = player.transform.position + player.transform.forward * lookAhead;
        navMeshAgent.destination = futurePosition;
    }

    /// <summary>
    /// Method to know the distance between the two agents
    /// </summary>
    /// <returns>distance to the agent seen</returns>
    public float DistanceToAgent()
    {
        return Vector3.Distance(lastKnownPosition, transform.position);
    }

    /// <summary>
    /// Method to check if we are too far or too close from the agent
    /// </summary>
    /// <param name="distance">distance to the agent's last known position</param>
    /// <param name="isFollowing">bool that tells us if we are in following state or in walk away state</param>
    /// <returns>true if we are too close or too far away</returns>
    public bool CheckDistance(float distance, bool isFollowing)
    {
        return distance < minFollowDistance;
    }

    /// <summary>
    /// Method to know if we reached the last known location
    /// </summary>
    /// <returns>true if we reached the location, false if we didn't</returns>
    public bool InLastLocationKnown()
    {
        return navMeshAgent.remainingDistance < reachedDestinationDistanceThreshold;
    }

    /// <summary>
    /// Method to set the last location known as the NavMeshAgent destination
    /// </summary>
    public void GoToLastLocationKnown()
    {
        navMeshAgent.destination = lastKnownPosition;
    }

    /// <summary>
    /// Method to walk away from the agent
    /// We define the direction as the opposite of the agent's
    /// We calculate the lookAhead of the agent and the opposite position
    /// We set the opposite position as our destination
    /// </summary>
    /*public void WalkAwayFromAgent()
    {
        Vector3 direction = lastKnownDirection * -1;
        float lookAhead = direction.magnitude / lastKnownSpeed;
        Vector3 oppositePosition = transform.position + direction * lookAhead;
        oppositePosition.y = 0;
        navMeshAgent.destination = oppositePosition;
    }*/

    /////////// REVISAR ESTOS TRES MÉTODOS //////////
    public bool OnInitialPosition()
    {
        return Mathf.Abs(Vector3.Distance(transform.position, initialTransform.position)) < reachedDestinationDistanceThreshold;
    }

    public void MoveToInitialPosition()
    {
        navMeshAgent.destination = initialTransform.position;
    }

    public void Idle()
    {
        Debug.Log(transform.rotation);
        Debug.Log(initialTransform.rotation);
        if(transform.position != initialTransform.position)
        {
            transform.position = initialTransform.position;
        }
        if(transform.rotation != initialTransform.rotation)
        {
            transform.rotation = initialTransform.rotation;
        }
    }

    public void StopAgent()
    {
        navMeshAgent.Stop();
    }
}
