using Mongodbproject.Model;

namespace Mongodbproject.Services;

public interface IStudesntRepository
{

    Task<List<Student>> GetAllAsynce();
    Task<Student> GetById(string id);
    Task<Student> InsertAsynce(Student student);
    Task UpdateAsynce(string id, Student student);
    Task DeleteAsynce(string id);   





}
