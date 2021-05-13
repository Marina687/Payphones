using MoscowPayphones.DomainObjects;
using MoscowPayphones.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowPayphones.ApplicationServices.GetPayphonesListUseCase
{
    public class GetPayphonesListUseCaseResponse : UseCaseResponse
    {
        public IEnumerable<Payphones> Payphones { get; }

        public GetPayphonesListUseCaseResponse(IEnumerable<Payphones> payphones) => Payphones = payphones;
    }
}
