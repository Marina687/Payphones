using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoscowPayphones.ApplicationServices.Interfaces;

namespace MoscowPayphones.ApplicationServices.GetPayphonesListUseCase
{
    public interface IGetPayphonesListUseCase : IUseCase<GetPayphonesListUseCaseRequest, GetPayphonesListUseCaseResponse>
    {
    }
}
