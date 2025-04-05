using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.ResultHandler;



    public class Result
    {
        public bool IsSuccess { get; }
        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error ?? Error.None;
        }

        public static Result Success() => new(true, Error.None);
        public static Result<T> Success<T>(T value) => new(value, true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result<T> Failure<T>(Error error) => new(default!, false, error);
    }


    public class Result<T> : Result
    {
        private readonly T _value;
        public T Value => IsSuccess ? _value : throw new InvalidOperationException("Cannot access value of failed result");

        protected internal Result(T value, bool isSuccess, Error error)
            : base(isSuccess, error) => _value = value;
    }





