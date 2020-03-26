using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cirlcleTry : MonoBehaviour
{
    
    public GameObject PrefabToClone;
    public List<GameObject> GeneratedObjects = new List<GameObject>();
    public GameObject TowerCenter;
    public float Radius = 4f;
    public int Height;

    public void Update(){
        if(Input.GetKeyDown(KeyCode.A))
        {
        Generate();
        }
    }

    public void Generate() {
        this.Clean();
        //How many brick i need for a complete circle based on selected radius (perimetre d'un cercle)
        for (int j = 0; j < this.Height; j++) {
        var circlePerimeter = (2f * Mathf.PI * this.Radius-j) + 1;
            for (int i = 0; i < circlePerimeter; i++) {
                float angle = 360f / circlePerimeter * i;
                Vector3 position = this.TowerCenter.transform.position + new Vector3(0,j,0) + Quaternion.Euler(0, angle, 0) * new Vector3(this.Radius-j, 0, 0);
                Quaternion dir = Quaternion.LookRotation(position - (this.TowerCenter.transform.position + new Vector3(0,0,0)));
                GameObject newLyCreatedObject = Instantiate(this.PrefabToClone,
                    position,
                    dir);
                this.GeneratedObjects.Add(newLyCreatedObject);
             /*  position = this.TowerCenter.transform.position + new Vector3(0,-j,0) + Quaternion.Euler(0, angle, 0) * new Vector3(this.Radius-j, 0, 0);
                 dir = Quaternion.LookRotation(position + (this.TowerCenter.transform.position + new Vector3(0,0,0)));
                GameObject newLyCreatedObject1 = Instantiate(this.PrefabToClone,
                    position,
                    dir);
                      this.GeneratedObjects.Add(newLyCreatedObject1);*/
            }
        }
    }

    public void Clean() {
        foreach (var generatedObject in this.GeneratedObjects) {
            Destroy(generatedObject);
        }
        this.GeneratedObjects.Clear();
    }
}