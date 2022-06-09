﻿using EMY.Papel.Restaurant.Core.Domain.Entities;

namespace EMY.Papel.Restaurant.Core.Domain.Common
{
    public class ResultModel<TEntity> where TEntity : BaseEntity
    {
        public ResultModel()
        {
            IsSuccess = false;
            Message = "";     
        }
        public bool IsSuccess { get; set; }
        public int resultType { get; set; }
        public string Message { get; set; }
        public TEntity Value { get; set; }
        public IList<TEntity> Values { get; set; }
    }
}
