using UnityEngine;

public class FSMFollowState : IState
{
    float timeSinceLastSeen;

    /// <summary>
    /// UpdateState for the Follow state
    /// We check if we see the agent
    /// If we do, we follow it and check if we are too close, in which case we change to the Walk Away state
    /// If we don't see the agent, we check if we are in the last place we saw it
    /// If we are, we change to the wander state
    /// If we are not, we go to the last known location
    /// </summary>
    /// <param name="controller">FSMController</param>
    public void UpdateState(FSMController controller)
    {
        if (controller.Perceive())
        {
            controller.FollowAgent();
            timeSinceLastSeen = 0;
        }
        else
        {
            bool locationReached = controller.InLastLocationKnown();
            if(locationReached)
            {
                if(timeSinceLastSeen < controller.SearchingTime)
                {
                    timeSinceLastSeen += Time.deltaTime;
                    return;
                }
                controller.ChangeToState(controller.BackToIdleState);
            }
            else
            {
                controller.GoToLastLocationKnown();
            }
        }
    }

    public void OnTrigger(FSMController controller, Collider other) { }
}
