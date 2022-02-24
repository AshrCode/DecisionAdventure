using Domain;
using System.Threading.Tasks;

namespace Application
{
    public interface IAdventureApp<T>
    {
        Task<DecisionTree<T>> GetDecisionTree(string key);

        Task<string> AddDecisionTree(DecisionTree<T> decisionTree);

        Task<string> UpdateDecisionTree(DecisionTree<T> decisionTree, string key);
    }
}
