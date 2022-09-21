using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FSMController : MonoBehaviour
{
    private IState currentState; // IState for the current state
    public FSMIdle IdleState; // Idle state
    public FSMFollowState FollowState; // Follow state
    public FSMWalkAwayState WalkAwayState; // Walk Away state

    [SerializeField] private Text stateText; // Text to show what state we are in

    private NavMeshAgent navMeshAgent; // NavMeshAgent component

    [Header("Perception")]
    [SerializeField] private GameObject agentSeenIndicator; // indicator for perception

    [SerializeField] private float perceptionRadius = 5; // radius of perception
    [Range(0, 360)] [SerializeField] private float perceptionAngle = 30; // angle of perception
    [SerializeField] private LayerMask agentMask; // layer mask for the agent
    [SerializeField] private LayerMask obstacleMask; // layer mask for the obstacles
    [SerializeField] private int numberOfRaysPerDegree; // number of rays per degree, used for the visual cone
    [SerializeField] private MeshFilter perceptionMeshFilter; // MeshFilter used for the visual cone

    private Mesh perceptionMesh; // Mesh used for the visual cone

    private Vector3 lastKnownPosition = new Vector3(); // last position known of the other agent
    private Vector3 lastKnownDirection = new Vector3(); // last direction known of the other agent
    private float lastKnownSpeed; // last speed known of the other agent
    private Transform otherAgent; // Transform of the other agent

    [Header("Follow and Walk Away")]
    [SerializeField] private float minFollowDistance = 2; // minimum distance for the follow state
    [SerializeField] private float maxWalkAwayDistance = 8; // maximum distance for the walk away state
    [SerializeField] private float reachedDestinationDistanceThreshold = 0f; // threshold for the reached last known destination method


    /// <summary>
    /// We get the NavMeshAgent component, initialize the states, initialize a random rotation and initialize the perception cone mesh
    /// </summary>
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        IdleState = new FSMIdle();
        FollowState = new FSMFollowState();
        WalkAwayState = new FSMWalkAwayState();

        ChangeToState(IdleState);

        int randomRotationMultiplier = Random.Range(0, 4);
        transform.Rotate(0, 90 * randomRotationMultiplier, 0);

        perceptionMesh = new Mesh();
        perceptionMesh.name = "Perception Mesh";
        perceptionMeshFilter.mesh = perceptionMesh;
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

        Collider[] agentsInPerceptionRadius = Physics.OverlapSphere(transform.position, perceptionRadius, agentMask);
        for (int i = 0; i < agentsInPerceptionRadius.Length; i++)
        {
            Transform agent = agentsInPerceptionRadius[i].transform;
            Vector3 direction = (agent.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, direction) <= perceptionAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, agent.position);
                if (!Physics.Raycast(transform.position, direction, distance, obstacleMask))
                {
                    lastKnownPosition = agent.position;
                    lastKnownDirection = agent.forward;
                    lastKnownSpeed = agent.GetComponent<NavMeshAgent>().speed;
                    otherAgent = agent;
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
        Vector3 direction = otherAgent.position - transform.position;
        float lookAhead = direction.magnitude / otherAgent.GetComponent<NavMeshAgent>().speed;
        Vector3 futurePosition = otherAgent.transform.position + otherAgent.transform.forward * lookAhead;
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
        if(isFollowing)
        {
            return distance < minFollowDistance;
        }
        else
        {
            return distance > maxWalkAwayDistance;
        }
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
    public void WalkAwayFromAgent()
    {
        Vector3 direction = lastKnownDirection * -1;
        float lookAhead = direction.magnitude / lastKnownSpeed;
        Vector3 oppositePosition = transform.position + direction * lookAhead;
        oppositePosition.y = 0;
        navMeshAgent.destination = oppositePosition;
    }
}
