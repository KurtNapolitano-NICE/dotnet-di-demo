using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using WebApi.Controllers;
using WebApi.Services;

namespace WebApi.Tests;

public class UnitTest1
{
    private readonly INextValueService _nextValueService;
    
    public UnitTest1()
    {
        _nextValueService = Substitute.For<INextValueService>();
    }

    [Fact]
    public void Test1()
    {
        _nextValueService.FetchNextValue().Returns(4);

        var valuesController = CreateValuesController();

        var result = valuesController.GetNextValue();

        var okResult = result.Should().BeAssignableTo<OkObjectResult>().Subject;

        okResult.Value.Should().Be(1);
    }


    [Fact]
    public void ScTest()
    {
        var sc = new ServiceCollection();
        sc.AddSingleton<INextValueService, NextValueService>();


        var provider = sc.BuildServiceProvider();

        var svc = provider.GetRequiredService<INextValueService>();

        var svc2 = provider.GetRequiredService<INextValueService>();
        
        svc.Should().Be(svc2);
    }


    private ValuesController CreateValuesController()
    {
        return new ValuesController(_nextValueService, null, null);
    }
}