using System.Collections.Generic;
using Modding;

namespace Speedrun_Tricks_Visual_Helper_1221
{
    
    public class GlobalSettings : IModSettings
    {
        public string ChangeModeKey = "";
        public bool Current_Mode_Name_AlwaysVisible = false;
        public string Color_true = "white";
        public string Color_false = "black";
        public bool Turnaround_OnlyinAir = false; 
        public bool Fireball_OnlyinAir = false; 
        public bool Dash_OnlyonGround = false; 
        
        public List<string> Available_Colors = new List<string>()
        {
            "Note: Not to be changed. Only here for reference",
            "white","black", "blue", "cyan" ,"grey", "green", "magenta", "red", "yellow"
        };
    } 
}
