using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters
{
    public class AnimationPlayer
    {
    }

    public class LayerNames
    {
        public const string PLATAFORM = "Plataform";
        public const string DESTROYER = "Destroyer";

    }

    public class InputNames
    {
        public const string AXIS_HORIZONTAL = "Horizontal";
        public const string KEY_JUMP = "space";
    }

    public class SceneNames
    {
        public const string MAIN_MENU = "MainMenu";
        public const string GAME = "Game";
    }

    public class Session
    {
        public const int SPRING = 0;
        public const int SUMMER = 1;
        public const int WINNER = 2;
    }
}
