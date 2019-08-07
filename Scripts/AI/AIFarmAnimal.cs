using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace MiCoRo.AI
{
    [RequireComponent(typeof(ThirdPersonNPC))]
    public class AIFarmAnimal : AINPC
    {

        // Use Initialization from BaseClass
        public override void Start()
        {
            character = GetComponent<ThirdPersonNPC>();
            base.Start();
        }

    }
}