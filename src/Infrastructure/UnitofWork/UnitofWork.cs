using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConnectionFactory _connectionFactory;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IDbConnection Connection => _connection ??= _connectionFactory.CreateConnection();
        public IDbTransaction Transaction => _transaction;

        public void Begin()
        {
            if (_connection == null)
                _connection = _connectionFactory.CreateConnection();

            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
            _transaction = null;
            _connection = null;
        }
    }
}
