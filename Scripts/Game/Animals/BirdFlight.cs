using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiCoRo.Game.Animal
{
    /// <summary>
    /// Bird flight: Bird flies
    /// </summary>
    public class BirdFlight : MonoBehaviour
    {

        private Rigidbody rb;
        public int speed = 10;
        private string voegelTag = "birdBox";
        private Bounds birdBounds;
        private Compass birdCompass;


        struct Compass
        {
            public float up, down, north, south, east, west;
        }

        void Start()
        {

            GameObject voegelGO = GameObject.FindGameObjectWithTag(voegelTag);
            BoxCollider birdCollider = voegelGO.GetComponent<BoxCollider>();
            birdBounds = birdCollider.bounds;
            birdCompass = FillCompass();
            rb = GetComponent<Rigidbody>();
            Vector3 flightDir = Random.insideUnitSphere;
            Vector3 rotTowards = this.transform.position + flightDir;
            transform.LookAt(rotTowards);
            rb.velocity = flightDir * speed;
        }

        void FixedUpdate()
        {
            ReflectDirection();
        }

        /// <summary>
        /// Reflects the velocity vector of rigidbody.
        /// </summary>
        void ReflectDirection()
        {
            bool outUp = OutOfBounds(birdCompass.up, this.transform.position.y, true);
            bool outDown = OutOfBounds(birdCompass.down, this.transform.position.y, false);
            bool outNorth = OutOfBounds(birdCompass.north, this.transform.position.z, true);
            bool outSouth = OutOfBounds(birdCompass.south, this.transform.position.z, false);
            bool outEast = OutOfBounds(birdCompass.east, this.transform.position.x, true);
            bool outWest = OutOfBounds(birdCompass.west, this.transform.position.x, false);

            Vector3 reflectVelocity = rb.velocity;
            bool reflect = false;

            if (outUp || outDown)
            {
                reflectVelocity.y *= -1;
                reflect = true;
            }
            else if (outNorth || outSouth)
            {
                reflectVelocity.z *= -1;
                reflect = true;
            }
            else if (outEast || outWest)
            {
                reflectVelocity.x *= -1;
                reflect = true;
            }

            ReflectRotate(reflectVelocity, reflect);

            rb.velocity = reflectVelocity;

        }

        /// <summary>
        /// Reflects the rotate.
        /// </summary>
        /// <param name="reflectVelocity">Reflect velocity.</param>
        /// <param name="reflect">If set to <c>true</c> reflect.</param>
        void ReflectRotate(Vector3 reflectVelocity, bool reflect)
        {
            if (reflect)
            {
                Vector3 rotTowards = this.transform.position + reflectVelocity;
                transform.LookAt(rotTowards);
            }
        }

        /// <summary>
        /// Outs the of bounds. greater means: out of bounds when valuePos > valRef
        /// </summary>
        /// <returns><c>true</c>, if of bounds was outed, <c>false</c> otherwise.</returns>
        /// <param name="valRef">Value reference.</param>
        /// <param name="valPos">Value position.</param>
        /// <param name="bigger">If set to <c>true</c> greater.</param>
        private bool OutOfBounds(float valRef, float valPos, bool greater)
        {
            bool retVal = false;
            if (greater)
            {
                retVal = valPos > valRef;
            }
            else
            {
                retVal = valPos < valRef;
            }

            return retVal;
        }


        private Compass FillCompass()
        {
            Compass birdCompass = new Compass();
            birdCompass.up = birdBounds.center.y + birdBounds.extents.y;
            birdCompass.down = birdBounds.center.y - birdBounds.extents.y;
            birdCompass.north = birdBounds.center.z + birdBounds.extents.z;
            birdCompass.south = birdBounds.center.z - birdBounds.extents.z;
            birdCompass.east = birdBounds.center.x + birdBounds.extents.x;
            birdCompass.west = birdBounds.center.x - birdBounds.extents.x;

            return birdCompass;
        }
    }
}