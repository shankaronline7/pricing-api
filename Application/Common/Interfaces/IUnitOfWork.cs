using Application.Common.Interfaces.Data;
using DSP.Pricing.Application.Common.Interfaces.Data;
using Pricing.Application.Common.Interfaces.Data;

namespace Pricing.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesWithTimeoutAsync(int seconds, CancellationToken ct);
        object? GetRepositoryByEntityName(string entityName);
        ISaveLeasingPriceRepository saveLeasingPriceRepository { get; }
        ILeasingCalculationRepository LeasingCalculation { get; }
        IMileageRepository Mileage { get; }
        IModelBaseDataRepository ModelBaseData { get; }
        IProductionCostRepository ProductionCost { get; }

        IBasePriceLeasingRepository BasePriceLeasing { get; }
        IBaseEditPriceLeasingRepository BaseEditPriceLeasing { get; }
        IUserRepository UserRepository { get; }
        /// <summary>IMasterDataSyncRepository
        /// When you expect a model back (async)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IList<T>> ExecWithStoreProcedureAsyncWithParam<T>(string query, params object[] parameters);
        Task<IList<T>> ExecWithStoreProcedureAsync<T>(string query);
        /// <summary>
        /// When you expect a model back
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<T> ExecWithStoreProcedure<T>(string query);
        /// <summary>
        /// Fire and forget (async)
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters);
        /// <summary>
        /// Fire and forget
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        void ExecuteWithStoreProcedure(string query, params object[] parameters);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<List<T>> ExecFunctionAsync<T>(string query) where T : class;
        Task<List<T>> ExecSqlFunctionAsync<T>(string query, string connectionString = "VKM") where T : class;
        Task<int> ExecSqlNonQueryAsync(string query, string connectionString = "VKM");
        Task<List<T>> ExecFunctionWithParmsAsync<T>(string query, object parameters) where T : class;

        Task<bool> ExecFunctionWithParmsTrueOrFalseAsync(string query, object parameters);

        /// <summary>
        /// Execute a query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="query">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <returns>
        /// A sequence of data of <typeparamref name="T"/>; if a basic type (int, string, etc) is queried then the data from the first column is assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        Task<IEnumerable<T>> QueryAsync<T>(string query, object? param = null) where T : class;
        /// <summary>
        /// Execute a command asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> ExecuteAsync(string sql, object? param = null);
        Task<List<T>> ExecStoredProcedureAsync<T>(string spName, params object[] parameters) where T : class;

        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        void DisableQueryTrackingBehavior();
        void DisableChangeTracking();
        void EnableChangeTracking();
    }
}