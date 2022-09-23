using UnityEngine;

public class FSMBackToIdle : IState
{
    /// <summary>
    /// UpdateState for the Walk Away state
    /// We walk away from the agent we saw
    /// We check if we are too far from the agent, in which case we go back to the Follow state
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
            controller.BackToInit();
            controller.ChangeToState(controller.IdleState);
        }
    }

    public void OnTrigger(FSMController controller, Collider other) { }
}
