using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara_detector : MonoBehaviour
{

    public float limit_pos;

    public float limit_neg;

    public float vel_cam = 5;

    public float timer_limit = 20;
    public float timer = 0;

    public float dir = 1;

    public GameObject[] agentes_zona;

    [Header("Perception")]
    [SerializeField] private GameObject agentSeenIndicator; // indicator for perception

    [SerializeField] private float perceptionRadius = 15; // radius of perception
    [Range(0, 360)] [SerializeField] private float perceptionAngle = 50; // angle of perception
    [SerializeField] private LayerMask playerMask; // layer mask for the agent
    [SerializeField] private LayerMask obstacleMask; // layer mask for the obstacles

    private Vector3 lastKnownPosition = new Vector3(); // last position known of the other agent
    private Vector3 lastKnownDirection = new Vector3(); // last direction known of the other agent
    private float lastKnownSpeed; // last speed known of the other agent
    private Transform player; // Transform of the other agent

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rotationVector = this.transform.rotation.eulerAngles;
        rotationVector.y += dir * vel_cam * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(rotationVector);
        timer += Time.deltaTime;
        if(timer >= timer_limit)
        {
            timer = 0;
            dir = dir * -1;
        }

        if(this.Perceive())
        {
            Debug.Log("Personaje detectado");
            foreach(GameObject agent in agentes_zona)
            {
                FSMController c = agent.GetComponent<FSMController>();
                c.Alarma(lastKnownPosition);
            }
        }
    }

    public bool Perceive()
    {
        //ActivateAgentSeenIndicator(false);

        Collider[] playersInSight = Physics.OverlapSphere(transform.position, perceptionRadius, playerMask);
        for (int i = 0; i < playersInSight.Length; i++)
        {
            Transform playerSeen = playersInSight[i].transform;
            Vector3 direction = (playerSeen.position - transform.position).normalized;
            //Debug.Log(Vector3.Angle(transform.forward, direction) + " <= " + perceptionAngle / 2);
            if (Vector3.Angle(transform.forward, direction) <= perceptionAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, playerSeen.position);
                if (!Physics.Raycast(transform.position, direction, distance, obstacleMask))
                {
                    lastKnownPosition = playerSeen.position;
                    lastKnownDirection = playerSeen.forward;
                    lastKnownSpeed = playerSeen.GetComponent<UnityEngine.AI.NavMeshAgent>().speed;
                    player = playerSeen;
                    //ActivateAgentSeenIndicator(true);
                    return true;
                }
            }
        }

        return false;
    }
}
