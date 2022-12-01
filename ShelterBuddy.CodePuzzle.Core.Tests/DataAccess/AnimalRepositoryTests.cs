using ShelterBuddy.CodePuzzle.Core.DataAccess;
using Shouldly;
using Xunit;

namespace ShelterBuddy.CodePuzzle.Core.Tests.DataAccess;

public class AnimalRepositoryTests
{
    private readonly AnimalRepository _sut;

    public AnimalRepositoryTests()
    {
        _sut = new();
    }

    [Fact]
    public void GetAll_CanLoadData()
    {
        var animals = _sut.GetAll();

        animals.ShouldNotBeEmpty();
    }
}