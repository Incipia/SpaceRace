using UnityEngine;
using System.Collections;

public class SliderMotorFlip : MonoBehaviour
{
	private SliderJoint2D sliderJoint;
	private float initialMotorSpeed;
	
	void Start()
	{
		sliderJoint = GetComponent<SliderJoint2D>();
		initialMotorSpeed = sliderJoint.motor.motorSpeed;
	}
	
	// Update is called once per frame
	void Update() 
	{
		updateSliderJointDirectionIfNecessary();
	}
	
	private void updateSliderJointDirectionIfNecessary()
	{	
		bool motorDirectionShouldChange = false;
		
		// used to change dirction, pos:left, neg:right
		float motorSpeedMultiplier = 1;
		
		switch (sliderJoint.limitState)
		{
		case JointLimitState2D.LowerLimit:
		{
			motorDirectionShouldChange = true;			
			motorSpeedMultiplier = (initialMotorSpeed < 1) ?  -1 : 1;
			break;
		}
		case JointLimitState2D.UpperLimit:
		{
			motorDirectionShouldChange = true;
			motorSpeedMultiplier = (initialMotorSpeed < 1) ?  1 : -1;
			break;
		}
		default:
			break;
		}
		
		if (motorDirectionShouldChange)
		{
			JointMotor2D newMotor = new JointMotor2D();
			newMotor.motorSpeed = initialMotorSpeed * motorSpeedMultiplier;
			newMotor.maxMotorTorque = sliderJoint.motor.maxMotorTorque;
			
			sliderJoint.motor = newMotor;
		}
	}
}
