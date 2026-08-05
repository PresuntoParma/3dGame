public class WalkState : PlayerState
{
    public WalkState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Update()
    {
        player.Move(player.walkSpeed);

        if (player.JumpPressed && player.IsGrounded)
        {
            stateMachine.ChangeState(player.JumpState);
            return;
        }

        if (player.Horizontal == 0 && player.Vertical == 0)
        {
            stateMachine.ChangeState(player.IdleState);
            return;
        }

        if (player.IsRunning)
        {
            stateMachine.ChangeState(player.RunState);
        }
    }
}