using UnityEngine;
using System.Collections;

/// <summary>
/// Varies the motor speed between -maxForce and maxForce at variationPerSec rate to "activate" the motors.
/// </summary>
public class VaryMotorSpeed : MonoBehaviour {

    private HingeJoint2D _hingeJoint;

    public float maxForce = 50f;
    public float variationPerSec = 15f;
    public bool incrementing = true;
    public float _curForce = 0;
    

	void Start () {
        _hingeJoint = gameObject.GetComponent<HingeJoint2D>();
        _curForce = Random.Range(-maxForce,maxForce);
        
	}


    void Update() {
        if (incrementing) {
            _curForce += variationPerSec * Time.deltaTime;
            if (_curForce > maxForce) incrementing = false;
        } else {
            _curForce -= variationPerSec * Time.deltaTime;
            if (_curForce < -maxForce) incrementing = true;
        }
       
        JointMotor2D tempMotor = new JointMotor2D();
        tempMotor.maxMotorTorque = 10000f;
        tempMotor.motorSpeed = _curForce;
        _hingeJoint.motor = tempMotor;
    }




	
}
