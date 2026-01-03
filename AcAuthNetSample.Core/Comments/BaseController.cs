using AcAuthNetSample.Core.Domain.Shared.Entities;
using AcAuthNetSample.Core.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AcAuthNetSample.Core.Comments {
    public class BaseController<T> : ControllerBase where T : BaseEntity {

        private readonly IBaseRepository<T> _repository;

        public BaseController(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("add")]
        public virtual async Task<IActionResult> Add(T data)
        {
             _repository.Add(data);
            await _repository.SaveChangeAsync();
            return JsonResultHelper.Success();
        }

        [HttpPatch]
        [Route("update/{id:int}")]
        public virtual async Task<IActionResult> Update([FromRoute]int id ,T data)
        {
            _repository.Update(data);
            await _repository.SaveChangeAsync();
            return JsonResultHelper.Success();
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            _repository.DeleteById(id);
            await _repository.SaveChangeAsync();
            return JsonResultHelper.Success();
        }
    }
}
