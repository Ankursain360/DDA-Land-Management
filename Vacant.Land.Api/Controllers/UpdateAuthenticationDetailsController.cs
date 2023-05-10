using Dto.Master;
using Libraries.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using Service.IApplicationService;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Vacant.Land.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UpdateAuthenticationDetailsController : ControllerBase
    {
        private readonly ILandverificationdetailsService _landverificationdetailsService;

        public UpdateAuthenticationDetailsController(ILandverificationdetailsService landverificationdetailsService)
        {
            _landverificationdetailsService = landverificationdetailsService;
        }
        [HttpPost]
        [Route("[action]")]
        [Route("api/UpdateAuthenticationDetails/SaveAcquiredlandAuthenticationDetails")]
        public async Task<IActionResult> SaveAcquiredlandAuthenticationDetails([FromBody] landverificationdetailsDto dto)
        {
            landverificationResponseDetails apiResponseDetails = new landverificationResponseDetails();
            List<landverificationdetailsDto> DtoList = new List<landverificationdetailsDto>();
            // var fetchBhigha = dto.signatureData.Select(x => x.villagedetails.Find(x => x.areaBhigha_Biswa_Biswana.HasValue));
            if (dto != null)
            {
                

                if (dto.signatureData != null && dto.signatureData[0].villagedetails != null)
                {
                    dto.AckID = Guid.NewGuid().ToString();
                    var data = await _landverificationdetailsService.Create(dto);

                    if (dto.signatureData != null && dto.signatureData.Count > 0)
                    {
                        for (int i = 0; i < dto.signatureData.Count; i++)
                        {
                            LandVerificationSignatureData tbl = new LandVerificationSignatureData();

                            tbl.signatureText = dto.signatureData[i].signatureText;
                            tbl.signatureType = dto.signatureData[i].signatureType;
                            tbl.subjectName = dto.signatureData[i].subjectName;
                            tbl.EmailId = dto.signatureData[i].EmailId;
                            tbl.TokenserialNo = dto.signatureData[i].TokenserialNo;
                            tbl.signature = dto.signatureData[i].signature;
                            tbl.signatureDate = dto.signatureData[i].signatureDate;
                            tbl.AccountName = dto.signatureData[i].AccountName;
                            tbl.AccountDesignation = dto.signatureData[i].AccountDesignation;
                            tbl.LandVerificationDetailsId = dto.Id;
                            int signatureResult = await _landverificationdetailsService.SaveSignatureData(tbl);
                            if (signatureResult != 0)
                            {
                               

                                if (dto.signatureData[i].villagedetails.Count > 0 && dto.signatureData[i].villagedetails.Count != 0)
                                {
                                    LandVerificationVillageDetails landtbl = new LandVerificationVillageDetails();
                                    landtbl.villageName = dto.signatureData[i].villagedetails[0].villageName;
                                    landtbl.khasra_No = dto.signatureData[i].villagedetails[0].khasra_No;
                                    landtbl.Bhigha = Convert.ToInt32(dto.signatureData[i].villagedetails[0].areaBhigha_Biswa_Biswana.Split('-')[0]);
                                    landtbl.Biswa = Convert.ToInt32(dto.signatureData[i].villagedetails[0].areaBhigha_Biswa_Biswana.Split('-')[1]);
                                    landtbl.Biswana = Convert.ToInt32(dto.signatureData[i].villagedetails[0].areaBhigha_Biswa_Biswana.Split('-')[2]);
                                    landtbl.notification_s_US_4 = dto.signatureData[i].villagedetails[0].notification_s_US_4;
                                    landtbl.notification_s_US_6 = dto.signatureData[i].villagedetails[0].notification_s_US_6;
                                    landtbl.notification_s_US_17 = dto.signatureData[i].villagedetails[0].notification_s_US_17;
                                    landtbl.notification_s_US_22 = dto.signatureData[i].villagedetails[0].notification_s_US_22;
                                    landtbl.LandVerificationSignatureId = signatureResult;
                                    await _landverificationdetailsService.SaveLandVillagedetails(landtbl);
                                }
                                else
                                {
                                    apiResponseDetails = new landverificationResponseDetails
                                    {
                                        responseCode = "200",
                                        responseMessage = "Record saved successfully!",
                                        response = DtoList
                                    };

                                    return Ok(apiResponseDetails);
                                }

                            }
                        }


                    }
                    apiResponseDetails = new landverificationResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "Record saved successfully!",
                        response = DtoList
                    };

                    return Ok(apiResponseDetails);
                }
                apiResponseDetails = new landverificationResponseDetails
                {
                    responseCode = "400",
                    responseMessage = "Please Insert signatureData and villagedetails data at least one time",
                    response = DtoList
                };

                return Ok(apiResponseDetails);
            }
            else
            {
                apiResponseDetails = new landverificationResponseDetails
                {
                    responseCode = "404",
                    responseMessage = " details not found",
                    response = DtoList
                };
                return Ok(apiResponseDetails);
            }
        }


    }
}
