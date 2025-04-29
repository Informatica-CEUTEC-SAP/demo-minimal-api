using Microsoft.AspNetCore.Mvc;

namespace DemoMinimalApi;

[ApiController]
[Route("[controller]")]
public class DespedirController: ControllerBase
{
    /// <summary>
    /// Endpoint para despedir al mundo.
    /// </summary>
    /// <returns>string</returns>
    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Despedirse()
    {
        var despedida = $"Adiós, mundo!";
        return Ok(despedida);
    }
}