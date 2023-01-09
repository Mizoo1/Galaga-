namespace Galaga.Menu
{
    public interface IGameState 
    {
        void Update();
        void Draw();
        void HandleInput();
    }
}