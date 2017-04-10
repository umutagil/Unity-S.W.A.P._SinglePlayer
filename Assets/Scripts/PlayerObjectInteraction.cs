using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Com.LuisPedroFonseca.ProCamera2D;

public delegate void MultipleSwap(List<SwappingCouple> couples, int damage);

public class PlayerObjectInteraction : MonoBehaviour {

    public event MultipleSwap OnMultipleSwap;
    private GameObject enemies;

    //private LightningControlScript lightningController;

    void Start()
    {
        //lightningController = GameObject.FindObjectOfType<LightningControlScript>();
        enemies = GameObject.Find("Enemies");
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        WeaponScript weapon = otherCollider.gameObject.GetComponent<WeaponScript>();
        if(weapon != null) // TODO: this part should be computed in another script
        {             
            gameObject.GetComponent<WeaponScript>().WeaponProperties.Copy(weapon.WeaponProperties);
            gameObject.GetComponent<WeaponScript>().shootingRate = weapon.WeaponProperties.shootingRate;
            Destroy(weapon.gameObject);
        }
        else if(otherCollider.gameObject.tag == "nuke")
        {            
            var exp = otherCollider.gameObject.GetComponent<ParticleSystem>();
            exp.Play();
            otherCollider.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(otherCollider.gameObject, exp.main.duration);

            int childCount = enemies.transform.childCount;
            if (enemies == null || childCount <= 0)
                return;

            int damage = 1;            
            SwapAll(enemies.transform, damage);

            ProCamera2DShake.Instance.Shake(0);
        }
        else if(otherCollider.gameObject.tag == "ElectricBomb")
        {
            if (enemies == null)
                return;

            Vector3 centerOfExplosion = otherCollider.gameObject.transform.position;
            //lightningController.BurstTheLightning(centerOfExplosion + new Vector3(-5, 0, 0), centerOfExplosion + new Vector3(5, 0, 0));
            otherCollider.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(otherCollider.gameObject);

            int damage = 1;
            int childCount = enemies.transform.childCount;
            SwapAll(enemies.transform, damage);
        }
    }

    // swapping all by couples, may be changed
    protected void SwapAll(Transform parent, int damage)
    {
        // compute swap indices of enemies
        int childCount = parent.childCount;
        List<int> childrenList = new List<int>();        
        for (int childID = 0; childID < childCount; childID++)
            childrenList.Add(childID);
        UnityExtensions.Shuffle<int>(childrenList);
        
        List<SwappingCouple> couples = new List<SwappingCouple>();
        for (int couple = 0; couple < childCount / 2; couple++ )
        {
            GameObject shooter1 = parent.transform.GetChild(childrenList[couple]).gameObject;
            GameObject shooter2 = parent.transform.GetChild(childrenList[childCount/2 + couple]).gameObject;
            couples.Add(new SwappingCouple(shooter1, shooter2));
        }

        // the last child should be damaged
        if (childCount % 2 == 1)
            parent.transform.GetChild(childrenList[childCount - 1]).GetComponent<HealthScript>().Damage(damage);

        if (OnMultipleSwap != null)
        {
            OnMultipleSwap(couples, damage);
        } 

    }

    

}
