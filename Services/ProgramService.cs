//using DCaptialProject.Models;
//using Microsoft.Azure.Cosmos;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace DCaptialProject.Services
//{
//    public class ProgramService
//    {
//        private readonly CosmosClient _cosmosClient;
//        private readonly string _databaseId;
//        private readonly string _containerId;

//        private Container _container;

//        public ProgramService(CosmosClient cosmosClient, IConfiguration configuration)
//        {
//            _cosmosClient = cosmosClient;
//            _databaseId = configuration["Cosmos:DatabaseId"];
//            _containerId = "ProgramNewCollection";

//            InitializeContainer().Wait();
//        }

//        private async Task InitializeContainer()
//        {
//            var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId);
//            _container = await database.Database.CreateContainerIfNotExistsAsync(_containerId, "/Id");
//        }

//        public async Task<ProgramModel> CreateProgramAsync(ProgramModel program)
//        {
//            // Implement logic to create a new program document in Cosmos DB
//            var response = await _container.CreateItemAsync(program);
//            return response.Resource;
//        }

//        public async Task<ProgramModel> GetProgramByIdAsync(string programId)
//        {
//            // Implement logic to retrieve a program document by ID from Cosmos DB
//            var response = await _container.ReadItemAsync<ProgramModel>(programId, new PartitionKey(programId));
//            return response.Resource;
//        }

//        public async Task<ProgramModel> UpdateProgramAsync(string programId, ProgramModel updatedProgram)
//        {
//            // Implement logic to update a program document in Cosmos DB
//            var response = await _container.ReplaceItemAsync(updatedProgram, programId, new PartitionKey(programId));
//            return response.Resource;
//        }

//        public async Task DeleteProgramAsync(string programId)
//        {
//            // Implement logic to delete a program document from Cosmos DB
//            await _container.DeleteItemAsync<ProgramModel>(programId, new PartitionKey(programId));
//        }
//    }
//}
