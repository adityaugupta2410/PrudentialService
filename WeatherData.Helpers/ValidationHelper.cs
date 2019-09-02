using System;

namespace WeatherData.Helpers
{
    public class ValidationHelper
    {
        #region Public Method : Custom Validation to check if Ids are all Int64 Type
        public bool ValidateCityIds(string CityIds)
        {
            bool IsValidInput = true;
            long temp = 0;
            if (!string.IsNullOrEmpty(CityIds))
            {
                if (CityIds.Contains(","))
                {
                    string[] cityId = CityIds.Split(',');

                    foreach (var id in cityId)
                    {
                        IsValidInput = IsValidInput && long.TryParse(id, out temp);
                    }
                }
                else
                    IsValidInput = IsValidInput && long.TryParse(CityIds, out temp);
            }
            else
                IsValidInput = false;

            return IsValidInput;
        }

        #endregion
    }
}