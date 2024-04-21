﻿using basic2_03.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace basic2_03.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IGameDB _gameDB;
    private readonly IMemoryDB _memoryDB;

    public LoginController(IGameDB gameDB, IMemoryDB memoryDB)
    {
        _gameDB = gameDB;
        _memoryDB = memoryDB;
    }

    [HttpPost]
    public async Task<PkLoginResponse> Post(PkLoginRequest request)
    {
        Console.WriteLine($"[Request Login] ID:{request.ID}, PW:{request.PW}");
        var response = new PkLoginResponse();

        return response;
    }
}


public class PkLoginRequest
{
    public string ID { get; set; }
    public string PW { get; set; }
}

public class PkLoginResponse
{
    public int Result { get; set; }
}
