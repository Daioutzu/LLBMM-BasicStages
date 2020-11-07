using LLHandlers;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace BasicStages
{
    static class FileSystem
    {
        //static readonly string SAVE_FOLDER = Application.dataPath + @"/Saves/";
        public static readonly string SAVE_FOLDER = Path.GetDirectoryName(Application.dataPath) + @"\ModSettings\";

        public static void Init()
        {
            if (!Directory.Exists(SAVE_FOLDER))
            {
                Directory.CreateDirectory(SAVE_FOLDER);
            }

            string path = SAVE_FOLDER + StageColorElement.saveName;
            if (File.Exists(path) == false)
            {
                List<StageColorElement> stageColorElements = new List<StageColorElement>()
                {
                    new StageColorElement(Stage.STADIUM, "#1a1a22", "#27272f"),
                    new StageColorElement(Stage.FACTORY,  "#1a1a22", "#27272f"),
                    new StageColorElement(Stage.OUTSKIRTS_2D,  "#1a1a22", "#27272f"),
                    new StageColorElement(Stage.SUBWAY_2D,  "#1a1a22", "#27272f"),
                    new StageColorElement(Stage.NONE,  "#1a1a22", "#27272f"),
                };
                SaveXml(stageColorElements);
            }
        }

        internal static void SaveXml(List<StageColorElement> stageColorElement)
        {
            TextWriter textWriter = new StreamWriter(SAVE_FOLDER + StageColorElement.saveName);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<StageColorElement>));
            xmlSerializer.Serialize(textWriter, stageColorElement);
            textWriter.Close();
        }

        internal static List<StageColorElement> LoadXml()
        {
            string path = SAVE_FOLDER + StageColorElement.saveName;
            if (File.Exists(path))
            {
                XmlSerializer des = new XmlSerializer(typeof(List<StageColorElement>));
                using (XmlReader reader = XmlReader.Create(SAVE_FOLDER + StageColorElement.saveName))
                {
                    return (List<StageColorElement>)des.Deserialize(reader);
                }
            }
            else
            {
                List<StageColorElement> stageColorElements = new List<StageColorElement>()
                {
                    new StageColorElement(Stage.NONE,  "#1a1a22", "#27272f"),
                };
                Debug.Log("[LLBMM] BasicStages: Could not load \"BasicStageColours.xml\". Using coded colours as a workaround");
                return stageColorElements;
            }
        }
    }
}
