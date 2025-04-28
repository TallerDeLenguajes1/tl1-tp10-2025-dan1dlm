// See https://aka.ms/new-console-template for more information
using System;
using System.Text.Json;
using System.Net.Http;
using espacioTarea;

HttpClient client = new HttpClient();

var url = "https://jsonplaceholder.typicode.com/todos/";
HttpResponseMessage response = await client.GetAsync(url);
response.EnsureSuccessStatusCode();
string responseBody = await response.Content.ReadAsStringAsync();
List<Tarea> listTAREA = JsonSerializer.Deserialize<List<Tarea>>(responseBody);

var tareasOrdenadas = listTAREA.OrderBy(t => t.completed).ToList();

foreach(var tr in listTAREA){
    Console.WriteLine("------------------");
    Console.WriteLine($"UserID: {tr.userId}'\nid: {tr.id}\ntitle: {tr.title}\ncompleted: {tr.completed}");
}

var opciones = new JsonSerializerOptions{
    WriteIndented = true
};

string ListaJsoneada = JsonSerializer.Serialize(tareasOrdenadas, opciones);

string ruta = "tareas.json";

if(File.exists(ruta)){
    File.AppendAlltext(ruta, ",", ListaJsoneada);
    Console.WriteLine("Contenido agreagado al archivo existente.");
}else{
    File.WriteAllText(ruta, "[" + ListaJsoneada + "]");
    Console.WriteLine("Archivo creado como tiene que ser");
}