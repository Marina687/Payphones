using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoscowPayphones.DomainObjects;
using MoscowPayphones.ApplicationServices.GetPayphonesListUseCase;
using MoscowPayphones.InfrastructureServices.Presenters;

namespace MoscowPayphones.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayphonesController : ControllerBase
    {
        private readonly ILogger<PayphonesController> _logger;
        private readonly IGetPayphonesListUseCase _getPayphonesListUseCase;

        public PayphonesController(ILogger<PayphonesController> logger, IGetPayphonesListUseCase getPayphonesListUseCase)
        {
            _logger = logger;
            _getPayphonesListUseCase = getPayphonesListUseCase;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPayphones()
        {
            var presenter = new MoscowPayphones.InfrastructureServices.Presenters.PayphonesListPresenter();
            await _getPayphonesListUseCase.Handle(GetPayphonesListUseCaseRequest.CreateAllPayphonesRequest(), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("{payphonesId}")]
        public async Task<ActionResult> GetPayphones(long payphonesId)
        {
            var presenter = new MoscowPayphones.InfrastructureServices.Presenters.PayphonesListPresenter();
            await _getPayphonesListUseCase.Handle(GetPayphonesListUseCaseRequest.CreatePayphonesRequest(payphonesId), presenter);
            return presenter.ContentResult;
        }
        [HttpGet("Streets/{streets}")]
        public async Task<ActionResult> GetStreetFilter(string Street)
        {
            var presenter = new PayphonesListPresenter();
            await _getPayphonesListUseCase.Handle(GetPayphonesListUseCaseRequest.CreateEventPayphonesRequest(Street), presenter);
            return presenter.ContentResult;
        }
    }
}
