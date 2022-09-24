using UnityEngine;

public class FSMPatrolState : IState
{
    /// <summary>
    /// UpdateState for the Patrol state
    /// </summary>
    /// <param name="controller">FSMController</param>
    public void UpdateState(FSMController controller)
    {
        if (controller.Perceive())
        {
            controller.ChangeToState(controller.FollowState);
        }
        else if (controller.ReachedDestination())
        {
            controller.NextPatrolPoint();
            controller.MoveToCurrentPoint();
        }
        else
        {
            controller.MoveToCurrentPoint();
        }
    }

    public void OnTrigger(FSMController controller, Collider other) { }
}
