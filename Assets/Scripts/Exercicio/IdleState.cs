public class IdleState : PlayerState
{
    public IdleState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Update()
    {
        if (player.JumpPressed && player.IsGrounded)
        {
            stateMachine.ChangeState(player.JumpState);
            return;
        }

        if (player.Horizontal != 0 || player.Vertical != 0)
        {
            if (player.IsRunning)
                stateMachine.ChangeState(player.RunState);
            else
                stateMachine.ChangeState(player.WalkState);
        }
    }
}