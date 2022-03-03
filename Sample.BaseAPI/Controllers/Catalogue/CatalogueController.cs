using Sample.Interface.Services.Catalogue;
using Sample.Models.Catalogue;
using Sample.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Sample.BaseAPI.Controllers.Catalogue
{
    [ApiController]
    public abstract class CatalogueController : ControllerBase
    {
        #region Catalogue

        protected IWardService wardService;
        protected IDistrictService districtService;
        protected ICountryService countryService;
        protected INationService nationService;
        protected ICityService cityService;

        #endregion

        #region Configuration

        protected IMapper mapper;
        protected IConfiguration configuration;

        #endregion

        public CatalogueController(IServiceProvider serviceProvider, IMapper mapper, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.configuration = configuration;

            wardService = serviceProvider.GetRequiredService<IWardService>();
            districtService = serviceProvider.GetRequiredService<IDistrictService>();
            cityService = serviceProvider.GetRequiredService<ICityService>();
            countryService = serviceProvider.GetRequiredService<ICountryService>();
            nationService = serviceProvider.GetRequiredService<INationService>();
        }

        /// <summary>
        /// Lấy danh sách quốc gia
        /// </summary>
        /// <param name="searchContent"></param>
        /// <returns></returns>
        [HttpGet("get-country-catalogue")]
        public async Task<AppDomainResult> GetCountryCatalogue(string searchContent)
        {
            var countries = await this.countryService.GetAsync(e => !e.Deleted && e.Active
            && (string.IsNullOrEmpty(searchContent) ||
            (e.Code.Contains(searchContent)
            || e.Name.Contains(searchContent)
            ))
            );
            return new AppDomainResult()
            {
                Data = mapper.Map<IList<CountryModel>>(countries),
                Success = true,
                ResultCode = (int)HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Lấy danh sách thành phố
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="searchContent"></param>
        /// <returns></returns>
        [HttpGet("get-city-catalogue/countryId")]
        public async Task<AppDomainResult> GetCityCatalogue(int? countryId, string searchContent,int PageIndex,int PageSize)
        {
            var cities = await this.cityService.GetAsync(e => !e.Deleted && e.Active
            && (!countryId.HasValue || e.CountryId == countryId.Value)
            && (string.IsNullOrEmpty(searchContent) ||
            (e.Code.Contains(searchContent)
            || e.Name.Contains(searchContent)
            )));
            int skip = (PageIndex - 1) * PageSize;
            int take = PageSize;
            var data = mapper.Map<IList<CityModel>>(cities).OrderBy(x => x.Name).Skip(skip).Take(take).ToList();
            PagedList<CityModel> pagedDataModel = new PagedList<CityModel> { TotalItem = cities.Count(), Items = data, PageIndex = PageIndex, PageSize = PageSize };
            return new AppDomainResult()
            {
                Data = pagedDataModel,
                Success = true,
                ResultCode = (int)HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Lấy danh sách quận
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="searchContent"></param>
        /// <returns></returns>
        [HttpGet("get-district-catalogue/cityId")]
        public async Task<AppDomainResult> GetDistrictCatalogue(int? cityId, string searchContent)
        {
            var districts = await this.districtService.GetAsync(e => !e.Deleted && e.Active
            && (!cityId.HasValue || e.CityId == cityId.Value)
            && (string.IsNullOrEmpty(searchContent) ||
            (e.Code.Contains(searchContent)
            || e.Name.Contains(searchContent)
            )));
            return new AppDomainResult()
            {
                Data = mapper.Map<IList<DistrictModel>>(districts).OrderBy(x => x.Name),
                Success = true,
                ResultCode = (int)HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Lấy danh sách phường
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="districtid"></param>
        /// <param name="searchContent"></param>
        /// <returns></returns>
        [HttpGet("get-ward-catalogue/cityId/districtId")]
        public async Task<AppDomainResult> GetWardCatalogue(int? cityId, int? districtid, string searchContent)
        {
            var wards = await this.wardService.GetAsync(e => !e.Deleted && e.Active
            && (!cityId.HasValue || e.CityId == cityId.Value)
            && (!districtid.HasValue || e.DistrictId == districtid.Value)
            && (string.IsNullOrEmpty(searchContent) ||
            (e.Code.Contains(searchContent)
            || e.Name.Contains(searchContent)
            )));
            return new AppDomainResult()
            {
                Data = mapper.Map<IList<WardModel>>(wards).OrderBy(x => x.Name),
                Success = true,
                ResultCode = (int)HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Lấy danh sách dân tộc
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="searchContent"></param>
        /// <returns></returns>
        [HttpGet("get-nation-catalogue/countryId")]
        public async Task<AppDomainResult> GetNationCatalogue(int? countryId, string searchContent)
        {
            var nations = await this.nationService.GetAsync(e => !e.Deleted && e.Active
            && (!countryId.HasValue || e.CountryId == countryId.Value)
            && (string.IsNullOrEmpty(searchContent) ||
            (e.Code.Contains(searchContent)
            || e.Name.Contains(searchContent)
            ))
            );
            return new AppDomainResult()
            {
                Data = mapper.Map<IList<NationModel>>(nations),
                Success = true,
                ResultCode = (int)HttpStatusCode.OK
            };
        }

    }
}
