using Libraries.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Repository.Common
{
    public static class ProcedureExtension
    {
		public static DbCommand LoadStoredProcedure(this DataContext context, string procedureName)
		{
			var cmd = context.Database.GetDbConnection().CreateCommand();
			cmd.CommandText = procedureName;
			cmd.CommandType = CommandType.StoredProcedure;
			return cmd;
		}

		public static DbCommand WithSqlParams(this DbCommand cmd, params (string, object)[] nameValueParamPairs)
		{
			foreach (var pair in nameValueParamPairs)
			{
				var param = cmd.CreateParameter();
				param.ParameterName = pair.Item1;
				param.Value = pair.Item2 ?? DBNull.Value;
				cmd.Parameters.Add(param);
			}

			return cmd;
		}

		public static async Task<IList<T>> ExecuteStoredProcedureAsync<T>(this DbCommand command) where T : class
		{
			using (command)
			{
				if (command.Connection.State == System.Data.ConnectionState.Closed)
					await command.Connection.OpenAsync();

				var reader = command.ExecuteReader();
				DataTable dt = new DataTable();
				dt.Load(reader);
				return ConvertDataTable<T>(dt);
			}
		}

		public static async Task<DataTable> ExecuteStoredProcedureAsync(this DbCommand command)
		{
			using (command)
			{
				if (command.Connection.State == ConnectionState.Closed)
					await command.Connection.OpenAsync();
				try
				{
					var reader = command.ExecuteReader();
					DataTable dt = new DataTable();
					dt.Load(reader);
					return dt;
				}
				catch
				{
					throw;
				}
				finally
				{
					command.Connection.Close();
				}
			}
		}

		public static async Task<string> ExecuteStoredProcedureAsync(this DbCommand command, string resultName)
		{
			using (command)
			{
				if (command.Connection.State == ConnectionState.Closed)
					await command.Connection.OpenAsync();
				try
				{
					var reader = command.ExecuteReader();
					string result = string.Empty;
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							result = reader[resultName].ToString();
						}
					}
					return result;
				}
				finally
				{
					command.Connection.Close();
				}
			}
		}

		private static IList<T> MapToList<T>(this DbDataReader dr)
		{
			var objList = new List<T>();
			var props = typeof(T).GetRuntimeProperties();

			var colMapping = dr.GetColumnSchema()
				.Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
				.ToDictionary(key => key.ColumnName.ToLower());

			if (dr.HasRows)
			{
				while (dr.Read())
				{
					T obj = Activator.CreateInstance<T>();
					foreach (var prop in props)
					{
						var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
						prop.SetValue(obj, val == DBNull.Value ? null : val);
					}
					objList.Add(obj);
				}
			}
			return objList;
		}

		private static List<T> ConvertDataTable<T>(DataTable dt)
		{
			List<T> data = new List<T>();
			foreach (DataRow row in dt.Rows)
			{
				T item = GetItem<T>(row);
				data.Add(item);
			}
			return data;
		}
		private static T GetItem<T>(DataRow dr)
		{
			Type temp = typeof(T);
			T obj = Activator.CreateInstance<T>();

			foreach (DataColumn column in dr.Table.Columns)
			{
				foreach (PropertyInfo pro in temp.GetProperties())
				{
					if (pro.Name == column.ColumnName)
						pro.SetValue(obj, dr[column.ColumnName], null);
					else
						continue;
				}
			}
			return obj;
		}

		public static DbCommand WithOutParams(this DbCommand cmd, params (string, object)[] nameValueParamPairs)
		{
			foreach (var pair in nameValueParamPairs)
			{
				var param = cmd.CreateParameter();
				param.ParameterName = pair.Item1;
				param.Value = pair.Item2 ?? DbType.String.ToString();
				param.Direction = ParameterDirection.Output;
				cmd.Parameters.Add(param);
			}

			return cmd;
		}
	}
}
