using System;
namespace Institute.Models.Models
{
	public class Result<T>
	{
		public T Value { get; }
		public bool IsSucces { get; }
		public string Error { get; }

		private Result(T value, bool isSuccess, string error)
		{
			Value = value;
			IsSucces = isSuccess;
			Error = error;
		}

		public static Result<T> Success(T value) => new Result<T>(value, true, null);
		public static Result<T> Failure(string error) => new Result<T>(default, false, error);
	}
}

