namespace Elysium.Hotbar
{
    public interface IView<T>
    {
        bool Enabled { get; }

        void Show();
        void Hide();
        void Refresh(T _data);
    }
}