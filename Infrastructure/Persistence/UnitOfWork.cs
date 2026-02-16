using Application.Common.Interfaces.Data;
using Application.Interfaces;
using Dapper;
using DSP.Pricing.Application.Common.Interfaces.Data;
using DSP.Pricing.Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Pricing.Application.Common.Interfaces;
using Pricing.Application.Common.Interfaces.Data;
using Pricing.Infrastructure.Persistence.Repositories;
using System.Data;

namespace Pricing.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        const string CONNECTION_STRING_NAME = "PricingTool";
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        private IBaseEditPriceLeasingRepository _baseEditPriceLeasingRepository;
        private IBasePriceLeasingRepository _basePriceLeasingRepository;
        private ILeasingCalculationRepository _leasingCalculationRepository;
        private IMileageRepository _mileageRepository;
        private IModelBaseDataRepository _modelBaseDataRepository;
        private IProductionCostRepository _productionCostRepository;
        private IUserRepository? _userRepository;
        private readonly IJwtTokenService _jwtTokenService;



        private ISaveLeasingPriceRepository _saveLeasingPriceRepository;
        public UnitOfWork(
            ApplicationDbContext context,
            IConfiguration configuration,
            IJwtTokenService jwtTokenService)
        {
            _context = context;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
        }
        public IUserRepository UserRepository => _userRepository ??(_userRepository = new UserRepository(_context));

        public IProductionCostRepository ProductionCost => _productionCostRepository ?? (_productionCostRepository = new ProductionCostRepository(_context));

        public IMileageRepository Mileage => _mileageRepository ?? (_mileageRepository = new MileageRepository(_context));
        public IModelBaseDataRepository ModelBaseData => _modelBaseDataRepository ?? (_modelBaseDataRepository = new ModelBaseDataRepository(_context));
        public IBaseEditPriceLeasingRepository BaseEditPriceLeasing => _baseEditPriceLeasingRepository ?? (_baseEditPriceLeasingRepository = new BasePriceEditLeasingRepository(_context));
        public IBasePriceLeasingRepository BasePriceLeasing => _basePriceLeasingRepository ?? (_basePriceLeasingRepository = new BasePriceLeasingRepository(_context));
        public ILeasingCalculationRepository LeasingCalculation => _leasingCalculationRepository?? (_leasingCalculationRepository = new LeasingCalculationRepository(_context));
        public ISaveLeasingPriceRepository saveLeasingPriceRepository => _saveLeasingPriceRepository ?? (_saveLeasingPriceRepository = new SaveLeasingPriceRepository(_context));


        public IUserRepository User
            => _userRepository ??= new UserRepository(_context);
       

        public IJwtTokenService JwtTokenService
            => _jwtTokenService;


        public async Task<IList<T>> ExecWithStoreProcedureAsyncWithParam<T>(string query, params object[] parameters)
        {
            return await _context.Database.SqlQueryRaw<T>(query, parameters).ToListAsync();
        }

        public async Task<IList<T>> ExecWithStoreProcedureAsync<T>(string query)
        {
            return await _context.Database.SqlQueryRaw<T>(query).ToListAsync();
        }

        //When you expect a model back
        public IEnumerable<T> ExecWithStoreProcedure<T>(string query)
        {
            return _context.Database.SqlQueryRaw<T>(query);
        }

        // Fire and forget (async)
        public async Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters)
        {
            await _context.Database.ExecuteSqlRawAsync(query, parameters);
        }

        // Fire and forget
        public void ExecuteWithStoreProcedure(string query, params object[] parameters)
        {
            _context.Database.ExecuteSqlRaw(query, parameters);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesWithTimeoutAsync(int seconds, CancellationToken ct)
        {
            var old = _context.Database.GetCommandTimeout();
            try
            {
                _context.Database.SetCommandTimeout(seconds);
                return await _context.SaveChangesAsync(ct);
            }
            finally
            {
                _context.Database.SetCommandTimeout(old);
            }
        }


        public async Task<List<T>> ExecFunctionAsync<T>(string query) where T : class
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<T>(
                    query,
                    commandType: CommandType.Text
                );
                return result.AsList();
            }
        }
        public async Task<List<T>> ExecStoredProcedureAsync<T>(string spName, params object[] parameters) where T : class
        {
            var connection = _context.Database.GetDbConnection();

            await using var command = connection.CreateCommand();
            command.CommandText = spName;
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    command.Parameters.Add(p);
                }
            }

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            var result = new List<T>();
            await using var reader = await command.ExecuteReaderAsync();

            var props = typeof(T).GetProperties();

            while (await reader.ReadAsync())
            {
                var item = Activator.CreateInstance<T>();
                foreach (var prop in props)
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                    {
                        prop.SetValue(item, reader[prop.Name]);
                    }
                }
                result.Add(item);
            }

            return result;
        }

        public async Task<List<T>> ExecSqlFunctionAsync<T>(string query, string connectionString = "VKM") where T : class
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<T>(
                    query,
                    commandType: CommandType.Text
                );
                return result.AsList();
            }
        }

        public async Task<int> ExecSqlNonQueryAsync(string query, string connectionString = "VKM")
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString(connectionString)))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(
                    query,
                    commandType: CommandType.Text
                );
                return result; // Number of rows affected
            }
        }

        public async Task<List<T>> ExecFunctionWithParmsAsync<T>(string query, object parameters) where T : class
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<T>(
                    query,
                    parameters,
                    commandType: CommandType.Text
                );
                return result.AsList();
            }
        }


        public async Task<bool> ExecFunctionWithParmsTrueOrFalseAsync(string query, object parameters)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME)))
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync<bool>(
                    query,
                    parameters,
                    commandType: CommandType.Text
                );
                return result;
            }
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object? param = null) where T : class
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME));
            await connection.OpenAsync();
            var result = await connection.QueryAsync<T>(
                query,
                param
            );
            await connection.CloseAsync();
            return result;
        }
        public async Task<int> ExecuteAsync(string sql, object? param = null)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString(CONNECTION_STRING_NAME));
            await connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql, param);
            await connection.CloseAsync();
            return result;
        }

        private readonly Dictionary<string, object> _repositoryMap = new();

        public object? GetRepositoryByEntityName(string entityName)
        {
            if (_repositoryMap.Count == 0)
            {

            }

            return _repositoryMap.TryGetValue(entityName, out var repo) ? repo : null;
        }


        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public void DisableQueryTrackingBehavior() =>
    _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        public void DisableChangeTracking() =>
    _context.ChangeTracker.AutoDetectChangesEnabled = false;

        public void EnableChangeTracking() =>
            _context.ChangeTracker.AutoDetectChangesEnabled = true;



        public void Dispose() => _context.Dispose();
    }
}
