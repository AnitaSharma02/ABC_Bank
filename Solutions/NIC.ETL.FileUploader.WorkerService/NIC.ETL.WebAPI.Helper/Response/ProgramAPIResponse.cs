using Core.Platform.ProgramMaster.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBL.ETL.WebAPI.Helper.Response
{

    internal class ProgramAPIResponse
    {
        public ProgramAPIResult results { get; set; }
    }

    class ProgramAPIResult
    {
        public bool IsSucessful { get; set; } = false;

        public string ErrorCode { get; set; } = string.Empty;

        public string ExceptionMessage { get; set; } = string.Empty;

        public ProgramDefinition ReturnObject { get; set; }
    }
}
