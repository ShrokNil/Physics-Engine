using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour
{
    public float fuelMass;              // [kg]
    public float maxThrust;             // kN [kg m s^-2]

    [Range(0, 1f)]
    public float thrustPercent;         // [none]

    public Vector3 thrustUnitVector;    // [none]
    private float currentThrust;        // N
    private PhysicsEngine physicsEngine; 

    // Use this for initialization
    void Start()
    {
        physicsEngine = GetComponent<PhysicsEngine>();
        physicsEngine.mass += fuelMass;
    }


    void FixedUpdate()
    {
        if (fuelMass > FuelThisUpdate()) { 
            fuelMass -= FuelThisUpdate();
            physicsEngine.mass -= FuelThisUpdate();
            physicsEngine.AddForce(thrustUnitVector);
            ExertForce();

        } else
        {
            Debug.LogWarning("out of rocket fuel");
        }
    }



    float FuelThisUpdate()
    {                           
        float exhaustMassFlow;                          // [
        float effectiveExhastVelocity;                  // [

        effectiveExhastVelocity = 4462f;                // [m s^-1]  liquid H O

        //thrust = massFlow * exhaustVelocity
        //massFlow = Thrust / exhaustVelocity

        exhaustMassFlow = currentThrust / effectiveExhastVelocity;

        return exhaustMassFlow * Time.deltaTime;
    }

    void ExertForce()
    {
        currentThrust = thrustPercent * maxThrust * 1000f;
        Vector3 thrustVector = thrustUnitVector.normalized * currentThrust; // N        //.normalized takes the only the directional information not the value.
        physicsEngine.AddForce(thrustVector);

    } 

}


