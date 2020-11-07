using LLHandlers;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using System;

namespace BasicStages
{
    [Serializable]
    public class StageColorElement
    {
        [XmlIgnore]
        public static string saveName
        {
            get { return "BasicStageColours.xml"; }
        }

        public StageColorElement() { }

        public StageColorElement(Stage stagename, string backColor, string sideColor)
        {
            name = stagename;
            backgroundColor = backColor;
            wallColor = sideColor;
        }
        [XmlAttribute("StageName")]
        public Stage name { get; private set; }
        [XmlAttribute("BackgroundColor")]
        public string backgroundColor { get; private set; }
        [XmlAttribute("SideColor")]
        public string wallColor { get; private set; }

        public Color GetBackgroundColor()
        {
            Color color;
            string htmlColor = backgroundColor.StartsWith("#") ? backgroundColor : $"#{backgroundColor}";
            if (ColorUtility.TryParseHtmlString(htmlColor, out color) == false)
            {
                color = Color.black;
                Debug.Log("[LLBM] BasicStages: Failed to load background colour. Is it in the right format? e.g. #1a1a22");
            }
            return color;
        }

        public Color GetWallColor()
        {
            Color color;
            string htmlColor = wallColor.StartsWith("#") ? wallColor : $"#{wallColor}";
            if (ColorUtility.TryParseHtmlString(htmlColor, out color) == false)
            {
                color = Color.black;
                Debug.Log("[LLBM] BasicStages: Failed to load wall colour. Is it in the right format? e.g. #27272f");
            }
            return color;
        }
    }
}
