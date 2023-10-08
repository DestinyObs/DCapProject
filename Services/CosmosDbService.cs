using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using DCapProject.Dtos;
using DCapProject.Interfaces;
using DCapProject.Models;
using DCaptialProject.Models;
using Microsoft.Azure.Cosmos;

namespace DCapProject.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly CosmosSettings _cosmosSettings;
        private CosmosClient _cosmosClient;
        private Container _container;

        public CosmosDbService(CosmosSettings cosmosSettings)
        {
            _cosmosSettings = cosmosSettings;
        }

        public async Task Initialize()
        {
            _cosmosClient = new CosmosClient(_cosmosSettings.AccountURL, _cosmosSettings.AuthKey);
            await InitializeContainer();
        }

        private async Task InitializeContainer()
        {
            var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_cosmosSettings.DatabaseId);
            _container = await database.Database.CreateContainerIfNotExistsAsync(_cosmosSettings.CollectionId, "/Id");
        }

        public IEnumerable<ProgramModel> GetPrograms()
        {
            if (_container == null)
            {
                // Log or handle the fact that the container is not initialized.
                // You might want to throw an exception or return an empty list.
                return Enumerable.Empty<ProgramModel>();
            }

            return _container.GetItemLinqQueryable<ProgramModel>().ToList();
        }


        public async Task<ProgramModel> GetProgram(Guid id)
        {
            var response = await _container.ReadItemAsync<ProgramModel>(id.ToString(), new PartitionKey(id.ToString()));
            return response.Resource;
        }

        public async Task<ProgramModel> CreateProgram(CreateProgramDTO createProgramDTO)
        {
            if (_container == null)
            {
                await Initialize();
            }
            // Perform validation using DTO attributes
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(createProgramDTO, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(createProgramDTO, validationContext, validationResults, validateAllProperties: true))
            {
                // DTO is not valid, handle the validation results (e.g., log or return an error response)
                // In this example, I'll log the error messages and return null
                foreach (var validationResult in validationResults)
                {
                    Console.WriteLine(validationResult.ErrorMessage);
                }

                return null;
            }

            try
            {
                var programModel = new ProgramModel
                {
                    Id = Guid.NewGuid(),
                    ProgramTitle = createProgramDTO.ProgramTitle,
                    Summary = createProgramDTO.Summary,
                    ProgramDescription = createProgramDTO.ProgramDescription,
                    ApplicantSkills = createProgramDTO.ApplicantSkills,
                    Benefits = createProgramDTO.Benefits,
                    ApplicationCriteria = createProgramDTO.ApplicationCriteria,
                    ProgramType = createProgramDTO.ProgramType,
                    ProgramStart = createProgramDTO.ProgramStart,
                    ApplicationOpen = createProgramDTO.ApplicationOpen,
                    ApplicationClose = createProgramDTO.ApplicationClose,
                    Duration = createProgramDTO.Duration,
                    ProgramLocations = createProgramDTO.ProgramLocations.Select(dto => MapToModel(dto)).ToList(),
                    FullyRemote = createProgramDTO.FullyRemote,
                    MinQualifications = createProgramDTO.MinQualifications,
                    MaxApplications = createProgramDTO.MaxApplications
                };

                var response = await _container.CreateItemAsync(programModel);
                return response.Resource;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                // In this example, I'll log the error and return null
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<ProgramModel> UpdateProgram(Guid id, UpdateProgramDTO updateProgramDTO)
        {
            var existingProgram = await GetProgram(id);
            if (existingProgram == null)
            {
                return null; // Not found
            }

            // Map properties from updateProgramDTO to existingProgram
            existingProgram.ProgramTitle = updateProgramDTO.ProgramTitle;
            existingProgram.Summary = updateProgramDTO.Summary;
            existingProgram.ProgramDescription = updateProgramDTO.ProgramDescription;
            existingProgram.ApplicantSkills = updateProgramDTO.ApplicantSkills;
            existingProgram.Benefits = updateProgramDTO.Benefits;
            existingProgram.ApplicationCriteria = updateProgramDTO.ApplicationCriteria;
            existingProgram.ProgramType = updateProgramDTO.ProgramType;
            existingProgram.ProgramStart = updateProgramDTO.ProgramStart;
            existingProgram.ApplicationOpen = updateProgramDTO.ApplicationOpen;
            existingProgram.ApplicationClose = updateProgramDTO.ApplicationClose;
            existingProgram.Duration = updateProgramDTO.Duration; 
            existingProgram.ProgramLocations = updateProgramDTO.ProgramLocations.Select(dto => MapToModel(dto)).ToList();
            existingProgram.FullyRemote = updateProgramDTO.FullyRemote;
            existingProgram.MinQualifications = updateProgramDTO.MinQualifications;
            existingProgram.MaxApplications = updateProgramDTO.MaxApplications;

            var response = await _container.ReplaceItemAsync(existingProgram, id.ToString(), new PartitionKey(id.ToString()));
            return response.Resource;


        }
        public ProgramLocation MapToModel(ProgramLocationDTO dto)
        {
            return new ProgramLocation
            {
                City = dto.City,
                Country = dto.Country
            };
        }


        public async Task DeleteProgram(Guid id)
        {
            await _container.DeleteItemAsync<ProgramModel>(id.ToString(), new PartitionKey(id.ToString()));
        }
    }
}
