﻿using AdvPOS.Pages;
using System;

namespace AdvPOS.Helpers
{
    public static class StaticData
    {
        public static string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        public static string GetUniqueID(string Prefix)
        {
            Random _Random = new Random();
            var result = Prefix + DateTime.Now.ToString("yyyyMMddHHmmss") + _Random.Next(1, 1000);
            return result;
        }
    }

    public static class DefaultUserPage
    {
        public static readonly string[] PageCollection =
            {
                MainMenu.Dashboard.PageName,
                MainMenu.UserProfile.PageName,
            };
    }
    public static class InvoiceType
    {
        public const int TempInvoice = 0;
        public const int RegularInvoice = 1;
        public const int DraftInvoice = 2;
        public const int QueoteInvoice = 3;
        public const int EditInvoice = 4;
    }
    public static class TranReturnType
    {
        public const int NoReturn = 0;
        public const int PartilaReturn = 1;
        public const int FullReturn = 2;
    }
    public static class ReturnLogType
    {
        public const string Sales = "Sales";
        public const string Purchase = "Purchase";
    }
    public static class PaymentStatusInfo
    {
        public const int Paid = 1;
        public const int Unpaid = 2;
    }
    public static class ItemPriceModel
    {
        public const int CostPrice = 1;
        public const int NormalPrice = 2;
        public const int TradePrice = 3;
        public const int PremiumPrice = 4;
        public const int OtherPrice = 5;
    }
    public static class ConnectionStrings
    {
        public const string connMSSQLNoCred = "connMSSQLNoCred";
        public const string connMSSQL = "connMSSQL";
        public const string connMySQL = "connMySQL";
        public const string connDockerBase = "connDockerBase";
        public const string connMSSQLProd = "connMSSQLProd";
        public const string connOthers = "connOthers";
    }
    public static class InvoicePaymentType
    {
        public const int SalesInvoicePayment = 1;
        public const int PurchasesInvoicePayment = 2;
    }
    public static class DBOperationType
    {
        public const int Add = 1;
        public const int Edit = 2;
        public const int Delete = 3;
        public const int Update = 4;
    }
    public static class EmailContent
    {
        public const string Subject = "Business ERP: Customer Invoice ";
        public const string Body = "Hi\nPlease get the Invoice from the email attachment.\n\nThanks\nAdmin, Business ERP\n";
    }
    public static class SampleBarcode
    {
        public const string Default = "data:image/png;base64,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msq5dFwpNlnRhIA3QURsg8ApTfzSxEPEgAqS13KXPh9b8Nk4khWtR8wSvSs6AMTbu0MJwgakFJv+/F6uTzRrwiwtHMgZWIrSinOrkHjzZ8JpidhEbDlFmks25jrULNebB/p2j1xssY4IKd0RgYrYRGwMYXktGrKEVmVHPS2kcNF9kdL2PueslRWrXGRFC62oVcH1/4WAEBAC84YA3WObt4lrPkJACAgBCwERm2JDCAiB3iEgYuvdkuqBhIAQELEpBoSAEOgdAiK23i2pHkgICAERm2JACAiB3iEgYuvdkuqBhIAQELEpBoSAEOgdAiK2OVzSd999N+yxxx7mzN55552w++67h88//zwceeSR4aOPPjKv3W677cKTTz4ZNt5443DWWWelf3/ppZfS56uf119/PRxyyCFht912C48++mhYunRp+Omnn8Kxxx4bXnnllYmxTz311HDDDTeENddcc/S7H3/8Mdx3333hwQcfTPPad999w+mnnx4OPPDAsMoqq4yum9W48QY///xzuPvuu0dz2GuvvUKc66GHHhpWX331OVxpTWlWCIjYZoVsi3FnQWzrr79+IqqPP/44Ec4ll1ySvnnj33//DVdeeWW46qqrwn777VdEbJ9++mk46aSTwnvvvTfx1DfeeGM488wzR9/y4SE2z7g//PBDIrFnn312Yg7XXHNNOO+882oE22J59NH/AwREbHO+SBXJVSpt2nSnXVsRyvbbbx+++uqrpGzWXXfd8P3334czzjgjKbqotJqK7fjjj0+EOO0nksq1114bDj744LDrrruGlVZaKbzxxhvh5JNPDptvvnl4+OGHw4YbbpiGqObR5bjxfcqoIK+44opw/fXXh+OOOy6pyfhsl19+eYjYPf3002GbbbaZ89XW9LpCQMTWFZIzGqdrYouq5qGHHgqXXXZZ2GmnnUK0oc8//3zYdtttwyOPPJKIbaONNnIRUO7RoxKM93jxxReT/d1iiy3cxMaO+8svvyQSjfOOBDtuOyu7fu6554YTTjhhNORff/0VnnnmmXDTTTclpbnzzjuHE088MV2z1lpr1W7tuXZGYaBhnQiI2JyArejLuyK2L7/8Mhx11FEpkd98882UvNEiRhsaVVy0qvG/KxLyKKtpBPTWW2+Fxx57LGyyySadEtv4uNVcd9xxx2SpV1555dGUqt/FnmIk2vjzzz//JGV34YUXTkw92vRIjhW5ea5d0bGh+9kIiNjmPDq6IrZKudx2223hzz//TPYwWrfqn++++y5Z0iaxVZsH6623XtpcOProo8NBBx00oWqaMH777bch2s1INldfffVIRTV7bF2M+8cff4RzzjknvPzyy+GWW24J+++/f7LDcQ5RnUabGkk9ktkaa6wR3n777fTfp5xyStpQiZb8119/Tfb8uuuuS0QcNz/ij+faOQ+lhZqeiG3Ol3sWxLbZZpuFs88+O+yzzz7hs88+C7G5/v77708ltnGYmqqmCWFUOXHMaHEfeOCBsNVWW00oqNxua5txo4KLVvLrr7/OrmgkvjinVVddNRHthx9+mHZxI6lVP3Fn95hjjkm4XHTRRelbldlr5zyMFm56IrY5X/KuiC3uhh5++OGJaHbZZZdkQe+9995w6623puMQ8T5R6bz66qvp982fv//+O3zyySfh0ksvDS+88ELafYxHOXKkFhXSPffck/6JRy6m/XQ1biShqK7iLmzsG0YCi8oybmjcfPPNaR7Rilbq7q677jKnVZFg7BPGf2euXW211eY8khZreiK2OV/vrogtjnPaaaeFp556Kmy55ZbhtddeC+eff/7IerL3ieQRSSJuNDR3SyNpxP5UJM/7778/7L333uafe2vCPqtxKwseFVg88ydim/OA72h6IraOgJzVMCzhxPtPuzb+bryH1pwve5+4k3jYYYeFJ554IhFF9fPbb7+ls3HPPfdc6mshpda8/yzGrRr/jz/++IjAq93ab775JsR+4zrrrGMunefaWa2/xi1DQMRWhtsK+xRLOG2J7YMPPggHHHBAIqw999xz4vl+//33dDYt2rm11147KbZNN900XRdP/Mcdxqi6YgM+93kLsFmMG1XZF198Ee68885wxx13JBUZj3tUu6WRRJctW5aIOPblNthgA1NZeq5dYUGhG0EERGwQohV/AfvmgUd1xfNpccdv/EzZ+OcryxbtabSY1hziebQmecWx46FY62f8FaxZjWu9XnbxxReH+M/42bRIphdccEG4/fbbs1MePwztuXbFR4ruaCEgYpvD2JhHYtthhx3CEUcckRRO9RZBBV0bYutq3HFii2PG/l58zWvrrbdORz+aP5Gw4pGXSPRxR3X8p/mWh+faOQynhZySiG0hl10PLQT6jYCIrd/rq6cTAguJgIhtIZddDy0E+o2AiK3f66unEwILiYCIbSGXXQ8tBPqNgIit3+urpxMCC4mAiG0hl10PLQT6jYCIrd/rq6cTAguJgIhtIZddDy0E+o2AiK3f66unEwILicB/DtqTcjMsK+wAAAAASUVORK5CYII=";
    }
}
