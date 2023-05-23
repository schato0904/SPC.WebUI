using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPC.WebUI.Common.Library
{
    public static class CTFMath
    {
        #region Methods
        /// <summary>
        /// 정규분포 계산(엑셀 함수와 파라미터 동일)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="mean">평균</param>
        /// <param name="std">표준편차</param>
        /// <param name="cumulative"></param>
        /// <returns></returns>
        public static double NORMDIST(double x, double mean, double std, bool cumulative)
        {
            if (cumulative)
            {
                return Phi(x, mean, std);
            }

            var tmp = 1 / (Math.Sqrt(2 * Math.PI) * std);
            return tmp * Math.Exp(-.5 * Math.Pow((x - mean) / std, 2));
        }

        private static double Phi(double z)
        {
            return 0.5 * (1.0 + erf(z / Math.Sqrt(2.0)));
        }

        private static double Phi(double z, double mu, double sigma)
        {
            return Phi((z - mu) / sigma);
        }

        private static double erf(double z)
        {
            var t = 1.0 / (1.0 + 0.5 * Math.Abs(z));

            // use Horner's method
            var ans = 1 -
                      t *
                      Math.Exp(
                          -z * z - 1.26551223 +
                          t *
                          (1.00002368 +
                           t *
                           (0.37409196 +
                            t *
                            (0.09678418 +
                             t *
                             (-0.18628806 +
                              t *
                              (0.27886807 +
                               t * (-1.13520398 + t * (1.48851587 + t * (-0.82215223 + t * 0.17087277)))))))));
            if (z >= 0)
            {
                return ans;
            }

            return -ans;
        }

        #endregion
    }
}
