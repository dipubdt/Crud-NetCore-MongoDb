using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mongodbproject.Common;
using Mongodbproject.Model;
using System.Security.AccessControl;

namespace Mongodbproject.Services;

public class StudesntRepository:IStudesntRepository
{

    private readonly IMongoCollection<Student> _collection;
    private readonly IWebHostEnvironment _webHostEnvironment;

    private readonly DbConfiguration _dbConfiguration;





    public StudesntRepository(IOptions<DbConfiguration> options, IWebHostEnvironment webHostEnvironment )
    {

        _dbConfiguration = options.Value;

        var client = new MongoClient( _dbConfiguration.ConnectionString) ;

        var database = client.GetDatabase(_dbConfiguration.DatabaseName);
        _collection = database.GetCollection<Student>(_dbConfiguration.CollectionName);
        _webHostEnvironment = webHostEnvironment;


    }

    public async Task<Student> InsertAsynce(Student student)
    {


        if (student.Photo?.Length> 0)
        {
            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, Commonvariables.PhotoLocation);
            string uploadileName = Guid.NewGuid().ToString() + "_" + student.Photo.FileName;
            string filepath = Path.Combine(uploadFolder, uploadileName);

            using (var  filestream = new FileStream(filepath, FileMode.Create, access: FileAccess.ReadWrite))

            {
                var file = student.Photo.OpenReadStream();

                await file.CopyToAsync(filestream);


            }

            student.Pics = uploadileName;



        }
         await  _collection.InsertOneAsync(student);

        return student;
    }

    public async Task DeleteAsynce(string id) => await _collection.DeleteOneAsync(x => x.Id == id).ConfigureAwait(false);

    public async Task<List<Student>> GetAllAsynce() => await _collection.Find(c => true).ToListAsync();

    public async Task<Student> GetById(string id) => await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();

    public  Task UpdateAsynce(string id, Student student) =>  _collection.ReplaceOneAsync(c => c.Id == id, student);
}
