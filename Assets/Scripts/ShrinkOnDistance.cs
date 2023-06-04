using UnityEngine;

public class ShrinkOnDistance : MonoBehaviour

    //POZN.: m�l by b�t p�esunut v conroleru
{
    private GameObject player; 
    public float shrinkDistance = 10; // vzd�lenost od kter� se platforma za�ne zmen�ovat
    public float shrinkAmount = 0.5f; // jak moc se platforma bude zmen�ovat

    private void Start()
    {
        player = GameObject.FindWithTag("Doodler");
    }
    void Update()
    {
        // calculate the distance between the player and the object
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // pokud je vzdalenost hr��e mensi nez shrink distance
        if (distance < shrinkDistance)
        {
            //spo��t� shrinkfactor
            float shrinkFactor = 1 - (distance / shrinkDistance) * shrinkAmount;

            // zmen�� objekt shrinkfaktorem
            transform.localScale = Vector3.one * shrinkFactor;
        }
    }
}