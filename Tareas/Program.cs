// See https://aka.ms/new-console-template for more information
using System;
using System.Text.Json;
using System.Net.Http;
using espacioTarea;

HttpClient client = new HttpClient(); //lo utilizo para realizar la solicitud HTTP a servicios web

var url = "https://jsonplaceholder.typicode.com/todos/";
HttpResponseMessage response = await client.GetAsync(url); //realiza la solicitud a la URL indiada y espera la respuesta del servidor
try
{
    response.EnsureSuccessStatusCode();  //verifica que la respuesta del servidor tenga un codigo de estado exitoso
}
catch (HttpRequestException ex) //caso en el que ocurrió un error con el servidor
{
    Console.WriteLine("La solicitud no fue exitosa: " + ex.Message); //mensaje de error de servidor
}

string responseBody = await response.Content.ReadAsStringAsync(); //recibe el json y lo convierte en un string, tiene la misma forma que el json real
List<Tarea> listTAREA = JsonSerializer.Deserialize<List<Tarea>>(responseBody); //transformo el arreglo en una lista de objetos de la clase Tarea

var tareasOrdenadas = listTAREA.OrderBy(t => t.completed).ToList(); //ordeno la lista en una nueva lista con los valores booleanos de false a true

foreach (var tr in listTAREA)
{
    Console.WriteLine("------------------");
    Console.WriteLine($"UserID: {tr.userId}'\nid: {tr.id}\ntitle: {tr.title}\ncompleted: {tr.completed}");
}

var opciones = new JsonSerializerOptions
{ //accion que tiene la propiedad de ordenar el arreglo de json, para que sea más legible
    WriteIndented = true
};

string ListaJsoneada = JsonSerializer.Serialize(tareasOrdenadas, opciones); //devuelvo los valores de la lista ordenada con la propiedad de opciones

string ruta = "tareas.json";

string jsonFinal = JsonSerializer.Serialize(tareasOrdenadas, opciones); //serializa las tareas ordenadas
File.WriteAllText(ruta, jsonFinal); //escribe todo lo encontrado en jsonFinal hacia la ruta especificada

Console.WriteLine("Tareas agregadas correctamente.");
