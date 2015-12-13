using UnityEngine;
using System.Collections;

public class GenerateFamilyTree : MonoBehaviour {

    [SerializeField]
    GameObject camera;

    [SerializeField]
    GameObject familyTreeNode;

    private int GetTreeHeight(MonsterData current) {
        if (current.parents != null) {
            int result1 = GetTreeHeight(current.parents[0]);
            int result2 = GetTreeHeight(current.parents[1]);
            return result1 < result2 ? result2 : result1;
        }
        return 1;
    }

    public void GenerateEntireTree(GameObject parentRef, Vector3 startingPos) {
        Vector3 currentPos = startingPos;
        GameObject node = Instantiate(familyTreeNode, currentPos, Quaternion.Euler(Vector3.zero)) as GameObject;
    }

    private Texture2D GenerateTexture(MonsterData current, int number) {
        Camera cam = Instantiate(camera, new Vector3(-1000, number * 500, 0), Quaternion.Euler(Vector3.zero)) as Camera;
        /*GameObject monster = Instantiate(current.model) as GameObject;
        monster.transform.SetParent(cam.transform);
        monster.transform.localPosition = Vector3.zero;  TODO create model */

        RenderTexture texture = new RenderTexture(Screen.width, Screen.height, 40);
        cam.targetTexture = texture;
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false); 

        cam.Render();

        RenderTexture.active = texture;
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0); 

        cam.targetTexture = null;
        RenderTexture.active = null; 
        Destroy(texture);
        Destroy(cam.gameObject);

        return screenShot;
    }
}
