using Domain;
using System.Threading.Tasks;

namespace Application
{
    public interface IAdventureApp<T>
    {
        Task<DecisionTree<T>> GetDecisionTree(string key);

        Task<string> SaveDecisionTree(DecisionTree<T> decisionTree, string key = null);
    }
}
