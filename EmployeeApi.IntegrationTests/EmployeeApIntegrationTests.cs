using System;
using Xunit;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using EmployeeApi.Models;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeApi.IntegrationTests
{
    public class EmployeeApIntegrationTests
    {
        [Fact]
        public async Task Test_Get_All()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/employees");
                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Post()
        {
            using(var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/employees", 
                    new StringContent(
                        JsonConvert.SerializeObject(new Employee() { Id = 10, FirstName = "San", LastName = "Zhang", HiredDate = DateTime.Today}), Encoding.UTF8, "application/json"));
                
                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }
    }
}
