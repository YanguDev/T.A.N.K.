using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float moveSpeed;
    public float maxOffset;
    private TankControls tankControls;

    private void Awake(){
        Initialize();
    }

    private void Initialize(){
        tankControls = new TankControls();
    }

    private void Update(){
        if(tankControls.Tank.Movement.IsPressed()){
            Move(tankControls.Tank.Movement.ReadValue<float>());
        }
    }

    private void Move(float direction){
        Vector3 move = new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
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
