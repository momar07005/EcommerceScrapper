using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumerations
{
    public static class StatusAssembler
    {
        public static RequestStatusEnum ToRequestStatusEnum(this ResponseStatusEnum responseStatusEnum)
        {
            switch (responseStatusEnum)
            {
                case ResponseStatusEnum.Failure:
                    return RequestStatusEnum.Failure;

                case ResponseStatusEnum.PartialSuccess:
                    return RequestStatusEnum.PartialSuccess;

                default:
                    return RequestStatusEnum.Success;
            }
        }
    }
}
