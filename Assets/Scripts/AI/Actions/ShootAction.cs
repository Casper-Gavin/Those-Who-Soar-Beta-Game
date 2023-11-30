using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Shoot", fileName = "ShootAction")]
public class ShootAction : AIAction
{
    private Vector2 aimDirection;

    public override void Act(StateController controller)
    {
        DetermineAim(controller);
        ShootPlayer(controller);
    }

    private void ShootPlayer(StateController controller)
    {
        // halt movement
        controller.CharacterMovement.SetHorizontal(0);
        controller.CharacterMovement.SetVertical(0);

        if (controller.CharacterWeapon.CurrentWeapon != null)
        {
            controller.CharacterWeapon.CurrentWeapon.WeaponAim.SetAim(aimDirection);
            controller.CharacterWeapon.CurrentWeapon.Attack();
        }
    }

    private void DetermineAim(StateController controller)
    {
        aimDirection = controller.Target.position - controller.transform.position;
    }
}