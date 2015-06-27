﻿namespace Glimpse.Adomd.AlternateType
{
    using System;
    using System.Data;
    using System.Xml;
    using Microsoft.AnalysisServices.AdomdClient;

    /// <summary>
    /// IAdomdCommand's implementation enabling glimpse instrumentation with MDX dumping support.
    /// </summary>
    public class GlimpseAdomdCommand : IAdomdCommand
    {
        private readonly CommandExecutor _commandExecutor;
        private readonly IAdomdCommand _innerCommand;
        private readonly GlimpseAdomdConnection _innerConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlimpseAdomdCommand"/> class.
        /// </summary>
        /// <param name="command">The wrapped adomdcommand.</param>
        /// <param name="connection">Its related glimpseadomdconnection.</param>
        public GlimpseAdomdCommand(IAdomdCommand command, GlimpseAdomdConnection connection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            _innerCommand = command;
            _innerConnection = connection;
            _commandExecutor = new CommandExecutor(this);
        }

        /// <summary>
        /// Gets the command identifier.
        /// </summary>
        public Guid CommandId { get; internal set; }

        /// <summary>
        /// Gets the connection identifier.
        /// </summary>
        public Guid ConnectionId
        {
            get { return _innerConnection.ConnectionId; }
        }

        /// <inheritdoc />
        public string CommandText
        {
            get { return _innerCommand.CommandText; }
            set { _innerCommand.CommandText = value; }
        }

        /// <inheritdoc />
        public int CommandTimeout
        {
            get { return _innerCommand.CommandTimeout; }
            set { _innerCommand.CommandTimeout = value; }
        }

        /// <inheritdoc />
        public CommandType CommandType
        {
            get { return _innerCommand.CommandType; }
            set { _innerCommand.CommandType = value; }
        }

        /// <inheritdoc />
        public IDbConnection Connection
        {
            get { return _innerConnection; }
            set { throw new NotImplementedException("This behavior is not yet implemented."); }
        }

        /// <inheritdoc />
        public IDataParameterCollection Parameters
        {
            get { return _innerCommand.Parameters; }
        }

        /// <inheritdoc />
        public IDbTransaction Transaction
        {
            get { return _innerCommand.Transaction; }
            set { _innerCommand.Transaction = value; }
        }

        /// <inheritdoc />
        public UpdateRowSource UpdatedRowSource
        {
            get { return _innerCommand.UpdatedRowSource; }
            set { _innerCommand.UpdatedRowSource = value; }
        }

        /// <inheritdoc />
        public void Cancel()
        {
            _innerCommand.Cancel();
        }

        /// <inheritdoc />
        public IDbDataParameter CreateParameter()
        {
            return _innerCommand.CreateParameter();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _innerCommand.Dispose();
        }

        /// <inheritdoc />
        public object Execute()
        {
            return _commandExecutor.Execute(_innerCommand.Execute, "Execute");
        }

        /// <inheritdoc />
        public XmlReader ExecuteXmlReader()
        {
            return _commandExecutor.Execute(_innerCommand.ExecuteXmlReader, "ExecuteXmlReader");
        }

        /// <inheritdoc />
        public CellSet ExecuteCellSet()
        {
            return _commandExecutor.Execute(_innerCommand.ExecuteCellSet, "ExecuteCellSet");
        }

        /// <inheritdoc />
        public int ExecuteNonQuery()
        {
            return _commandExecutor.Execute(_innerCommand.ExecuteNonQuery, "ExecuteNonQuery");
        }

        /// <inheritdoc />
        public IDataReader ExecuteReader()
        {
            return _commandExecutor.Execute(_innerCommand.ExecuteReader, "ExecuteReader");
        }

        /// <inheritdoc />
        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return _commandExecutor.Execute(() => _innerCommand.ExecuteReader(behavior), "ExecuteReader");
        }

        /// <inheritdoc />
        public object ExecuteScalar()
        {
            return _commandExecutor.Execute(_innerCommand.ExecuteScalar, "ExecuteScalar");
        }

        /// <inheritdoc />
        public void Prepare()
        {
            _innerCommand.Prepare();
        }
    }
}