using LLHandlers;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BasicStages
{
    class BasicStages : MonoBehaviour
    {
        private const string modVersion = "1.0.4";
        private const string repositoryOwner = "Daioutzu";
        private const string repositoryName = "LLBMM-BasicStages";

        public static BasicStages Instance { get; private set; }
        public static ModMenuIntegration MMI = null;
        public static bool InGame => World.instance != null && (DNPFJHMAIBP.HHMOGKIMBNM() == JOFJHDJHJGI.CDOFDJMLGLO || DNPFJHMAIBP.HHMOGKIMBNM() == JOFJHDJHJGI.LGILIJKMKOD);

        public static void Initialize()
        {
            GameObject gameObject = new GameObject("BasicStages", typeof(BasicStages));
            Instance = gameObject.GetComponent<BasicStages>();
            DontDestroyOnLoad(gameObject);
        }

#if useAssetBundle != true

        private readonly static string resourceFolder = Application.dataPath + "/Managed/BasicStagesResources";
        private readonly static string bundleLocation = resourceFolder + "/Bundles/bs_materials";
        private static AssetBundle uiBundle;
        public static Dictionary<string, Material> materialAssets = new Dictionary<string, Material>();
        public static Dictionary<string, Sprite> spriteAssets = new Dictionary<string, Sprite>();
        public static bool bundleLoaded { get; private set; }

        static void LoadAssets()
        {
            if (File.Exists(bundleLocation))
            {
                uiBundle = AssetBundle.LoadFromFile(bundleLocation);
                Material[] materials = uiBundle.LoadAllAssets<Material>();
#if DEBUG
                string txt = ""; 
#endif
                for (int i = 0; i < materials.Length; i++)
                {
                    materialAssets.Add(materials[i].name, materials[i]);
#if DEBUG
                    txt += $"[LLBMM] Material: {materials[i].name}\n";
#endif
                }
                bundleLoaded = true;
#if DEBUG
                Debug.Log($"{txt}");
#endif
            }
            else
            {
                Debug.Log("[LLBMM] BasicStages: The \"stagecolours\" could not be loaded. Using coded colours as a workaround");
            }
        }

#endif

        void Awake()
        {
            FileSystem.Init();
            LoadAssets();
        }

        void Start()
        {
            if (MMI == null) { MMI = gameObject.AddComponent<ModMenuIntegration>(); Debug.Log("[LLBMM] BasicStages: Added GameObject \"ModMenuIntegration\""); }
            MMI.OnInitConfig += AddModOptions;
            Debug.Log("[LLBMM] BasicStages Started");
        }

        private void AddModOptions(ModMenuIntegration MMI)
        {
            var stageNames = GetStageNames(false);
            var stage2DNames = GetStageNames(true);
            MMI.AddToWriteQueue("(bool)overrideAllStagesToBasic", "true");
            MMI.AddToWriteQueue("(gap)stageGap1", "");
            MMI.AddToWriteQueue("(header)header1", "Basic Stages");
            for (int i = 0; i < stageNames.Length; i++)
            {
                MMI.AddToWriteQueue($"(bool){stageNames[i]}", "false");
            }
            MMI.AddToWriteQueue("(gap)stageGap2", "");
            MMI.AddToWriteQueue("(header)header2", "Basic Retro Stages");
            for (int i = 0; i < stage2DNames.Length; i++)
            {
                MMI.AddToWriteQueue($"(bool){stage2DNames[i]}", "false");
            }
        }
        void ModMenuInit()
        {
            if ((MMI != null && !modIntegrated) || LLModMenu.ModMenu.Instance.inModSubOptions && LLModMenu.ModMenu.Instance.currentOpenMod == gameObject.name)
            {

                allStageAreBasic = MMI.GetTrueFalse(MMI.configBools["(bool)overrideAllStagesToBasic"]);

                for (int i = 0; i < stageNames.Length; i++)
                {
                    stagesBasic[(Stage)i + 3] = MMI.GetTrueFalse(MMI.configBools[$"(bool){stageNames[i]}"]);
                }
                for (int i = 0; i < stage2DNames.Length; i++)
                {
                    stages2DBasic[(Stage)i + 20] = MMI.GetTrueFalse(MMI.configBools[$"(bool){stage2DNames[i]}"]);
                }
                if (!modIntegrated) { Debug.Log("[LLBMM] AdvancedTraining: ModMenuIntegration Done"); };
                modIntegrated = true;
            }
        }
#if false

        private void NewAddModOptions(ModMenuIntegration mmi)
        {
            var stageNames = GetStageNames(false);
            var stage2DNames = GetStageNames(true);
            mmi.AddEntryToWriteQueue("overrideAllStagesToBasic", "true", "bool");
            mmi.AddEntryToWriteQueue("numberOfThings", "10", "int");
            mmi.AddEntryToWriteQueue("stageGap1", "10", "gap");
            mmi.AddEntryToWriteQueue("header1", "Basic Stages", "header");
            for (int i = 0; i < stageNames.Length; i++)
            {
                mmi.AddEntryToWriteQueue($"{stageNames[i]}", "false", "bool");
            }
            mmi.AddEntryToWriteQueue("stageGap2", "10", "gap");
            mmi.AddEntryToWriteQueue("header2", "Basic Retro Stages", "header");
            for (int i = 0; i < stage2DNames.Length; i++)
            {
                mmi.AddEntryToWriteQueue($"{stage2DNames[i]}", "false", "bool");
            }
        }

        void NewModMenuInit()
        {
            if ((MMI != null && !modIntegrated) || LLModMenu.ModMenu.Instance.inModSubOptions && LLModMenu.ModMenu.Instance.currentOpenMod == gameObject.name)
            {
                allStageAreBasic = MMI.NewGetTrueFalse("overrideAllStagesToBasic"); ;

                for (int i = 0; i < stageNames.Length; i++)
                {
                    stagesBasic[(Stage)i + 3] = MMI.NewGetTrueFalse($"{stageNames[i]}");
                }
                for (int i = 0; i < stage2DNames.Length; i++)
                {
                    stages2DBasic[(Stage)i + 20] = MMI.NewGetTrueFalse($"{stage2DNames[i]}");
                }
                if (!modIntegrated) { Debug.Log("[LLBMM] AdvancedTraining: ModMenuIntegration Done"); };
                modIntegrated = true;
            }
        }

#endif
        bool initialStageCheck = false;
        private bool modIntegrated;
        private bool allStageAreBasic;
        Dictionary<Stage, bool> stagesBasic = new Dictionary<Stage, bool>();
        Dictionary<Stage, bool> stages2DBasic = new Dictionary<Stage, bool>();
        static readonly string[] stageNames = new string[]
        {
            "outskirts",
            "sewers",
            "desert",
            "elevator",
            "factory",
            "subway",
            "stadium",
            "streets",
            "pool",
            "room21",
        };

        static readonly string[] stage2DNames = new string[]
        {
            "retroOutskirts",
            "retroPool",
            "retroSewers",
            "retroRoom21",
            "retroStreets",
            "retroSubway",
            "retroFactory",
        };

        public static string[] GetStageNames(bool is2d = false)
        {
            return is2d ? stage2DNames : stageNames;
        }

        void Update()
        {
            ModMenuInit();
        }

        void LateUpdate()
        {
            if (InGame)
            {
                if (GameObject.Find("Background") && initialStageCheck == false)
                {
                    initialStageCheck = true;
                    if (CheckBasicStageChoice())
                    {
                        gameObject.AddComponent<StageVisualHandler>();
                    }
                }
            }
            else if (initialStageCheck == true)
            {
                Destroy(gameObject.GetComponent<StageVisualHandler>());
                initialStageCheck = false;
            }
        }

        bool CheckBasicStageChoice()
        {
            if (allStageAreBasic)
            {
                return true;
            }
            else
            {
                try
                {
                    return StageBackground.BG.instance.is2D ? stages2DBasic[StageHandler.curStage] : stagesBasic[StageHandler.curStage];
                }
                catch (System.Exception)
                {
                    Debug.Log("[LLBMM] Stage wasn't listed");
                    return false;
                }
            }
        }
    }
}
