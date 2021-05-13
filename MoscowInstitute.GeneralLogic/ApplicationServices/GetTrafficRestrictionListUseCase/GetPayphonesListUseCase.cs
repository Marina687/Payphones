using System.Threading.Tasks;
using System.Collections.Generic;
using MoscowPayphones.DomainObjects;
using MoscowPayphones.DomainObjects.Ports;
using MoscowPayphones.ApplicationServices.Ports;

namespace MoscowPayphones.ApplicationServices.GetPayphonesListUseCase
{
    public class GetPayphonesListUseCase : IGetPayphonesListUseCase
    {
        private readonly IReadOnlyPayphonesRepository _readOnlyPayphonesRepository;

        public GetPayphonesListUseCase(IReadOnlyPayphonesRepository readOnlyPayphonesRepository)
            => _readOnlyPayphonesRepository = readOnlyPayphonesRepository;

        public async Task<bool> Handle(GetPayphonesListUseCaseRequest request, IOutputPort<GetPayphonesListUseCaseResponse> outputPort)
        {
            IEnumerable<Payphones> payphones = null;
            if (request.PayphonesId != null)
            {
                var payphone = await _readOnlyPayphonesRepository.GetPayphones(request.PayphonesId.Value);
                payphones = (payphone != null) ? new List<Payphones>() { payphone } : new List<Payphones>();

            }
            else if (request.Street != null)
            {
                payphones = await _readOnlyPayphonesRepository.QueryPayphones(new StreetCriteria(request.Street));
            }
            else
            {
                payphones = await _readOnlyPayphonesRepository.GetAllPayphones();
            }
            outputPort.Handle(new GetPayphonesListUseCaseResponse(payphones));
            return true;
        }
    }
}
