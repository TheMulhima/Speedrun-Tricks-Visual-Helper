using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Speedrun_Tricks_Visual_Helper
{
    public class Bind_Key_GUI : MonoBehaviour
    {

        public GUIStyle Default_Label, Default_Button;
        private static bool checking_for_bind;
        private static string looking_for_input;
        private const int width = 300, height = 200;

        private void Awake()
        {
            checking_for_bind = false;
        }

        private IEnumerator Start()
        {
            var instance = SpeedrunTricksVisualHelper.Instance;
            yield return new WaitWhile(()=>HeroController.instance == null);

            yield return new WaitForSecondsRealtime(2f);
            
            instance.CreateCanvas();
            instance._textObj.text = "No key bind present please pause to bind a key";
            GameManager.instance.StartCoroutine(
                instance.DeleteText("No key bind present please pause to bind a key"));
        }

        public void OnGUI()
        {
            var gm = GameManager.instance;
            var instance = SpeedrunTricksVisualHelper.Instance;
            var settings = instance.settings;

            if (settings.ChangeModeKey != "") return;
            if (!gm.isPaused) return;
            if (checking_for_bind) return;

            Default_Label = new GUIStyle(GUI.skin.label)
            {
                fontSize = 24,
                alignment = TextAnchor.MiddleCenter,
            };
            Default_Button = new GUIStyle(GUI.skin.button)
            {
                fontSize = 24,
                alignment = TextAnchor.MiddleCenter,
            };

            GUI.contentColor = Color.white;
            GUI.skin.font.name = "TrajanBold";

            GUILayout.BeginArea(new Rect( 20,
                Screen.height -  (height + 20), width, height));

            GUILayout.Label("SpeedrunTricksVisualHelper", Default_Label);
            GUI.contentColor = Color.gray;
            GUILayout.Label("Bind Key to switch modes", Default_Label);
            GUI.contentColor = Color.white;
            if (GUILayout.Button($"Click me to bind", Default_Button))
            {
                instance.CreateCanvas();
                instance._textObj.text = "Waiting for input ";
                checking_for_bind = true;
            }

            GUILayout.EndArea();
        }
        private List<string> Keys = new List<string>(){
        "backspace",
        "delete",
        "tab",
        "clear",
        "return",
        "pause",
        "space",
        "up",
        "down",
        "right",
        "left",
        "insert",
        "home",
        "end",
        "page up",
        "page down",
        "f1",
        "f2",
        "f3",
        "f4",
        "f5",
        "f6",
        "f7",
        "f8",
        "f9",
        "f10",
        "f11",
        "f12",
        "f13",
        "f14",
        "f15",
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "!",
        "\"",
        "#",
        "$",
        "&",
        "'",
        "(",
        ")",
        "*",
        "+",
        ",",
        "-",
        ".",
        "/",
        ":",
        ";",
        "<",
        "=",
        ">",
        "?",
        "@",
        "[",
        "\\",
        "]",
        "^",
        "_",
        "`",
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "g",
        "h",
        "i",
        "j",
        "k",
        "l",
        "m",
        "n",
        "o",
        "p",
        "q",
        "r",
        "s",
        "t",
        "u",
        "v",
        "w",
        "x",
        "y",
        "z",
        "numlock",
        "caps lock",
        "scroll lock",
        "right shift",
        "left shift",
        "right ctrl",
        "left ctrl",
        "right alt",
        "left alt",
        "[0]",
        "[1]",
        "[2]",
        "[3]",
        "[4]",
        "[5]",
        "[6]",
        "[7]",
        "[8]",
        "[9]",
        "[+]",
        "[-]",
        "[*]",
        "[/]",
        "[.]",
        "mouse 0",
        "mouse 1",
        "mouse 2",
        };

        public void Update()
        {
            var instance = SpeedrunTricksVisualHelper.Instance;
            var settings = instance.settings;
            if (!checking_for_bind) return;

            if (Input.GetKeyDown("escape"))
            {
                looking_for_input = "";
                settings.ChangeModeKey = looking_for_input;
                SpeedrunTricksVisualHelper.Instance._textObj.text = "The binding was unsuccessful";
                GameManager.instance.StartCoroutine(
                    instance.DeleteText("The binding was unsuccessful"));
                checking_for_bind = false;
            }

            foreach (string keypress in Keys)
            {
                if (Input.GetKeyDown(keypress))
                {
                    looking_for_input = keypress;

                    Input.GetKeyDown(looking_for_input);

                    settings.ChangeModeKey = looking_for_input;
                    SpeedrunTricksVisualHelper.Instance._textObj.text = $"The key bind is {looking_for_input}";
                    GameManager.instance.StartCoroutine(
                        instance.DeleteText($"The key bind is {looking_for_input}"));
                    checking_for_bind = false;

                }
            }
        }
    }
}