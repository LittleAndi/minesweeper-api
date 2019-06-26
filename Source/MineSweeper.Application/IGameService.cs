using System.Threading.Tasks;
using MineSweeper.Domain;

namespace MineSweeper.Application
{
    public interface IGameService
    {
        Task<Game> StartGame();
    }
}