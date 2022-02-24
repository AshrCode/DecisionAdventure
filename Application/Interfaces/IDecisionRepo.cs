using Domain;
using System.Threading.Tasks;

namespace Application
{
    public interface IDecisionRepo<T>
    {
        Task<DecisionTree<T>> GetById(string key);

        Task AddDecision(DecisionTree<T> decisionTree, string key);

        Task UpdateDecision(DecisionTree<T> decisionTree, string key);
    }
}
