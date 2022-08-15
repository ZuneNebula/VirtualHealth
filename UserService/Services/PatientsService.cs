using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserService.Config;
using UserService.Models;

namespace UserService.Services
{
    public class PatientsService
    {
        private readonly IMongoCollection<Patient> patientCollection;

        public PatientsService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            patientCollection = mongoDatabase.GetCollection<Patient>(databaseSettings.Value.PatientsCollectionName);
        }

        public async Task<List<Patient>> GetAsync()
        {
            return await patientCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Patient> GetAsync(String email)
        {
            return await patientCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Patient> CreateAsync(Patient patient)
        {
            await patientCollection.InsertOneAsync(patient);

            return patient;
        }

        public async Task<Patient> UpdateAsync(String email, Patient patient)
        {
            await patientCollection.ReplaceOneAsync(x => x.Email == email, patient);

            return patient;
        }

        public async Task<Boolean> DeleteAsync(String email)
        {
            await patientCollection.DeleteOneAsync(x => x.Email == email);

            return true;
        }


    }
}
