using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tank))]
public class TankController : MonoBehaviour
{
    public float maxOffset;
    private TankControls tankControls;
    private Tank tank;

    private void Awake(){
        Initialize();
    }

    private void Initialize(){
        tank = GetComponent<Tank>();
        tankControls = new TankControls();

        tankControls.Tank.Shoot.performed += ctx => tank.ShootProjectile();
    }

    private void Update(){
        if(tankControls.Tank.Movement.IsPressed()){
            Move(tankControls.Tank.Movement.ReadValue<float>());
        }
    }

    private void Move(float direction){
        Vector3 move = new Vector3(direction * tank.stats.moveSpeed * Time.deltaTime, 0, 0);
        transform.Translate(move, Space.Self);
        LimitOffset();
    }

    private void LimitOffset(){
        // Make sure the tank does not move above the max offset
        Vector3 maxPosition = transform.localPosition;

        if(maxPosition.x > maxOffset)
            maxPosition.x = maxOffset;
        else if(maxPosition.x < -maxOffset)
            maxPosition.x = -maxOffset;

        transform.localPosition = maxPosition;
    }

    private void OnEnable(){
        tankControls.Enable();
    }

    private void OnDisable(){
        tankControls.Disable();
    }
}
