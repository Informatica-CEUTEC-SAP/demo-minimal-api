using Microsoft.AspNetCore.Mvc;

namespace DemoMinimalApi;

[ApiController]
[Route("[controller]")]
public class ProductoController: ControllerBase
{
    //Harcodear data de ejemplo
        public ProductoController()
        {
            Productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Producto A", Precio = 10.99m, Stock = 100, FechaCreacion = DateTime.Now },
                new Producto { Id = 2, Nombre = "Producto B", Precio = 20.50m, Stock = 50, FechaCreacion = DateTime.Now },
                new Producto { Id = 3, Nombre = "Producto C", Precio = 15.75m, Stock = 75, FechaCreacion = DateTime.Now }
            };
        }
        private List<Producto> Productos { get; }
    
        //Entidades tienen 4 acciones basicas=> HTPP
        // C reate - Crear                      POST
        // R ead - Leer                         GET
        // U pdate - Actualizar                 PATCH/PUT
        // D elete -Eliminar                    DELETE
    // Create - POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateProducto([FromBody] ProductoRequest request)
        {
            // Logic to add the product (e.g., save to database)
            if (request == null)
            {
                return BadRequest();
            }
            var newProducto = new Producto
            {
                Id = this.Productos.Max(p => p.Id) + 1,
                Nombre = request.Nombre,
                Precio = request.Precio,
                Stock = request.Stock,
                FechaCreacion = DateTime.Now
            };
            this.Productos.Add(newProducto);
            
            return CreatedAtAction(nameof(GetProductoById), new { id = newProducto.Id }, newProducto);
        }
    
        // Read - GET all
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetProductos()
        {
            // Logic to retrieve all products
            return Ok(this.Productos);
        }
    
        // Read - GET by ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProductoById(int id)
        {
            var response = this.Productos.FirstOrDefault(p => p.Id == id);
            return Ok(response);
        }
    
        // Update - PUT
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateProducto(int id, [FromBody] Producto producto)
        {
            // Logic to update the product
            if (id != producto.Id)
            {
                return BadRequest();
            }
            // Replace with actual update logic
            var existingProducto = this.Productos.FirstOrDefault(p => p.Id == id);
            if (existingProducto == null)
            {
                return NotFound();
            }
            existingProducto.Nombre = producto.Nombre;
            existingProducto.Precio = producto.Precio;
            existingProducto.Stock = producto.Stock;
            existingProducto.FechaCreacion = producto.FechaCreacion;
  
            return Ok(producto);
        }
    
        // Delete - DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProducto(int id)
        {
            // Logic to delete the product
            var producto = this.Productos.Remove(this.Productos.FirstOrDefault(p => p.Id == id));
            return Ok();
        }
}

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public DateTime FechaCreacion { get; set; }
}

public record ProductoRequest(string Nombre, decimal Precio, int Stock);