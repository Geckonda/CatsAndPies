using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.Entities.PiesTables;
using CatsAndPies.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Services.Implementations
{
    public class PieService : IPieService
    {
        private readonly IPieRepository _pieRepository;

        public PieService(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public async Task<Result<List<PieEntity>>> TryGetAllPies()
        {
            var pies = await _pieRepository.GetAll();
            if (pies.Count == 0)
                return Result<List<PieEntity>>.ErrorResult();
            return Result<List<PieEntity>>.SuccessResult(pies);
        }
    }
}
