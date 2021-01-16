using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.BetUtilities
{
    public class BetUtilities
    {
        private static readonly Random _random = new Random();
        public static BetModel ValidateBet(BetModel objBet)
        {
            BetModel objResponse = new BetModel();
            try
            {
                if (objBet.iBetNumber != 0)
                {
                    objResponse.strBetColor = ValidateNumberBet(objBet.iBetNumber);
                    objResponse.strBetKey = Parameters.KEY_COLOR_AND_NUMBER_BET;
                }
                else
                {
                    objResponse.strBetColor = ValidateColorBet(objBet.strBetColor);
                    objResponse.strBetKey = Parameters.KEY_COLOR_BET;
                }

                string ValueValid = ValidateValueBet(objBet.lgBetValue);
                if (ValueValid == Parameters.INVALID_VALUE)
                {
                    return objResponse = new BetModel();
                }
                objResponse.lgBetValue = objBet.lgBetValue;
                objResponse.iFkRouletteId = objBet.iFkRouletteId;
                objResponse.iFkClientId = objBet.iFkClientId;
                objResponse.iBetNumber = objBet.iBetNumber;

                return objResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static ResponseBetModel CalculateWinnersBet()
        {
            ResponseBetModel objResponse = new ResponseBetModel();
            try
            {
                objResponse.iWinningNumber = RandomNumber(0, 36);
                if (objResponse.iWinningNumber % 2 == 0)
                {
                    objResponse.strWinningColor = Parameters.ROJO;
                }
                else
                {
                    objResponse.strWinningColor = Parameters.NEGRO;
                }

                return objResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        private static string ValidateNumberBet(int NumberBet)
        {
            string strResponse = string.Empty;
            try
            {
                if (NumberBet < 0 || NumberBet > 36)
                {
                    strResponse = Parameters.INVALID_NUMBER;
                }
                else
                {
                    if (NumberBet % 2 == 0)
                    {
                        strResponse = Parameters.ROJO;
                    }
                    else
                    {
                        strResponse = Parameters.NEGRO;
                    }
                }

                return strResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string ValidateColorBet(string ColorBet)
        {
            string strResponse = string.Empty;
            try
            {
                if (ColorBet.Substring(0, 1).ToUpper().Contains("R"))
                {
                    strResponse = Parameters.ROJO;
                }
                else
                {
                    strResponse = Parameters.NEGRO;
                }

                return strResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string ValidateValueBet(double ValueBet)
        {
            string strResponse = string.Empty;
            try
            {
                if (ValueBet < 0 || ValueBet > 10000)
                {
                    strResponse = Parameters.INVALID_VALUE;
                }
                else
                {
                    strResponse = Parameters.VALID_VALUE;
                }

                return strResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
