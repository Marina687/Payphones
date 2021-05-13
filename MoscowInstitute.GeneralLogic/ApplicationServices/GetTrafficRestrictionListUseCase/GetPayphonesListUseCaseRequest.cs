using MoscowPayphones.ApplicationServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoscowPayphones.ApplicationServices.GetPayphonesListUseCase
{
    public class GetPayphonesListUseCaseRequest : IUseCaseRequest<GetPayphonesListUseCaseResponse>
    {
        public string Street { get; private set; }
        public long? PayphonesId { get; private set; }

        private GetPayphonesListUseCaseRequest()
        { }

        public static GetPayphonesListUseCaseRequest CreateAllPayphonesRequest()
        {
            return new GetPayphonesListUseCaseRequest();
        }

        public static GetPayphonesListUseCaseRequest CreatePayphonesRequest(long payphonesId)
        {
            return new GetPayphonesListUseCaseRequest() { PayphonesId = payphonesId };
        }
            public static GetPayphonesListUseCaseRequest CreateEventPayphonesRequest(string Street)
            {
            return new GetPayphonesListUseCaseRequest() { Street = Street };  
        }

        }
    }


