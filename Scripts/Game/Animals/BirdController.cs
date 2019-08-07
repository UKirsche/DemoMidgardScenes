using UnityEngine;
using System.Collections;



namespace MiCoRo.Game.Animal
{

    /// <summary>
    /// Bird controller: erzeugt Anzahl von Vögeln mit verschiedener Flugrichtung und Geschwindigkeit. 
    /// </summary>
    public class BirdController : MonoBehaviour
    {

        enum SpawnQuadrant
        {
            One,
            Two,
            Three,
            Four
        }

        public GameObject bird;
        public GameObject birdsArea;
        public int birdNumber;
        public int waitBird = 10;


        // Use this for initialization
        void Start()
        {
            StartCoroutine(BirdWave());
        }


        IEnumerator BirdWave()
        {
            for (int i = 0; i < birdNumber; i++)
            {
                Instantiate(bird, BirdSpawnPoint(), Quaternion.identity);
                yield return new WaitForSeconds(waitBird);
            }
        }


        private Vector3 BirdSpawnPoint()
        {
            BoxCollider birdCollider = birdsArea.GetComponent<BoxCollider>();
            Bounds birdBounds = birdCollider.bounds;

            //Choose upper quadrant of bounding box for spawn:
            SpawnQuadrant spQuad = GetRandowmSpawnQuadrant();
            return GetSpawnPoint(birdBounds, spQuad);

        }


        private SpawnQuadrant GetRandowmSpawnQuadrant()
        {
            int randVal = Random.Range(1, 5);
            return (SpawnQuadrant)randVal;
        }

        private Vector3 GetSpawnPoint(Bounds bounds, SpawnQuadrant spQ)
        {
            Vector3 spPoint = Vector3.zero;
            Vector3 addVector = spPoint;
            int extX = Mathf.RoundToInt(bounds.extents.x);
            int extY = Mathf.RoundToInt(bounds.extents.y);
            int extZ = Mathf.RoundToInt(bounds.extents.z);

            switch (spQ)
            {
                case SpawnQuadrant.One:
                    addVector.y += Random.Range(1, extY);
                    addVector.x -= Random.Range(1, extX);
                    addVector.z += Random.Range(1, extZ);

                    break;
                case SpawnQuadrant.Two:
                    addVector.y += Random.Range(1, extY);
                    addVector.x -= Random.Range(1, extX);
                    addVector.z -= Random.Range(1, extZ);
                    break;
                case SpawnQuadrant.Three:
                    addVector.y += Random.Range(1, extY);
                    addVector.x += Random.Range(1, extX);
                    addVector.z -= Random.Range(1, extZ);
                    break;
                case SpawnQuadrant.Four:
                    addVector.y += Random.Range(1, extY);
                    addVector.x += Random.Range(1, extX);
                    addVector.z += Random.Range(1, extZ);
                    break;
                default:
                    break;
            }
            spPoint = bounds.center + addVector;

            return spPoint;
        }
    }

}