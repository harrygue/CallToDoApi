using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CallToDoApi.Models;

namespace CallToDoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/CallTodo")]
    public class CallTodoController : Controller
    {
        // GET: api/CallTodo
        [HttpGet]
        // public IEnumerable<string> Get()
        public async Task<IActionResult> CallAllTodo()
        {
            //return new string[] { "value1", "value2" };
            using(var client = new HttpClient())
            {
                try
                {
                    string uri = "/api/todo/";
                    client.BaseAddress = new Uri("http://localhost:59126");
                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawResult = JsonConvert.DeserializeObject<List<ToDoItem>>(stringResult);
                    
                    return Ok(rawResult);
                }
                catch(HttpRequestException httpReqEx)
                {
                    return BadRequest(httpReqEx.Message);
                }
            }
        }

        // GET: api/CallTodo/5
        [HttpGet("{id}", Name = "Get")]
        //public string CallTodo(int id)
        // [HttpGet("[action]/{callTodo}")]
        public async Task<IActionResult> CallTodo(int id)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    string uri = "/api/todo/" + id;
                    client.BaseAddress = new Uri("http://localhost:59126");
                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawResult = JsonConvert.DeserializeObject<ToDoItem>(stringResult);
                    return Ok(new
                    {
                        Id = rawResult.Id, Name = rawResult.Name, IsComplete = rawResult.IsComplete
                    });
                }
                catch (HttpRequestException httpReqEx)
                {
                    return BadRequest(httpReqEx.Message);
                        // BadRequest("Error getting Todo from ToDoApi: {httpReqEx.Message}");
                }
              
            }


        }
        
        // POST: api/CallTodo
        [HttpPost]
        // public void Post([FromBody]string value)
        public async Task<Uri> Create([FromBody] ToDoItem item) // [FromBody] is needed here
        {
            using(var client = new HttpClient())
            {
                try
                {
                    string uri = "/api/todo/";
                    client.BaseAddress = new Uri("http://localhost:59126");
                    var dataAsString = JsonConvert.SerializeObject(item);
                    var content = new StringContent(dataAsString);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await client.PostAsync(uri, content);
                    response.EnsureSuccessStatusCode();
                    return response.Headers.Location;
                }
                catch(HttpRequestException ex)
                {
                    return null;
                }
            }
        }
        
        // PUT: api/CallTodo/5
        [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        public async Task<ToDoItem> UpdateTodoAsync (long id, ToDoItem item) // removed [FromBody] 
        {
            using(var client = new HttpClient())
            {
                string uri = "/api/todo/" + id;    // +item.Id;
                client.BaseAddress = new Uri("http://localhost:59126");
                var dataAsString = JsonConvert.SerializeObject(item);
                var content = new StringContent(dataAsString); // as HttpContent;
                
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PutAsync(uri, content);
                response.EnsureSuccessStatusCode();
                var stringResult = await response.Content.ReadAsStringAsync();
                var rawResult = JsonConvert.DeserializeObject<ToDoItem>(stringResult);
                return new ToDoItem { Id = rawResult.Id, IsComplete = rawResult.IsComplete, Name = rawResult.Name };
                // this sequence works, status code is 204 (no content) but value gets updated
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        // public void Delete(int id)
        // HttpStatusCode is not within .net Core or aspnet core; works but maybe not with Linux etc.
        // public async Task<System.Net.HttpStatusCode> DeleteTodoAsync ([FromBody] ToDoItem item) // postman
        public async Task<System.Net.HttpStatusCode> DeleteTodoAsync(long id)
        {
            using(var client = new HttpClient())
            {
                string uri = "/api/todo/" + id;
                client.BaseAddress = new Uri("http://localhost:59126");
                var response = await client.DeleteAsync(uri);
                return response.StatusCode;
            }
        }
    }
}
