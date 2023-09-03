using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mongodbproject.Model;
using Mongodbproject.Services;

namespace Mongodbproject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudesntRepository _studesntRepository;

    public StudentController(IStudesntRepository studesntRepository )
    {
        _studesntRepository = studesntRepository;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetAll ()

    {
        var data = await _studesntRepository.GetAllAsynce();
         return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> Get(string  id)
    {
        var data = await _studesntRepository.GetById( id);
        return Ok(data);

    }

    [HttpPost]
    public async Task<ActionResult<Student>> Post([FromForm] Student student )
    {

        await _studesntRepository.InsertAsynce(student);

        return Ok(student);

    }

    [HttpPut]

    public async Task<ActionResult<Student>> UpdateAsynce(string id, Student student)
    {

        await _studesntRepository.UpdateAsynce(id, student);

        return Ok(student);

    }


    [HttpDelete]

    public async Task<ActionResult<Student>> DeleteAsynce(string id)

    {

        await _studesntRepository.DeleteAsynce(id);

        return Ok();

    }


















}
