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
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Vacant.Land.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UpdateAuthenticationDetailsController : ControllerBase
    {
        private readonly ILandverificationdetailsService _landverificationdetailsService;
        private readonly IConfiguration _configuration;
       
        public UpdateAuthenticationDetailsController(ILandverificationdetailsService landverificationdetailsService, IConfiguration configuration)
        {
            _landverificationdetailsService = landverificationdetailsService;
            _configuration = configuration;
        }
        [HttpPost] 
        [Route("[action]")]
        [Route("api/UpdateAuthenticationDetails/SaveAuthenticationDetails")]
        public async Task<IActionResult> SaveAuthenticationDetails([FromBody] landverificationdetailsDto dto)
        {
            landverificationResponseDetails apiResponseDetails = new landverificationResponseDetails();
            List<landverificationdetailsDto> DtoList = new List<landverificationdetailsDto>();
            if (dto != null)
            {
                string LogPath = _configuration.GetSection("FilePaths:Logs:LogFilePath").Value.ToString();
                string LogFileName = LogPath+ "/LMIS_Data_verfication_logs_" + DateTime.Now.ToString("dd-mm-yyyy_hh_mm_ss") + ".txt";
                var logdata = JsonConvert.SerializeObject(dto);
                if (!Directory.Exists(LogPath))
                {
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(LogPath);
                }
                using (StreamWriter writer = new StreamWriter(LogFileName, true))
                {
                    writer.WriteLine("------------- Log Captured at" + DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt") + "----------");
                    writer.WriteLine("-- Data Received from DDA Verification Software ---");
                    writer.WriteLine(logdata);
                    writer.WriteLine("   ");
                    writer.WriteLine("-- end of Log");
                }

                if (dto.signatureData != null && dto.signatureData[0].villagedetails != null)
                {
                    for (int i = 0; i < dto.signatureData.Count; i++)
                    {
                        if (dto.signatureData[i].villagedetails == null)
                        {
                            apiResponseDetails = new landverificationResponseDetails
                            {
                                responseCode = "403",
                                responseMessage = "Please Insert missing field signatureData/villagedetails  ",
                                response = DtoList
                            };

                            return Ok(apiResponseDetails);
                        }
                    }

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
                                if (dto.signatureData != null && dto.signatureData[i].villagedetails != null)
                                {



                                    if (dto.signatureData[i].villagedetails.Count > 0 && dto.signatureData[i].villagedetails != null)
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
                                }

                            }
                        }


                    }
                    apiResponseDetails = new landverificationResponseDetails
                    {
                        responseCode = "200",
                        responseMessage = "Record saved successfully!",
                        AckID = dto.AckID,
                        response = DtoList
                    };

                    var successresponse= JsonConvert.SerializeObject(apiResponseDetails);
                    using (StreamWriter writer = new StreamWriter(LogFileName, true))
                    {
                        writer.WriteLine("------------- Log Captured at" + DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt") + "----------");
                        writer.WriteLine("-- Response Data sent to DDA Verification Software ---");
                        writer.WriteLine(successresponse);
                        writer.WriteLine("   ");
                        writer.WriteLine("-- end of Log");
                    }
                    return Ok(apiResponseDetails);
                }


                apiResponseDetails = new landverificationResponseDetails
                {
                    responseCode = "403",
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
