using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// contact manager
/// </summary>

public class Particle2DLink : MonoBehaviour
{
    [SerializeField] float lengthOfRod;
	[SerializeField] float restitutionCoefficient;

    public Particle2D particle1;
    public Particle2D particle2;

    private void Start()
    {
		Integrator.particleLinkList.Add(this);
    }

    public void Update()
    {
		float length = Vector2.Distance(particle1.transform.position, particle2.transform.position);

		Vector2 normal = particle2.transform.position - particle1.transform.position;
		normal.Normalize();
		float penetration = length - lengthOfRod;

		if (length > lengthOfRod)
		{
			Particle2DContact newContact = new Particle2DContact { };
			newContact.setNewVars(particle1, particle2, restitutionCoefficient, normal, penetration, new Vector2(0f,0f), new Vector2(0f, 0f));
			Integrator.contactList.Add(newContact);
		}
		else
		{
			Particle2DContact newContact = new Particle2DContact { };
			newContact.setNewVars(particle1, particle2, restitutionCoefficient, -normal, -penetration, new Vector2(0f, 0f), new Vector2(0f, 0f));
			Integrator.contactList.Add(newContact);
		}
	}
}
