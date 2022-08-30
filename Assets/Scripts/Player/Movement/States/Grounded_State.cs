using States;
using UnityEngine;

public class Grounded_State : State
{
    public Grounded_State(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(player.jumping);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!player.jumping.grounded)
        {
            stateMachine.ChangeState(player.jumping);
        }
        player.move();
    }
}
