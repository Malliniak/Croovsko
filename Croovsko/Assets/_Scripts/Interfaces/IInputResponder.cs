namespace DefaultNamespace
{
    public interface IInputResponder
    {
        void AfterHoldTouch();
        void NoInput();
        void OnTouchUp();
        void OnHold();
    }
}