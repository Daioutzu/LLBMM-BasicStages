using LLHandlers;
using StageBackground;
using System.Collections.Generic;
using UnityEngine;

namespace BasicStages
{
    class StageVisualHandler : MonoBehaviour
    {
        static Material stageBackgroundMat = new Material(Shader.Find("Unlit/Color")) { color = new Color(0.184f, 0.192f, 0.212f) };
        static Material stageBackgroundOutLineMat = new Material(Shader.Find("Unlit/Color")) { color = new Color(0.125f, 0.133f, 0.145f) };

        static Material stageTopFront;
        static Material stageTopInside;
        static Material stageColumnRightInside;
        static Material stageColumnLeftInside;
        static Material stageColumnFront;
        static Material stageBackground;

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
            Debug.Log("[LLBMM] StageVisualHandler: Destroyed");
        }

        static void GetColorsForCurrentStage()
        {
            stageColorElements = FileSystem.LoadXml();

            if (BasicStages.bundleLoaded)
            {
                stageBackground = BasicStages.materialAssets["Tile"];
                stageTopFront = Instantiate(BasicStages.materialAssets["Tile"]);
                stageColumnFront = Instantiate(BasicStages.materialAssets["Tile"]);
                stageColumnFront.mainTextureScale = new Vector2(1, 8);
                stageTopFront.mainTextureScale = new Vector2(26, 1);
                stageTopInside = BasicStages.materialAssets["Bar_Inside"];
                stageColumnLeftInside = BasicStages.materialAssets["Column_Inside"];
                stageColumnRightInside = Instantiate(BasicStages.materialAssets["Column_Inside"]);
                stageColumnRightInside.mainTextureScale = new Vector2(1, 8);
            }
            else
            {
                stageBackground = stageTopInside = stageColumnRightInside = stageColumnLeftInside = new Material(Shader.Find("Unlit/Color")) { color = new Color(0.184f, 0.192f, 0.212f) };
                stageTopFront = stageColumnFront = new Material(Shader.Find("Unlit/Color")) { color = new Color(0.125f, 0.133f, 0.145f) };
                Debug.Log("[LLBMM] BasicStages: The \"stagecolours\" could not be loaded. Using coded colours as a workaround");
            }

            for (int i = 0; i < stageColorElements.Count; i++)
            {
                if (stageColorElements[i]?.name == StageHandler.curStage)
                {
                    SetMaterialColours(i);
                    break;
                }
                else if (stageColorElements[i]?.name == Stage.NONE)
                {
                    SetMaterialColours(i);
                }
            }
        }

        static void SetMaterialColours(int i)
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

        static void CreateStageMesh(IBGCBLLKIHA obCenter, IBGCBLLKIHA obSize)
        {
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

            CreateMeshObject("LeftColumn_Inside", leftRightFaceVerts, stageColumnLeftInside);
            CreateMeshObject("LeftColumn_Face", leftFrontFaceVerts, stageColumnFront);

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

            CreateMeshObject("RightColumn_Inside", rightLeftFaceVerts, stageColumnRightInside);
            CreateMeshObject("RightColumn_Face", rightFrontFaceVerts, stageColumnFront);

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

            CreateMeshObject("FloorMesh", floorVerts, stageBackground);
            CreateMeshObject("BackMesh", backgroundVerts, stageBackground);

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

            CreateMeshObject("TopBar_Face", topFrontFaceVerts, stageTopFront);
            CreateMeshObject("TopBar_Inside", topUnderFaceVerts, stageTopInside);

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

            CreateMeshObject("BottomBar_Face", bottomFrontFaceVerts, stageTopFront);
            CreateMeshObject("BottomBar_Inside", bottomInsideFaceVerts, stageTopFront);
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
