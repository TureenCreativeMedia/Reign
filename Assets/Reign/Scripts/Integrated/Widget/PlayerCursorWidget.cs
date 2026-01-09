namespace reign
{
    public class PlayerCursorWidget : ReignWidget
    {
        public override void OnCreate()
        {
            base.OnCreate();
            Cursor.Instance.SetCursor("", new(){ b_Visible = false, u_LockMode = UnityEngine.CursorLockMode.Locked });
        }
    }
}
