using System.Collections;
using System.Reflection;
using Modding;
using UnityEngine;
using UnityEngine.UI;
using GlobalEnums;

namespace Speedrun_Tricks_Visual_Helper
{
    public class SpeedrunTricksVisualHelper:Mod, ITogglableMod
    {
        public SpeedrunTricksVisualHelper() : base("Speedrun Tricks Visual Helper") { }
        public override string GetVersion() => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        
        internal static SpeedrunTricksVisualHelper Instance;
        
        public GlobalModSettings settings = new GlobalModSettings(); 

        public override ModSettings GlobalSettings
        {
            get => settings;
            set => settings = (GlobalModSettings) value;
        }
        private Text _textObj;
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
            if (settings.ChangeModeKey == "")
            {
                GameManager.instance.StartCoroutine((PleaseWaitToShowText(true, "No Key Bound, Please Bind a Key")));
            }
        }

        private void CheckKeyBind_saveGame(int id)
        {
            if (settings.ChangeModeKey == "")
            {
                GameManager.instance.StartCoroutine((PleaseWaitToShowText(true, "No Key Bound, Please Bind a Key")));
            }
        }

        private IEnumerator PleaseWaitToShowText(bool Check_for_input,string Text)
        {
            if (Check_for_input) yield return new WaitUntil(()=>HeroController.instance.CanInput());
                
            yield return new WaitForSecondsRealtime(3f);
            
            PrintText(Text);
        }
        private void Instance_HeroUpdateHook()
        {
            if (HeroController.instance == null) return; 
            if (settings.ChangeModeKey == "") return;
            
            var HC = HeroController.instance;
            var HCS = HC.cState;
            
            if (Input.GetKeyDown(settings.ChangeModeKey))
            {
                ChangeSetting();
                PrintText("Change Setting");
            }

            if (current_setting == 1)
            {
                if (settings.Turnaround_OnlyinAir)
                {
                    if (!HCS.onGround)
                    {
                        if (HCS.facingRight) Flash(settings.Color_true);
                        if (!HCS.facingRight) Flash(settings.Color_false);
                        
                        if (settings.Show_CanFireball_InLogs_While_InTurnAround_Mode)
                        {
                            if (HC.CanCast()) Log("Can Cast");
                            else Log("Not able to use spells");
                        }
                    }
                }
                else
                {
                    if (HCS.facingRight) Flash(settings.Color_true);
                    if (!HCS.facingRight) Flash(settings.Color_false);
                        
                    if (settings.Show_CanFireball_InLogs_While_InTurnAround_Mode)
                    {
                        if (HC.CanCast()) Log("Can Cast");
                        else Log("Not able to use spells");
                    }    
                }

            }

            if (current_setting == 2)
            {
                if (settings.Fireball_OnlyinAir)
                {
                    if (!HCS.onGround)
                    {
                        if (HC.CanCast()) Flash(settings.Color_true);
                        else Flash(settings.Color_false);
                    }
                }
                else
                {
                    if (HC.CanCast()) Flash(settings.Color_true);
                    else Flash(settings.Color_false);
                }
            }

            if (current_setting == 3)
            {
                if(settings.Dash_OnlyonGround)
                {
                    if (HCS.onGround)
                    {
                        if (CanDash()) Flash(settings.Color_true);

                        else Flash(settings.Color_false);
                    }
                }
                else
                {
                    if (CanDash()) Flash(settings.Color_true);

                    else Flash(settings.Color_false);
                }
            }

            if (current_setting == 4)
            {
                if (HCS.wallSliding)
                {
                    Flash(settings.Color_true);
                }

                if (!HCS.wallSliding)
                {
                    Flash(settings.Color_false);
                }
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
            if (WhichSetting == 4) WhichSetting = 0;
            else WhichSetting++;
        }
        private bool CanDash()
        {
            return (bool) typeof(HeroController).GetMethod("CanDash", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(HeroController.instance, null);
        }

        #region Thank you 56 for allowing me to steal HC code. 
        //The original can be found in: https://github.com/fifty-six/HollowKnight.Lightbringer
        private void CreateCanvas()
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
                    default:
                        textToPrint = "Mode: None";
                        break;
                }
            }
            else if (WhatPrint == "No Key Bound. Please Bind a Key")
            {
                textToPrint = "No Key Bound. Please Bind a Key";
            }
            else
            {
                textToPrint = "";
            }

            _textObj.text = textToPrint;
            if (!settings.Current_Mode_Name_AlwaysVisible)
            {
                GameManager.instance.StartCoroutine((PleaseWaitToShowText(false,"Clear")));
            }
            
        }

        #endregion
        
        
        public void Unload()
        {
            ModHooks.Instance.HeroUpdateHook -= Instance_HeroUpdateHook;
            ModHooks.Instance.SavegameLoadHook -= CheckKeyBind_saveGame;
            ModHooks.Instance.NewGameHook -= CheckKeyBind_newGame;
        }

        
    }
}

 
