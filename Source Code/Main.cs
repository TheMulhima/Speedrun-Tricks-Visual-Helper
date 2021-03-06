using System;
using System.Collections;
using System.Reflection;
using Modding;
using UnityEngine;
using UnityEngine.UI;

namespace Speedrun_Tricks_Visual_Helper
{
    public class SpeedrunTricksVisualHelper:Mod, ITogglableMod
    {
        public SpeedrunTricksVisualHelper() : base("Speedrun Tricks Visual Helper") { }
        public override string GetVersion() => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        
        internal static SpeedrunTricksVisualHelper Instance;
        public Text _textObj;
        private GameObject _canvas;
        
        private int current_setting, WhichSetting;
  
        public override void Initialize()
        {
            Instance = this;
            ModHooks.Instance.HeroUpdateHook += Instance_HeroUpdateHook;
            ModHooks.Instance.SavegameLoadHook += CheckKeyBind_saveGame;
            ModHooks.Instance.NewGameHook += CheckKeyBind_newGame;

        }
        private void CheckKeyBind_newGame()
        {
            try
            {
                Input.GetKeyDown(settings.ChangeModeKey);
            }
            catch
            {
                settings.ChangeModeKey = "";
                
            }
            
            
            if (settings.ChangeModeKey == "")
            {
                GameManager.instance.gameObject.AddComponent<Bind_Key_GUI>();
            }
        }

        private void CheckKeyBind_saveGame(int id)
        {
            CheckKeyBind_newGame();
        }
        private void Instance_HeroUpdateHook()
        {
            if (settings.ChangeModeKey == "") return;
            
            var HC = HeroController.instance;
            var HCS = HC.cState;
            
            if (Input.GetKeyDown(settings.ChangeModeKey))
            {
                ChangeSetting();
                PrintText("Change Setting");
            }

            switch (current_setting)
            {
                case 1:
                    if (settings.Turnaround_OnlyinAir)
                    {
                        if (!HCS.onGround)
                        {
                            Flash(HCS.facingRight ? settings.Color_true : settings.Color_false);
                        }
                    }
                    else
                    {
                        Flash(HCS.facingRight ? settings.Color_true : settings.Color_false);
                    }
                    break;
                
                case 2:
                    if (settings.Fireball_OnlyinAir)
                    {
                        if (!HCS.onGround)
                        {
                            Flash(HC.CanCast() ? settings.Color_true : settings.Color_false);
                        }
                    }
                    else
                    {
                        Flash(HC.CanCast() ? settings.Color_true : settings.Color_false);
                    }
                    break;
                
                case 3:
                    if(settings.Dash_OnlyonGround)
                    {
                        if (HCS.onGround)
                        {
                            Flash(CanDash() ? settings.Color_true : settings.Color_false);
                        }
                    }
                    else
                    {
                        Flash(CanDash() ? settings.Color_true : settings.Color_false);
                    }
                    break;
                case 4:
                    Flash(CanWallJump() ? settings.Color_true : settings.Color_false);
                    break;
                case 5:
                    Flash(CanJump() ? settings.Color_true : settings.Color_false);
                    break;
                case 6:
                    Flash(CanAttack() ? settings.Color_true : settings.Color_false);
                    break;
                case 7:
                    Flash(HC.CanOpenInventory() ? settings.Color_true : settings.Color_false);
                    break;
            }
        }

        //flash values
        private float amount = 1f;
        private float timeUp = 0f;
        private float stayTime = 0.1f;
        private float timeDown = 0f;
        private void Flash(string color)
        {
            var spriteFlash = HeroController.instance.gameObject.GetComponent<SpriteFlash>();
            switch (color)
            {
                case "white":
                    spriteFlash.flash(Color.white, amount, timeUp, stayTime, timeDown);
                    break;
                case "black":
                    spriteFlash.flash(Color.black, amount, timeUp, stayTime, timeDown);
                    break;
                case "blue":
                    spriteFlash.flash(Color.blue, amount, timeUp, stayTime, timeDown);
                    break;
                case "cyan":
                    spriteFlash.flash(Color.cyan, amount, timeUp, stayTime, timeDown);
                    break;
                case "gray":
                    spriteFlash.flash(Color.gray, amount, timeUp, stayTime, timeDown);
                    break;
                case "green":
                    spriteFlash.flash(Color.green, amount, timeUp, stayTime, timeDown);
                    break;
                case "grey":
                    spriteFlash.flash(Color.grey, amount, timeUp, stayTime, timeDown);
                    break;
                case "magenta":
                    spriteFlash.flash(Color.magenta, amount, timeUp, stayTime, timeDown);
                    break;
                case "red":
                    spriteFlash.flash(Color.red, amount, timeUp, stayTime, timeDown);
                    break;
                case "yellow":
                    spriteFlash.flash(Color.yellow, amount, timeUp, stayTime, timeDown);
                    break;
            }
        }
        
        private void ChangeSetting()
        {
            current_setting = WhichSetting;
            if (WhichSetting == 6) WhichSetting = 0;
            else WhichSetting++;
        }
        private bool CanDash()
        {
            return (bool) typeof(HeroController).GetMethod("CanDash", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(HeroController.instance, null);
        }
        
        private bool CanJump()
        {
            return (bool) typeof(HeroController).GetMethod("CanJump", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(HeroController.instance, null);
        }
        
        private bool CanAttack()
        {
            return (bool) typeof(HeroController).GetMethod("CanAttack", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(HeroController.instance, null);
        }
        private bool CanWallJump()
        {
            return (bool) typeof(HeroController).GetMethod("CanWallJump", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(HeroController.instance, null);
        }
        


        #region  //The original code can be found in: https://github.com/fifty-six/HollowKnight.Lightbringer
        public void CreateCanvas()
        {
            if (_canvas != null) return;

            CanvasUtil.CreateFonts();
            _canvas = CanvasUtil.CreateCanvas(RenderMode.ScreenSpaceOverlay, new Vector2(1920, 1080));
            UnityEngine.Object.DontDestroyOnLoad(_canvas);

            GameObject canvas = CanvasUtil.CreateTextPanel
            (
                _canvas,
                "",
                24,
                TextAnchor.LowerRight,
                new CanvasUtil.RectData
                (
                    new Vector2(0, 50),
                    new Vector2(0, 45),
                    new Vector2(0, 0),
                    new Vector2(1, 0),
                    new Vector2(0.5f, 0.5f)
                )
            );

            _textObj = canvas.GetComponent<Text>();
            _textObj.font = CanvasUtil.TrajanBold;
            _textObj.text = "";
            _textObj.fontSize = 24;
        }

        private void PrintText(string WhatPrint)
        {
            CreateCanvas();
            string textToPrint;

            if (WhatPrint == "Change Setting")
            {
                switch (current_setting)
                {
                    case 1:
                        textToPrint = "Mode: Turnaround";
                        break;
                    case 2:
                        textToPrint = "Mode: Fireball";
                        break;
                    case 3:
                        textToPrint = "Mode: Dash";
                        break;
                    case 4:
                        textToPrint = "Mode: WallJump";
                        break;
                    case 5:
                        textToPrint = "Mode: Jump";
                        break;
                    case 6:
                        textToPrint = "Mode: Attack";
                        break;
                    case 7:
                        textToPrint = "Mode: Inventory";
                        break;
                    default:
                        textToPrint = "Mode: None";
                        break;
                }
            }
            else if (WhatPrint == "No Key Bind Present please bind a key from the settings file.")
            {
                textToPrint = "No Key Bind Present please bind a key from the settings file.";
            }
            else
            {
                textToPrint = "";
            }

            _textObj.text = textToPrint;

            if (!settings.Current_Mode_Name_AlwaysVisible)
            {
                GameManager.instance.StartCoroutine(DeleteText(textToPrint));
            }
        }
        public IEnumerator DeleteText(string calling_text)
        {
            yield return new WaitForSecondsRealtime(3f);
            if (_textObj.text == calling_text) _textObj.text = "";
        }

        #endregion


        public void Unload()
        {
            ModHooks.Instance.HeroUpdateHook -= Instance_HeroUpdateHook;
            ModHooks.Instance.SavegameLoadHook -= CheckKeyBind_saveGame;
            ModHooks.Instance.NewGameHook -= CheckKeyBind_newGame;
            UnityEngine.Object.Destroy(_canvas);
        }

        #region 1432 specific
        public GlobalModSettings settings = new GlobalModSettings(); 

        public override ModSettings GlobalSettings
        {
            get => settings;
            set => settings = (GlobalModSettings) value;
        }
        

        #endregion
        
        
    }
}

 