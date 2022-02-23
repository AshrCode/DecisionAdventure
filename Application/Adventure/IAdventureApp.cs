using Domain;
using System.Threading.Tasks;

namespace Application
{
    public interface IAdventureApp<T>
    {
        Task<DecisionTree<T>> GetDecisionTree(string key);

        Task<string> CreateDecisionTree(DecisionTree<T> decisionTree);
    }
}
