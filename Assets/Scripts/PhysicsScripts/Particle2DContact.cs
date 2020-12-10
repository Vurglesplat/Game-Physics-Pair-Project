using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// heavily based on code from the previous assignments, which in turn was based on code
/// from Ian Millington - Game Physics Engine Development
/// </summary>

public class Particle2DContact
{
    public Particle2D particle1;
    public Particle2D particle2;

    float restitutionCoefficient = 0.9f;

    public float penetration;
	public Vector2 move1;
	public Vector2 move2;

	public Vector2 contactNorm;

	public void setNewVars(Particle2D part1, Particle2D part2, float resCoeff,
		Vector2 contactNormal, float pen, Vector2 move1, Vector2 move2)
    {
		particle1 = part1;
		particle2 = part2;
		restitutionCoefficient = resCoeff;
		contactNorm = contactNormal;
		penetration = pen;
		this.move1 = move1;
		this.move2 = move2;
    }

	public float CalculateSeparatingVelocity()
	{
		Vector2 relativeVel = particle1.velocity;
		if (particle2)
		{
			relativeVel -= particle2.velocity;
		}
		return Vector2.Dot(relativeVel, contactNorm);
	}

	public void Resolve(double dt)
    {
		ResolveVelocity(dt);
		ResolveInterpenetration(dt);
	}

	void ResolveVelocity(double dt)
    {
		Vector2 relativeVel = particle1.velocity;
		if (particle2)
		{
			relativeVel -= particle2.velocity;
			contactNorm = particle2.transform.position - particle1.transform.position;
		}

		float separatingVel = Vector2.Dot(relativeVel, contactNorm);

		if (separatingVel > 0.0f)//already separating so need to resolve
			return;

		float newSepVel = -separatingVel * restitutionCoefficient;


		Vector2 velFromAcc = particle1.acceleration;
		if (particle2)
			velFromAcc -= particle2.acceleration;
		float accCausedSepVelocity = Vector2.Dot(velFromAcc, contactNorm) * (float)dt;

		if (accCausedSepVelocity < 0.0f)
		{
			newSepVel += restitutionCoefficient * accCausedSepVelocity;
			if (newSepVel < 0.0f)
				newSepVel = 0.0f;
		}

		float deltaVel = newSepVel - separatingVel;

		float totalInverseMass = 1.0f / particle1.inverseMass;
		if (particle2)
			totalInverseMass += particle2.inverseMass;

		if (totalInverseMass <= 0)//all infinite massed objects
			return;

		float impulse = deltaVel / totalInverseMass;
		Vector2 impulsePerIMass = contactNorm * impulse;

		Vector2 newVelocity = particle1.velocity + impulsePerIMass * particle1.inverseMass;
		particle1.velocity = newVelocity;
		if (particle2)
		{
			Vector2 newPart2Velocity = particle2.velocity + impulsePerIMass * -particle2.inverseMass;
			particle2.accumulatedForces += newPart2Velocity;
		}
	}
	void ResolveInterpenetration(double dt)
    {
        if (penetration <= 0.0f)
            return;

        float totalInverseMass = particle1.inverseMass;
        if (particle2)
            totalInverseMass += particle2.inverseMass;

        if (totalInverseMass <= 0)//all infinite massed objects
            return;

        Vector2 movePerIMass = contactNorm * (penetration / totalInverseMass);

		//Debug.Log("mov per I Mass = " + movePerIMass);

        move1 = movePerIMass * particle1.inverseMass;
        if (particle2)
            move2 = -movePerIMass * particle2.inverseMass;
        else
            move2 = new Vector2(0.0f, 0.0f);

        Vector2 newPosition = new Vector2(particle1.transform.position.x, particle1.transform.position.y) + (move1);
        particle1.transform.position = newPosition;
		if (particle2)
		{
		    newPosition = new Vector2(particle2.transform.position.x, particle2.transform.position.y) - (move2);
		    particle2.transform.position = newPosition;
		}

	}

}
