using System;
using System.Collections.Generic;

namespace Airport.BusinessLogic.Services
{
  public interface IService<TModel>
  {
    IList<TModel> GetAll();
    TModel GetById(int id);
    TModel Create(TModel model);
    TModel Update(TModel model);
    void Delete(TModel model);
    void Delete(int id);
  }
}