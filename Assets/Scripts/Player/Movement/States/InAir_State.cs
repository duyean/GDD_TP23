using States;
using UnityEngine;

public class InAir_State : State
{
    public InAir_State(Player player, StateMachine stateMachine) : base(player, stateMachine)
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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.jumping.grounded)
        {
            stateMachine.ChangeState(player.grounded);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.rb.velocity = new Vector2(player.direction.x * player.speed, player.currentVelocity.y);
    }
}
