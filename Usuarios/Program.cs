// See https://aka.ms/new-console-template for more information
using System;
using System.Text.Json;
using System.Net.Http;
using espacioUsuarios;

HttpClient client = new HttpClient(); //lo utilizo para realizar la solicitud HTTP a servicios web

var url = "https://jsonplaceholder.typicode.com/users";
HttpResponseMessage response = await client.GetAsync(url); //realiza la solicitud a la URL indiada y espera la respuesta del servidor
try
{
    response.EnsureSuccessStatusCode();  //verifica que la respuesta del servidor tenga un codigo de estado exitoso
}
catch (HttpRequestException ex) //caso en el que ocurrió un error con el servidor
{
    Console.WriteLine("La solicitud no fue exitosa: " + ex.Message); //mensaje de error de servidor
}

string responseBody = await response.Content.ReadAsStringAsync();

List<Usuarios> usuarios = JsonSerializer.Deserialize<List<Usuarios>>(responseBody);

for (int i = 0; i < 5; i++)
{
    Console.WriteLine("DATOS DE USUARIO N°: " + (i + 1));
    Console.WriteLine($"name: {usuarios[i].name}\nemail: {usuarios[i].email}");
    Console.WriteLine("Adress");
    Console.WriteLine($"street: {usuarios[i].address.street}\nsuite: {usuarios[i].address.suite}");
    Console.WriteLine("----------------");
}