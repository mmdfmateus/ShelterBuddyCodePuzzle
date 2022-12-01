using Microsoft.AspNetCore.Mvc;
using ShelterBuddy.CodePuzzle.Api.Models;
using ShelterBuddy.CodePuzzle.Api.Models.Validations;
using ShelterBuddy.CodePuzzle.Core.DataAccess;
using ShelterBuddy.CodePuzzle.Core.Entities;

namespace ShelterBuddy.CodePuzzle.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IRepository<Animal, Guid> repository;
    private readonly AnimalModelValidator _animalModelValidator;

    public AnimalController(IRepository<Animal, Guid> animalRepository, AnimalModelValidator animalModelValidator)
    {
        repository = animalRepository;
        _animalModelValidator = animalModelValidator;
    }

    [HttpGet]
    public AnimalModel[] Get() => repository.GetAll().Select(animal => new AnimalModel
    {
        Id = $"{animal.Id}",
        Name = animal.Name,
        Colour = animal.Colour,
        DateFound = animal.DateFound,
        DateLost = animal.DateLost,
        MicrochipNumber = animal.MicrochipNumber,
        DateInShelter = animal.DateInShelter,
        DateOfBirth = animal.DateOfBirth,
        AgeText = animal.AgeText,
        AgeMonths = animal.AgeMonths,
        AgeWeeks = animal.AgeWeeks,
        AgeYears = animal.AgeYears,
        Species = animal.Species,
    }).ToArray();

    [HttpPost]
    public async Task<ActionResult> Post(AnimalModel newAnimal)
    {
        var validationResult = await _animalModelValidator.ValidateAsync(newAnimal);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var animal = new Animal
        {
            Name = newAnimal.Name,
            Colour = newAnimal.Colour,
            DateFound = newAnimal.DateFound,
            DateLost = newAnimal.DateLost,
            MicrochipNumber = newAnimal.MicrochipNumber,
            DateInShelter = newAnimal.DateInShelter,
            DateOfBirth = newAnimal.DateOfBirth,
            AgeMonths = newAnimal.AgeMonths,
            AgeWeeks = newAnimal.AgeWeeks,
            AgeYears = newAnimal.AgeYears,
            Species = newAnimal.Species,
        };
        repository.Add(animal);
        return Ok(animal);
    }
}