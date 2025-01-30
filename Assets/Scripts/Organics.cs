using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class Organics : MonoBehaviour
{

    [Header("Prefabs")]
    public GameObject corpseAcid; // Prefab to spawn upon collision


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContactAcid()
    {


        //store position of unit before destroying it

        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;
        //play death voice effect, then destroy the unit

        //spawn the corpse object at the stored position of unit
        if (corpseAcid != null)
        {
            Instantiate(corpseAcid, currentPosition, currentRotation);
        }
        Destroy(gameObject);

    }
}
