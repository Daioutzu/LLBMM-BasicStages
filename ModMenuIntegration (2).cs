// Script used to connect to ModMenu
using LLModMenu;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BasicStages
{
    public class ModMenuIntegration : MonoBehaviour
    {
        private Config config;
        private bool mmAdded = false;
        public delegate void ModMenuEventHandler(ModMenuIntegration MMI);
        public event ModMenuEventHandler OnInitConfig;

        public List<Entry> writeQueue = new List<Entry>();

        private void Start()
        {
            InitConfig();
        }

        private void Update()
        {
            if (mmAdded == false)
            {
                try
                {
                    ModMenu.Instance.mods.Add(base.gameObject.name);
                    mmAdded = true;
                }
                catch (Exception)
                {
                    Debug.Log("[LLBMM] ModMenuIntegration: No Instance of the ModMenu");
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

            OnInitConfig?.Invoke(this);
            this.config = ModMenu.Instance.configManager.InitConfig(gameObject.name, writeQueue);
            writeQueue.Clear();
        }

        public void AddToWriteQueue(string key, string value)
        {
            string[] splits = key.Remove(0, 1).Split(')');
            if (splits[0] == "bool")
            {
                if (value == "true") value = "True";
                if (value == "false") value = "False";
            }
            writeQueue.Add(new Entry(splits[1], value, splits[0]));
        }

        public void AddEntryToWriteQueue(string key, string value, string type)
        {
            if (type == "bool")
            {
                if (value == "true") value = "True";
                if (value == "false") value = "False";
            }
            writeQueue.Add(new Entry(key, value, type));
        }

        public KeyCode GetKeyCode(string keyName)
        {
            return this.config.GetKeyCode(keyName.Split(')')[1]);
        }
        public KeyCode NewGetKeyCode(string keyName)
        {
            return this.config.GetKeyCode(keyName);
        }

        public bool GetTrueFalse(string boolName)
        {
            return this.config.GetBool(boolName.Split(')')[1]);
        }

        public bool NewGetTrueFalse(string boolName)
        {
            return this.config.GetBool(boolName);
        }

        public int GetSliderValue(string sliderName)
        {
            return this.config.GetSliderValue(sliderName.Split(')')[1]);
        }

        public int NewGetSliderValue(string sliderName)
        {
            return this.config.GetSliderValue(sliderName);
        }

        public int GetInt(string intName)
        {
            return this.config.GetInt(intName.Split(')')[1]);
        }

        public int NewGetInt(string intName)
        {
            return this.config.GetInt(intName);
        }
    }
}