namespace AttTypeDefine
{
    //水果所在的状态
    public enum eFruitState
    {
        FruitState_None,
        FruitState_Login,
        FruitState_Game,
    };

    //水果类型
    public enum eFruitType
    {
        Fruit_None,
        Fruit_Melon,
        Fruit_MelonQuarter,
        Fruit_MelonHalf,
        Fruit_Lemon,
        Fruit_LemonHalf,
        Fruit_LemonQuarter,
        Fruit_Pear,
        Fruit_PearHalf,
        Fruit_PearQuarter,
        Fruit_Missle,
        Fruit_Size = 11,
    };

    //登录状态
    public enum StartGameState
    {
        State_None,//空状态
        State_StartPicAnim,//游戏开场图片
        State_StartGameNameAnim,//
        State_FruitAnim,//水果动画
    };


    //场景
    public enum eSceneName
    {
        Scene_Login,
        Scene_Game,
    };
    //<新场景信息>
    public class CNextSceneInfo
    {
        public string m_strSceneName;
        public string m_strResPath;
    }

    //声音类型
    public enum eAudioType
    {
        Audio_BackGround,
        Audio_CutFruit,
    }

}