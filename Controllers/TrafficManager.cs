using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Traffic_ManagerHW2.Controllers.Constants;
using Traffic_ManagerHW2.Controllers.WebAppAppServices;

namespace Traffic_ManagerHW2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrafficManager : ControllerBase
    {

        public TrafficManager()
        {
        }

        [HttpPut]
        [Route("/enqueue")]
        public ContentResult TrafficManagerToEnqueue(int iterations,[FromBody] string dataToDigest)
        { 
            //Object validation
            if (iterations <= 0 || dataToDigest is null || dataToDigest.Length == 0)
            {
                return new ContentResult() { Content = ErrorConstants.InvalidRequestEnqueue, StatusCode = 400 };
            }

            var client = new HttpClient();
            var LoadBalancer = new LoadBalancerClass();

            var dataJson = JsonConvert.SerializeObject(dataToDigest);
            string url = LoadBalancer.LoadMachineUrl();
            var response = client.PostAsync(string.Format(url, "/Enqueue?iterations=" , iterations), new StringContent(dataJson, Encoding.UTF8, "application/json"));


            var workId = response.Result.ToString();

            return new ContentResult() { Content = workId, StatusCode = (int?)response.Result.StatusCode };
        }

        [HttpPost]
        [Route("/pullCompleted")]
        public ContentResult TrafficManagerToDequeue(int top)
        {
            //Object validation
            if (top <= 0)
            {
                return new ContentResult() { Content = ErrorConstants.InvalidRequestDequeue, StatusCode = 400 };
            }

            var LoadBalancer = new LoadBalancerClass();
            string url = LoadBalancer.LoadMachineUrl();

            var client = new HttpClient();
            var response = client.GetAsync(string.Format(url, "/Dequeue?top=",top));

            var preparedData = response.Result;

            return new ContentResult() { Content = JsonConvert.SerializeObject(preparedData), StatusCode = (int?)preparedData.StatusCode };

        }
    }
}