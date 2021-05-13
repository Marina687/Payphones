using System.Net;
using Newtonsoft.Json;
using MoscowPayphones.ApplicationServices.Ports;
using MoscowPayphones.ApplicationServices.GetPayphonesListUseCase;

namespace MoscowPayphones.InfrastructureServices.Presenters
{
    public class PayphonesListPresenter : IOutputPort<GetPayphonesListUseCaseResponse>
    {
        public JsonContentResult ContentResult { get; }

        public PayphonesListPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GetPayphonesListUseCaseResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            ContentResult.Content = response.Success ? JsonConvert.SerializeObject(response.Payphones) : JsonConvert.SerializeObject(response.Message);
        }
    }
}
