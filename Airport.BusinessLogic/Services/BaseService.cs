using System;
using System.Collections.Generic;
using Airport.Data.UnitOfWork;
using Airport.Data.Models;
using AutoMapper;

using Airport.Common.Exceptions;

namespace Airport.BusinessLogic.Services
{
  public class BaseService<TModel, TEntity> : IService<TModel> where TEntity : Entity
  {
    protected IUnitOfWork _unitOfWork;

    public BaseService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public TModel Create(TModel model)
    {
      var entity = Mapper.Map<TEntity>(model);
      entity = _unitOfWork.Set<TEntity>().Create(entity);
      _unitOfWork.SaveChanges();

      return Mapper.Map<TModel>(entity);
    }

    public virtual void Delete(TModel model)
    {
      var entity = Mapper.Map<TEntity>(model);
      _unitOfWork.Set<TEntity>().Delete(entity);
      _unitOfWork.SaveChanges();
    }

    public virtual void Delete(int id)
    {
      _unitOfWork.Set<TEntity>().Delete(id);
      _unitOfWork.SaveChanges();
    }

    public virtual IList<TModel> GetAll()
    {
      var entities = _unitOfWork.Set<TEntity>().Get();
      return Mapper.Map<IList<TModel>>(entities);
    }

    public virtual TModel GetById(int id)
    {
      var entity = _unitOfWork.Set<TEntity>().Get(id);
      if (entity == null)
        throw new NotFoundException(typeof(TEntity).Name + " with such id was not found");

      return Mapper.Map<TModel>(entity);
    }

    public virtual TModel Update(TModel model)
    {
      var entity = Mapper.Map<TEntity>(model);
      entity = _unitOfWork.Set<TEntity>().Update(entity);
      _unitOfWork.SaveChanges();

      return Mapper.Map<TModel>(entity);
    }
  }
}