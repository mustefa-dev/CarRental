using CarRental.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers;

public class FileController: CarRental.Properties.BaseController{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService) {
        _fileService = fileService;
    }

    
    [HttpPost("multi")]
    public async Task<IActionResult> Upload([FromForm] IFormFile[] files) => Ok(await _fileService.Upload(files));



}