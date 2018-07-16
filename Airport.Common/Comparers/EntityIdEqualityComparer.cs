using System;
using System.Collections.Generic;

namespace Airport.Common.Comparers
{
  public class EntityIdEqualityComparer<TEntity> : IEqualityComparer<TEntity>
  {
    public bool Equals(TEntity x, TEntity y)
    {
      if (x.GetType() != y.GetType())
      {
        return false;
      }

      var idField = typeof(TEntity).GetProperty("Id");

      var xId = idField.GetValue(x);
      var yId = idField.GetValue(y);

      return xId.Equals(yId);
    }

    public int GetHashCode(TEntity obj)
    {
      var idField = typeof(TEntity).GetProperty("Id");
      return idField.GetValue(obj).GetHashCode();
    }
  }
}