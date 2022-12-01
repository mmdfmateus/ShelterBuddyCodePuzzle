using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShelterBuddy.CodePuzzle.Api.Controllers;
using ShelterBuddy.CodePuzzle.Api.Models;
using ShelterBuddy.CodePuzzle.Api.Models.Validations;
using ShelterBuddy.CodePuzzle.Core.DataAccess;
using ShelterBuddy.CodePuzzle.Core.Entities;
using Shouldly;
using Xunit;

namespace ShelterBuddy.CodePuzzle.Api.Tests.Controllers
{
    public class AnimalControllerTests
    {
        private readonly AnimalController _sut;
        private readonly Mock<IRepository<Animal, Guid>> _repositoryMock;
        private readonly AnimalModelValidator _animalModelValidator;

        public AnimalControllerTests()
        {
            _repositoryMock = new();
            _animalModelValidator = new();

            _sut = new(_repositoryMock.Object, _animalModelValidator);
        }

        [Theory]
        [InlineData(null, "'Name' must not be null")]
        [InlineData("", "'Name' must not be empty")]
        [InlineData("   ", "'Name' must not be empty")]
        public async Task Create_ReturnsBadRequestIfNameIsNullOrEmpty(string name, string errorMessageExpected)
        {
            var request = new AnimalModel()
            {
                Name = name
            };

            var response = await _sut.Post(request);

            var badRequest = response as BadRequestObjectResult;
            (badRequest.Value as List<ValidationFailure>).ShouldContain(item => item.ErrorMessage == errorMessageExpected);
        }

        [Theory]
        [InlineData(null, "'Species' must not be null")]
        [InlineData("", "'Species' must not be empty")]
        [InlineData("   ", "'Species' must not be empty")]
        public async Task Create_ReturnsBadRequestIfSpeciesIsNullOrEmpty(string species, string errorMessageExpected)
        {
            var request = new AnimalModel()
            {
                Species = species
            };

            var response = await _sut.Post(request);

            var badRequest = response as BadRequestObjectResult;
            (badRequest.Value as List<ValidationFailure>).ShouldContain(item => item.ErrorMessage == errorMessageExpected);
        }

        [Fact]
        public async Task Create_ReturnsBadRequestIfDateOfBirthAndAgeAreMissing()
        {
            var request = new AnimalModel()
            {
                Name = "animal name",
                Species = "German Sheppard",
                DateOfBirth = null,
                AgeYears = null, 
                AgeMonths = null,
                AgeWeeks = null
            };

            var response = await _sut.Post(request);

            var badRequest = response as BadRequestObjectResult;
            (badRequest.Value as List<ValidationFailure>).ShouldContain(item => item.ErrorMessage == "'DateOfBirth' or 'Age' must be provided");
        }

        [Theory]
        [InlineData(false, null, null, null)]
        [InlineData(true, 2022, null, null)]
        [InlineData(true, null, 9, null)]
        [InlineData(true, null, null, 2)]
        public async Task Create_ReturnsOkIfDateOfBirthOrAnyAgeIsProvided(bool isNullDate, int? ageYears, int? ageMonths, int? ageWeeks)
        {
            var request = new AnimalModel()
            {
                Name = "animal name",
                Species = "German Sheppard",
                DateOfBirth = isNullDate ? null : DateTime.Now,
                AgeYears = ageYears,
                AgeMonths = ageMonths,
                AgeWeeks = ageWeeks
            };

            var response = await _sut.Post(request);

            response.ShouldBeOfType<OkObjectResult>();
        }
    }
}
