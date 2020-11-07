// Script used to connect to ModMenu
using System.Collections.Generic;
using UnityEngine;
using LLModMenu;
using System.IO;
using System;

namespace BasicStages
{
    public class ModMenuIntegration : MonoBehaviour
    {
        private ModMenu mm;
        private bool mmAdded = false;

        public Dictionary<string, string> configKeys = new Dictionary<string, string>();
        public Dictionary<string, string> configBools = new Dictionary<string, string>();
        public Dictionary<string, string> configInts = new Dictionary<string, string>();
        public Dictionary<string, string> configSliders = new Dictionary<string, string>();
        public Dictionary<string, string> configHeaders = new Dictionary<string, string>();
        public Dictionary<string, string> configGaps = new Dictionary<string, string>();
        public Dictionary<string, string> configText = new Dictionary<string, string>();
        public List<string> writeQueue = new List<string>();
        readonly string iniLocation = Path.GetDirectoryName(Application.dataPath) + @"\ModSettings\";

        private void Start()
        {
            try { ReadIni(); } catch { Debug.Log("ModMenu: Could not load " + @"\ModSettings\" + gameObject.name + ".ini" + " so we will create it instead"); }
            if (!Directory.Exists(iniLocation))
            {
                Directory.CreateDirectory(iniLocation);
                Debug.Log("[LLBMM] AdvancedTraining: Created ModSettings Folder");
            }
            if (!File.Exists(iniLocation + gameObject.name + ".ini"))
            {
                InitConfig();
                Debug.Log($"[LLBMM] AdvancedTraining: {gameObject.name}.ini Created in {iniLocation}");
                return;
            }
            ReadIni();
        }

        private void Update()
        {
            mm = FindObjectOfType<ModMenu>();
            if (mm != null)
            {
                if (mmAdded == false)
                {
                    mm.mods.Add(base.gameObject.name);
                    mmAdded = true;
                }
            }
        }

        private void InitConfig()
        {
            /*
             * Mod menu now uses a single function to add options etc. (AddToWriteQueue)
             * your specified options should be added to this function in the same format as stated under
             * 
            Keybindings:
            AddToWriteQueue("(key)keyName", "LeftShift");                                       value can be: Any KeyCode as a string e.g. "LeftShift"

            Options:
            AddToWriteQueue("(bool)boolName", "true");                                          value can be: ["true" | "false"]
            AddToWriteQueue("(int)intName", "27313");                                           value can be: any number as a string. For instance "123334"
            AddToWriteQueue("(slider)sliderName", "50|0|100");                                  value must be: "Default value|Min Value|MaxValue"
            AddToWriteQueue("(header)headerName", "Header Text");                               value can be: Any string
            AddToWriteQueue("(gap)gapName", "identifier");                                      value does not matter, just make name and value unique from other gaps

            ModInformation:
            AddToWriteQueue("(text)text1", "Descriptive text");                                  value can be: Any string
            */


            // Insert your options here \/
            var stageNames = BasicStages.GetStageNames(false);
            var stage2DNames = BasicStages.GetStageNames(true);
            AddToWriteQueue("(bool)overrideAllStagesToBasic", "true");
            AddToWriteQueue("(gap)stageGap1", "");
            AddToWriteQueue("(header)header1", "Basic Stages");
            for (int i = 0; i < stageNames.Length; i++)
            {
                AddToWriteQueue($"(bool){stageNames[i]}", "false");
            }
            AddToWriteQueue("(gap)stageGap2", "");
            AddToWriteQueue("(header)header2", "Basic Retro Stages");
            for (int i = 0; i < stage2DNames.Length; i++)
            {
                AddToWriteQueue($"(bool){stage2DNames[i]}", "false");
            }
            ModMenu.Instance.WriteIni(gameObject.name, writeQueue, configKeys, configBools, configInts, configSliders, configHeaders, configGaps, configText);
            writeQueue.Clear();
        }

        public void ReadIni()
        {
            string[] lines = File.ReadAllLines(Directory.GetParent(Application.dataPath).FullName + @"\ModSettings\" + gameObject.name + ".ini");
            configBools.Clear();
            configKeys.Clear();
            configInts.Clear();
            configSliders.Clear();
            configHeaders.Clear();
            configGaps.Clear();
            configText.Clear();
            foreach (string line in lines)
            {
                if (line.StartsWith("(key)"))
                {
                    string[] split = line.Split('=');
                    configKeys.Add(split[0], split[1]);
                }
                else if (line.StartsWith("(bool)"))
                {
                    string[] split = line.Split('=');
                    configBools.Add(split[0], split[1]);
                }
                else if (line.StartsWith("(int)"))
                {
                    string[] split = line.Split('=');
                    configInts.Add(split[0], split[1]);
                }
                else if (line.StartsWith("(slider)"))
                {
                    string[] split = line.Split('=');
                    configSliders.Add(split[0], split[1]);
                }
                else if (line.StartsWith("(header)"))
                {
                    string[] split = line.Split('=');
                    configHeaders.Add(split[0], split[1]);
                }
                else if (line.StartsWith("(gap)"))
                {
                    string[] split = line.Split('=');
                    configGaps.Add(split[0], split[1]);
                }
                else if (line.StartsWith("(text)"))
                {
                    string[] split = line.Split('=');
                    configText.Add(split[0], split[1]);
                }
            }
        }

        public void AddToWriteQueue(string key, string value)
        {
            if (key.StartsWith("(key)"))
            {
                configKeys.Add(key, value);
                writeQueue.Add(key);
            }
            else if (key.StartsWith("(bool)"))
            {
                configBools.Add(key, value);
                writeQueue.Add(key);
            }
            else if (key.StartsWith("(int)"))
            {
                configInts.Add(key, value);
                writeQueue.Add(key);
            }
            else if (key.StartsWith("(slider)"))
            {
                configSliders.Add(key, value);
                writeQueue.Add(key);
            }
            else if (key.StartsWith("(header)"))
            {
                configHeaders.Add(key, value);
                writeQueue.Add(key);
            }
            else if (key.StartsWith("(gap)"))
            {
                configGaps.Add(key, value);
                writeQueue.Add(key);
            }
            else if (key.StartsWith("(text)"))
            {
                configText.Add(key, value);
                writeQueue.Add(key);
            }
        }

        public KeyCode GetKeyCode(string keyCode)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (vKey.ToString() == keyCode)
                {
                    return vKey;
                }
            }
            return KeyCode.A;
        }

        public bool GetTrueFalse(string boolName)
        {
            if (boolName == "true") return true;
            else return false;
        }

        public int GetSliderValue(string sliderName)
        {
            string[] vals = configSliders[sliderName].Split('|');
            return Convert.ToInt32(vals[0]);
        }

        public int GetInt(string intName)
        {
            return Convert.ToInt32(configInts[intName]);
        }
    }
}