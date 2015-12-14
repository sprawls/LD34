using UnityEngine;
using System.Collections;

public class FamilyTreeCreater : MonoBehaviour {

    [SerializeField]
    private Transform lineRepositionObject;

    private GameObject treeNode;
    private float xOffset = 170f;
    private float yOffset = 300f;

    void Awake() {
        treeNode = Resources.Load("TreeNode") as GameObject;
    }

    public void Create() {
        Vector3 currentPos = new Vector3(0, 100, 2);
        int generationCount = GetTreeHeight(GameManager.Instance.player.currentMonster);
        MonsterData current = GameManager.Instance.player.currentMonster; 

        for (int i = 0; i < generationCount; i++) {
            //Instantiate himself
            GameObject node = Instantiate(treeNode) as GameObject;
            node.transform.SetParent(transform);
            node.transform.localPosition = currentPos;
            node.GetComponent<FamilyTreeNode>().SetUp(new Vector3(0, i == 0 ? 0 : -yOffset, 0), new Sprite() /*TODO Replace with picture*/, current.name, 10, Color.red, current); //TODO replace with good data

            currentPos += new Vector3(0, yOffset, 0);
            if (current.parents != null && current.parents.Count != 0) {
                //Instantiate mother
                GameObject motherNode = Instantiate(treeNode) as GameObject;
                motherNode.transform.SetParent(transform);
                motherNode.transform.localPosition = currentPos + new Vector3(xOffset, -40f, 0);
                motherNode.GetComponent<FamilyTreeNode>().SetUp(new Vector3(-xOffset, -yOffset + 40f, 0), new Sprite() /*TODO Replace with picture*/ , current.parents[1].name, 5, Color.blue, null); //TODO replace with good data

                current = current.parents[0];
            }
        }

        foreach (UILineRenderer line in GetComponentsInChildren<UILineRenderer>()) {
            line.transform.SetParent(lineRepositionObject);
        }
    }

    private Texture2D GenerateTexture(MonsterData current, int number) {
        Camera cam = Instantiate(new Camera(), new Vector3(-1000, number * 500, 0), Quaternion.Euler(Vector3.zero)) as Camera;
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

        return screenShot; //TODO turn to sprite
    }

    private int GetTreeHeight(MonsterData current) {
        int count = 0;
        while (current.parents != null && current.parents.Count > 0) {
            count++;
            current = current.parents[0];
        }
        return count + 1;
    }
}
