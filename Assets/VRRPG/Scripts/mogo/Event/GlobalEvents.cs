public enum GlobalEvents : int
{
    ChangeHp = 1, //��ɫѪ���ı�
    ChangeLevel,  //��ɫ�ȼ��ı�
    ChangeForce,
    Death,        //��ɫ����
    GetItem,      //��ɫ��õ���
    GetTask,      
    FinishTask,
    OpenFunction,
    GetActivity,
    EnterInstance,
    LeaveInstance,
    OpenGUI,
    ButtonClick,
    SkillAvailable,
    TimeLimitEvent,
    Achiement,
    ArenicCredit
}
static public class UIEvent
{
    public readonly static string AddInstance = "";
    public readonly static string FllowTarget = "FllowTarget";
    public readonly static string QuitGame = "QuitGame";
    //ʰȡ����Ʒ
    public static string PICK_UP_ITEM = "PickUpItem";
    //AIѰ·����λ��
    public static string AiTargetReached = "AiTargetReached";
    public static string ROLE_DEAD = "RoleDead";
    public static string GAME_SCENE_INITED = "GAME_SCENE_INITED";
}