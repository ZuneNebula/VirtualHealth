using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserService.Config;
using UserService.Models;

namespace UserService.Services
{
    public class DoctorsService
    {
        private readonly IMongoCollection<Doctor> doctorCollection;

        public DoctorsService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            doctorCollection = mongoDatabase.GetCollection<Doctor>(databaseSettings.Value.DoctorsCollectionName);
        }

        public async Task<List<Doctor>> GetAsync()
        {
            return await doctorCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Doctor> GetAsync(String email)
        {
            return await doctorCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Doctor> CreateAsync(Doctor doctor)
        {
            await doctorCollection.InsertOneAsync(doctor);

            return doctor;
        }

        public async Task<Doctor> UpdateAsync(String email,Doctor doctor)
        {
            await doctorCollection.ReplaceOneAsync(x => x.Email == email, doctor);

            return doctor;
        }

        public async Task<Boolean> DeleteAsync(String email)
        {
            await doctorCollection.DeleteOneAsync(x => x.Email == email);

            return true;
        }


    }
}
