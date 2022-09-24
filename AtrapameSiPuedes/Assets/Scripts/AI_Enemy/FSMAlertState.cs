using UnityEngine;

public class FSMAlertState : IState
{
    private float timeSearching;

    /// <summary>
    /// UpdateState for the Alert state
    /// </summary>
    /// <param name="controller">FSMController</param>
    public void UpdateState(FSMController controller)
    {
        if (controller.Perceive())
        {
            controller.ChangeToState(controller.FollowState);
            timeSearching = 0;
        }
        else if (controller.ReachedDestination())
        {
            if (timeSearching < controller.SearchingTime)
            {
                timeSearching += Time.deltaTime;
                return;
            }
            if (controller.PatrolAgent)
            {
                controller.ChangeToState(controller.PatrolState);
            }
            else
            {
                controller.ChangeToState(controller.BackToIdleState);
            }
        }
        else
        {
            controller.MoveToAlertPosition();
            timeSearching = 0;
        }
    }

    public void OnTrigger(FSMController controller, Collider other) { }
}
