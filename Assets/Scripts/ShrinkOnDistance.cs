using UnityEngine;

public class ShrinkOnDistance : MonoBehaviour

    //POZN.: mìl by být pøesunut v conroleru
{
    private GameObject player; 
    public float shrinkDistance = 10; // vzdálenost od které se platforma zaène zmenšovat
    public float shrinkAmount = 0.5f; // jak moc se platforma bude zmenšovat

    private void Start()
    {
        player = GameObject.FindWithTag("Doodler");
    }
    void Update()
    {
        // calculate the distance between the player and the object
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // pokud je vzdalenost hráèe mensi nez shrink distance
        if (distance < shrinkDistance)
        {
            //spoèítá shrinkfactor
            float shrinkFactor = 1 - (distance / shrinkDistance) * shrinkAmount;

            // zmenší objekt shrinkfaktorem
            transform.localScale = Vector3.one * shrinkFactor;
        }
    }
}