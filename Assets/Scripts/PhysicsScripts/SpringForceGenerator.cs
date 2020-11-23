using UnityEngine;

public class SpringForceGenerator : ForceGenerator2D
{
    public Particle2D part1;
    public Particle2D part2;
    
    [SerializeField] float restLength;
    [Range(1f, 10f)] [SerializeField] float springConstant;


    // Start is called before the first frame update
    void Start()
    {
        ForceManager.AddForceGenerator(this);
    }

    override public void UpdateForce(Particle2D theObject, double dt)
    {
        if (!part1 || !part2)
        {
            Destroy(this);
        }
        else
        {
            Vector2 diff = part2.transform.position - part1.transform.position;

            float dist = Mathf.Sqrt((diff.x * diff.x) + (diff.y * diff.y));

            float magnitude = dist - restLength;

            magnitude *= springConstant;

            diff.Normalize();
            diff *= magnitude;

            part1.accumulatedForces += diff;
            part2.accumulatedForces += (-1 * diff);
        }
    }

}
