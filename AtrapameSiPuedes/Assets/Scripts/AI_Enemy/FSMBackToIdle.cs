using UnityEngine;

public class FSMBackToIdle : IState
{
    /// <summary>
    /// UpdateState for the Back to Idle state
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
