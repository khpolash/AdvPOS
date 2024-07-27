using AdvPOS.ConHelper;
using AdvPOS.Data;
using AdvPOS.Helpers;
using AdvPOS.Models.CommonViewModel;
using AdvPOS.Models.EmailConfigViewModel;
using AdvPOS.Models.SendEmailHistoryViewModel;
using AdvPOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdvPOS.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class PaymentShareController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _iCommon;
        private readonly ISalesService _iSalesService;
        private readonly IDBOperation _iDBOperation;
        private readonly IEmailSender _emailSender;
        private readonly string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" + " --footer-line --footer-font-size \"10\" --footer-spacing 6 --footer-font-name \"calibri light\"";

        public PaymentShareController(ApplicationDbContext context, ICommon iCommon, IDBOperation iDBOperation, ISalesService iSalesService, IEmailSender emailSender)
        {
            _context = context;
            _iCommon = iCommon;
            _iSalesService = iSalesService;
            _iDBOperation = iDBOperation;
            _emailSender = emailSender;
        }

        [Authorize(Roles = Pages.MainMenu.Payment.RoleName)]
        [HttpGet]
        public IActionResult OpenSendMailPaymentInvoice(Int64 _PaymentId, Int64 _InvoiceDocType, Int64 _HideCompanyInfo)
        {
            ViewBag.GetddlEmailConfig = new SelectList(_iCommon.GetddlEmailConfig(), "Id", "Name");
            ViewBag.GetddlUserEmail = new SelectList(_iCommon.GetddlCustomerEmail(), "Id", "Name");

            SendEmailViewModel _SendEmailViewModel = new();
            var _Payment = _context.Payment.FirstOrDefault(x => x.Id == _PaymentId);
            _SendEmailViewModel.InvoiceId = _Payment.Id;
            _SendEmailViewModel.ReceiverEmailId = _Payment.CustomerId;

            _SendEmailViewModel.Subject = EmailContent.Subject;
            _SendEmailViewModel.Body = EmailContent.Body;

            _SendEmailViewModel.IsHideCompanyInfo = _HideCompanyInfo;
            _SendEmailViewModel.InvoiceDocType = _InvoiceDocType;
            return PartialView("_Share", _SendEmailViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> SendMailPaymentInvoice(SendEmailViewModel vm)
        {
            JsonResultViewModel _JsonResultViewModel = new();
            SendEmailHistoryCRUDViewModel _SendEmailHistoryCRUDViewModel = new();
            try
            {
                var _PrintPaymentInvoice = await _iSalesService.PrintPaymentInvoice(vm.InvoiceId);
                _PrintPaymentInvoice.SendEmailViewModel = vm;

                EmailConfigCRUDViewModel _EmailConfigCRUDViewModel = await _context.EmailConfig.FindAsync(vm.SenderEmailId);
                SendEmailViewModel _SendEmailViewModel = _EmailConfigCRUDViewModel;
                _SendEmailViewModel.Subject = vm.Subject;
                _SendEmailViewModel.Body = vm.Body;
                _SendEmailViewModel.ReceiverEmail = vm.ReceiverEmail;
                _SendEmailViewModel.IsSSL = _EmailConfigCRUDViewModel.SSLEnabled;

                var viewAsPdf = new ViewAsPdf("PaymentInvoiceReportPDF", _PrintPaymentInvoice)
                {
                    FileName = "Invoice_" + vm.InvoiceId + "_" + DateTime.Now + ".pdf",
                    PageSize = Size.A4,
                    PageMargins = { Left = 1, Right = 1 }
                };
                byte[] pdfBytes = await viewAsPdf.BuildFile(ControllerContext);
                Stream _Stream = new MemoryStream(pdfBytes);
                _SendEmailViewModel.FileStream = _Stream;
                _SendEmailViewModel.FileName = viewAsPdf.FileName;
                _SendEmailViewModel.FileType = "content/pdf";
                var result = await _emailSender.SendEmailByGmailAsync(_SendEmailViewModel);

                _SendEmailHistoryCRUDViewModel.InvoiceId = vm.InvoiceId;
                _SendEmailHistoryCRUDViewModel.SenderEmail = _SendEmailViewModel.SenderEmail;
                _SendEmailHistoryCRUDViewModel.ReceiverEmail = _SendEmailViewModel.ReceiverEmail;
                _SendEmailHistoryCRUDViewModel.UserName = HttpContext.User.Identity.Name;

                if (result.Status == TaskStatus.RanToCompletion)
                {
                    //await AddDocumentHistory(vm.Id, _Document.AssignEmployeeId, "Document Shared Using Email.");
                    _JsonResultViewModel.AlertMessage = "Email Send Successfully. Invoice Id: " + vm.InvoiceId;
                    _JsonResultViewModel.Id = vm.InvoiceId;
                    _SendEmailHistoryCRUDViewModel.Result = "Success";
                }
                else
                {
                    _JsonResultViewModel.AlertMessage = "Email Send Failed. Status: " + result.Status;
                    _SendEmailHistoryCRUDViewModel.Result = "Failed, status: " + result.Status;
                }

                await _iDBOperation.AddSendEmailHistory(_SendEmailHistoryCRUDViewModel);
                return new JsonResult(_JsonResultViewModel);
            }
            catch (Exception ex)
            {
                Syslog.Write(Syslog.Level.Warning, "AdvPOSCustomlog", ex.Message);
                _JsonResultViewModel.IsSuccess = false;
                _JsonResultViewModel.AlertMessage = ex.Message;
                _SendEmailHistoryCRUDViewModel.Result = "Failed, status: " + ex.Message;
                await _iDBOperation.AddSendEmailHistory(_SendEmailHistoryCRUDViewModel);
                return new JsonResult(_JsonResultViewModel);
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> DownloadPaymentInvoice(Int64 _PaymentId)
        {
            var _PrintPaymentInvoice = await _iSalesService.PrintPaymentInvoice(_PaymentId);
            var rpt = new ViewAsPdf();
            rpt.PageOrientation = Orientation.Portrait;
            rpt.CustomSwitches = footer;

            rpt.FileName = "Payment_Invoice_Report_" + _PaymentId + ".pdf";
            rpt.ViewName = "PaymentInvoiceReportPDF";
            rpt.Model = _PrintPaymentInvoice;
            return rpt;
        }
    }
}
