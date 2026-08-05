public class JumpState : PlayerState
{
    private bool jumped;

    public JumpState(PlayerController player, StateMachine stateMachine)
        : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        jumped = false;
    }

    public override void Update()
    {
        if (!jumped)
        {
            player.Jump();
            jumped = true;
        }

        float speed = player.IsRunning ? player.runSpeed : player.walkSpeed;
        player.Move(speed);

        if (player.IsGrounded && player.rb.velocity.y <= 0)
        {
            if (player.Horizontal == 0 && player.Vertical == 0)
                stateMachine.ChangeState(player.IdleState);
            else if (player.IsRunning)
                stateMachine.ChangeState(player.RunState);
            else
                stateMachine.ChangeState(player.WalkState);
        }
    }
}