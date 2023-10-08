using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DCapProject.Dtos;
using DCaptialProject.Models;

namespace DCapProject.Interfaces
{
    public interface ICosmosDbService
    {
        Task Initialize();
        IEnumerable<ProgramModel> GetPrograms();
        Task<ProgramModel> GetProgram(Guid id);
        Task<ProgramModel> CreateProgram(CreateProgramDTO createProgramDTO);
        Task<ProgramModel> UpdateProgram(Guid id, UpdateProgramDTO updateProgramDTO);
        Task DeleteProgram(Guid id);
    }
}
