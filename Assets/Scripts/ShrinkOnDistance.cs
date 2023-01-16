using UnityEngine;

public class ShrinkOnDistance : MonoBehaviour
{
    public GameObject player; // assign the player object in the inspector
    public float shrinkDistance = 10; // the distance at which the object will start shrinking
    public float shrinkAmount = 0.5f; // the amount the object will shrink by

    private void Start()
    {
        player = GameObject.FindWithTag("Doodler");
    }
    void Update()
    {
        // calculate the distance between the player and the object
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // pokud je vzdalenost hr·Ëe mensi nez shrink distance
        if (distance < shrinkDistance)
        {
            //spoËÌt· shrinkfactor
            float shrinkFactor = 1 - (distance / shrinkDistance) * shrinkAmount;

            // zmenöÌ objekt shrinkfaktorem
            transform.localScale = Vector3.one * shrinkFactor;
        }
    }
}