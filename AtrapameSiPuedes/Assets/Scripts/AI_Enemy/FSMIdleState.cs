using UnityEngine;

public class FSMIdleState : IState
{
    /// <summary>
    /// UpdateState for the Idle state
    /// </summary>
    /// <param name="controller">FSMController</param>
    public void UpdateState(FSMController controller)
    {
        if (controller.Perceive())
        {
            controller.ChangeToState(controller.FollowState);
        }
        else if (!controller.OnInitialPosition())
        {
            controller.MoveToInitialPosition();
        }
        else
        {
            controller.StopAgent();
            controller.Idle();
        }
    }

    public void OnTrigger(FSMController controller, Collider other) { }
}
