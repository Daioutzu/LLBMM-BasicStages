using LLHandlers;
using StageBackground;
using System.Collections.Generic;
using UnityEngine;

namespace BasicStages
{
    class StageVisualHandler : MonoBehaviour
    {
        static Material stageBackgroundMat = new Material(Shader.Find("Unlit/Color"))
        {
            color = new Color(0.184f, 0.192f, 0.212f)
        };
        static Material stageBackgroundOutLineMat = new Material(Shader.Find("Unlit/Color"))
        {
            color = new Color(0.125f, 0.133f, 0.145f)
        };

        static Material stageTopFront, stageTopInside, stageColumnRightInside, stageColumnLeftInside, stageColumnFront, stageBackground;

        static List<StageColorElement> stageColorElements = new List<StageColorElement>();

        void Start()
        {
            GetColorsForCurrentStage();
            CreateStageMesh(World.instance.GetStageCenter(), World.instance.stageSize);
            BG.StopMatchIntro();
            Debug.Log("[LLBMM] StageVisualHandler: Created");
        }

        void Destroy()
        {
            Destroy(bgMeshObject);
            Destroy(leftMeshObject);
            Destroy(rightMeshObject);
            Destroy(topMeshObject);
            Destroy(bottomMeshObject);
            Debug.Log("[LLBMM] StageVisualHandler: Destroyed");
        }

        static void GetColorsForCurrentStage()
        {
            stageColorElements = FileSystem.LoadXml();

            stageTopFront = BasicStages.materialAssets["Bar_Front"];
            stageTopInside = BasicStages.materialAssets["Bar_Inside"];
            stageColumnFront = BasicStages.materialAssets["Column_Front"];
            stageColumnRightInside = BasicStages.materialAssets["Column_Inside_Right"];
            stageColumnLeftInside = BasicStages.materialAssets["Column_Inside_Left"];
            stageBackground = BasicStages.materialAssets["Background"];

            for (int i = 0; i < stageColorElements.Count; i++)
            {
                if (stageColorElements[i]?.name == StageHandler.curStage)
                {
                    stageBackgroundMat.SetColor("_Color", stageColorElements[i].GetBackgroundColor());
                    stageBackgroundOutLineMat.SetColor("_Color", stageColorElements[i].GetWallColor());

                    stageTopFront.SetColor("_Color", stageColorElements[i].GetWallColor());
                    stageTopInside.SetColor("_Color", stageColorElements[i].GetWallColor());
                    stageColumnFront.SetColor("_Color", stageColorElements[i].GetWallColor());
                    stageColumnRightInside.SetColor("_Color", stageColorElements[i].GetWallColor());
                    stageColumnLeftInside.SetColor("_Color", stageColorElements[i].GetWallColor());
                    stageBackground.SetColor("_Color", stageColorElements[i].GetBackgroundColor());
                    break;
                }
                else if (stageColorElements[i]?.name == Stage.NONE)
                {
                    stageBackgroundMat.SetColor("_Color", stageColorElements[i].GetBackgroundColor());
                    stageBackgroundOutLineMat.SetColor("_Color", stageColorElements[i].GetWallColor());

                    stageTopFront.SetColor("_Color", stageColorElements[i].GetWallColor());
                    stageTopInside.SetColor("_Color", stageColorElements[i].GetWallColor());
                    stageColumnFront.SetColor("_Color", stageColorElements[i].GetWallColor());
                    stageColumnRightInside.SetColor("_Color", stageColorElements[i].GetWallColor());
                    stageColumnLeftInside.SetColor("_Color", stageColorElements[i].GetWallColor());
                    stageBackground.SetColor("_Color", stageColorElements[i].GetBackgroundColor());
                }
            }

        }

        static GameObject bgMeshObject;
        static GameObject leftMeshObject;
        static GameObject rightMeshObject;
        static GameObject topMeshObject;
        static GameObject bottomMeshObject;
        static GameObject insideBackgroundMeshObject;
        static GameObject insideLeftMeshObject;
        static void CreateStageMesh(IBGCBLLKIHA obCenter, IBGCBLLKIHA obSize)
        {

            Vector3[] mainVerts = new Vector3[4];
            Vector3[] leftVerts = new Vector3[4];
            Vector3[] topVerts = new Vector3[4];
            Vector3[] rightVerts = new Vector3[4];
            Vector3[] bottomVerts = new Vector3[4];
            Vector3[] insideBackground = new Vector3[4];
            Vector3[] insideLeftVerts = new Vector3[4];
            Vector3[] insideBottomVerts = new Vector3[4];
            Vector3[] insideTopVerts = new Vector3[4];
            Vector3[] insideRightVerts = new Vector3[4];


            Vector2 center = ConvertTo.Vector2(obCenter);
            Vector2 halfSize = ConvertTo.Vector2(obSize) * new Vector2(0.5f, 0.5f);

            mainVerts[0] = new Vector3(center.x - halfSize.x, center.y - halfSize.y);// Bottom Left
            mainVerts[1] = new Vector3(center.x - halfSize.x, center.y + halfSize.y);// Top Left
            mainVerts[2] = new Vector3(center.x + halfSize.x, center.y - halfSize.y);// Bottom Right
            mainVerts[3] = new Vector3(center.x + halfSize.x, center.y + halfSize.y);// Top Right

            float thicc = 3f;
            float backgroundOffset = 2f;
            float foregroundOffset = -0.2f;
            float bottomOffset = -10f;

            Vector3 zOffset = new Vector3(0, 0, foregroundOffset);
            leftVerts[0] = mainVerts[0] + zOffset;
            leftVerts[1] = leftVerts[0] - new Vector3(thicc, 0) + zOffset;
            leftVerts[2] = mainVerts[1] + zOffset;
            leftVerts[3] = leftVerts[2] - new Vector3(thicc, 0) + zOffset;

            rightVerts[1] = mainVerts[2] + zOffset;
            rightVerts[0] = rightVerts[1] + new Vector3(thicc, 0) + zOffset;
            rightVerts[3] = mainVerts[3];
            rightVerts[2] = rightVerts[3] + new Vector3(thicc, 0) + zOffset;

            topVerts[0] = leftVerts[3] + zOffset;
            topVerts[1] = topVerts[0] + new Vector3(0, thicc) + zOffset;
            topVerts[2] = rightVerts[2] + zOffset;
            topVerts[3] = topVerts[2] + new Vector3(0, thicc) + zOffset;

            bottomVerts[1] = leftVerts[1] + zOffset;
            bottomVerts[0] = bottomVerts[1] - new Vector3(0, thicc) + zOffset;
            bottomVerts[3] = rightVerts[0] + zOffset;
            bottomVerts[2] = bottomVerts[3] - new Vector3(0, thicc) + zOffset;


            insideLeftVerts[0] = new Vector3(center.x - halfSize.x, center.y - halfSize.y, foregroundOffset);
            insideLeftVerts[1] = new Vector3(center.x - halfSize.x, center.y + halfSize.y, foregroundOffset);
            insideLeftVerts[2] = new Vector3(mainVerts[0].x, mainVerts[0].y, backgroundOffset);
            insideLeftVerts[3] = new Vector3(mainVerts[1].x, mainVerts[1].y, backgroundOffset);

            insideBottomVerts[0] = new Vector3(center.x - halfSize.x, center.y - halfSize.y, bottomOffset);
            insideBottomVerts[1] = new Vector3(insideBottomVerts[0].x, insideBottomVerts[0].y, backgroundOffset);
            insideBottomVerts[2] = new Vector3(center.x + halfSize.x, center.y - halfSize.y, bottomOffset);
            insideBottomVerts[3] = new Vector3(insideBottomVerts[2].x, insideBottomVerts[2].y, backgroundOffset);

            insideTopVerts[1] = new Vector3(center.x - halfSize.x, center.y + halfSize.y, foregroundOffset);
            insideTopVerts[0] = new Vector3(insideTopVerts[1].x, insideTopVerts[1].y, backgroundOffset);
            insideTopVerts[3] = new Vector3(center.x + halfSize.x, center.y + halfSize.y, foregroundOffset);
            insideTopVerts[2] = new Vector3(insideTopVerts[3].x, insideTopVerts[3].y, backgroundOffset);

            insideRightVerts[2] = new Vector3(center.x + halfSize.x, center.y - halfSize.y, foregroundOffset);
            insideRightVerts[0] = new Vector3(insideRightVerts[2].x, insideRightVerts[2].y, backgroundOffset);
            insideRightVerts[3] = new Vector3(center.x + halfSize.x, center.y + halfSize.y, foregroundOffset);
            insideRightVerts[1] = new Vector3(insideRightVerts[3].x, insideRightVerts[3].y, backgroundOffset);

            insideBackground[0] = new Vector3(center.x - halfSize.x, center.y - halfSize.y, backgroundOffset);
            insideBackground[1] = new Vector3(center.x - halfSize.x, center.y + halfSize.y, backgroundOffset);
            insideBackground[2] = new Vector3(center.x + halfSize.x, center.y - halfSize.y, backgroundOffset);
            insideBackground[3] = new Vector3(center.x + halfSize.x, center.y + halfSize.y, backgroundOffset);

            CreateStageMeshes();

            /*camera = Instantiate(GameCamera.GetBackCam());
            camera.name = "ATCamera";
            camera.transform.SetPositionAndRotation(new Vector3(0, 2, -11), Quaternion.identity);
            camera.gameObject.SetActive(false);*/
#if DEBUG

            string txt2 = "";
            foreach (var name in FindObjectsOfType<GameObject>())
            {
                string str = (name.transform.parent != null) ? name.transform.parent.gameObject.name : "NO PARENT";
                if (str == "Background" || str == "Eclipse")
                {
                    txt2 += $"{name.transform.parent.gameObject.name}/{name.name}/ Tag: {name.tag}\n";
                }
            }
            Debug.Log(txt2);
#endif
            ClearStageAssets(BG.instance.obsEclipse);
            ClearStageAssets(BG.instance.obsNormal);
            ClearStageAssets(BG.instance.eclipseMaterials);
            ClearStageAssets(BG.instance.eclipseColorRenderers);

            BG.instance.obsEclipse = null;
            BG.instance.obsNormal = null;
            BG.instance.eclipseMaterials = null;
            BG.instance.replaceMaterials = null;
            BG.instance.eclipseColorRenderers = null;
            BG.instance.eclipseEvents.RemoveAllListeners();

            GameObject background = GameObject.Find("Background");
            GameObject eclipse = GameObject.Find("Eclipse");
            GameObject bgLayer2 = GameObject.Find("BGLayer2_Animated");
            GameObject pedestrians = GameObject.Find("PedestriansLayer");
            GameObject blimp1 = GameObject.Find("Blimp_Animated 1");
            GameObject blimp2 = GameObject.Find("Blimp_Animated 2");
            GameObject blimp3 = GameObject.Find("Blimp_Animated 3");

            ClearStage(background);
            ClearStage(eclipse);
            ClearStage(bgLayer2);
            ClearStage(pedestrians);
            ClearStage(blimp1);
            ClearStage(blimp2);
            ClearStage(blimp3);

            //bgMeshObject = CreateMeshObject("BasicMainMesh", mainVerts);
            //leftMeshObject = CreateMeshObject("BasicLeftMesh", leftVerts);
            //rightMeshObject = CreateMeshObject("BasicRightMesh", rightVerts);
            //topMeshObject = CreateMeshObject("BasicTopMesh", topVerts);
            //bottomMeshObject = CreateMeshObject("BasicBottomMesh", bottomVerts);
            //insideBackgroundMeshObject = CreateMeshObject("BackMesh", insideBackground);
            //insideLeftMeshObject = CreateMeshObject("insideStageLeft", insideLeftVerts);
            //CreateMeshObject("insideStageBottom", insideBottomVerts);
            //CreateMeshObject("insideStageTop", insideTopVerts);
            //CreateMeshObject("insideStageRight", insideRightVerts);
        }

        static void CreateStageMeshes()
        {
            float columBackOffset = 1f;
            float sideOffset = 0.5f;
            float column_foregroundOffset = -0.2f;

            Vector2 center = ConvertTo.Vector2(World.instance.GetStageCenter());
            Vector2 halfSize = ConvertTo.Vector2(World.instance.stageSize) * new Vector2(0.5f, 0.5f);

            //Creates Left Column
            Vector3[] leftRightFaceVerts = new Vector3[4];
            Vector3[] leftFrontFaceVerts = new Vector3[4];

            leftRightFaceVerts[0] = new Vector3(center.x - halfSize.x, center.y - halfSize.y, column_foregroundOffset);
            leftRightFaceVerts[1] = new Vector3(center.x - halfSize.x, center.y + halfSize.y, column_foregroundOffset);
            leftRightFaceVerts[2] = new Vector3(leftRightFaceVerts[0].x, leftRightFaceVerts[0].y, columBackOffset);
            leftRightFaceVerts[3] = new Vector3(leftRightFaceVerts[1].x, leftRightFaceVerts[1].y, columBackOffset);

            leftFrontFaceVerts[3] = new Vector3(leftRightFaceVerts[1].x, leftRightFaceVerts[1].y, column_foregroundOffset);
            leftFrontFaceVerts[2] = new Vector3(leftRightFaceVerts[0].x, leftRightFaceVerts[0].y, column_foregroundOffset);
            leftFrontFaceVerts[1] = new Vector3(leftFrontFaceVerts[3].x - sideOffset, leftFrontFaceVerts[3].y, column_foregroundOffset);
            leftFrontFaceVerts[0] = new Vector3(leftFrontFaceVerts[3].x - sideOffset, leftFrontFaceVerts[2].y, column_foregroundOffset);

            //Creates Right Column
            Vector3[] rightLeftFaceVerts = new Vector3[4];
            Vector3[] rightFrontFaceVerts = new Vector3[4];

            rightLeftFaceVerts[2] = new Vector3(center.x + halfSize.x, center.y - halfSize.y, column_foregroundOffset);
            rightLeftFaceVerts[3] = new Vector3(center.x + halfSize.x, center.y + halfSize.y, column_foregroundOffset);
            rightLeftFaceVerts[1] = new Vector3(rightLeftFaceVerts[3].x, rightLeftFaceVerts[3].y, columBackOffset);
            rightLeftFaceVerts[0] = new Vector3(rightLeftFaceVerts[2].x, rightLeftFaceVerts[2].y, columBackOffset);

            rightFrontFaceVerts[0] = new Vector3(rightLeftFaceVerts[2].x, rightLeftFaceVerts[2].y, column_foregroundOffset);
            rightFrontFaceVerts[1] = new Vector3(rightLeftFaceVerts[3].x, rightLeftFaceVerts[3].y, column_foregroundOffset);
            rightFrontFaceVerts[2] = new Vector3(rightFrontFaceVerts[0].x + sideOffset, rightFrontFaceVerts[0].y, column_foregroundOffset);
            rightFrontFaceVerts[3] = new Vector3(rightFrontFaceVerts[1].x + sideOffset, rightFrontFaceVerts[1].y, column_foregroundOffset);

            //Creates Background
            float backgroundOffset = 5f;
            float foregroundOffset = -10f;
            float verticalOffset = 2.3f;
            float floorVerticalOffset = -0.05f;
            float ratio = 1.2f;
            float height = 11;

            Vector3[] backgroundVerts = new Vector3[4];

            backgroundVerts[0] = new Vector3(-(height * ratio), -verticalOffset, backgroundOffset);
            backgroundVerts[1] = new Vector3(-(height * ratio), height - verticalOffset, backgroundOffset);
            backgroundVerts[2] = new Vector3(height * ratio, -verticalOffset, backgroundOffset);
            backgroundVerts[3] = new Vector3(height * ratio, height - verticalOffset, backgroundOffset);

            //Creates Floor
            Vector3[] floorVerts = new Vector3[4];

            floorVerts[1] = new Vector3(backgroundVerts[0].x, floorVerticalOffset, backgroundVerts[0].z);
            floorVerts[3] = new Vector3(backgroundVerts[2].x, floorVerticalOffset, backgroundVerts[2].z);
            floorVerts[0] = new Vector3(backgroundVerts[0].x, floorVerticalOffset, foregroundOffset);
            floorVerts[2] = new Vector3(backgroundVerts[2].x, floorVerticalOffset, foregroundOffset);

            //Creats Top bar
            Vector3[] topFrontFaceVerts = new Vector3[4];
            Vector3[] topUnderFaceVerts = new Vector3[4];

            topFrontFaceVerts[0] = new Vector3(leftFrontFaceVerts[1].x, leftFrontFaceVerts[1].y, leftFrontFaceVerts[1].z);
            topFrontFaceVerts[1] = new Vector3(topFrontFaceVerts[0].x, leftFrontFaceVerts[3].y + sideOffset, topFrontFaceVerts[0].z);
            topFrontFaceVerts[2] = new Vector3(rightFrontFaceVerts[3].x, rightFrontFaceVerts[3].y, rightFrontFaceVerts[3].z);
            topFrontFaceVerts[3] = new Vector3(topFrontFaceVerts[2].x, topFrontFaceVerts[2].y + sideOffset, topFrontFaceVerts[2].z);

            topUnderFaceVerts[0] = new Vector3(topFrontFaceVerts[0].x, topFrontFaceVerts[0].y, columBackOffset);
            topUnderFaceVerts[1] = new Vector3(leftFrontFaceVerts[1].x, leftFrontFaceVerts[1].y, leftFrontFaceVerts[1].z);
            topUnderFaceVerts[2] = new Vector3(topFrontFaceVerts[2].x, topFrontFaceVerts[2].y, columBackOffset);
            topUnderFaceVerts[3] = new Vector3(rightFrontFaceVerts[3].x, rightFrontFaceVerts[3].y, rightFrontFaceVerts[3].z);

            //Creats Bottom Bar
            Vector3[] bottomFrontFaceVerts = new Vector3[4];
            Vector3[] bottomInsideFaceVerts = new Vector3[4];

            bottomFrontFaceVerts[1] = new Vector3(leftFrontFaceVerts[0].x, leftFrontFaceVerts[0].y, leftFrontFaceVerts[0].z);
            bottomFrontFaceVerts[3] = new Vector3(rightFrontFaceVerts[2].x, rightFrontFaceVerts[2].y, rightFrontFaceVerts[2].z);
            bottomFrontFaceVerts[0] = new Vector3(bottomFrontFaceVerts[1].x, bottomFrontFaceVerts[1].y - sideOffset, bottomFrontFaceVerts[1].z);
            bottomFrontFaceVerts[2] = new Vector3(bottomFrontFaceVerts[3].x, bottomFrontFaceVerts[3].y - sideOffset, bottomFrontFaceVerts[3].z);

            bottomInsideFaceVerts[0] = new Vector3(bottomFrontFaceVerts[1].x, bottomFrontFaceVerts[1].y, bottomFrontFaceVerts[1].z);
            bottomInsideFaceVerts[1] = new Vector3(bottomInsideFaceVerts[0].x, bottomInsideFaceVerts[0].y, columBackOffset);
            bottomInsideFaceVerts[2] = new Vector3(bottomFrontFaceVerts[3].x, bottomFrontFaceVerts[3].y, bottomFrontFaceVerts[3].z);
            bottomInsideFaceVerts[3] = new Vector3(bottomInsideFaceVerts[2].x, bottomInsideFaceVerts[2].y, columBackOffset);

            CreateMeshObject("insideleftFaceVerts", leftRightFaceVerts, stageColumnLeftInside);
            CreateMeshObject("leftFrontFaceVerts", leftFrontFaceVerts, stageColumnFront);

            CreateMeshObject("insiderightFaceVerts", rightLeftFaceVerts, stageColumnRightInside);
            CreateMeshObject("rightFrontFaceVerts", rightFrontFaceVerts, stageColumnFront);
            CreateMeshObject("TopFrontFaceVerts", topFrontFaceVerts, stageTopFront);
            CreateMeshObject("insideTopUnderFaceVerts", topUnderFaceVerts, stageTopInside);

            CreateMeshObject("bottomFrontFaceVerts", bottomFrontFaceVerts, stageTopFront);
            CreateMeshObject("bottomInsideFaceVerts", bottomInsideFaceVerts, stageTopFront);

            CreateMeshObject("BackMesh", backgroundVerts, stageBackground);
            CreateMeshObject("FloorMesh", floorVerts, stageBackground);

        }

        #region Asset Clearing Methods
        static void ClearStageAssets(GameObject[] gameObjects)
        {
            if (gameObjects != null)
            {
                for (int i = gameObjects.Length - 1; i >= 0; i--)
                {
                    Destroy(gameObjects[i].gameObject);
                }
            }
        }

        static void ClearStageAssets(Material[] materials)
        {
            if (materials != null)
            {
                for (int i = materials.Length - 1; i >= 0; i--)
                {
                    Destroy(materials[i]);
                }
            }
        }
        static void ClearStageAssets(Renderer[] renderers)
        {
            if (renderers != null)
            {
                for (int i = renderers.Length - 1; i >= 0; i--)
                {
                    Destroy(renderers[i]);
                }
            }
        }
        #endregion

        static void ClearStage(GameObject gameObject)
        {
            if (gameObject != null)
            {
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    bool flag = gameObject.transform.GetChild(i).name.Contains("Positions");
                    bool flag2 = gameObject.transform.GetChild(i).name.Contains("Light");
#if DEBUG
                    string txt = ""; 
#endif
                    if (flag == false && flag2 == false)
                    {
                        //gameObject.transform.GetChild(i).gameObject.SetActive(false);
                        gameObject.transform.GetChild(i).gameObject.SetActive(false);
#if DEBUG
                        txt += $"{gameObject.transform.GetChild(i).gameObject.name}"; 
#endif
                    }
#if DEBUG
                    if (txt != "")
                    {
                        Debug.Log($"{txt} - Disabled");
                    } 
#endif
                }
            }
        }

        static GameObject CreateMeshObject(string meshName, Vector3[] vertices, Material material)
        {
            Mesh mesh = new Mesh();
            int[] triangles = new int[] { 0, 1, 2, 2, 1, 3 };
            Vector2[] uv = new Vector2[]
            {
                new Vector2(0,0),
                new Vector2(0,1),
                new Vector2(1,0),
                new Vector2(1,1),
            };

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
            mesh.RecalculateNormals();

            GameObject meshObject = new GameObject(meshName, typeof(MeshFilter), typeof(MeshRenderer));
            //gameObject.transform.parent = camera.transform;
            meshObject.transform.localScale = Vector3.one;
            meshObject.GetComponent<MeshFilter>().mesh = mesh;
            meshObject.GetComponent<MeshRenderer>().material = material;
            return meshObject;
        }
    }
}
